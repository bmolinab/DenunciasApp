using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using OAuthXamarin.Model;
using OAuthXamarin.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using OAuthXamarin.Helpers;
using System.Diagnostics;
using OAuthXamarin.View;
using System.Collections.Generic;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System.Windows.Input;

namespace OAuthXamarin.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private UserC userC;
        public INavigation Navigation;
        ApiService apiService;
        DialogService dialogService;
        public Command addPhotoCommand { get; set; }
        public Command viewMapCommand { get; set; }
        public Command RefreshDenuncias { get; set; }
        public Command MyProfileCommand { get; set; }

        private Command<object> tapCommand;


        public Command<object> TapCommand
        {
            get { return tapCommand; }
            set { tapCommand = value; }
        }

        private Command<object> tapCommand2;


        public Command<object> TapCommand2
        {
            get { return tapCommand2; }
            set { tapCommand2 = value; }
        }

        private List<ComplainRequest> _listdenuncia { get; set; }
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
        public List<ComplainRequest> ListDenuncia
        {
            get { return _listdenuncia; }
            set
            {
                _listdenuncia = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ListDenuncia"));
            }
        }
        public bool isRefreshing;

        public MainViewModel(UserC user)
        {               
            initProfile(user);
            addPhotoCommand = new Command(async () => await ExecuteAddPhotoCommand());
            viewMapCommand = new Command(async () => await ExecuteMapCommand());
            RefreshDenuncias = new Command(async () => await ExecuteUpdateList());
            MyProfileCommand = new Command(async () => await ExecuteMyProfileCommand());

            tapCommand = new Command<object>(OnTapped);
            tapCommand2 = new Command<object>(OnTapped2);

            //  CommentCommand = new Command(async () => await commentCommand(Navigation));
        }

        private async void OnTapped(object obj)
        {
            ComplainRequest denuncia = (ComplainRequest) obj;
            await Navigation.PushAsync(new CommentView(denuncia));
            Debug.WriteLine("hola");
        }

        private async void OnTapped2(object obj)
        {
            ComplainRequest denuncia = (ComplainRequest)obj;
            await Navigation.PushAsync(new ProfileView(false, denuncia.IdUser));
            Debug.WriteLine("hola");
        }

        async Task initProfile(UserC user)
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            ListDenuncia = await apiService.GetComplain();
            Debug.WriteLine("" + ListDenuncia.Count);
            if (user.FacebookId != null || user.TwitterId != null)
            {
                var x = await apiService.Register(user);
                var usuario = (UserC)x.Result;
                App.Instance.userC = usuario;
                this.userC = usuario;
                Debug.WriteLine(userC.IdUser);
            }
            Settings.userID = userC.IdUser;
            Debug.WriteLine(Settings.userID);
        }
        async Task ExecuteUpdateList()
        {
            ListDenuncia = await apiService.GetComplain();
            IsRefreshing = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        async Task ExecuteAddPhotoCommand()
        {
            string mensaje = "Crea una nueva publicación";
            string op1 = "Tomar foto";
            string op2 = "Elegir de galería";


            string Type = await dialogService.OptionMessage(mensaje, op1, op2);
          if (Type.Equals(op1))
            {
               await ExecuteTakePhotoCommand();

            }
          else if (Type.Equals(op2))
            {
                await ExecutePickPhotoCommand();
            }

        }
        async Task ExecutePost(INavigation navigation)
        {
            //  await navigation.PushModalAsync(new View.RegisterView());
            App.Current.MainPage = new RegisterView();

            await App.Navigator.PushAsync(new RegisterView());


            Debug.WriteLine("hola");
        }
        async Task ExecuteTakePhotoCommand()
        {
            await Plugin.Media.CrossMedia.Current.Initialize();

            if (!Plugin.Media.CrossMedia.Current.IsCameraAvailable || !Plugin.Media.CrossMedia.Current.IsTakePhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "Aceptar");
            }

            file = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Photos",
                Name = "NuevaMulta.jpg",
                PhotoSize = PhotoSize.Small,
                AllowCropping=true,
                
               
                
                //CustomPhotoSize=100,
                //CompressionQuality=80,
            });

            if (file != null)
            {
              var  imageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();

                    return stream;
                });
                // App.Current.MainPage =new  NewPostView(imageSource);
               
               await App._NavPage.Navigation.PushAsync(new NewPostView(imageSource, file));

                // await App.Navigator.PushAsync(new NewPostView("1"));


            }
        }
        async Task ExecutePickPhotoCommand()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
               await dialogService.ShowMessage("Photos Not Supported", ":( Permission not granted to photos.");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

            });


            if (file == null)
                return;

          var  image = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });

            await App._NavPage.Navigation.PushAsync(new NewPostView(image, file));

        }
        async Task ExecuteMapCommand()
        {            
            await App._NavPage.Navigation.PushAsync(new MapView(ListDenuncia));
        }
        async Task ExecuteMyProfileCommand()
        {
            await App._NavPage.Navigation.PushAsync(new ProfileView(true,App.Instance.userC.IdUser));
           // await Navigation.PushAsync(new ProfileView());
        }


    }

}