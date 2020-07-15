namespace EFoodBLL.IntranetModels
{
    /// <summary>
    /// Datos necesarios para realizar el insert del tipo de tarjeta
    /// </summary>
    public class CardType
    {
        public string Type { get; set; }
    }

    /// <summary>
    /// Sirve para recuperar los datos de un tipo de tarjeta.
    /// </summary>
    public class ReturnCardType
    {
        public int PkCode { get; set; }
        public string Type { get; set; }
    }

    /// <summary>
    /// Esta clase puede ser utilizada para mostrar la lista de los tipos de tarjeta. 
    /// </summary>
    public class TypeCardsList
    {
        public int PkCode { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
    }
    
    /// <summary>
    /// Esta clase es para realizar la conexion entre el procesador de pago y el tipo de tarjeta.
    /// </summary>
    public class RelationCardProcessor
    {
        public int PkCode { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
    }
}