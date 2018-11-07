namespace OtoSanayi.WepApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TOSS_Duyuru",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DuyuruBaslik = c.String(nullable: false, maxLength: 150),
                        KisaDuyuruIcerik = c.String(nullable: false, maxLength: 300),
                        DuyuruIcerik = c.String(nullable: false),
                        DuyuruTarih = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TOSS_Firma_Kategori",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KategoriAdi = c.String(nullable: false, maxLength: 75),
                        KategoriAciklama = c.String(nullable: false, maxLength: 300),
                        Secili = c.Boolean(nullable: false),
                        KategoriResim = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TOSS_FirmaKategori",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Firma_ID = c.Int(),
                        FirmaKategori_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TOSS_Firma", t => t.Firma_ID)
                .ForeignKey("dbo.TOSS_Firma_Kategori", t => t.FirmaKategori_ID)
                .Index(t => t.Firma_ID)
                .Index(t => t.FirmaKategori_ID);
            
            CreateTable(
                "dbo.TOSS_Firma",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Aktif = c.Boolean(nullable: false),
                        FirmaAdi = c.String(nullable: false, maxLength: 300),
                        FirmaYetkili = c.String(nullable: false, maxLength: 300),
                        FirmaTel = c.String(nullable: false),
                        Adres = c.String(nullable: false, maxLength: 300),
                        KisaAciklama = c.String(maxLength: 250),
                        Aciklama = c.String(),
                        FirmaHarita = c.String(),
                        Logo = c.String(maxLength: 100),
                        FirmaLink = c.String(),
                        FirmaMail = c.String(),
                        FirmaFace = c.String(),
                        FirmaTwitter = c.String(),
                        FirmaGoogle = c.String(),
                        FirmaInstagram = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TOSS_Ilan",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 500),
                        KisaAciklama = c.String(nullable: false, maxLength: 150),
                        Aciklama = c.String(nullable: false),
                        IlanTarih = c.DateTime(nullable: false),
                        IlanKategoriID = c.Int(nullable: false),
                        FirmaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TOSS_Firma", t => t.FirmaID, cascadeDelete: true)
                .ForeignKey("dbo.TOSS_Ilan_Kategori", t => t.IlanKategoriID, cascadeDelete: true)
                .Index(t => t.IlanKategoriID)
                .Index(t => t.FirmaID);
            
            CreateTable(
                "dbo.TOSS_Ilan_Resim",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ResimYol = c.String(nullable: false, maxLength: 150),
                        IlanID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TOSS_Ilan", t => t.IlanID, cascadeDelete: true)
                .Index(t => t.IlanID);
            
            CreateTable(
                "dbo.TOSS_Ilan_Kategori",
                c => new
                    {
                        IlanKategoriID = c.Int(nullable: false, identity: true),
                        KategoriAdi = c.String(nullable: false, maxLength: 75),
                    })
                .PrimaryKey(t => t.IlanKategoriID);
            
            CreateTable(
                "dbo.TOSS_Firma_Resim",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ResimYol = c.String(maxLength: 150),
                        FirmaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TOSS_Firma", t => t.FirmaID, cascadeDelete: true)
                .Index(t => t.FirmaID);
            
            CreateTable(
                "dbo.TOSS_Haber",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HaberBaslik = c.String(nullable: false, maxLength: 300),
                        KisaHaberIcerik = c.String(nullable: false),
                        HaberIcerik = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TOSS_Haber_Resim",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ResimYol = c.String(maxLength: 150),
                        HaberID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TOSS_Haber", t => t.HaberID, cascadeDelete: true)
                .Index(t => t.HaberID);
            
            CreateTable(
                "dbo.TOSS_Hizmet_Kategori",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HizmetKategoriAdi = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TOSS_Hizmet",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HizmetBaslik = c.String(nullable: false, maxLength: 300),
                        KisaHizmetIcerik = c.String(nullable: false, maxLength: 150),
                        HizmetIcerik = c.String(nullable: false),
                        HizmetKategoriID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TOSS_Hizmet_Kategori", t => t.HizmetKategoriID, cascadeDelete: true)
                .Index(t => t.HizmetKategoriID);
            
            CreateTable(
                "dbo.TOSS_Hizmet_Resim",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ResimYol = c.String(nullable: false, maxLength: 150),
                        HizmetID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TOSS_Hizmet", t => t.HizmetID, cascadeDelete: true)
                .Index(t => t.HizmetID);
            
            CreateTable(
                "dbo.TOSS_Kullanici",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 150),
                        Soyadi = c.String(nullable: false, maxLength: 75),
                        KullaniciAdi = c.String(nullable: false, maxLength: 75),
                        Parola = c.String(nullable: false, maxLength: 75),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TOSS_Kurumsal_Kategori",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KategoriAdi = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TOSS_Kurumsal",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Baslik = c.String(nullable: false, maxLength: 500),
                        KisaIcerik = c.String(nullable: false),
                        Icerik = c.String(nullable: false),
                        KurumsalKategoriID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TOSS_Kurumsal_Kategori", t => t.KurumsalKategoriID, cascadeDelete: true)
                .Index(t => t.KurumsalKategoriID);
            
            CreateTable(
                "dbo.TOSS_Kurumsal_Resim",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ResimYol = c.String(maxLength: 150),
                        KurumsalID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TOSS_Kurumsal", t => t.KurumsalID, cascadeDelete: true)
                .Index(t => t.KurumsalID);
            
            CreateTable(
                "dbo.TOSS_Link",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LinkAdi = c.String(nullable: false, maxLength: 150),
                        LinkAdres = c.String(nullable: false, maxLength: 75),
                        LinkLogo = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TOSS_Mevzuat_Kategori",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KategoriAdi = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TOSS_Mevzuat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Adi = c.String(nullable: false, maxLength: 250),
                        Mevzuatlink = c.String(nullable: false, maxLength: 250),
                        MevzuatKategoriID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TOSS_Mevzuat_Kategori", t => t.MevzuatKategoriID, cascadeDelete: true)
                .Index(t => t.MevzuatKategoriID);
            
            CreateTable(
                "dbo.TOSS_Reklam",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ReklamCesit = c.Int(nullable: false),
                        ReklamAdi = c.String(maxLength: 150),
                        ReklamLink = c.String(maxLength: 150),
                        ResimYol = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TOSS_Slider",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SliderBaslik = c.String(nullable: false, maxLength: 150),
                        SliderLink = c.String(nullable: false, maxLength: 150),
                        SliderYol = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TOSS_Mevzuat", "MevzuatKategoriID", "dbo.TOSS_Mevzuat_Kategori");
            DropForeignKey("dbo.TOSS_Kurumsal_Resim", "KurumsalID", "dbo.TOSS_Kurumsal");
            DropForeignKey("dbo.TOSS_Kurumsal", "KurumsalKategoriID", "dbo.TOSS_Kurumsal_Kategori");
            DropForeignKey("dbo.TOSS_Hizmet", "HizmetKategoriID", "dbo.TOSS_Hizmet_Kategori");
            DropForeignKey("dbo.TOSS_Hizmet_Resim", "HizmetID", "dbo.TOSS_Hizmet");
            DropForeignKey("dbo.TOSS_Haber_Resim", "HaberID", "dbo.TOSS_Haber");
            DropForeignKey("dbo.TOSS_FirmaKategori", "FirmaKategori_ID", "dbo.TOSS_Firma_Kategori");
            DropForeignKey("dbo.TOSS_FirmaKategori", "Firma_ID", "dbo.TOSS_Firma");
            DropForeignKey("dbo.TOSS_Firma_Resim", "FirmaID", "dbo.TOSS_Firma");
            DropForeignKey("dbo.TOSS_Ilan", "IlanKategoriID", "dbo.TOSS_Ilan_Kategori");
            DropForeignKey("dbo.TOSS_Ilan_Resim", "IlanID", "dbo.TOSS_Ilan");
            DropForeignKey("dbo.TOSS_Ilan", "FirmaID", "dbo.TOSS_Firma");
            DropIndex("dbo.TOSS_Mevzuat", new[] { "MevzuatKategoriID" });
            DropIndex("dbo.TOSS_Kurumsal_Resim", new[] { "KurumsalID" });
            DropIndex("dbo.TOSS_Kurumsal", new[] { "KurumsalKategoriID" });
            DropIndex("dbo.TOSS_Hizmet_Resim", new[] { "HizmetID" });
            DropIndex("dbo.TOSS_Hizmet", new[] { "HizmetKategoriID" });
            DropIndex("dbo.TOSS_Haber_Resim", new[] { "HaberID" });
            DropIndex("dbo.TOSS_Firma_Resim", new[] { "FirmaID" });
            DropIndex("dbo.TOSS_Ilan_Resim", new[] { "IlanID" });
            DropIndex("dbo.TOSS_Ilan", new[] { "FirmaID" });
            DropIndex("dbo.TOSS_Ilan", new[] { "IlanKategoriID" });
            DropIndex("dbo.TOSS_FirmaKategori", new[] { "FirmaKategori_ID" });
            DropIndex("dbo.TOSS_FirmaKategori", new[] { "Firma_ID" });
            DropTable("dbo.TOSS_Slider");
            DropTable("dbo.TOSS_Reklam");
            DropTable("dbo.TOSS_Mevzuat");
            DropTable("dbo.TOSS_Mevzuat_Kategori");
            DropTable("dbo.TOSS_Link");
            DropTable("dbo.TOSS_Kurumsal_Resim");
            DropTable("dbo.TOSS_Kurumsal");
            DropTable("dbo.TOSS_Kurumsal_Kategori");
            DropTable("dbo.TOSS_Kullanici");
            DropTable("dbo.TOSS_Hizmet_Resim");
            DropTable("dbo.TOSS_Hizmet");
            DropTable("dbo.TOSS_Hizmet_Kategori");
            DropTable("dbo.TOSS_Haber_Resim");
            DropTable("dbo.TOSS_Haber");
            DropTable("dbo.TOSS_Firma_Resim");
            DropTable("dbo.TOSS_Ilan_Kategori");
            DropTable("dbo.TOSS_Ilan_Resim");
            DropTable("dbo.TOSS_Ilan");
            DropTable("dbo.TOSS_Firma");
            DropTable("dbo.TOSS_FirmaKategori");
            DropTable("dbo.TOSS_Firma_Kategori");
            DropTable("dbo.TOSS_Duyuru");
        }
    }
}
