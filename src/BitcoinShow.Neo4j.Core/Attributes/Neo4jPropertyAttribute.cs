using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinShow.Neo4j.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class Neo4jPropertyAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
