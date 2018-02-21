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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MapViewModel(List<ComplainRequest> _ListDenuncia)
        {
            mapa = new TKCustomMap();
            //  Locator();
            locations = new ObservableCollection<TKCustomMapPin>();
            _listdenuncia = new List<ComplainRequest>();
            ListDenuncia.Clear();
            foreach (var denuncia in _ListDenuncia)
            {
                ListDenuncia.Add(denuncia);
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


    }

}
