using OAuthXamarin.Model;
using OAuthXamarin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OAuthXamarin.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapView : ContentPage
    {
        MapViewModel viewModel;

        public MapView(List<ComplainRequest> _ListDenuncia)
        {
            InitializeComponent();

            viewModel = new MapViewModel(_ListDenuncia);
            viewModel.Navigation = this.Navigation;
            this.BindingContext = viewModel;
        }
    }
}