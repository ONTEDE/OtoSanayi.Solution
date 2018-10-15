using OtoSanayi.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoSanayi.DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Duyuru> Duyurular { get; set; }
        public DbSet<FirmaKategori> FirmaKategoriler { get; set; }
        public DbSet<Firma> Firmalar { get; set; }
        public DbSet<FirmaResim> FirmaResimler { get; set; }
        public DbSet<Haber> Haberler { get; set; }
        public DbSet<HaberResim> HaberResimler { get; set; }
        public DbSet<HizmetKategori> HizmetKategoriler { get; set; }
        public DbSet<Hizmet> Hizmetler { get; set; }
        public DbSet<HizmetResim> HizmetResimler { get; set; }
        public DbSet<IlanKategori> IlanKategoriler { get; set; }
        public DbSet<Ilan> Ilanlar { get; set; }
        public DbSet<IlanResim> IlanResimler { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<KurumsalKategori> KurumsalKategoriler { get; set; }
        public DbSet<Kurumsal> Kurumsallar { get; set; }
        public DbSet<KurumsalResim> KurumsalResimler { get; set; }
        public DbSet<Link> Linkler { get; set; }
        public DbSet<MevzuatKategori> MevzuatKategoriler { get; set; }
        public DbSet<Mevzuat> Mevzuatlar { get; set; }
        public DbSet<Reklam> Reklamlar { get; set; }
        public DbSet<Firma_Kategori1> KategoriFirma { get; set; }

        public DbSet<Slider> Sliderlar { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
            
        }

    }
}
