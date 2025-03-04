using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.Model
{
    public class data
    {
        public string name { get; set; }
        public string lname { get;set; }
        public int id { get;set; }
        public data(string name, string lname, int id)
        {
            this.name = name;
            this.lname = lname;
            this.id = id;
        }
    }
}
