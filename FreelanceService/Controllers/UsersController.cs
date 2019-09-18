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
    public class UsersController : Controller
    {
        IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork uow)
        {
            _unitOfWork = uow;

        }
        // GET: View
        public async Task<ActionResult> Index()
        {
            var users = await _unitOfWork.UserRepos.GetAll();
        
            return View(users);
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
        public async Task<ActionResult> Create(User model)
        {
            try
            {

                await _unitOfWork.UserRepos.AddUser(model);
               await  _unitOfWork.CommitAsync();

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