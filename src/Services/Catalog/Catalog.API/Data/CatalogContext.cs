using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration) //it reaches setting.json
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString")); //by driver nuget
            var database=client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));   //actually creating database
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products); //Bakılacak
        }

        public IMongoCollection<Product> Products { get; } //product collection
    }
}
