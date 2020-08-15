namespace EFood_Client.UtilsMethdos
{
    public static class Transaction
    {
        private static string _transaction = string.Empty;
        public static string GetTransaction() => _transaction;
        public static void SetTransaction(string transaction) => _transaction = transaction;
        public static void DeleteTransaction() => _transaction = string.Empty;
    }
}