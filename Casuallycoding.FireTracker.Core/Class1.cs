using System;
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
            var connectionString = "mongodb://localhost:27017/CasuallyCoding";

            MongoClient dbClient = new MongoClient(connectionString);

            var dbList = dbClient.ListDatabases().ToList();


            var database = dbClient.GetDatabase("Financial_Independence");

            var collection =database.GetCollection<Savings>("Savings");
            //.InsertOne(new Savings() { SavingsId = 1,Value = 10});

            var val =  collection.Find<Savings>(p => p.SavingsId ==1).FirstOrDefault();

            var filter = Builders<Savings>.Filter.Eq("SavingsId", 1);
            var savings1 = collection.Find(filter).FirstOrDefault();
            Console.WriteLine(savings1.Value.ToString());



        }
    }



    [DataContract]
    public class Savings
    {
        [BsonId]
        [DataMember]
        public int SavingsId { get; set; }

        [DataMember]
        public int Value { get; set; }

    }
}
