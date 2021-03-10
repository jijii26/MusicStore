using MusicStore.Models;
using MusicStore.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.Controllers
{
    [Authorize]
    public class MusiqueController : Controller
    {
        Depot depot = new Depot();
        
        public ActionResult Index(int GenreId)
        {
            List<Album> la = depot.Albums.List();

            la = la.Where(l => l.GenreId.Equals(GenreId)).ToList();

            return View(la);
        }
    }
}