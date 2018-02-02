using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OAuthXamarin.Services;
using OAuthXamarin.Model;
using Xamarin.Forms;
using OAuthXamarin.Helpers;

namespace OAuthXamarin.ViewModel
{
    public class RegisterViewModel
    {
        
        public Command TakePhotoCommand { get; set; }
        public Command RegisterCommand { get; set; }
        public UserC UserData { get; set;}
        public INavigation Navigation;
        public ApiService services;
        public DialogService dialogService;


        public RegisterViewModel()
        {
            services = new ApiService();
            UserData = new UserC();
            dialogService = new DialogService();

            RegisterCommand = new Command(async () => await ExecuteRegisterCommand(UserData));

        }

        async Task ExecuteTakePhotoCommand(INavigation navigation)
        {
            await navigation.PushModalAsync(new View.RegisterView());
        }

        async Task ExecuteRegisterCommand(UserC userC)
        {
          var a=  await services.Register(userC) ;
            if (a.IsSuccess)
            {
                dialogService.ShowMessage(Constants.TittelApp, "¡Registro realizado con éxito!");
            }

        }

    }
}
