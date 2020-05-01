using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace hastabakim.Controllers
{
    public class GirisController:ApiController
    {
        klinikEntities _ent = new klinikEntities();
        [HttpGet]
        public int AdminGiris(string ka, string sifre)
        {
            admin a = _ent.admin.FirstOrDefault(g => g.adminKA == ka && g.adminSifre == sifre);
            if(a == null)
            {
                return 0;
            }
            else
            {
                return a.adminID;
            }
        }
        [HttpGet]
        public int PersonelGiris(string kap, string sifre)
        {
            personel p = _ent.personel.FirstOrDefault(t => t.personelKA == kap && t.personelSifre == sifre);
            if(p == null)
            {
                return 0;

            }
            else
            {
                return p.personelID;

            }
        }
    }
}