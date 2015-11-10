using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SumpPal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Private Fields

        private GpioController gpio;
        private GpioPin pin4;

        #endregion Private Fields

        #region Public Fields

        // Using a DependencyProperty as the backing store for SumpIsOn.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SumpIsOnProperty =
            DependencyProperty.Register("SumpIsOn", typeof(bool), typeof(MainPage), new PropertyMetadata(0));

        #endregion Public Fields

        #region Public Properties

        public bool SumpIsOn
        {
            get
            {
                return (bool)GetValue(SumpIsOnProperty);
            }
            set
            {
                if (gpio != null && pin4 != null)
                {
                    pin4.Write(value ? GpioPinValue.Low : GpioPinValue.High);
                }

                SetValue(SumpIsOnProperty, value);
            }
        }

        #endregion Public Properties

        #region Public Constructors

        public MainPage()
        {
            this.InitializeComponent();

            gpio = GpioController.GetDefault();
            pin4 = gpio.OpenPin(4);
            pin4.SetDriveMode(GpioPinDriveMode.Output);
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task Execute()
        {
            while (true)
            {
                SumpIsOn = true;
                await Task.Delay(TimeSpan.FromSeconds(1));

                SumpIsOn = false;
                await Task.Delay(TimeSpan.FromMilliseconds(600));
            }
        }

        #endregion Public Methods

        #region Private Methods

        private async void Main_Loaded(object sender, RoutedEventArgs e)
        {
            await Execute();
        }

        #endregion Private Methods
    }
}