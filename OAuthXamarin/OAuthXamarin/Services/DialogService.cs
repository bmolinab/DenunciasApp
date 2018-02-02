using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthXamarin.Services
{
    public class DialogService
    {
        public async Task ShowMessage(string title, string message)
        {
            await App.Current.MainPage.DisplayAlert(title, message, "Aceptar");
        }
       public async Task<string> OptionMessage(string mensaje, string op1, string op2)
        {
            var answer = await App.Current.MainPage.DisplayActionSheet(mensaje, "Cancelar", null, op1, op2);

            return answer;
        }
    }
}
