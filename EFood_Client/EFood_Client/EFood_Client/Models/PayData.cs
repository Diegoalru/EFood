using EFood_Client.UtilsMethdos;
using EFoodBLL.ClientModels;
using EFoodBLL.IntranetModels;
using EFoodDB.EFood_Client;

namespace EFood_Client.Models
{
    public class PayData
    {
        public string Client { get; set; }
        public string Direction { get; set; }
        public string Phone { get; set; }
        
        /// <summary>
        /// Efectivo, Tarjeta y Cheque. 
        /// </summary>
        public string Type { get; set; }
        public string CardNumber { get; set; }
        
        /// <summary>
        /// Tipo: Visa, MasterCard, etc...
        /// </summary>
        public string TypeCard { get; set; }
        public string CheckNumber { get; set; }
        public string CheckAccount { get; set; }
        public decimal Total { get; set; }

        public PayData()
        {
            var administrationMethods = new AdministrationMethods();
            
            var client = ClientUtils.GetClient();
            var type = PayMethod.GetType();
            var total = Shopping.GetAmount();

            Client = $"{client.Name} {client.Surname}";
            Direction = client.Direction;
            Phone = client.Phone;
            Total = total;
            
            switch (type)
            {
                case 1:
                    Type = "Efectivo";
                    TypeCard = "No aplica";
                    CardNumber = "No aplica";
                    CheckNumber = "No aplica";
                    CheckAccount = "No aplica";
                    break;
                case 2:
                    Type = "Cheque";
                    TypeCard = administrationMethods.ReturnCardType(PayMethod.GetCardClient().Type).Result.Type; 
                    CardNumber = PayMethod.GetCardClient().CardNumber;
                    CheckNumber = "No aplica";
                    CheckAccount = "No aplica";
                    break;
                case 3:
                    Type = "Cheque";
                    TypeCard = "No aplica";
                    CardNumber = "No aplica";
                    CheckNumber = PayMethod.GetCheckClient().Number;
                    CheckAccount = PayMethod.GetCheckClient().Account.ToString();
                    break;
            }
        }
        
    }
}