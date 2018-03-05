using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OAuthXamarin.ViewModel;
using OAuthXamarin.Services;
using System;
using System.Diagnostics;
using OAuthXamarin.Model;

namespace OAuthXamarin.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ContentPage
    {
        MainViewModel viewModel;

        public MainView()
        {
            InitializeComponent();

            viewModel = new MainViewModel(App.Instance.userC);
            viewModel.Navigation = this.Navigation;
            this.BindingContext = viewModel;

            ListDenuncias.ItemTapped += (object sender, ItemTappedEventArgs e) => {
                if (e.Item == null) return;
                ((ListView)sender).SelectedItem = null; 
            };

        }          
}
}
