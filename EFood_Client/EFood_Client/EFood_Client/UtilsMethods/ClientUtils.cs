using EFoodBLL.ClientModels;

namespace EFood_Client.UtilsMethdos
{
    public static class ClientUtils
    {
        private static Client _client = null;
        public static void SetClient(Client client) => _client = client;
        public static Client GetClient() => _client;
    }
}