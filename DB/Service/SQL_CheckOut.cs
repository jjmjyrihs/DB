using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class SQL_CheckOut
    {
        public void InsertOder(Model.CustomerOrder Data)
        {
            //Customer_Order sql
            string sql = "insert into dbo.Customer_Order(Order_ID,Customer_Email,Order_Date,Subcriber_Name,Subcriber_Cellphone" +
                ",Subscriber_Address)";
            //sql_Book_ID
            
        }
    }
}
