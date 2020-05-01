using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace hastabakim.Controllers
{
    public class HastaController:ApiController
    {
        klinikEntities _ent = new klinikEntities();
        [HttpGet]
        public List<HastaTip> HastalariGetir()
        {
            return _ent.hasta.Select(p => new HastaTip()
            {
                hastaID = p.hastaID,
                hastaAd = p.hastaAd,
                hastaSoyad = p.hastaSoyad,
                hastaGelisNedeni = p.hastaGelisNedeni
            }).ToList();
        }
        [HttpPost]
        public List<HastaTip> YeniHasta(hasta kayit)
        {
            try
            {
                hasta h = new hasta();
                h.hastaID = kayit.hastaID;
                h.hastaAd = kayit.hastaAd;
                h.hastaSoyad = kayit.hastaSoyad;
                h.hastaGelisNedeni = kayit.hastaGelisNedeni;
                _ent.hasta.Add(h);
                _ent.SaveChanges();
                return HastalariGetir();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        [HttpGet]
        public List<HastaTip> HastaSil(int hastaID)
        {
            List<bakim> a = _ent.bakim.Where(p => p.hastaID == hastaID).ToList();
            if (a != null)
            {
                _ent.bakim.RemoveRange(a);
                _ent.SaveChanges();
            }
            _ent.hasta.Remove(_ent.hasta.Find(hastaID));
            _ent.SaveChanges();
            return HastalariGetir();
        }
    }
    public class HastaTip
    {
        public int hastaID { get; set; }
        public string hastaAd { get; set; }
        public string hastaSoyad { get; set; }

        public string hastaGelisNedeni { get; set; }
    }
}