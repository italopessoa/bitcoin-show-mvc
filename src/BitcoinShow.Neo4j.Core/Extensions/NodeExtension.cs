using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BitcoinShow.Neo4j.Core.Attributes;
using Neo4j.Driver.V1;

namespace BitcoinShow.Neo4j.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class NodeExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <returns></returns>
        public static T Map<T>(this object node) where T : Neo4jNode
        {
            T result = (T)Activator.CreateInstance(typeof(T));
            IDictionary<string, string> neo4jModelProperties = new Dictionary<string, string>();
            foreach (PropertyInfo propInfo in typeof(T).GetProperties())
            {
                IEnumerable<Neo4jPropertyAttribute> attrs = propInfo.GetCustomAttributes<Neo4jPropertyAttribute>(false);
                foreach (Neo4jPropertyAttribute attr in attrs)
                {
                    string propName = propInfo.Name;
                    string neo4jAttr = attr.Name;
                    neo4jModelProperties.Add(neo4jAttr, propName);
                }
            }

            INode nodeAux = node as INode;
            foreach (KeyValuePair<string, string> property in neo4jModelProperties)
            {
                PropertyInfo propertyInfo = result.GetType().GetProperty(property.Value);
                propertyInfo.SetValue(result, nodeAux.Properties[property.Key]);
            }
            PropertyInfo propertyInfoId = result.GetType().GetProperty("Id");
            propertyInfoId.SetValue(result, nodeAux.Id);

            return result;
        }
    }
}
