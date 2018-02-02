using OAuthXamarin.Model;
using OAuthXamarin.Services;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OAuthXamarin.ViewModel
{
    public class NewPostViewModel: INotifyPropertyChanged
    {
        public Command PostCommand { get; set; }
        ApiService apiService;
        public INavigation Navigation;

        private MediaFile file;
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
        public NewPostViewModel(ImageSource IS)
        {
            apiService = new ApiService();
            PostCommand = new Command(async () => await ExecutePostCommand());
          
            ImageSource = IS;

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


        public async Task initData()
        {
            apiService = new ApiService();
            category = new List<Category>();
            Category = await apiService.GetCategory();            
        }

        async Task ExecutePostCommand()
        {
            var denuncia = new Complain
            {
                IdUser = App.Instance.userC.IdUser,
                Title = "test",
                Photo= "https://www.24morelos.com/wp-content/uploads/2017/07/bache.png",
                Description="Test descripcion",           
            };
            await apiService.PostComplain(denuncia);
        }


    }
}
