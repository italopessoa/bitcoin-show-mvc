﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BitcoinShow.Neo4j.Core.Repository.Interface;
using Neo4j.Driver.V1;

namespace BitcoinShow.Neo4j.Core
{
    /// <summary>
    /// Neo4j Repository
    /// </summary>
    public class Neo4jRepository : INeo4jRepository
    {
        private readonly IDriver _driver;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uri">Neo4j bolt uri like bolt://127.0.0.1:7687</param>
        public Neo4jRepository(string uri)
        {
            _driver = GraphDatabase.Driver(uri, AuthTokens.None);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uri">Neo4j bolt uri like bolt://127.0.0.1:7687</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        public Neo4jRepository(string uri, string username, string password)
        {
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(username, password));
        }

        /// <summary>
        /// Create a new node
        /// </summary>
        /// <param name="query">Cypher query.</param>
        /// <returns>Return the node created.</returns>
        /// <example> 
        /// This sample shows how write a cypher query to call the <see cref="CreateCypherAsync(string)"/> method.
        /// <code>
        /// CREATE (n:Person {name: 'Jhon'}) RETURN n
        /// </code>
        /// </example>
        /// <remarks>The uuid is not returned when a node is created.</remarks>
        public async Task<INode> CreateCypherAsync(string query)
        {
            using (ISession session = _driver.Session(AccessMode.Write))
            {
                IStatementResultCursor result = await session.RunAsync(query);
                INode node = null;
                if (await result.FetchAsync())
                {
                    node = result.Current[result.Keys[0]].As<INode>();
                }
                return node;

            }
        }

        /// <summary>
        /// Match a label, (node type) by its uuid
        /// </summary>
        /// <param name="label">Label name</param>
        /// <param name="uuid">UUID</param>
        /// <returns><see cref="INode"/></returns>
        /// /// <example> 
        /// This sample shows how write a cypher query to call the <see cref="MatchLabelByUUIDCypherAsync(string, string)"/> method.
        /// <code>
        /// MATCH (p:Person {uuid: '75e88b00-1fc6-11e8-b7fc-2cd05a628834'}) RETURN p
        /// </code>
        /// </example>
        public async Task<INode> MatchLabelByUUIDCypherAsync(string label, string uuid)
        {
            if (string.IsNullOrEmpty(label) || string.IsNullOrWhiteSpace(label))
                throw new ArgumentNullException(nameof(label));
            if (string.IsNullOrEmpty(uuid) || string.IsNullOrWhiteSpace(uuid))
                throw new ArgumentNullException(nameof(uuid));

            using (ISession session = _driver.Session(AccessMode.Read))
            {
                List<INode> nodes = new List<INode>();
                IStatementResultCursor result = await session.RunAsync($"MATCH (label:{label} {{uuid: '{uuid}' }}) RETURN label");

                INode node = null;
                if (await result.FetchAsync())
                {
                    node = result.Current[result.Current.Keys[0]].As<INode>();
                }
                return node;
            }
        }

        /// <summary>
        /// Match nodes with only one key
        /// </summary>
        /// <param name="query">Cypher query</param>
        /// <returns>List of <see cref="INode"/></returns>
        /// <example> 
        /// This sample shows how write a cypher query to call the <see cref="MatchSingleKeyCypherAsync(string)"/> method.
        /// <code>
        /// MATCH (key:Person) RETURN key
        /// </code>
        /// </example>
        public async Task<List<INode>> MatchSingleKeyCypherAsync(string query)
        {
            using (ISession session = _driver.Session(AccessMode.Read))
            {
                List<INode> nodes = new List<INode>();
                IStatementResultCursor result = await session.RunAsync(query);
                await result.ForEachAsync(r =>
                {
                    nodes.Add(r.Keys[0].As<INode>());
                });
                return nodes;
            }
        }

        /// <summary>
        /// Remove relations and delete node by its uuid
        /// </summary>
        /// <param name="label">Label name</param>
        /// <param name="uuid">UUID</param>
        /// /// <example> 
        /// This sample shows how write a cypher query to call the <see cref="DeleteLabelByUUIDCypherAsync(string, string)"/> method.
        /// <code>
        /// MATCH (p:Person {uuid: '75e88b00-1fc6-11e8-b7fc-2cd05a628834'}) DETACH DELETE p
        /// </code>
        /// </example>
        public async Task DeleteLabelByUUIDCypherAsync(string label, string uuid)
        {
            using (ISession session = _driver.Session(AccessMode.Write))
            {
                List<INode> nodes = new List<INode>();
                await session.RunAsync($"MATCH (label:{label} {{uuid: '{uuid}' }}) DETACH DELETE label");
            }
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            _driver.Dispose();
        }
    }
}
