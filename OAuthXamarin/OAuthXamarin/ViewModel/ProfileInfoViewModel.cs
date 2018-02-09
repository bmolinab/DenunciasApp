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

namespace OAuthXamarin.ViewModel
{
    public class ProfileInfoViewModel : INotifyPropertyChanged
    {
        private UserC userC;
        public INavigation Navigation;
        ApiService apiService;
        DialogService dialogService;
        public Command addPhotoCommand { get; set; }
        private List<ComplainRequest> _listdenuncia { get; set; }
        Position Location;


        //public Command TakePhotoCommand { get; set; }
        //public Command PickPhotoCommand { get; set; }
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

        public  ProfileInfoViewModel(UserC user)
        {
            //TakePhotoCommand = new Command(async () => await ExecuteTakePhotoCommand());
            //PickPhotoCommand = new Command(async () => await ExecutePickPhotoCommand());           
            initProfile(user);
            addPhotoCommand = new Command(async () => await ExecuteAddPhotoCommand());
            Location = new Position();
            Location.Latitude = -1;
            Location.Longitude = -1;
        }

        async Task initProfile(UserC user)
        {

            apiService = new ApiService();
            dialogService = new DialogService();

            // ListDenuncia = new List<Complain>();

            ListDenuncia = await apiService.GetComplain();
            Debug.WriteLine("" + ListDenuncia.Count);

            if (user.FacebookId != null || user.TwitterId != null)
            {
                var x = await apiService.Register(user);
                var usuario = (UserC)x.Result;
                this.userC = usuario;
                Debug.WriteLine(userC.IdUser);
            }
            Settings.userID = userC.IdUser;
            Debug.WriteLine(Settings.userID);

        }



        public event PropertyChangedEventHandler PropertyChanged;
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



    }
}
