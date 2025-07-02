using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using focusApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace focusApp.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var projects = DP.Listeleme<Project>("ProjectList").ToList();
            return View(projects);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var users = DP.Listeleme<User>("UserList").ToList();
            ViewBag.Users = new SelectList(users, "UserId", "Username");
            return View(new Project());
        }


        [HttpPost]
        public IActionResult Create(Project project)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", project.UserId);
            param.Add("@Name", project.Name);
            param.Add("@Color", project.Color);

            DP.ExecuteReturn("ProjectAdd", param);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var users = DP.Listeleme<User>("UserList").ToList();
            ViewBag.Users = new SelectList(users, "UserId", "Username");
            var param = new DynamicParameters();
            param.Add("@ProjectId", id);
            var project = DP.Listeleme<Project>("ProjectGetById", param).FirstOrDefault();
            return View(project);
        }

        [HttpPost]
        public IActionResult Edit(Project project)
        {
            var param = new DynamicParameters();
            param.Add("@ProjectId", project.ProjectId);
            param.Add("@UserId", project.UserId);
            param.Add("@Name", project.Name);
            param.Add("@Color", project.Color);

            DP.ExecuteReturn("ProjectUpdate", param);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var param = new DynamicParameters();
            param.Add("@ProjectId", id);
            DP.ExecuteReturn("ProjectDelete", param);
            return RedirectToAction("Index");
        }



    }
}

