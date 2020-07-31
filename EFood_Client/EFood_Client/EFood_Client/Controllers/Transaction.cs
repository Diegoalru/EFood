using System;
using System.Threading.Tasks;
using EFoodDB.EFood_Client;

namespace EFood_Client.Controllers
{
    public static class Transaction
    {
        private static string TransactionId { get; set; } = string.Empty;

        public static string GetTransaction() => TransactionId;

        public static async Task<int> InitalizeTransaction()
        {
            var methods = new ClientMethods();
            while (true)
            {
                var transaction = await methods.CreateTransaction();
                var existResult = await methods.ExistTransacction(transaction);
                switch (existResult)
                {
                    case true:
                        continue;
                    
                    case false:
                        TransactionId = transaction;
                        return 1;
                    
                    default:
                        return 0;
                }
            }
        }

        public static void DeleteTransaction()
        {
            TransactionId = string.Empty;   
        }
    }
}