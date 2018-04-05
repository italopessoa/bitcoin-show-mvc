using Neo4j.Map.Extension.Attributes;
using Neo4j.Map.Extension.Model;

namespace BitcoinShow.Neo4j.Core
{
    /// <summary>
    /// 
    /// </summary>
    [Neo4jLabel("User")]
    public class UserNode : Neo4jNode
    {
        public UserNode()
        {
            
        }

        public UserNode(string name, Gender gender)
        {
            Name = name;
            Gender = gender;
        }
        
        /// <summary>
        /// 
        /// </summary>
        [Neo4jProperty(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Neo4jProperty(Name = "gender")]
        public Gender Gender { get; set; }
    }
}
