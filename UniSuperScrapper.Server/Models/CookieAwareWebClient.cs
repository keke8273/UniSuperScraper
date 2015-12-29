using System;
using System.Net;

namespace UpSoft.UniSuperScrapper.ServiceLibrary.Models
{
    [System.ComponentModel.DesignerCategory("Code")]
    internal class CookieAwareWebClient : WebClient
    {
        //An aptly named container to store the Cookie
        public CookieContainer CookieContainer { get; private set; }

        public CookieAwareWebClient()
        {
            CookieContainer = new CookieContainer();
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            //Grabs the base request being made 
            var request = (HttpWebRequest)base.GetWebRequest(address);
            //Adds the existing cookie container to the Request
            request.CookieContainer = CookieContainer;

            return request;
        }
    }
}
