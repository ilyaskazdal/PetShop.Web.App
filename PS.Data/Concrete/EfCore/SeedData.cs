using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PS.Entity;
using PS.Data.Concrete.EfCore;

namespace PS.Data.Concrete.EfCore
{
    public class SeedData
    {
        public static void TestVerileriDoldur(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<PSContext>();

            if (context != null)
            {


                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();

                }

                if (!context.Contacts.Any())
                {
                    context.Contacts.AddRange(
                        new Entity.Contact
                        {

                            Name = "ali",
                            Surname = "veli",
                            Message = "codebase 1. mesaj",
                            EMail = "aliveli@gmail.com",
                            Purpose = true,
                            CreatedDate = DateTime.Now.AddDays(-20),
                            IsDeleted = false

                        });

                    context.SaveChanges();

                }

                if (!context.Products.Any())
                {

                    context.Products.AddRange(

                        new Product
                        {

                            ProductName = "KöpekMaması",
                            ProductDescription = "food",
                            ProductTypeId = 1,
                            UnitPrice = 10,
                            UnitInStock = 5,
                            CategoryId = 1,
                            Image = "kopekmamasi.jpg"

                        },
                    new Product
                    {

                        ProductName = "KediMaması",
                        ProductDescription = "food",
                        ProductTypeId = 1,
                        UnitPrice = 10,
                        UnitInStock = 10,
                        CategoryId = 2,
                        Image = "kedimamasi.jpg"


                    },
                    new Product
                    {

                        ProductName = "KediTasması",
                        ProductDescription = "material",
                        ProductTypeId = 3,
                        UnitPrice = 10,
                        UnitInStock = 10,
                        CategoryId = 2,


                    },
                    new Product
                    {

                        ProductName = "KöpekOyuncağı",
                        ProductDescription = "toy",
                        ProductTypeId = 2,
                        UnitPrice = 20,
                        UnitInStock = 50,
                        CategoryId = 1,
                        Image = "kopekoyuncagi.jpg"


                    });
                    context.SaveChanges();
                }

                if (!context.Categories.Any())
                {

                    context.Categories.AddRange(

                        new Category
                        {

                            CategoryName = "Köpek",
                            CategoryDescription = "Dogs",


                        },

                    new Category
                    {

                        CategoryName = "Kedi",
                        CategoryDescription = "Cats",

                    });

                    context.SaveChanges();
                }

                if (!context.ProductTypes.Any())
                {

                    context.ProductTypes.AddRange(

                        new ProductType
                        {

                            ProductTypeName = "food",


                        },

                    new ProductType
                    {

                        ProductTypeName = "toy",


                    },
                    new ProductType
                    {

                        ProductTypeName = "material",


                    });

                    context.SaveChanges();
                }
                if (!context.Users.Any())
                {

                    context.Users.AddRange(

                        new User
                        {

                            UserName = "ilyas",
                            Password = "1234",
                            UserEmail = "ilyaskazdal@gmail.com"


                        },

                    new User
                    {

                        UserName = "ali",
                        Password = "123456",
                        UserEmail = "ali@gmail.com"




                    });
                    context.SaveChanges();




                }
            }

        }
    }
}
