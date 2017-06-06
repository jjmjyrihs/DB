using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ShippingCar
    {
        public string Customer_Email { get; set; }
        public string Book_Name { get; set; }
        public string Book_Price { get; set; }
        public int Book_Quantity { get; set; }
        public int Order_Quantity { get; set; }
        public string Book_ID { get; set; }
        public string Book_Img { get; set; }
        public string Action { get; set; }
        public string SubTotalId { get; set; }
    }
}
