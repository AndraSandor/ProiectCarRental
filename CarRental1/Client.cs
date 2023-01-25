using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental1
{
    public class Client
    {
        private int _id;
        private string _name;
        private string _add;
        private string _phone;
        public int Customer_id {
            get
            {
                return _id;
            }
            set 
            {
                _id = value;
            }
        }
        public string CustomerName {
            get 
            { 
                return _name;
            } 
            set 
            {
                _name = value.ToString();
            } 
        }
        public string CustomerAdd 
        {
            get
            {
                return _add;
            }
            set 
            {
                _add = value.ToString();
            }
        }
        public string Phone 
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value.ToString();
            }
        }

    }
}
