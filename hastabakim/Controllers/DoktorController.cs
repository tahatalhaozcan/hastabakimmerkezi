using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace hastabakim.Controllers
{
    public class DoktorController:ApiController
    {
        klinikEntities _ent = new klinikEntities();
        [HttpGet]
        public List<DoktorTip>DoktorlariGetir()
        {
            return _ent.doktor.Select(p => new DoktorTip()
            {
                doktorID = p.doktorID,
                doktorAd = p.doktorAd,
                doktorSoyad = p.doktorSoyad
            }).ToList();
        }
        [HttpPost]
        public List<DoktorTip> YeniDoktor(doktor kayit)
        {
            try
            {
                doktor d = new doktor();
                d.doktorID = kayit.doktorID;
                d.doktorAd = kayit.doktorAd;
                d.doktorSoyad = d.doktorSoyad;
                _ent.doktor.Add(d);
                _ent.SaveChanges();
                return DoktorlariGetir();
            }
            catch(Exception ex)
            {
                return null;
            }
         
        }
        [HttpGet]
        public List<DoktorTip> DoktorSil(int doktorID)
        {
            List<bakim> a = _ent.bakim.Where(p => p.doktorID == doktorID).ToList();
            if(a != null)
            {
                _ent.bakim.RemoveRange(a);
                _ent.SaveChanges();
            }
            _ent.doktor.Remove(_ent.doktor.Find(doktorID));
            _ent.SaveChanges();
            return DoktorlariGetir();
        }
    }
    public class DoktorTip
    {
        public int doktorID { get; set; }
        public string doktorAd { get; set; }
        public string doktorSoyad { get; set; }
    }
}