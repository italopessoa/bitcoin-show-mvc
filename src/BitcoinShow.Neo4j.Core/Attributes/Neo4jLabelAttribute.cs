using System;
using System.Collections.Generic;
using System.Text;

namespace BitcoinShow.Neo4j.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class Neo4jLabelAttribute : Attribute
    {
        public string Name { get; set; }
        public Neo4jLabelAttribute(string name)
        {
            Name = name;
        }
    }
}
