using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using UpSoft.UniSuperScrapper.ClientService;
using UpSoft.UniSuperScrapper.DbAccess;
using UpSoft.UniSuperScrapper.DbAccess.Service;
using UpSoft.UniSuperScrapper.ServiceLibrary.EventArguments;
using UpSoft.UniSuperScrapper.ServiceLibrary.Interfaces;
using UpSoft.UniSuperScrapper.ServiceLibrary.Models;

namespace UpSoft.UniSuperScrapper.ServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class UniSuperScrapperClientService : IUniSuperScrapperClientService
    {
        private readonly UnisuperScrapperDbAccessService _dbAccessService;
        private readonly UniSuperWebScrapper _webScrapper;
        private readonly UniSuperWebParser _webParser;
        private bool _isRunning = false;

        public UniSuperScrapperClientService()
        {
            _dbAccessService = new UnisuperScrapperDbAccessService();
            _webScrapper = new UniSuperWebScrapper();
            _webParser = new UniSuperWebParser();
        }

        public void Run()
        {
            ThreadPool.QueueUserWorkItem(
                (o) =>
                {
                    while (true)
                    {
                        RunOnce();
                        Thread.Sleep(Properties.Settings.Default.ExecutionPeriod);
                    }
                }
                );
            RaiseClientServicedStatusChanged(new ClientServicedStatusChangedArg("Web monitor engine started"));
        }

        public void RunOnce()
        {
            if (_isRunning) return;

            _isRunning = true;

            //Get all the users
            IList<User> users;
            if (!_dbAccessService.GetAllUsers(out users)) return;
            
            foreach (var user in users)
            {
                RaiseClientServicedStatusChanged(new ClientServicedStatusChangedArg(string.Format("Retrieving balance for {0} {1}", user.FirstName.Trim(), user.LastName.Trim())));

                var html = _webScrapper.DownloadStringDashPage(user.UnisuperUsername, user.UnisuperPassword);
                var newBalance = _webParser.ParseBalance(html);
                if (newBalance == null) continue;
                newBalance.UserId = user.Id;
                Balance lastBalance;

                _dbAccessService.GetCurrentBalance(user.Id, out lastBalance);

                if (_dbAccessService.AddBalance(newBalance))
                {
                    RaiseClientServicedStatusChanged(new ClientServicedStatusChangedArg(string.Format("Current balance for {0} {1} : ${2} at {3}", 
                        user.FirstName.Trim(), 
                        user.LastName.Trim(), 
                        newBalance.Amount,
                        newBalance.TimeStamp)));

                    RaiseClientServicedStatusChanged(new ClientServicedStatusChangedArg(string.Format("Last balance for {0} {1} : ${2} at {3}",
                        user.FirstName.Trim(),
                        user.LastName.Trim(),
                        lastBalance.Amount,
                        lastBalance.TimeStamp)));

                    var diff = new BalanceDifference(lastBalance, newBalance);

                    RaiseClientServicedStatusChanged(new ClientServicedStatusChangedArg(string.Format("Balance {0} by ${1}, {2:P}",
                            diff.TransitionType,
                            diff.Amount,
                            diff.Percentage)));
                }
                else
                {
                    RaiseClientServicedStatusChanged(new ClientServicedStatusChangedArg(string.Format("Duplicated balance for {0} {1} :${2} at {3}",
                        user.FirstName.Trim(),
                        user.LastName.Trim(),
                        newBalance.Amount,
                        newBalance.TimeStamp))); ;
                }
            }

            _isRunning = false;
        }

        public OperationResult RegisterUser(string firstName, string lastName, string midName, string userName, string password)
        {
            throw new NotImplementedException();
        }

        public OperationResult Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public OperationResult GetBalance(int userId, out Balance balance)
        {
            RaiseClientServicedStatusChanged(new ClientServicedStatusChangedArg(string.Format("Get balance for userid: {0}", userId)));

            var res = OperationResult.Fialed;

            if (_dbAccessService.GetCurrentBalance(userId, out balance))
            {
                res = OperationResult.Successful;
            }

            return res;
        }

        public OperationResult GetHistory(int userId, TimeSpan timespan, ref IList<Balance> balances)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<ClientServicedStatusChangedArg> ClientServicedStatusChanged;

        protected virtual void RaiseClientServicedStatusChanged(ClientServicedStatusChangedArg e)
        {
            var handler = ClientServicedStatusChanged;
            if (handler != null) 
                handler(this, e);
        }
    }
}
