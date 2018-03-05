using Newtonsoft.Json;
using OAuthXamarin.Helpers;
using OAuthXamarin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using OAuthXamarin.Helpers;
using System.IO;
using System.Diagnostics;

namespace OAuthXamarin.Services
{
    public class ApiService
    {
        public async Task<Response> Login(UserC usuario)
        {
            try
            {              
                var request = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(Constants.ServiceUrl);
                var url =Constants.Usuario+"Login";
                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Usuario o Contraseña incorrecto",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<Response>(result);
                return  user;
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                throw;
            }

        }   
        public async Task<Response> Register(UserC usuario)
        {
            try
            {               
                var request = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(Constants.ServiceUrl);
                var url = Constants.Usuario + "Registrar";
                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Usuario o Contraseña incorrecto",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserC>(result);                                
                return new Response
                {
                    IsSuccess=true,
                    Message="UserData",
                    Result = user
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                throw;
            }
        }
        public async Task<Response> PostComplain(Complain denuncia, DataFile photo)
        {
            try
            {
                var requestphoto = JsonConvert.SerializeObject(photo);
                var contentphoto = new StringContent(requestphoto, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(Constants.ServiceUrl);
                var urlphoto = Constants.Denuncia + "UploadComplainPicture";
                var responsephoto = await client.PostAsync(urlphoto, contentphoto);
                if (!responsephoto.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "error con la foto",
                    };
                }
                var resultphoto = await responsephoto.Content.ReadAsStringAsync();
                var photodata = JsonConvert.DeserializeObject<Response>(resultphoto);
                denuncia.Photo = Constants.ServiceUrl + "/" + photodata.Message;
                var request = JsonConvert.SerializeObject(denuncia);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var url = Constants.Denuncia + "PostComplain";
                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Denuncia inCorrecta",
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                var DenunciaResponse = JsonConvert.DeserializeObject<Response>(result);
                if (DenunciaResponse.IsSuccess)
                {
                    Debug.WriteLine("funciona");
                }               
                return DenunciaResponse;
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                throw;
            }
        }       
        public async Task<List<Category>> GetCategory()
        {
            try
            {
                var client = new System.Net.Http.HttpClient();
                var response = await client.GetAsync(Constants.ServiceUrl+ Constants.Category + "GetCategorias");
                string CatJson = await response.Content.ReadAsStringAsync();
                var catResponse = JsonConvert.DeserializeObject<Response>(CatJson);
                var  catList = JsonConvert.DeserializeObject<List<Category>>(catResponse.Result.ToString());
                return catList;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<List<Subcategory>> GetSubCategoryByCategory(Category categoria)
        {
            try
            {
                var request = JsonConvert.SerializeObject(categoria);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(Constants.ServiceUrl);
                var url = Constants.SubCategory + "SubcategoryByCategory";
                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var result = await response.Content.ReadAsStringAsync();
                var subcatResponse = JsonConvert.DeserializeObject<Response>(result);
                var subcatList = JsonConvert.DeserializeObject<List<Subcategory>>(subcatResponse.Result.ToString());
                return subcatList;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }

        }
        public async Task<List<CommentRequest>> GetCommentsbyComplaint(ComplainRequest denuncia)
        {
            try
            {
                var request = JsonConvert.SerializeObject(denuncia);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(Constants.ServiceUrl);
                var url = Constants.Comments + "byComplaint";
                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var result = await response.Content.ReadAsStringAsync();
                if (result != null)
                {
                    var Comentarios = JsonConvert.DeserializeObject<List<CommentRequest>>(result);
                    return Comentarios;

                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }


        }

        public async Task<ProfileData> GetDataProfile(int id)
        {
            try
            {
                var request = JsonConvert.SerializeObject(id);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(Constants.ServiceUrl);
                var url = Constants.Usuario + "GetDataProfile";
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();
                var Datos = JsonConvert.DeserializeObject<ProfileData>(result);
                return Datos;
            }
            catch
            {
                return null;
            }

        }

        public async Task<Response> PostComment(CommentRequest comentario)
        {
            try
            {
                var request = JsonConvert.SerializeObject(comentario);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(Constants.ServiceUrl);
                var url = Constants.Comments + "Post";
                var response = await client.PostAsync(url, content);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var result = await response.Content.ReadAsStringAsync();
                if (result != null)
                {
                    var Comentarios = JsonConvert.DeserializeObject<Response>(result);
                    return Comentarios;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                return null;
                throw;
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
        public async Task<List<ComplainRequest>>GetComplain()
        {
            try
            {
                var client = new System.Net.Http.HttpClient();
                var response = await client.GetAsync(Constants.ServiceUrl + Constants.Denuncia+ "GetApproved" );
                string ComplainJson = await response.Content.ReadAsStringAsync();
                var ComplainResponse = JsonConvert.DeserializeObject(ComplainJson);
                var complainList = JsonConvert.DeserializeObject<List<ComplainRequest>>(ComplainResponse.ToString());
                return complainList;
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public async Task<List<ComplainRequest>> MyComplain()
        {
            try
            {
                var request = JsonConvert.SerializeObject(App.Instance.userC.IdUser);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(Constants.ServiceUrl);
                var url = Constants.Denuncia + "MyComplain";
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();
                var Datos = JsonConvert.DeserializeObject<List<ComplainRequest>>(result);
                return Datos;
            }
            catch
            {
                return null;
            }

        }

        public async Task<List<ComplainRequest>> UserComplain(int id)
        {
            try
            {
                var request = JsonConvert.SerializeObject(id);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(Constants.ServiceUrl);
                var url = Constants.Denuncia + "UserComplain";
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();
                var Datos = JsonConvert.DeserializeObject<List<ComplainRequest>>(result);
                return Datos;
            }
            catch
            {
                return null;
            }

        }


    }
}
