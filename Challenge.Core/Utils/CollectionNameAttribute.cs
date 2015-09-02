using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Core.Utils
{
    [AttributeUsage(AttributeTargets.Class |
                       AttributeTargets.Struct)]
    class CollectionNameAttribute : Attribute
    {
        public string Name { get; set; }
        public CollectionNameAttribute(string name)
        {
            Name = name;
        }
    }
}
