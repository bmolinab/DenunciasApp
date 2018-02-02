using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using OAuthXamarin;
using ImageCircle.Forms.Plugin.Droid;
using Android.Views;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using OAuthXamarin;
using FFImageLoading.Forms.Droid;
using Android.Util;

namespace OAuthXamarin.Droid

{
    [Activity(Label = "QuejasApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            CachedImageRenderer.Init(true);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            InitDeviceValues();
            //LoadApplication(new App());
            ImageCircleRenderer.Init();

            SetPage(App.Instance.GetMainPage());

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected void InitDeviceValues()
        {
            var metrics = Resources.DisplayMetrics;

            int statusBarHeight = 0, totalHeight = 0, contentHeight = 0;
            int resourceId = Resources.GetIdentifier("status_bar_height", "dimen", "android");
            statusBarHeight = Resources.GetDimensionPixelSize(resourceId);

            totalHeight = Resources.DisplayMetrics.HeightPixels;
            contentHeight = totalHeight - statusBarHeight;

            bool AppAlreadyInitialised = Measurements.VirtualScreenHeight != 0;

            Measurements.AvailableVirtualScreenHeight = (int)(contentHeight / metrics.Density);
            Measurements.VirtualScreenWidth = (int)(metrics.WidthPixels / metrics.Density);
            Measurements.HeightInPixels = metrics.HeightPixels;
            Measurements.WidthInPixels = metrics.WidthPixels;
            Measurements.VirtualScreenHeight = (int)(totalHeight / metrics.Density);

            int DPs = (int)TypedValue.ApplyDimension(ComplexUnitType.In, 1, metrics);
            Measurements.InchInVirtualUnits = (int)(DPs / metrics.Density);

            if (AppAlreadyInitialised == false)
            {
                Measurements.InitValues();
            }
        }

    }
}

