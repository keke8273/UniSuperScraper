using System;
using System.Collections.Generic;
using System.ServiceModel;
using UpSoft.UniSuperScrapper.ClientService;
using UpSoft.UniSuperScrapper.DbAccess;

namespace UpSoft.UniSuperScrapper.ServiceLibrary.Interfaces
{
    [ServiceContract(Namespace = "http://upsoft.com.au")]
    public interface IUniSuperScrapperClientService
    {
        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="firstName">User's first name.</param>
        /// <param name="lastName">User's last name.</param>
        /// <param name="midName">Uer's middle name (optioanl).</param>
        /// <param name="userName">Username to login to UniSuper.</param>
        /// <param name="password">Password to login to UniSuper.</param>
        [OperationContract]
        OperationResult RegisterUser(string firstName, string lastName, string midName, string userName, string password);

        [OperationContract]
        OperationResult Login(string userName, string password);

        [OperationContract]
        OperationResult GetBalance(int userId, out Balance balance);

        [OperationContract]
        OperationResult GetHistory(int userId, TimeSpan timespan, ref IList<Balance> balances);
    }

    public interface IUniSuperScrapperClientServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void NotifyBalance(Balance balance);
    }
}