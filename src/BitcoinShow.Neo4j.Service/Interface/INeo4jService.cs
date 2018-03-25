using System.Collections.Generic;
using System.Threading.Tasks;
using BitcoinShow.Neo4j.Core;
using Neo4j.Map.Extension.Model;

namespace BitcoinShow.Neo4j.Service.Interface
{
    /// <summary>
    /// Neo4j generic method interface
    /// </summary>
    public interface INeo4jService<T> where T : Neo4jNode
    {
        /// <summary>
        /// Create a new node
        /// </summary>
        /// <param name="node">Graph node values</param>
        /// <returns>T instance</returns>
        Task<T> CreateAsync(T node);

        /// <summary>
        /// Match a node by UUID
        /// </summary>
        /// <param name="UUID">Graph node UUID</param>
        /// <returns>T instance</returns>
        Task<T> MatchByUUIDAsync(string uuid);

        /// <summary>
        /// Match nodes by properties values
        /// </summary>
        /// <param name="node">Graph node</param>
        /// <returns>List of T</returns>
        Task<List<T>> MatchByPropertiesAsync(T node);

        /// <summary>
        /// Delete node
        /// </summary>
        /// <param name="uuid">Graph node UUID</param>
        /// <returns>True if the node was successfully deleted</returns>
        Task<bool> DeleteByUUIDAsync(string uuid);
    }
}
