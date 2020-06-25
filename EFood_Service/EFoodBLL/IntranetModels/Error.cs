using System;

namespace EFoodBLL.IntranetModels
{
    public class Error
    {
        /// <summary>
        /// Numero de error.
        /// </summary>
        public int Code { get; set; }
        
        /// <summary>
        /// Metodo donde se creo el error.
        /// </summary>
        public string Method { get; set; }
        
        /// <summary>
        /// Mensaje o descripcion del error.
        /// </summary>
        public string Message { get; set; }
    }

    public class ErrorList
    {
        public int PkCode { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}