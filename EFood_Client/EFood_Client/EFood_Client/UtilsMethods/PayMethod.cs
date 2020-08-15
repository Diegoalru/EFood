using EFoodBLL.ClientModels;

namespace EFood_Client.UtilsMethdos
{
    public class PayMethod
    {
        private static int Type { get; set; }
        private static Card_Client _CardClient;
        private static Check _Check;

        public static int GetType() => Type;
        public static Card_Client GetCardClient() => _CardClient;
        
        public static Check GetCheckClient() => _Check;

        public static void SetType(int type) => Type = type;
        
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