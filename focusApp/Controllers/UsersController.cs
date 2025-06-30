using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using focusApp.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace focusApp.Controllers
{
    public class UsersController : Controller
    {
        // GET: /<controller>/
       
        public IActionResult Index()
        {
            var users = DP.Listeleme<User>("UserList").ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            var param = new DynamicParameters();
            param.Add("@Username", user.Username);
            param.Add("@Email", user.Email);
            param.Add("@PasswordHash", user.PasswordHash); // hashlenmiş olacak

            DP.ExecuteReturn("UserAdd", param);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", id);
            var user = DP.Listeleme<User>("UserGetById", param).FirstOrDefault();
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", user.UserId);
            param.Add("@Username", user.Username);
            param.Add("@Email", user.Email);
            param.Add("@PasswordHash", user.PasswordHash);

            DP.ExecuteReturn("UserUpdate", param);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", id);
            DP.ExecuteReturn("UserDelete", param);
            return RedirectToAction("Index");
        }




    }
}

