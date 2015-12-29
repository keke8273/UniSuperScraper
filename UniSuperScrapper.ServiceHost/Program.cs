using System;
using System.ServiceModel;
using UpSoft.UniSuperScrapper.ServiceLibrary;
using UpSoft.UniSuperScrapper.ServiceLibrary.EventArguments;

namespace UniSuperScrapper.ClientServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***** Console Based WCF Host *****");

            var uniSuperScrapperClientService = new UniSuperScrapperClientService();
            uniSuperScrapperClientService.ClientServicedStatusChanged += OnClientServicedStatusChanged;

            using (var serviceHost = new ServiceHost(uniSuperScrapperClientService))
            {
                serviceHost.Open();

                DisplayInfo(serviceHost);

                uniSuperScrapperClientService.Run();

                Console.ReadLine();
            }
        }

        private static void DisplayInfo(ServiceHost serviceHost)
        {
            foreach (var endpoint in serviceHost.Description.Endpoints)
            {
                Console.WriteLine("Address: {0}", endpoint.Address);
                Console.WriteLine("Binding: {0}", endpoint.Binding.Name);
                Console.WriteLine("Contract: {0}", endpoint.Contract.Name);
                Console.WriteLine("");
            }
        }

        private static void OnClientServicedStatusChanged(object sender, ClientServicedStatusChangedArg eventArg)
        {
            Console.WriteLine(eventArg.Message);
        }
    }
}
