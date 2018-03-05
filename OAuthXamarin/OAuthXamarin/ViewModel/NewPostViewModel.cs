using OAuthXamarin.Helpers;
using OAuthXamarin.Model;
using OAuthXamarin.Services;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OAuthXamarin.ViewModel
{
    public  class NewPostViewModel: INotifyPropertyChanged
    {
        ApiService apiService;
        public INavigation Navigation;
        public int idUser=0;
        DialogService dialogService;
        private MediaFile file;

        #region Porperties
        public Complain denuncia { get; set; }
        public Command PostCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Category> category;
        public List<Category> Category
        
        {
            set
            {
                if (category != value)
                {
                    category= value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Category"));
                }
            }
            get
            {
                return category;
            }

        }
        public List<Subcategory> subcategory;
        public List<Subcategory> Subcategory
        {
            set
            {
                if (subcategory != value)
                {
                    subcategory = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Subcategory"));
                }
            }
            get
            {
                return subcategory;
            }

        }
        private ImageSource imageSource;
        public ImageSource ImageSource
        {
            set
            {
                if (imageSource != value)
                {
                    imageSource = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ImageSource"));
                }
            }
            get
            {
                return imageSource;
            }
        }
        #endregion
        public NewPostViewModel(ImageSource IS, MediaFile file)
        {
            dialogService = new DialogService();
            apiService = new ApiService();
            denuncia = new Complain();
            idUser = App.Instance.userC.IdUser;
            PostCommand = new Command(async () => await ExecutePostCommand(Navigation));          
            ImageSource = IS;
            this.file = file;




        }


        Category catseleccionada;
        public Category CatSelectedItem
        {
            get
            {
                return catseleccionada;
            }
            set
            {
                // marcaseleccionada = ;
                LoadSubCategory(value);
                
                catseleccionada = value;
            }
        }
        public async Task LoadSubCategory(Category _category)
        {
            apiService = new ApiService();
            subcategory = new List<Subcategory>();
            Subcategory = await apiService.GetSubCategoryByCategory(_category);
        }
        Subcategory Subcatseleccionada;
        public Subcategory SubCatSelectedItem
        {
            get
            {
                return Subcatseleccionada;
            }
            set
            {
                // marcaseleccionada = ;

                Subcatseleccionada = value;
            }
        }



   

        public async Task initData()
        {
            apiService = new ApiService();
            category = new List<Category>();
            Category = await apiService.GetCategory();

            if (CrossGeolocator.Current.IsGeolocationEnabled)
            {
                try
                {
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;
                  
                   
                    var position = await locator.GetPositionAsync();

                    denuncia.Longitude =position.Longitude;

                    denuncia.Latitude =position.Latitude;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("Error : {0}", e);
                }
            }



        }
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        async Task ExecutePostCommand(INavigation navigation)
        {
           
            Debug.WriteLine(denuncia.Title);
            denuncia.IdUser = idUser ;
            denuncia.IdSubcategory = Subcatseleccionada.IdSubcategory;
            var array = ReadFully(file.GetStream());
          DataFile archivo=  new DataFile 
            {
                Name = denuncia.Title+denuncia.IdUser+".jpg",
                Extension = ".jpg",
                FileData = array
            };
            var denunciaResponse = await apiService.PostComplain(denuncia, archivo );
            if (denunciaResponse.IsSuccess)
            {
                await dialogService.ShowMessage(Constants.TittelApp, "La denuncia se realizó con éxito");
                await navigation.PushAsync(new View.MainView());
                
            }
        }


    }
}
