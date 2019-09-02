using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreelanceService.DAL.Entities;
using FreelanceService.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelanceService.Web.Controllers
{
    public class ViewController : Controller
    {
        IDbContext _db;

        public ViewController(IDbContext db)
        {
            _db = db;

        }
        // GET: View
        public ActionResult Index()
        {
        
            return View();
        }

        // GET: View/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: View/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: View/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User model)
        {
            try
            {

                _db.UserRepos.AddUser(model);
             //   _db.Commit();
                return RedirectToAction();
            }
            catch
            {
                return View();
            }
        }

        // GET: View/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: View/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: View/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: View/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}