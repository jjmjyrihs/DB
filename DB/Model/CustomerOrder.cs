using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class CustomerOrder
    {
        
        public string Order_ID { get; set; }
        public string Customer_Email { get; set; }
        public string Order_Date { get; set; }
        public string Shipping_Date { get; set; }
        public string Subscriber_Name { get; set; }
        public string Subscriber_Cellphone { get; set; }
        public string Subscriber_Email { get; set; }
        public string Subscriber_Address { get; set; }
        public string Action { get; set; }

        //下單書本數量
        public string Order_Quantity { get; set; }
    }
}
