using OAuthXamarin.Helpers;
using OAuthXamarin.ViewModel;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OAuthXamarin.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewPostView : ContentPage
	{
        NewPostViewModel viewModel;
        public NewPostView(ImageSource IS, MediaFile file)
        {
            viewModel = new NewPostViewModel(IS,file);
            viewModel.initData();
            InitializeComponent();
            viewModel.Navigation = this.Navigation;
            this.BindingContext = viewModel;
        }
    }
}