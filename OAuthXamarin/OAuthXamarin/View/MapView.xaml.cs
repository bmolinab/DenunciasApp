using OAuthXamarin.Model;
using OAuthXamarin.ViewModel;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace OAuthXamarin.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapView : ContentPage, INotifyPropertyChanged
    {
        MapViewModel viewModel;

        public MapView(List<ComplainRequest> _ListDenuncia)
        {
            InitializeComponent();
            viewModel = new MapViewModel(_ListDenuncia);
            viewModel.Navigation = this.Navigation;
            this.BindingContext = viewModel;
          //  Locator();

           

        }

        //async void Locator()
        //{
        //    var locator = CrossGeolocator.Current;
        //    locator.DesiredAccuracy = 25;
        //    var location = await locator.GetPositionAsync();
        //    var position = new Position(location.Latitude, location.Longitude);
        //    await Task.Delay(3000);
        //    MapSpan _mapRegion = MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(2));

        //    MapRegion = MapSpan.FromCenterAndRadius(MapRegion.Center, Distance.FromKilometers(100));

        //    //  Mapa.MoveToMapRegion((MapSpan.FromCenterAndRadius((position), Distance.FromMiles(.3))), true);
        //    //Mapa.MoveToRegion(MapSpan.FromCenterAndRadius((position), Distance.FromMiles(.3)));

        //}
    }
}