using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

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
    public class DetectShakeTest
    {
        SensorSpeed speed = SensorSpeed.Game;
        bool Shake = false;
        public DetectShakeTest()
        {
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
        }

        void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            Shake = true;

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
    }
}
