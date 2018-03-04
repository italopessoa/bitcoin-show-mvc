using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neo4j.Driver.V1;

namespace BitcoinShow.Neo4j.Core.Repository.Interface
{
    /// <summary>
    /// Neo4j query methods definition
    /// </summary>
    internal interface INeo4jRepository : IDisposable
    {
        /// <summary>
        /// Create a new node
        /// </summary>
        /// <param name="query">Cypher query</param>
        /// <returns>Return the node created.</returns>
        /// <remarks>The uuid is not returned when a node is created.</remarks>
        Task<INode> CreateCypherAsync(string query);

        /// <summary>
        /// Match nodes with only one key
        /// </summary>
        /// <param name="query">Cypher query</param>
        /// <returns>List of <see cref="INode"/></returns>
        Task<List<INode>> MatchSingleKeyCypherAsync(string query);

        /// <summary>
        /// Match a label, (node type) by its uuid
        /// </summary>
        /// <param name="label">Label name</param>
        /// <param name="uuid">UUID</param>
        /// <returns><see cref="INode"/></returns>
        Task<INode> MatchLabelByUUIDCypherAsync(string label, string uuid);

        /// <summary>
        /// Remove relations and delete node by its uuid
        /// </summary>
        /// <param name="label">Label name</param>
        /// <param name="uuid">UUID</param>
        Task DeleteLabelByUUIDCypherAsync(string label, string uuid);
    }
}
