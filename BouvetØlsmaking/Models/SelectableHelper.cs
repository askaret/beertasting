using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BouvetØlsmaking.Models
{
    public class SelectableHelper
    {
        public bool IsSelected { get; set; }
        public int Id { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public string Value4 { get; set; }
        public string Value5 { get; set; }
        public string Value6 { get; set; }

        public SelectableHelper(int id, string value1, string value2="", string value3 = "", string value4 = "", string value5 = "", string value6 = "")
        {
            IsSelected = false;
            Id = id;
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
            Value4 = value4;
            Value5 = value5;
            Value6 = value6;
        }
    }
}
