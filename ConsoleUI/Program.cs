using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    //SOLID
    //Open Closed Principle
    //DTO:Data Transformation Object 
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new EfProductDal(), new CategoryManager(new EfCategoryDal()));
            //ProductTest(productManager);
            //CategoryTest();
            var result = productManager.GetProductDetails();

            if (result.Success==true)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + "/" + product.CategoryName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);
            }
        }
       

        private static void ProductTest(ProductManager productManager)
        {
            foreach (var product in productManager.GetAll().Data)
            {
                Console.WriteLine(product.ProductName);

            }
            Console.WriteLine("-------------------");
            foreach (var product in productManager.GetAllByCategoryId(2).Data)
            {
                Console.WriteLine(product.ProductName);
            }
            Console.WriteLine("-------------------");
            foreach (var product in productManager.GetByUnitPrice(40, 100).Data)
            {
                Console.WriteLine(product.ProductName);
            }
            Console.WriteLine("-------------------");
            foreach (var product in productManager.GetProductDetails().Data)
            {
                Console.WriteLine(product.ProductName + "/" + product.CategoryName);
            }
        }
    }
}
