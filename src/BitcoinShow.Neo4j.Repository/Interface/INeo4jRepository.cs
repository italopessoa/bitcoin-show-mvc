using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neo4j.Map.Extension.Model;

namespace BitcoinShow.Neo4j.Repository.Interface
{
    /// <summary>
    /// Neo4j Repository definition
    /// </summary>
    public interface INeo4jRepository : IDisposable
    {
        /// <summary>
        /// Create a new node
        /// </summary>
        /// <param name="query">Cypher query</param>
        /// <returns>Return the node created.</returns>
        /// <remarks>The uuid is not returned when a node is created.</remarks>
        Task<T> CreateCypherAsync<T>(string query) where T : Neo4jNode;

        /// <summary>
        /// Match nodes with only one key
        /// </summary>
        /// <param name="query">Cypher query</param>
        /// <returns>List of <see cref="INode"/></returns>
        Task<List<T>> MatchSingleKeyCypherAsync<T>(string query) where T : Neo4jNode;

        /// <summary>
        /// Match a label, (node type) by its uuid
        /// </summary>
        /// <param name="label">Label name</param>
        /// <param name="uuid">UUID</param>
        /// <returns><see cref="INode"/></returns>
        Task<T> MatchLabelByUUIDCypherAsync<T>(string label, string uuid) where T : Neo4jNode;

        /// <summary>
        /// Remove relations and delete node by its uuid
        /// </summary>
        /// <param name="label">Label name</param>
        /// <param name="uuid">UUID</param>
        Task DeleteLabelByUUIDCypherAsync<T>(string label, string uuid) where T : Neo4jNode;
    }
}
