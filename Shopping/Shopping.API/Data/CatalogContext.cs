using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Shopping.API.Models;
using System;
using System.Collections.Generic;

namespace Shopping.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }

        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["DatabaseSettings:ConnectionStrings"]);
            var database = client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);

            Products = database.GetCollection<Product>(configuration["DatabaseSettings:CollectionName"]);
            //SeedData(Products);
            //CatalogContextSeed.SeedData(Products);

            List<Product> list = Products.Find<Product>
                    (p => true).ToList<Product>();

            foreach (Product product in list)
            {
                Console.WriteLine($"ProductID: {product.Id} - Product Name: {product.Name}");
            }

        }

        
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if(!existProduct )
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                    {
                        Name="I Phone x",
                        Description="This phone is the company's biggerst change to its IPhone",
                        ImageFile="product-1.jpg",
                        Price=950.00M,
                        Category="Smart Phone",
                        Summery="Summery"
                    },
                    new Product()
                    {
                        Name="Samsung XXX",
                        Description="This phone is the company's biggerst change to its Samsung",
                        ImageFile="product-2.jpg",
                        Price=560.00M,
                        Category="Smart Phone",
                        Summery="Summery"
                    },
                    new Product()
                    {
                        Name="LG 600 A",
                        Description="This phone is the company's biggerst change to its IPhone",
                        ImageFile="product-3.jpg",
                        Price=950.00M,
                        Category="Smart Phone",
                        Summery="Summery"
                    }

            };
            
            
        }



        

    }
}
