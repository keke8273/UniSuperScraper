using System;
using System.Collections.Generic;

namespace UpSoft.UniSuperScrapper.DbAccess.Interface
{
    public interface IUniSuperScrapperDbAccessService
    {
        bool AddUser(string firstName, string lastName, string midName = null);

        bool GetAllUsers(out IList<User> users);

        bool AddBalance(int userId, DateTime timeStamp, decimal amount);

        bool GetCurrentBalance(int userId, out Balance balance);
    }
}
