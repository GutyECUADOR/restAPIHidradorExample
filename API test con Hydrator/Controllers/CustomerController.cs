using API_test_con_Hydrator.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_test_con_Hydrator.Controllers
{
    [Route("api/v1/clientes")]
    public class CustomerController: Controller {

        [HttpGet()]
        public JsonResult GetCustomersAnonimos()
        {
          
            return new JsonResult(new List<object>()
                {
                    new { CustomerID=1, Nombre="Jose Gutierrez" },
                    new { CustomerID=2, Nombre="Luis Marquez" },
                    new { CustomerID=3, Nombre="Juan Molina" }
                });
        }

        [HttpGet("all")]
        public JsonResult GetCustomers()
        {
            return new JsonResult(CustomerManager.Instancia.Customers);
        }

        [HttpGet("cliente/{codCliente}")]
        public IActionResult GetCustomers(int codCliente) {

            CustomerDTO result = CustomerManager.Instancia.Customers
                .FirstOrDefault(customer => 
                    customer.Id == codCliente
                 );

            if (result != null) {
                return Ok(result);
            }
            else {
                return NotFound();
            }
        }

        
    }
}
