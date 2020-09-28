using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Casuallycoding.FireTracker.Core
{
    class MongoRepository<T>  : IReadOnlyRepository<T> where T : IDatatable
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(string connectionString, string databaseName)
        {

            _client=  new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
            _collection = _database.GetCollection<T>(typeof(T).Name);

        }

        /// <summary>
        /// Creates an instance of a value if it doesn't already exist.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public T Create(T value)
        {
            if (value.ID == 0)
            {
                var valuesInList = ReadAll();
                var maxId =  valuesInList.Count() > 0 ? valuesInList.Max(p => p.ID) : 0;
                value.ID =maxId + 1;
            }

            var val = Read(value); //Look for val
            if (val == null)
                _collection.InsertOne(value); // Create if doesn't exist
            return value;
        }

        /// <summary>
        /// Reads from the database named
        /// </summary>
        /// <returns></returns>
        public T Read(T value)
        {
            var val = _collection.Find<T>(p => p.ID == value.ID).FirstOrDefault(); //Look for val
            return val;
        }


        /// <summary>
        /// Reads dependent on the expression passed.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Read(Expression<Func<T,bool>> search)
        {
            var val = _collection.Find<T>(search).ToEnumerable<T>();  //Look for val
            return val;
        }

        /// <summary>
        /// Gets all values in the collection
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> ReadAll()
        {
            return Read(p => true); 
        }

        
    }
}
