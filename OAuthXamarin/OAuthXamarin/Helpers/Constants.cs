using OAuthXamarin.Model;

namespace OAuthXamarin.Helpers
{
    public  class Constants
    {
        #region SocialNetworkConection
        public const string FacebookAppID = "204055350149161";
        public const string FacebookAuthURL = "https://m.facebook.com/dialog/oauth/";
        public const string FacebookRedirectURL = "https://www.facebook.com/connect/login_success.html";
        public const string FacebookProfileInfoURL = "https://graph.facebook.com/me?fields=name,picture,work,website,religion,location,locale,link,cover,age_range,birthday,devices,email,first_name,last_name,gender,hometown,is_verified,languages";
        public const string FacebookScope = "email";

        public const string TwitterKey = "4gkqH38zuIkjyWhLiLGeg";
        public const string TwitterSecret = "dlNNrJjh1KtPxyZtEIuwbzvJbXICNjllvjSMV7xI";
        public const string TwitterRequestURL = "https://api.twitter.com/oauth/request_token";
        public static string TwitterAuthURL = "https://twitter.com/oauth/authenticate";
        public const string TwitterURLAccess = "https://api.twitter.com/oauth/access_token";
        public const string TwitterCallbackURL = "http://digitalstrategy.com.ec/";
        public const string TwitterProfileInfoURL = "https://api.twitter.com/1.1/account/verify_credentials.json";
        #endregion

        #region AppConstants
        public const string ServiceUrl = "http://quejasws.azurewebsites.net";
        public const string Usuario = "/api/UserCs/";
        public const string Denuncia = "/api/Complains/";
        public const string Category = "/api/Categories/";
        public const string SubCategory = "api/Subcategories/";

        public const string TittelApp = "QuejasAPP";
        public ImgTemp imgTemp { get; internal set; }
        #endregion
    }
}
