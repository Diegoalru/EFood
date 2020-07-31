using EFoodBLL.ClientModels;

namespace EFood_Client.Controllers
{
    public static class PayMethod
    {
        public static int _Type { get; set; }
        public static Card_Client _CardClient;
        public static Check _Check;

        public static Card_Client GetCardClient() => _CardClient;
        public static Check GetCheckClient() => _Check;
        public static void SetCardClient(Card_Client data)
        {
            _CardClient = data;
            _Check = null;
        }
        
        public static void SetCheckClient(Check data)
        {
            _CardClient = null;
            _Check = data;
        }

    }
}