﻿using System.Collections.Generic;
using EF6TempTableKit.SqlCommands;

namespace EF6TempTableKit.DbContext
{
    /// <summary>Container for Temp-Table SQL</summary>
    public sealed class TempTableContainer
    {
        public TempTableContainer()
        {
            this.TempSqlQueriesList = new Queue<KeyValuePair<string, Query>>();
            this.TempOnTempDependencies = new Dictionary<string, HashSet<string>>();
        }

        /// <summary>
        /// List of Temp-Table-Sql Queries to append
        /// </summary>
        internal Queue<KeyValuePair<string, Query>> TempSqlQueriesList { get; }

        /// <summary>
        /// Key is node. 
        /// Value are children - at the top is an item that has dependencies on the items below. Last item has no any dependencies.
        /// </summary>
        internal IDictionary<string, HashSet<string>> TempOnTempDependencies { get; }

        /// <summary>
        /// Reinitializes the Container
        /// </summary>
        public void Reinitialize()
        {
            this.TempSqlQueriesList.Clear();
            this.TempOnTempDependencies.Clear();
        }
    }
}