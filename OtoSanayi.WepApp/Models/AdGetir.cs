using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OtoSanayi.WepApp.Models
{
    public static class AdGetir
    {
        public static string Ad(string a) {

            if (a.Length>45)
            {
                return a.Substring(0,45)+" ...";

            }

            return a;
        }
        public static string KisaAd(string a)
        {

            if (a.Length > 45)
            {
                return a.Substring(0, 45)+" ...";

            }

            return a;
        }
        
        public static string UzunAd(string a)
        {

            if (a.Length > 100)
            {
                return a.Substring(0, 99) + " ...";

            }

            return a;
        }
        public static string BaskanMesaj(string a)
        {

            if (a.Length > 320)
            {
                return a.Substring(0, 319) + " ...";

            }

            return a;
        }


        public static string ResimAd(string a)
        {
            char[] karakterler=new char[]{'?','*','%','!','/','$','&',' ' };

            a = a.Trim().ToLower();
            foreach (char item in karakterler)
            {
                a.Replace(item,'-');
            }
            if (a.Length > 45)
            {
                a= a.Substring(0, 45);

            }
            return a+"_"+Guid.NewGuid().ToString().Substring(0,5);
            
        }
        public static string LinkAd(string a)
        {
            char[] tr = new char[] { 'ç', 'ş', 'ü', 'ğ', 'ı', 'ö', '?', '*', '%', '!', '/', '$', '&',' ' };
            char[] eng = new char[] { 'c', 's', 'u', 'g', 'i', 'o', '-', '-', '-', '-', '-', '-', '-','-' };
            try
            {
                a = a.Trim().ToLower();
                for (int i = 0; i < tr.Length; i++)
                {
                    a = a.Replace(tr[i], eng[i]);
                }
                if (a.Length > 45)
                {
                    a = a.Substring(0, 45);

                }
            }
            catch (Exception)
            {

                return "";
            }
            return a;

        }
        public static string DescAd(string a)
        {
            char[] tr = new char[] {  '?', '*', '%', '!', '/', '$', '&', ' ' };
            char[] eng = new char[] { '-', '-', '-', '-', '-', '-', '-', '-' };
            try
            {
                a = a.Trim().ToLower();
                for (int i = 0; i < tr.Length; i++)
                {
                    a = a.Replace(tr[i], eng[i]);
                }
                
            }
            catch (Exception)
            {

                return "";
            }
            return a;

        }
    }
}