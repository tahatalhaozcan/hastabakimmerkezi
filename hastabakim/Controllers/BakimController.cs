using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace hastabakim.Controllers
{
    public class BakimController:ApiController
    {
        klinikEntities _ent = new klinikEntities();
        [HttpGet]
        public List<BakimTip> BakilanHastalar(int hastaID)
        {
            return _ent.bakim.Where(p => p.hastaID == hastaID).Select(p => new BakimTip()
            {

                doktorAd = p.doktor.doktorAd,
                doktorSoyad = p.doktor.doktorSoyad,
                hastaGelisNedeni = p.hasta.hastaGelisNedeni,
                ilacAd = p.ilac.ilacAd,
                ilacDoz = p.ilac.ilacDoz,
                doktorID = p.doktorID,
                hastaID = p.hastaID,
                bakimID = p.bakimID
            }).ToList();
        }
        [HttpPost]
        public List<BakimTip> KlinikYeniGiris(BakimTip veri)
        {
            try
            {
                bakim b = new bakim();
                b.hastaID = veri.hastaID;
                b.doktorID = veri.doktorID;
                b.ilacID = veri.ilacID;
                _ent.bakim.Add(b);
                _ent.SaveChanges();
                return BakilanHastalar(veri.hastaID);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        [HttpGet]
        public List<BakimTip> KlinikCikis (int bakimID)
        {
            try
            {
                bakim s = _ent.bakim.Find(bakimID);
                int hastaid = s.hastaID;
                _ent.bakim.Remove(s);
                _ent.SaveChanges();
                return BakilanHastalar(hastaid);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

    }
    public class BakimTip
    {
        public int bakimID { get; set; }
        public int doktorID { get; set; } 
        public string doktorAd { get; set; }
        public string doktorSoyad { get; set; }
        public int hastaID { get; set; }
        public string hastaGelisNedeni { get; set; }
        public int ilacID { get; set; }
        public string ilacAd { get; set; }
        public int ilacDoz { get; set; }
    }
}