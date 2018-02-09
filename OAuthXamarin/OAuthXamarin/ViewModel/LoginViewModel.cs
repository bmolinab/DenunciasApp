using Newtonsoft.Json;
using OAuthXamarin.Helpers;
using OAuthXamarin.Model;
using OAuthXamarin.Services;
using Plugin.Geolocator;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OAuthXamarin.ViewModel
{
    public class LoginViewModel
    {
        ApiService apiService;
        DialogService dialogService;
        public Command FacebookLoginCommand { get; set; }
        public Command TwitterLoginCommand { get; set; }
        public Command RegisterCommand { get; set; }
        public Command LoginCommand { get; set; }
        
        public UserC usuario { get; set; }

        public INavigation Navigation;

        public LoginViewModel()
        {
            FacebookLoginCommand = new Command(async () => await ExecuteFacebookLoginCommand(Navigation));
            TwitterLoginCommand = new Command(async () => await ExecuteTwitterLoginCommand(Navigation));
            RegisterCommand = new Command(async () => await ExecuteRegisterCommand(Navigation));
            apiService = new ApiService();
            dialogService = new DialogService();
            var ciudad = GetGeoLocation();
            usuario = new UserC();
            LoginCommand = new Command(async() => await ExecuteLoginCommand(Navigation));
        }

        async Task ExecuteLoginCommand(INavigation navigation)
        {
            var request = await apiService.Login(usuario);
            if (request.IsSuccess)
            {
             //  await navigation.PushModalAsync(new View.RegisterView());
                var userc = JsonConvert.DeserializeObject<UserC>(request.Result.ToString());
             //   var userc = (UserC)request.Result;
                App.Instance.userC = userc;
                
                await navigation.PushAsync(new View.ProfileInfoView());               
            }
            else
            {
                await dialogService.ShowMessage(Constants.TittelApp, request.Message);
            }

        }

        async Task ExecuteRegisterCommand(INavigation navigation)
        {
            await navigation.PushModalAsync(new View.RegisterView());
        }
        async Task ExecuteFacebookLoginCommand(INavigation navigation)
        {          
            await navigation.PushModalAsync(new View.LoginFacebookView("Facebook"));
        }

        async Task ExecuteTwitterLoginCommand(INavigation navigation)
        {
            await navigation.PushModalAsync(new View.LoginTwitterView("Twitter"));
        }

        public async Task<string> GetGeoLocation()
        {


            var hasPermission = await Utils.CheckPermissions(Permission.Location);
            if (hasPermission)

            {

                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var Location = await locator.GetPositionAsync();
              //  var possibleAddresses = await locator(Location);
                //foreach (var a in possibleAddresses)
                //{
                //    if (a.Locality != null || a.Locality != " ")
                //    {
                //        return a.Locality.ToString();
                //    }
                //}
                return "No se encontro ciudad";
            }
            return "No tiene permiso ";

        }

    }
}
