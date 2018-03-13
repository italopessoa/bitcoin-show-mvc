using Neo4j.Map.Extension.Attributes;
using Neo4j.Map.Extension.Model;

namespace BitcoinShow.Neo4j.Facade.Model
{
    /// <summary>
    /// 
    /// </summary>
    [Neo4jLabel("User")]
    public class UserNode : Neo4jNode
    {
        /// <summary>
        /// 
        /// </summary>
        [Neo4jProperty(Name = "name")]
        public string Name { get; set; }
    }
}
