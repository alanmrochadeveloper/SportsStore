using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app){

            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();

            context.Database.Migrate();
            
            if(!context.Products.Any()){
                context.Products.AddRange(
                    new Product {
                        Name = "Kayak", 
                        Description = "A boat for one person",
                        Category = "Watersports", 
                        Price = 275 
                        },
                    new Product {
                        Name = "Soccer Ball",
                        Description = "FIFA-approved size and weight",
                        Category = "Soccer",
                        Price = 19.50m 
                        },
                    new Product {
                        Name = "Corner Flags",
                        Description = "Give your playing field a professional touch",
                        Category = "Soccer", 
                        Price = 34.95m 
                        },
                    new Product {
                        Name = "Stadium",
                        Description = "Flat-packed 35,000-seat stadium",
                        Category = "Soccer", 
                        Price = 79500 
                        },
                    new Product {
                        Name = "Thinking Cap",
                        Description = "Improve brain efficiency by 75%",
                        Category = "Chess", 
                        Price = 16 
                        },
                    new Product {
                        Name = "Human Chess Board",
                        Description = "A fun game for the family",
                        Category = "Chess", 
                        Price = 75 
                        },
                    new Product {
                        Name = "Bling-Bling King",
                        Description = "Gold-plated, diamond-studded King",
                        Category = "Chess", 
                        Price = 1200
                    },
                    new Product
                    {
                        Name = "Chess Master",
                        Description = "Silver-plated, gold-studded King",
                        Category = "Chess",
                        Price = 190
                    },
                    new Product
                    {
                        Name = "Goalkeeper Gloves",
                        Description = "White, Thick and comfortable",
                        Category = "Soccer",
                        Price = 25.5m
                    },
                    new Product
                    {
                        Name = "Extended socks",
                        Description = "Huge length, comfortable socks",
                        Category = "Soccer",
                        Price = 10.1m
                    },
                    new Product
                    {
                        Name = "Lord of the rings",
                        Description = "Fantasy,  J.R.R. Tolkien",
                        Category = "Books",
                        Price = 5
                    },
                    new Product
                    {
                        Name = "The king slayer",
                        Description = "The name of the wind, Patrick Rothfuss",
                        Category = "Books",
                        Price = 7
                    },
                    new Product
                    {
                        Name = "Seasons",
                        Description = "Shawshank remdeption...Green mile...etc, Stephen King",
                        Category = "Books",
                        Price = 4
                    },
                    new Product
                    {
                        Name = "Who moved my cheese?!",
                        Description = "Unknown, Unknown",
                        Category = "Books",
                        Price = 4
                    },
                    new Product
                    {
                        Name = "Unlimited power",
                        Description = "Unleash your power, Anthony Robbins",
                        Category = "Books",
                        Price = 6
                    }, 
                    new Product
                    {
                        Name = "C# 7.0 .net core",
                        Description = "Learn to program csharp, Andrew Troelsen",
                        Category = "Books",
                        Price = 50
                    },
                    new Product
                    {
                        Name = "Asp .net core 2nd Edition",
                        Description = "Learn to program asp net core, Adam Freeman",
                        Category = "Books",
                        Price = 50
                    }
                );
                context.SaveChanges();
            }
        }
    }
}