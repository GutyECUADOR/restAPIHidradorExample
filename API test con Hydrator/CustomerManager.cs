using API_test_con_Hydrator.Models;
using Foundation.ObjectHydrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_test_con_Hydrator
{
    public class CustomerManager
    {
        public static CustomerManager Instancia { get; } = new CustomerManager();
        public IList<CustomerDTO> Customers { get; set; }

        //Constructor
        public CustomerManager() {
            // Hydrator es un paquete nuget que permite la generacion de datos de ejemplo a partir de una clase dada
            Hydrator<CustomerDTO> hydratorData = new Hydrator<CustomerDTO>();
            this.Customers = hydratorData.GetList(5);
            /* Variable de random define la cant. de ordenes para cada cliente */
            Random numOrders = new Random();
            Hydrator<OrdersDTO> ordenesHydrator = new Hydrator<OrdersDTO>();

            foreach (CustomerDTO customer in this.Customers) {
                /*Agregamos numeros de ordenes aleatorias para cada cliente*/
                customer.ordenes = ordenesHydrator.GetList(numOrders.Next(1,10));
            }
            
        }

        /*Busca en el repositorio todos los ids y retorna el maximo +1*/
        public int getNextOrderID() {
            int maxOrderID = this.Customers.SelectMany(customers => customers.ordenes)
                                     .Max( ordenes => ordenes.OrderId);
            return maxOrderID++;
         }

        
    }
}
