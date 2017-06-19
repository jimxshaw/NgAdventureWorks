﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWK.WebAPI.Models
{
    public class Product
    {
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public string ProductCode { get; set; }

        public int ProductId { get; set; }

        public DateTime ReleaseDate { get; set; }



    }
}
