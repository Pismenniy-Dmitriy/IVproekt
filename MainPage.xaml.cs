using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using IOTProjectt;

namespace IOTProjectt
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            DetectShakeTest Shake = new DetectShakeTest();
            Shake.ToggleAccelerometer();
        }
    }

    public static class GlobalVariables
    {
        public static bool Shake = false;
    }

    public class DetectShakeTest
    {
        SensorSpeed speed = SensorSpeed.Game;
        public DetectShakeTest()
        {
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
            Accelerometer.ShakeDetected += TakeVideoAsync;
        }

        void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            GlobalVariables.Shake = true;

        }

        public void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                else
                    Accelerometer.Start(speed);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                System.Environment.Exit(0);
            }
            catch (Exception ex)
            {
                System.Environment.Exit(0);
            }
        }

        [Obsolete]
        public static async void TakeVideoAsync(object sender, EventArgs e)
        {
                try
                {
                    var video = await MediaPicker.CaptureVideoAsync(new MediaPickerOptions
                    {
                        Title = $"project.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.mp4"
                    });

                    var newFile = Path.Combine(FileSystem.AppDataDirectory, video.FileName);
                    using (var stream = await video.OpenReadAsync())
                    using (var newStream = File.OpenWrite(newFile))
                        await stream.CopyToAsync(newStream);

                    if(newFile != null)
                {
                    Device.OpenUri(new Uri("https://www.google.al/intl/ru/drive/"));
                }
                }
                catch (Exception ex)
                {
                    System.Environment.Exit(0);
                }
            }
        

    }

}
