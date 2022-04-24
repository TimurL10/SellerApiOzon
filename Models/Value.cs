using System;
using System.Collections.Generic;
using System.Text;

namespace SellerApiOzon.Models
{
   

    public class Value
    {
        public int dictionary_value_id { get; set; }
        public string value { get; set; }
    }

    public class Attribute
    {
        public int id { get; set; }
        public List<Value> values { get; set; }
    }

    public class Item
    {
        public List<Attribute> attributes { get; set; }
        public string barcode { get; set; }
        public string category_id { get; set; }
        public int depth { get; set; }
        public string dimension_unit { get; set; }
        public int height { get; set; }
        public List<string> images { get; set; }
        public string name { get; set; }
        public string offer_id { get; set; }
        public string price { get; set; }
        public string vat { get; set; }
        public int weight { get; set; }
        public string weight_unit { get; set; }
        public int width { get; set; }
    }

    public class Root
    {
        public List<Item> items { get; set; }
    }


}
