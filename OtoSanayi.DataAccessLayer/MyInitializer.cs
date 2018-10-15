using OtoSanayi.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoSanayi.DataAccessLayer
{
   public class MyInitializer:CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            Kullanici admin = new Kullanici() { Adi = "Admin", Soyadi = "Admin", KullaniciAdi = "Admin", Parola = "12345" };
            context.Kullanicilar.Add(admin);
            int i = context.SaveChanges();

        }
    }
   
}
