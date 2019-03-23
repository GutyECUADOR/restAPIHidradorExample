using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_test_con_Hydrator.Models
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string ClienteID { get; set; }
        public string NombreCIA { get; set; }
        public string NombreContacto { get; set; }
        public string CargoContacto { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Pais { get; set; }
        public string Telefono { get; set; }
  
        public ICollection<OrdersDTO> ordenes { get; set; }

    }
}
