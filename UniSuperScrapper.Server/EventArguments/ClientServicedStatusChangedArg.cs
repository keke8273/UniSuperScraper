using System;

namespace UpSoft.UniSuperScrapper.ServiceLibrary.EventArguments
{

    public class ClientServicedStatusChangedArg : EventArgs
    {
        private readonly string _message;

        public ClientServicedStatusChangedArg(string message)
        {
            _message = message;
        }

        public string Message {
            get { return _message; }
        }
    }
}
