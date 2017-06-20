using Microsoft.Toolkit.Uwp.UI.Animations;
using RssReader.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RssReader
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    partial class ExtendedSplash : Page
    {
        Compositor _compositor;
        SpriteVisual _hostSprite;

        internal Rect splashImageRect; // Rect to store splash screen image coordinates.
        private SplashScreen splash; // Variable to hold the splash screen object.
        internal bool dismissed = false; // Variable to track splash screen dismissal status.
        

        // Define methods and constructor
        public ExtendedSplash(SplashScreen splashscreen, bool loadState)
        {
            this.InitializeComponent();

            splash = splashscreen;
            if (splash != null)
            {
                applyAcrylicAccent(mainGrid);
                // Register an event handler to be executed when the splash screen has been dismissed.
                splash.Dismissed += new TypedEventHandler<SplashScreen, Object>(DismissedEventHandler);

                // Retrieve the window coordinates of the splash screen image.
                splashImageRect = splash.ImageLocation;


            }

            // Create a Frame to act as the navigation context
           
        }

        // Include code to be executed when the system has transitioned from the splash screen to the extended splash screen (application's first view).
        async void DismissedEventHandler(SplashScreen sender, object e)
        {
            dismissed = true;
            // Complete app setup operations here...
            //applyAcrylicAccentAndFade(mainGrid);
            await Task.Delay(2500);
            DismissExtendedSplash();
        }

        async void DismissExtendedSplash()
        {

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                Window.Current.Content = null;
                App.shell.Content = null;
                //App.shell.AppFrame.Navigate(typeof(MasterDetailPage));
                App.shell = new AppShell();
                App.shell.AppFrame.Navigate(typeof(MasterDetailPage));
                Window.Current.Content = App.shell;
            });

        }



        private void mainGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_hostSprite != null)
                _hostSprite.Size = e.NewSize.ToVector2();
        }

        private async void applyAcrylicAccent(Panel panel)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
                _hostSprite = _compositor.CreateSpriteVisual();
                _hostSprite.Size = new Vector2((float)panel.ActualWidth, (float)panel.ActualHeight);

                ElementCompositionPreview.SetElementChildVisual(panel, _hostSprite);
                _hostSprite.Brush = _compositor.CreateHostBackdropBrush();
                mainGrid.Fade(1f, 2500).StartAsync();
            });

        }

        private void fadeAnimation()
        {





        }
    }
}
