using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace hastabakim.Controllers
{
    public class IlacController:ApiController
    {
        klinikEntities _ent = new klinikEntities();
        [HttpGet]
        public List<IlacTip> IlaclariGetir()
        {
            return _ent.ilac.Select(p => new IlacTip()
            {
                ilacID = p.ilacID,
                ilacAd = p.ilacAd,
                ilacDoz = p.ilacDoz
            }).ToList();
        }
        [HttpPost]
        public List<IlacTip> YeniIlac(ilac giris)
        {
            try
            {
                ilac i = new ilac();
                i.ilacID = giris.ilacID;
                i.ilacAd = giris.ilacAd;
                i.ilacDoz = giris.ilacDoz;
                _ent.ilac.Add(i);
                _ent.SaveChanges();
                return IlaclariGetir();
              
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        [HttpGet]
        public List<IlacTip> IlacSil(int ilacID)
        {
            List<bakim> a = _ent.bakim.Where(p => p.ilacID == ilacID).ToList();
            if (a != null)
            {
                _ent.bakim.RemoveRange(a);
                _ent.SaveChanges();
            }
            _ent.ilac.Remove(_ent.ilac.Find(ilacID));
            _ent.SaveChanges();
            return IlaclariGetir();
        }
    }
    public class IlacTip
    {
        public int ilacID { get; set; }
        public string ilacAd { get; set; }
        public int ilacDoz { get; set; }
    }
}