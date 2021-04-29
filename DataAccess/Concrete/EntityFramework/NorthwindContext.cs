using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //Context : Db tabloları ile proje classlarını bağlamak
    public class NorthwindContext:DbContext
    {
        // override onConfiguring methodu projen hangi veritabanı ile ilişkili olduğunu belirteceğimiz method
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // trusted_connection=true, kullanıcı adı şifre girmeden ilerleme
            // @ : \(ters slashı) normal \(ters slash) olarak algılama 
            optionsBuilder.UseSqlServer(@"Server = (localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true");
        }
        // DbSet ile hangi nesnem hangi nesne ile karşılık gelecek belirtme
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; } 
    }
}
