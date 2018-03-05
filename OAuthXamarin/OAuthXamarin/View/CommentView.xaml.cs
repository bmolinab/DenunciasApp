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
    public partial class CommentView : ContentPage
    {
        CommentViewModel viewModel;
        public CommentView(ComplainRequest _denuncia)
        {
            InitializeComponent();
            viewModel = new CommentViewModel(_denuncia);
            viewModel.Navigation = this.Navigation;
            this.BindingContext = viewModel;
        }
    }
}