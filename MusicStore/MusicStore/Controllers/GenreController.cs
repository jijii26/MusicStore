using MusicStore.Models;
using MusicStore.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicStore.Controllers
{
    [Authorize(Users = "admin")]
    public class GenreController : Controller
    {
       public Depot depot = new Depot();
       
        public ActionResult Index()
        {
            return View(this.depot.Genres.List());
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View(new Genre());
        }

  
        [HttpPost]
        public ActionResult Create(Genre g)
        {
            try
            {
                
                depot.Genres.Add(g);
                return RedirectToAction("Index", "Genre");
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", e.Message);
                return View(g);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Genre g = depot.Genres.Find(id);
            if (g == null) { return this.RedirectToAction("Index", "Genre"); }
            return View(g);
        }

      
        [HttpPost]
        public ActionResult Edit(Genre g)
        {
            try
            {
               

                depot.Genres.Update(g);
                return RedirectToAction("Index", "Genre");
            }
            catch (SqlException e)
            {
                ModelState.AddModelError("", e.Message);
                return View(g);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
           Genre g = depot.Genres.Find(id);
            if (g == null) { return this.RedirectToAction("Index", "Genre"); }
            return View(g);
        }

   
        [HttpPost]
        public ActionResult Delete(Genre g)
        {
            try
            {
                depot.Genres.Delete(g);
                return RedirectToAction("Index", "Genre");
            }
            catch
            {
                return View(g);
            }
        }
    }
}
