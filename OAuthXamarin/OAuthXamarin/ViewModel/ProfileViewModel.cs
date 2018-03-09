using OAuthXamarin.Model;
using OAuthXamarin.Services;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OAuthXamarin.ViewModel
{
        public class ProfileViewModel : INotifyPropertyChanged
    {
        private ProfileData userC;
        public INavigation Navigation;
        ApiService apiService;
        DialogService dialogService;
        public Command addCommentCommand { get; set; }
        public Command EditCommand { get; set; }
        public Command RefreshComments { get; set; }
        public CommentRequest comentario { get; set; }
        private List<ComplainRequest> _listdenuncia { get; set; }

        private bool isVisible = true;
        private bool isEditable = false;
        private bool isProfile = true;

        public bool IsEditable
        {
            set
            {
                if (isEditable != value)
                {
                    isEditable = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEditable"));
                }
            }
            get { return isEditable; }
        }
        public bool IsProfile
        {
            set
            {
                if (isProfile != value)
                {
                    isProfile = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsProfile"));
                }
            }
            get { return isProfile; }
        }
        public bool IsVisible
        {
            set
            {
                if (isVisible != value)
                {
                    isVisible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsVisible"));
                }
            }
            get { return isVisible; }
        }

        public ComplainRequest denuncia { get; set; }
        private MediaFile file;
        private ImageSource imageSource;
        public ProfileData User
        {
            get { return userC; }
            set
            {
                userC = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;



        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public List<ComplainRequest> ListDenuncia
        {
            get { return _listdenuncia; }
            set
            {
                _listdenuncia = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ListDenuncia"));
            }
        }
        public ProfileViewModel(bool areyou, int id)
        {
            this.denuncia = denuncia;
            IsVisible = areyou;
            if (areyou)
            {
                initComment();
            }
            else
            {
                initComment2(id);
            }
            EditCommand = new Command(async () => await ExecuteEditCommand());
            //RefreshComments = new Command(async () => await ExecuteUpdateList());
        }
        async Task ExecuteEditCommand()
        {
            IsProfile = false;
            IsEditable = true;
            IsVisible = false;
        }


        async Task initComment()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            User = await apiService.GetDataProfile(App.Instance.userC.IdUser);
            comentario = new CommentRequest();
            ListDenuncia = await apiService.MyComplain();
            Debug.WriteLine("" + comentario.UserName);
        }
        async Task initComment2(int id)
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            User = await apiService.GetDataProfile(id);
            comentario = new CommentRequest();
            ListDenuncia = await apiService.UserComplain(id);
            Debug.WriteLine("" + comentario.UserName);
        }
    }
}
