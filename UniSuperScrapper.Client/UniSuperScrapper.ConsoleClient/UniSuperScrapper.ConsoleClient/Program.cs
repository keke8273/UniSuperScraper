using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UpSoft.UniSuperScrapper.DbAccess;

namespace UniSuperScrapper.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new UniSuperScrapperClientServiceClient())
            {
                Balance balance;
                var opRes = client.GetBalance(1, out balance);

                Console.WriteLine("{0} has ${1} @ {2}", balance.UserId, balance.Amount, balance.TimeStamp);
            }

            Console.ReadLine();
        }
    }
}
