using System;

namespace EFoodBLL.IntranetModels
{
    /// <summary>
    /// Propiedades para la creacion de un registro en la bitacora.
    /// </summary>
    public class Log
    {    
        /// <summary>
        /// Numero o codigo del error.
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Mensaje o explicación del error.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Usuario activo que provoco el error.
        /// </summary>
        public string User { get; set; }
    }

    /// <summary>
    /// Clase que contiene las propiedades necesarias para el listado de la bitacora
    /// </summary>
    public class LogList
    {
        /// <summary>
        /// Identificador unico del error.
        /// </summary>
        public int PkCode { get; set; }
        
        /// <summary>
        /// Fecha y hora cuando se provoco el error.
        /// </summary>
        public DateTime DateTime { get; set; }
        
        /// <summary>
        /// Codigo o numero del error.
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// Mensaje o explicacion del error.
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Usuario con el que sucedio el error.
        /// </summary>
        public string User { get; set; }
    }
}