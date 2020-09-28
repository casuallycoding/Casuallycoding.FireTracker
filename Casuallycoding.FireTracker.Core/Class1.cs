using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Casuallycoding.FireTracker.Core
{


    // install MongoDB.Driver
    // Install mongo db https://docs.mongodb.com/guides/server/install/   -> https://www.mongodb.com/try/download/community
    //https://www.mongodb.com/blog/post/quick-start-c-sharp-and-mongodb--creating-documents
    public class Class1
    {


        public void Run()
        {


            IReadOnlyRepository<Savings> mongoRepositroy = new MongoRepository<Savings>("mongodb://localhost:27017/CasuallyCoding", "Financial_Independence");            
            
            mongoRepositroy.Create(new Savings() {Type = "Savings 1", Value = 0  });
            mongoRepositroy.Create(new Savings() {Type = "Savings 2", Value = 0});
            mongoRepositroy.Create(new Savings() {Type = "Savings 3", Value = 0 });

            var val2s = mongoRepositroy.Read(p => p.Type == "Savings 1");

            



        }
    }



    [DataContract]
    public class Savings : IDatatable
    {
        [BsonId]
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public double Value { get; set; }
        
        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public DateTime Date { get; set; } = DateTime.Now;



    }
}
