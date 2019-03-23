using API_test_con_Hydrator.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_test_con_Hydrator.Controllers
{
    // api/v1/clientes/cliente/21/ordenes

    [Route("api/v1/clientes")]
    public class OrdenesController : Controller {

        [HttpGet("cliente/{codCliente}/ordenes", Name = "GetOrden")]
        public IActionResult GetOrdenes(int codCliente) {
            CustomerDTO result = CustomerManager.Instancia.Customers
                .FirstOrDefault( cliente => cliente.Id == codCliente);

            if (result != null){
                return Ok(result.ordenes);
            }
            else {
                return NotFound();
            }
        }

        // /cliente/21/ordenes
        [HttpGet("cliente/{codCliente}/ordenes/{codOrden}")]
        public IActionResult GetOrdenByNum(int codCliente, int codOrden) {
            CustomerDTO customerDTO = CustomerManager.Instancia.Customers
                .FirstOrDefault(cliente => cliente.Id == codCliente);

            if (customerDTO != null)
            {
               OrdersDTO orderDTO =  customerDTO.ordenes
                                                .FirstOrDefault( orden => orden.OrderId == codOrden);

                if (orderDTO != null) {
                    return Ok(orderDTO);
                }
                else {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        // cliente/{codCliente}/ordenes/

        [HttpPost("cliente/{codCliente}/ordenes")]
        public IActionResult CreateOrder(int codCliente, [FromBody]OrdersDTO orderForm) {
            if (orderForm != null) {
                CustomerDTO customerDTO = CustomerManager.Instancia.Customers
                .FirstOrDefault(cliente => cliente.Id == codCliente);

                if (customerDTO != null && ModelState.IsValid) {
                    
                    OrdersDTO newOrderDTO = new OrdersDTO() { 
                        OrderId = CustomerManager.Instancia.getNextOrderID(),
                        CustomerId = orderForm.CustomerId,
                        EmpleadoID = orderForm.EmpleadoID,
                        fechaOrden = orderForm.fechaOrden,
                        fechaCompra = orderForm.fechaCompra,
                        fechaEntrega = orderForm.fechaEntrega,
                        formaCompra = orderForm.formaCompra,
                        flete = orderForm.flete,
                        nombreEnvio = orderForm.nombreEnvio,
                        direcionEnvio = orderForm.direcionEnvio,
                        ciudadEnvio = orderForm.ciudadEnvio,
                        codigoPostalEnvio = orderForm.codigoPostalEnvio,
                        paisEnvio = orderForm.paisEnvio
                    };

                    customerDTO.ordenes.Add(newOrderDTO);
                    return CreatedAtRoute("GetOrden", new { codCliente = customerDTO.Id, codOrden = newOrderDTO.OrderId }, newOrderDTO);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            else {
                return BadRequest();
            }
        }

        /*Metodo PUT requiere envio completo del objeto, actualizacion completa*/
        [HttpPut("cliente/{codCliente}/ordenes/{codOrden}")]
        public IActionResult UpdatePutOrder(int codCliente, int codOrden, [FromBody] OrdersDTOUpDate orderForm) {

            if (orderForm != null)
            {
                // Buscamos el codigo del cliente
                CustomerDTO customerDTO = CustomerManager.Instancia.Customers
                .FirstOrDefault(cliente => cliente.Id == codCliente);

                //Comprobamos que exista el cliente y el form sea correcto
                if (customerDTO != null && ModelState.IsValid)
                {
                   
                    //Buscamos la orden con ese numero
                    OrdersDTO OrderToUpDate = customerDTO.ordenes.FirstOrDefault(orden => orden.OrderId == codOrden);

                    if (OrderToUpDate != null) {
                        //Rellenamos valos del form al objeto

                        OrderToUpDate.CustomerId = orderForm.CustomerId;
                        OrderToUpDate.EmpleadoID = orderForm.EmpleadoID;
                        OrderToUpDate.fechaOrden = orderForm.fechaOrden;
                        OrderToUpDate.fechaCompra = orderForm.fechaCompra;
                        OrderToUpDate.fechaEntrega = orderForm.fechaEntrega;
                        OrderToUpDate.formaCompra = orderForm.formaCompra;
                        OrderToUpDate.flete = orderForm.flete;
                        OrderToUpDate.nombreEnvio = orderForm.nombreEnvio;
                        OrderToUpDate.direcionEnvio = orderForm.direcionEnvio;
                        OrderToUpDate.ciudadEnvio = orderForm.ciudadEnvio;
                        OrderToUpDate.codigoPostalEnvio = orderForm.codigoPostalEnvio;
                        OrderToUpDate.paisEnvio = orderForm.paisEnvio;

                        return NoContent();
                    }
                    else {
                        return NotFound();
                    }

                   
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        /*Actualizacion parcial del objecto con patch*/
        /* Detalle de la solicitud patch para acutalizar el nodo requerido
         [
	        {
		        "op": "replace",
		        "path": "/nombreEnvio",
		        "value": "Jose Gutierrez"
	        }
        ] */

        [HttpPatch("cliente/{codCliente}/ordenes/{codOrden}")]
        public IActionResult UpdateOrder(int codCliente, int codOrden, [FromBody] JsonPatchDocument<OrdersDTO> patchDocument)
        {

            if (patchDocument != null)
            {
                // Buscamos el cliente
                CustomerDTO customerDTO = CustomerManager.Instancia.Customers
                .FirstOrDefault(cliente => cliente.Id == codCliente);

                //Comprobamos que exista el cliente y el form sea correcto
                if (customerDTO != null && ModelState.IsValid)
                {
                    
                    //Buscamos la orden con ese numero
                    OrdersDTO OrderToUpDate = customerDTO.ordenes.FirstOrDefault(orden => orden.OrderId == codOrden);

                    if (OrderToUpDate != null)
                    {
                        //Rellenamos valos del form al objeto
                        OrdersDTO editOrder = new OrdersDTO()
                        {
                            OrderId = OrderToUpDate.OrderId,
                            CustomerId = OrderToUpDate.CustomerId,
                            EmpleadoID = OrderToUpDate.EmpleadoID,
                            fechaOrden = OrderToUpDate.fechaOrden,
                            fechaCompra = OrderToUpDate.fechaCompra,
                            fechaEntrega = OrderToUpDate.fechaEntrega,
                            formaCompra = OrderToUpDate.formaCompra,
                            flete = OrderToUpDate.flete,
                            nombreEnvio = OrderToUpDate.nombreEnvio,
                            direcionEnvio = OrderToUpDate.direcionEnvio,
                            ciudadEnvio = OrderToUpDate.ciudadEnvio,
                            codigoPostalEnvio = OrderToUpDate.codigoPostalEnvio,
                            paisEnvio = OrderToUpDate.paisEnvio
                        };

                        patchDocument.ApplyTo(editOrder, ModelState);

                        if (ModelState.IsValid) {


                            // Actualizacion a la DB o al repositorio
                            OrderToUpDate.CustomerId = editOrder.CustomerId;
                            OrderToUpDate.EmpleadoID = editOrder.EmpleadoID;
                            OrderToUpDate.fechaOrden = editOrder.fechaOrden;
                            OrderToUpDate.fechaCompra = editOrder.fechaCompra;
                            OrderToUpDate.fechaEntrega = editOrder.fechaEntrega;
                            OrderToUpDate.formaCompra = editOrder.formaCompra;
                            OrderToUpDate.flete = editOrder.flete;
                            OrderToUpDate.nombreEnvio = editOrder.nombreEnvio;
                            OrderToUpDate.direcionEnvio = editOrder.direcionEnvio;
                            OrderToUpDate.ciudadEnvio = editOrder.ciudadEnvio;
                            OrderToUpDate.codigoPostalEnvio = editOrder.codigoPostalEnvio;
                            OrderToUpDate.paisEnvio = editOrder.paisEnvio;

                            return NoContent();
                        } else {
                            return BadRequest(ModelState);
                        }




                    }
                    else
                    {
                        return NotFound();
                    }


                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
