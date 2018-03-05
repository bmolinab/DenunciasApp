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
    public partial class ProfileView : ContentPage
    {
        ProfileViewModel viewModel;
        public ProfileView(bool areyou, int id)
        {
            viewModel = new ProfileViewModel(areyou, id);
            InitializeComponent();
            viewModel.Navigation = this.Navigation;
            this.BindingContext = viewModel;
        }
    }
}