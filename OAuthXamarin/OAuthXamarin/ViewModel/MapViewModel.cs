using FFImageLoading.Forms;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using OAuthXamarin.Model;
using OAuthXamarin.Services;
using Plugin.Geolocator;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TK.CustomMap;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace OAuthXamarin.ViewModel
{
   

    public class MapViewModel : INotifyPropertyChanged
    {
    public INavigation Navigation;
    ApiService apiService;
    DialogService dialogService;
    public event PropertyChangedEventHandler PropertyChanged;
    private List<ComplainRequest> _listdenuncia { get; set; }    
    private MediaFile file;
        TK.CustomMap.MapSpan _mapRegion = TK.CustomMap.MapSpan.FromCenterAndRadius(new TK.CustomMap.Position(0, 0), TK.CustomMap.Distance.FromKilometers(2));


        public ObservableCollection<TKCustomMapPin> locations;
        public TKCustomMap mapa { get; set; }
        

        public List<ComplainRequest> ListDenuncia
        {
            get { return _listdenuncia; }
            set
            {
                _listdenuncia = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ListDenuncia"));
            }
        }

        public TK.CustomMap.MapSpan MapRegion
        {
            get { return _mapRegion; }
            set
            {
                if (_mapRegion != value)
                {
                    _mapRegion = value;
                    OnPropertyChanged("MapRegion");
                }
            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MapViewModel(List<ComplainRequest> _ListDenuncia)
        {
              Locator();
            locations = new ObservableCollection<TKCustomMapPin>();
            Locations.Clear();
            _listdenuncia = new List<ComplainRequest>();
            ListDenuncia.Clear();
            foreach (var denuncia in _ListDenuncia)
            {
                ListDenuncia.Add(denuncia);

                var cachedImage = new CachedImage()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    DownsampleToViewSize = true,
                 
                    Transformations = new System.Collections.Generic.List<ITransformation>() {
                     new GrayscaleTransformation(),
                     new CircleTransformation(),
                         },
                    Source = denuncia.Photo,
                    CacheType= FFImageLoading.Cache.CacheType.Disk                    
                };
                string imagenPin = "";
                Color colorpin = Color.White;
                if(denuncia.IdSubcategory<3)
                {
                    imagenPin = "qpt_mov";
                    colorpin = Color.Blue;

                }
                else if(denuncia.IdSubcategory<6)
                    {
                    imagenPin = "qpt_edu";
                    colorpin = Color.Red;

                }
                else if(denuncia.IdSubcategory<9)
                    {
                    imagenPin = "qpt_inc";
                    colorpin = Color.Yellow;

                }
                var pin = new TKCustomMapPin
                {
                    Position = new TK.CustomMap.Position((double)denuncia.Latitude, (double)denuncia.Longitude),
                    Title = denuncia.Title,
                    Subtitle= denuncia.Description,
                    DefaultPinColor=colorpin,
                    ShowCallout = true,  
                    
                }; 
               
                Locations.Add(pin); 
            }
        }



        public ObservableCollection<TKCustomMapPin> Locations
        {
            protected set
            {
                locations = Locations;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Locations"));

            }
            get { return locations; }
        }

        async void Locator()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 25;
            var location = await locator.GetPositionAsync();
            var position = new TK.CustomMap.Position(location.Latitude, location.Longitude);
            await Task.Delay(3000);
            _mapRegion = TK.CustomMap.MapSpan.FromCenterAndRadius(position, TK.CustomMap.Distance.FromKilometers(2));
            
            MapRegion = TK.CustomMap.MapSpan.FromCenterAndRadius(_mapRegion.Center, TK.CustomMap.Distance.FromKilometers(.3));

            
            


            //  Mapa.MoveToMapRegion((MapSpan.FromCenterAndRadius((position), Distance.FromMiles(.3))), true);
            //Mapa.MoveToRegion(MapSpan.FromCenterAndRadius((position), Distance.FromMiles(.3)));

        }

    }

}
