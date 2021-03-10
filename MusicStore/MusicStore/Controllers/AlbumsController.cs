using MusicStore.Models;
using MusicStore.Models.DataModels;
using MusicStore.Models.ViewModels;
using MusicStore.Models.ViewModels.Album;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.Controllers
{
    [Authorize(Users = "admin")]
    public class AlbumsController : Controller
    {
        public Depot depot = new Depot();
    
        //barre Filtre
        public ActionResult Index(string filtreTitre = "", string filtreArtiste = "", int? filtreGenre = null)
        {
            ViewBag.filtreTitre = filtreTitre;
            ViewBag.filtreArtiste = filtreArtiste;
            ViewBag.filtreGenre = filtreGenre;
            var l = this.depot.Albums.List();
            if (filtreTitre != "")
            {
                l = l.Where(p => p.Titre.Contains(filtreTitre)).ToList();
            }
            if (filtreArtiste != "")
            {
                l = l.Where(p => p.Artiste.Contains(filtreArtiste)).ToList();
            }
            if (filtreGenre != null)
            {
                l = l.Where(p => p.GenreId.Equals(filtreGenre)).ToList();
            }
            return View(l);

        }

       
        public ActionResult Create()
        {
            return View(new AjouterAlbum());
        }

        [HttpPost]
        public ActionResult Create(AjouterAlbum aa)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Album a = new Album();
                    a.AnneeParution = aa.AnneeParution;
                    a.Artiste = aa.Artiste;
                    a.Description = aa.Description;
                    a.GenreId = aa.GenreId;
                    a.Prix = aa.Prix;
                    a.Titre = aa.Titre;

                    depot.Albums.Add(a);

                    if (aa.CoverFile != null && aa.CoverFile.ContentLength > 0)
                    {
                        aa.CoverFile.SaveAs(Server.MapPath(a.Cover));
                    }
                    return RedirectToAction("Index", "Albums");
                }
                catch (SqlException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            return View(aa);
        }

        public ActionResult Edit(int id)
        {
            Album a = depot.Albums.Find(id);
            ModifierAlbum modifierAlbum = new ModifierAlbum();

            if (a == null)
            { 
                return RedirectToAction("Index", "Albums"); 
            }

            modifierAlbum.AnneeParution = a.AnneeParution;
            modifierAlbum.Artiste = a.Artiste;
            modifierAlbum.Description = a.Description;
            modifierAlbum.GenreId = a.GenreId;
            modifierAlbum.Prix = a.Prix;
            modifierAlbum.Titre = a.Titre;
            modifierAlbum.AlbumId = a.AlbumId;
            return View(modifierAlbum);
        }

        [HttpPost]
        public ActionResult Edit(ModifierAlbum modifierAlbum)
        {
            if (this.ModelState.IsValid)
            {
                try
                {

                    Album a = new Album();

                    a.AlbumId = modifierAlbum.AlbumId;
                    a.AnneeParution = modifierAlbum.AnneeParution;
                    a.Artiste = modifierAlbum.Artiste;
                    a.Description = modifierAlbum.Description;
                    a.GenreId = modifierAlbum.GenreId;
                    a.Prix = modifierAlbum.Prix;
                    a.Titre = modifierAlbum.Titre;

                    depot.Albums.Update(a);

                    if (modifierAlbum.CoverFile != null && modifierAlbum.CoverFile.ContentLength > 0)
                    {
                        modifierAlbum.CoverFile.SaveAs(Server.MapPath(a.Cover));
                    }
                    return RedirectToAction("Index", "Albums");
                }
                catch (SqlException e)
                {
                    ModelState.AddModelError("", e.Message);

                }
            }
            return View(modifierAlbum);
        }

        public ActionResult Delete(int id)
        {
            Album a = depot.Albums.Find(id);
            if (a == null) 
            { 
                return RedirectToAction("Index", "Albums"); 
            }
            return View(a);
        }


        [HttpPost]
        public ActionResult Delete(Album a)
        {
            try
            {
               
                depot.Albums.Delete(a);
                System.IO.File.Delete(this.Server.MapPath(a.Cover));
                return RedirectToAction("Index", "Albums");
            }
            catch (SqlException e)
            {
                
                this.ModelState.AddModelError("", e.Message);
            }
            return this.View(a);

        }
    }
}
