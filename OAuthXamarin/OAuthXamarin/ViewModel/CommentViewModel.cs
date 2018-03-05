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
    public class CommentViewModel : INotifyPropertyChanged
    {
        private UserC userC;
        public INavigation Navigation;
        ApiService apiService;
        DialogService dialogService;
        public Command addCommentCommand { get; set; }
        public Command RefreshComments { get; set; }
        public CommentRequest comentario { get; set; }
        private List<CommentRequest> _listcomment { get; set; }
        public ComplainRequest denuncia { get; set; }
        private MediaFile file;
        private ImageSource imageSource;
        public UserC User
        {
            get { return userC; }
            set
            {
                userC = value;
                OnPropertyChanged();
            }
        }
        public List<CommentRequest> ListComment
        {
            get { return _listcomment; }
            set
            {
                _listcomment = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ListComment"));
            }
        }
        public bool isRefreshing;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CommentViewModel(ComplainRequest denuncia)
        {
            this.denuncia = denuncia;
            initComment();

            addCommentCommand = new Command(async () => await ExecuteAddCommentCommand());
            RefreshComments = new Command(async () => await ExecuteUpdateList());
        }
        async Task initComment()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            
            ListComment = await apiService.GetCommentsbyComplaint(denuncia);
            
            comentario = new CommentRequest();
            Debug.WriteLine("" + ListComment.Count);           
        }
        async Task ExecuteUpdateList()
        {
            comentario = new CommentRequest();
            ListComment = await apiService.GetCommentsbyComplaint(denuncia);
            IsRefreshing = false;
        }

        async Task ExecuteAddCommentCommand()
        {
            comentario.IdComplain = denuncia.IdComplain;
            comentario.IdUser = App.Instance.userC.IdUser;

         var comentar=  await apiService.PostComment(comentario);
            if (comentar != null && comentar.IsSuccess)
            {
                dialogService.ShowMessage("Comentario", comentar.Message);
                await ExecuteUpdateList();
                

            }

        }

        public bool IsRefreshing
        {
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRefreshing"));
                }
            }
            get { return isRefreshing; }
        }

    }
}
