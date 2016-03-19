using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AppXF.Data;
using AppXF.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.FormsBook.Toolkit;

namespace AppXF.Views
{


    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPlatePage : ContentPage, IDisposable
    {
        private string ImageLocation;
        private string SoundLocation;
        private List<string> pickerList;
        private string category;
        private bool animationFlag;
        private bool animantionAppeared;
        private static int count;


        public AddPlatePage()
        {

            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);
            InitializeComponent();
            SubscribeMessengers();
            InitializePicker();
            count++;
            Debug.WriteLine("********************///////////***************Created total " + count);

        }
        public void Dispose()
        {
            newPlateImage.Source = null;
            finishBtn.Clicked -= FinishRequested;
            recordButton.Clicked -= Record_Button_OnClicked;
            voiceSwitch.Toggled -= VoiceSwitch_OnToggled;
            chooseBtn.Clicked -= ChoosePhoto;
            picBtn.Clicked -= TakePhoto;
            categoryPicker.SelectedIndexChanged -= CategoryPicker_SelectedIndexChanged;
            stackLayout.Children.Clear();
            Content = null;
            BindingContext = null;
            count--;
            GC.SuppressFinalize(this);
            Debug.WriteLine("*******************///////////***************Disposer, remaining " + count);


        }

        protected override async void OnAppearing()
        {
            Device.OnPlatform(

                iOS: async () =>
                {
                    if (animantionAppeared == false)
                    {
                        await StartAnimation();
                    }
                },
                Android: async () => { await StartAnimation(); }


                );

            base.OnAppearing();
            animantionAppeared = true;
        }

        private void InitializePicker()
        {
            pickerList = new List<string>()
            {
                "Food",
                "Actions"
            };

            foreach (var pic in pickerList)
            {
                categoryPicker.Items.Add(pic);
            }
            pickerList = null;
            categoryPicker.SelectedIndexChanged += CategoryPicker_SelectedIndexChanged;
        }

        private void CategoryPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (categoryPicker.SelectedIndex == -1)
            {
                category = "Food";
            }
            else
            {
                category = categoryPicker.Items[categoryPicker.SelectedIndex];

            }
        }

        private void SubscribeMessengers()
        {
            Task.Run(() =>
            {
                MessagingCenter.Subscribe<IPictureTaker, string>(this, "pictureTaken",
    (s, arg) =>
    {
        ImageLocation = arg;
        newPlateImage.Source = ImageSource.FromFile(arg);
    });

                MessagingCenter.Subscribe<IVoiceRecorder, string>(this, "voiceRecorded",
                    (s, arg) => { SoundLocation = arg; });
            });

        }

        private void UnsubscribeMessengers()
        {
            Task.Run(() =>
            {
                MessagingCenter.Unsubscribe<IPictureTaker, string>(this, "pictureTaken");

                MessagingCenter.Unsubscribe<IVoiceRecorder, string>(this, "voiceRecorded");
            });
        }


        private void VoiceSwitch_OnToggled(object sender, ToggledEventArgs e)
        {
            recordButton.IsEnabled = !recordButton.IsEnabled;


            if (recordButton.IsEnabled)
            {
                synthLabel.TextColor = Color.White;
                voiceLabel.TextColor = Color.Lime;
                //  DependencyService.Get<IVoiceRecorder>().PrepareRecord();

            }
            else
            {
                synthLabel.TextColor = Color.Lime;
                voiceLabel.TextColor = Color.White;

            }
        }

        private void Record_Button_OnClicked(object sender, EventArgs e)
        {
            if (recordButton.Text.Equals("Record Voice"))
            {
                // DependencyService.Get<IVoiceRecorder>().Record();
                recordButton.Text = "Stop Recording";

                animationFlag = true;
                ColorAnimation(recordButton);

            }
            else
            {
                //  DependencyService.Get<IVoiceRecorder>().StopRecord();
                recordButton.Text = "Record Voice";
                animationFlag = false;
            }
        }

        private void ChoosePhoto(object sender, EventArgs e)
        {
            DependencyService.Get<IPictureTaker>().ChosePic();
        }

        private void TakePhoto(object sender, EventArgs e)
        {
            DependencyService.Get<IPictureTaker>().SnapPic();
        }

        private async void FinishRequested(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(ImageLocation))
            {
                await DisplayAlert("", "You forgot to make/choose photo", "Ok");
            }
            else if (string.IsNullOrEmpty(itemNameEntry.Text))
            {
                await DisplayAlert("", "You forgot to print the name", "Ok");
            }
            else
            {
                DataBaseInstance.DatabaseInstance.SavePlate(new DataPlate()
                {
                    IsSynth = !recordButton.IsEnabled,
                    ItemName = itemNameEntry.Text,
                    ImagePath = ImageLocation,
                    AudioPath = SoundLocation,
                    Category = category ?? "Food"
                });
                foreach (View view in stackLayout.Children)
                {
                    await Task.WhenAny(view.FadeTo(0, 100, Easing.SinIn), Task.Delay(20));
                    await Task.WhenAny(view.ScaleTo(0, 100, Easing.SinIn), Task.Delay(20));
                }
                await Navigation.PushModalAsync(new MainPage());

                Unhandler();
            }
        }

        private void Unhandler()
        {

            UnsubscribeMessengers();
            this.Dispose();
            GC.Collect();

        }

        private async Task StartAnimation()
        {
            double offset = 1000;

            foreach (View view in stackLayout.Children)
            {
                view.TranslationX = offset;
                view.Opacity = 0;
                offset *= -1;
            }

            foreach (View view in stackLayout.Children)
            {
                await Task.WhenAny(view.FadeTo(1, 250, Easing.SpringOut), Task.Delay(20));

                await Task.WhenAny(view.TranslateTo(0, 0, 250, Easing.SpringOut), Task.Delay(20));
            }

        }

        private async void ColorAnimation(Button btn)
        {

            Action<Color> btnBackCallback;
            while (animationFlag)
            {
                btnBackCallback = color => btn.BackgroundColor = color;

                await
                    Task.WhenAll(btn.RgbColorAnimation(Color.FromHex("#CB3A33"), Color.FromHex("#FF0C00"),
                            btnBackCallback, 500));
                await Task.WhenAll(btn.RgbColorAnimation(Color.FromHex("#FF0C00"), Color.FromHex("#CB3A33"),
                        btnBackCallback, 500));
            }

            btnBackCallback = null;

        }
    }
}
