﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REV_UTS_72220538_Mobile.Data
{

    public class category
    {

        public int categoryId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
    public class CategoryWithSelection : category
    {
        public bool IsSelected { get; set; }
    }
}
