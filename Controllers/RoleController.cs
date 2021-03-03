using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapstoneTranslator.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CapstoneTranslator.Models;

namespace CapstoneTranslator.Controllers
{
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View(new IdentityRole());
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            await roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }

            //LanguageDbContext context;

            //public RoleController()
            //{
            //    context = new LanguageDbContext();
            //}

            ///// <summary>
            ///// Get All Roles
            ///// </summary>
            ///// <returns></returns>
            ////public ActionResult Index()
            ////{
            //    //var Roles = context.Roles.ToList();
            //    //return View(Roles);
            ////}

            ///// <summary>
            ///// Create  a New role
            ///// </summary>
            ///// <returns></returns>
            //public ActionResult Create()
            //{
            //    var Role = new IdentityRole();
            //    return View(Role);
            //}

            ///// <summary>
            ///// Create a New Role
            ///// </summary>
            ///// <param name="Role"></param>
            ///// <returns></returns>
            //[HttpPost]
            //public ActionResult Create(IdentityRole Role)
            //{
            //    //context.Roles.Add(Role);
            //    context.SaveChanges();
            //    return RedirectToAction("Index");
            //}
        
    }
}