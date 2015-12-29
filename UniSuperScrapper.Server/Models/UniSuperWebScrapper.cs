using System.Collections.Specialized;
using System.Text;

namespace UpSoft.UniSuperScrapper.ServiceLibrary.Models
{
    internal class UniSuperWebScrapper
    {
        private const string HomeAddress = "https://memberonline.unisuper.com.au/cps/memberonline/site/loginprep.xml";
        private const string LoginAddress = "https://memberonline.unisuper.com.au/cps/memberonline/site/login.xml";
        private const string DashBoardAddress = "https://memberonline.unisuper.com.au/cps/memberonline/site/Dashboard.htm";

        internal string DownloadStringDashPage(string userName, string password)
        {
            var client = new CookieAwareWebClient();

            var loginData = new NameValueCollection
            {
                {"username", userName},
                {"password", password}
            };

            client.DownloadString(HomeAddress);

            client.UploadValues(LoginAddress, "POST", loginData);

            return client.DownloadString(DashBoardAddress);
        }
    }
}
