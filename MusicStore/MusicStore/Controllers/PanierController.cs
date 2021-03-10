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
    public class PanierController : Controller
    {
        public Depot depot = new Depot();
        public ActionResult Index()
        {
            Panier p = new Panier();
            p.UtilisateurId = depot.Utilisateurs.FindByUsername(User.Identity.Name).UtilisateurId;
            ViewBag.total = 0;
            return View(depot.Panier.TousLesArticles(p.UtilisateurId));
        }
        public ActionResult Acheter(int AlbumId)
        {
            Panier p = new Panier();
            p.UtilisateurId = depot.Utilisateurs.FindByUsername(User.Identity.Name).UtilisateurId;
            depot.Panier.AjouterUnArticle(AlbumId, p.UtilisateurId);

            return RedirectToAction("Index", "Panier");
        }
        public ActionResult SupprimerUnArticle(int AlbumId)
        {
            Panier p = new Panier();
            p.UtilisateurId = depot.Utilisateurs.FindByUsername(User.Identity.Name).UtilisateurId;
            depot.Panier.SupprimerUnArticle(AlbumId, p.UtilisateurId);

            return RedirectToAction("Index", "Panier");
        }
        public ActionResult ViderPanier()
        {
            Panier p = new Panier();
            p.UtilisateurId = depot.Utilisateurs.FindByUsername(User.Identity.Name).UtilisateurId;
            depot.Panier.ViderLePanier(p.UtilisateurId);

            return RedirectToAction("Index", "Panier");
        }


    }
}