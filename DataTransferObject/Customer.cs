﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject
{
    public class Customer
    {
        public int IdCustomer { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string streetname { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public int IdCity { get; set; }

    }
}
