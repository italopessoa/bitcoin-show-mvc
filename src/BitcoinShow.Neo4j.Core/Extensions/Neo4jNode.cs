using System;
using System.Collections.Generic;
using System.Text;
using BitcoinShow.Neo4j.Core.Attributes;

namespace BitcoinShow.Neo4j.Core.Extensions
{
    public abstract class Neo4jNode
    {
        public IReadOnlyDictionary<string, object> Properties
        {
            get; set;
        }


        public long Id
        {
            get; set;
        }

        [Neo4jProperty(Name = "uuid")]
        public string UUID { get; set; }
    }
}
