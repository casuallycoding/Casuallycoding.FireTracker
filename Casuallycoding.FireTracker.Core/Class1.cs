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


            IImmutableRepository<Savings> mongoRepositroy = new MongoRepository<Savings>("mongodb://localhost:27017/CasuallyCoding", "Financial_Independence");

            mongoRepositroy.Create(new Savings() { Type = "Savings 1", Value = 0 });
            mongoRepositroy.Create(new Savings() { Type = "Savings 2", Value = 0 });
            mongoRepositroy.Create(new Savings() { Type = "Savings 3", Value = 0 });
            var val2s = mongoRepositroy.Read(p => p.Type == "Savings 1");
            mongoRepositroy.ReadAll().GroupBy(p => p.Type);


            IImmutableRepository<Property> propertyRepo = new MongoRepository<Property>("mongodb://localhost:27017/CasuallyCoding", "Financial_Independence");
            propertyRepo.Create(new Property() { Type = "House 1", OriginalPurchasePrice = 80000, Value = 100000, Debts = 50000, PercentOwned = 0.5 });





            IImmutableRepository<Investments> investmentRepo = new MongoRepository<Investments>("mongodb://localhost:27017/CasuallyCoding", "Financial_Independence");
            investmentRepo.Create(new Investments() { Type = "Vanguard 1",  Value = 400, TotalInvested = 1000 });

            var val = investmentRepo.ReadLast();

        }
    }


    public class Investments : DataValueBase
    {
        [DataMember]
        public double TotalInvested { get; set; }

        [DataMember]
        public double GainsOrLosses
        {
            get => (Value - TotalInvested);
            set {}
        }


        [DataMember]
        public double RateOfReturn
        {
            get => (GainsOrLosses / TotalInvested);
            set {}

        }
    }

    public class Savings : DataValueBase
    {

       
    }


    public class Property : DataValueBase
    {

        [DataMember]
        public double OriginalPurchasePrice { get; set; }

        [DataMember]
        public double Debts { get; set; }

        [DataMember]
        public double PercentOwned { get; set; } = 1.0;


        [DataMember]
        public double Equity
        {
            get => (Value - Debts) * PercentOwned;
            set { }
        }

    }

    public class Pensions : DataValueBase
    {

    }

    [DataContract]
    public abstract class DataValueBase : IDatatable
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
