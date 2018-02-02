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
	public partial class RegisterView : ContentPage
	{
        RegisterViewModel viewModel;

        public RegisterView ()
		{
			InitializeComponent ();
            viewModel = new RegisterViewModel();
            viewModel.Navigation = this.Navigation;
            this.BindingContext = viewModel;
        }
	}
}