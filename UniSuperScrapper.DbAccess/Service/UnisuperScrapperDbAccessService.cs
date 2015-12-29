using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UpSoft.UniSuperScrapper.DbAccess.Interface;

namespace UpSoft.UniSuperScrapper.DbAccess.Service
{
    public class UnisuperScrapperDbAccessService : IUniSuperScrapperDbAccessService
    {
        public bool AddUser(string firstName, string lastName, string midName = null)
        {
            using (var dbContext = new UnisuperContext())
            try
            {
                dbContext.Users.Add(new User() { FirstName = firstName, LastName = lastName, MiddleName = midName });
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool GetAllUsers(out IList<User> users)
        {
            using (var dbContext = new UnisuperContext())
                users = dbContext.Users.ToList<User>();
            return true;
        }

        public bool AddBalance(int userId, DateTime timeStamp, decimal amount)
        {
            var balance = new Balance() { UserId = userId, TimeStamp = timeStamp, Amount = amount };
            return AddBalance(balance);
        }

        public bool AddBalance(Balance newBalance)
        {
            using (var dbContext = new UnisuperContext())
            {
                if (dbContext.Balances.Any(b => b.Amount == newBalance.Amount && b.TimeStamp == newBalance.TimeStamp && b.UserId == newBalance.UserId))
                {
                    //Duplicated record
                    return false;
                }

                dbContext.Balances.Add(newBalance);
                dbContext.SaveChanges();
            }

            return true;
        }

        public bool GetCurrentBalance(int userId, out Balance balance)
        {
            using (var dbContext = new UnisuperContext())
                balance = dbContext.Balances.Include(b => b.User).OrderByDescending(b => b.Id).First(b => b.UserId == userId);
            return true;
        }

        public void FixEfProviderServicesProblem()
        {
            //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            //Make sure the provider assembly is available to the running application. 
            //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
