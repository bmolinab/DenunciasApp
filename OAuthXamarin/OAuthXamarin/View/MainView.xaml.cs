using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OAuthXamarin.ViewModel;
using OAuthXamarin.Services;
using System;
using System.Diagnostics;
using OAuthXamarin.Model;
using OAuthXamarin.Helpers;

namespace OAuthXamarin.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ContentPage
    {
        MainViewModel viewModel;

        public MainView()
        {
            InitializeComponent();
            try
            {
                
                if (App.Instance.userC==null)
                {
                    var id =Settings.userID;
                    if (id != null)
                    {
                        App.Instance.userC = new UserC
                        {
                            IdUser = id,
                        };
                    }

                }

            }
            catch
            {

            }
            viewModel = new MainViewModel(App.Instance.userC);
            viewModel.Navigation = this.Navigation;
            this.BindingContext = viewModel;

            ListDenuncias.ItemTapped += 
                (object sender, ItemTappedEventArgs e) => {
                if (e.Item == null) return;
                ((ListView)sender).SelectedItem = null; 
            };

            
        }          
}
}
