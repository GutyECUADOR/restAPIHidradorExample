using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_test_con_Hydrator.Models
{
    public class OrdersDTOUpDate {
       
        public string CustomerId { get; set; }
        public int EmpleadoID { get; set; }
        public DateTime fechaOrden { get; set; }
        public DateTime fechaCompra { get; set; }
        public DateTime fechaEntrega { get; set; }
        public int formaCompra { get; set; }
        public decimal flete { get; set; }
        public string nombreEnvio { get; set; }
        [MinLength(10,ErrorMessage = "La direccion debe ser mayor a 10")]
        public string direcionEnvio { get; set; }
        public string ciudadEnvio { get; set; }
        public string codigoPostalEnvio { get; set; }
        public string paisEnvio { get; set; }
    }
}
