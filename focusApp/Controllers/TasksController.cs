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
    public class TasksController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var tasks = DP.Listeleme<FocusTask>("TaskList").ToList();
            return View(tasks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(FocusTask task)
        {
            var param = new DynamicParameters();
            param.Add("@UserId", task.UserId);
            param.Add("@ProjectId", task.ProjectId);
            param.Add("@Title", task.Title);
            param.Add("@Description", task.Description);
            param.Add("@Priority", task.Priority);
            param.Add("@EstimatedMins", task.EstimatedMins);
            param.Add("@DueDate", task.DueDate);

            DP.ExecuteReturn("TaskAdd", param);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var param = new DynamicParameters();
            param.Add("@TaskId", id);
            var task = DP.Listeleme<FocusTask>("TaskGetById", param).FirstOrDefault();
            return View(task);
        }

        [HttpPost]
        public IActionResult Edit(FocusTask task)
        {
            var param = new DynamicParameters();
            param.Add("@TaskId", task.TaskId);
            param.Add("@UserId", task.UserId);
            param.Add("@ProjectId", task.ProjectId);
            param.Add("@Title", task.Title);
            param.Add("@Description", task.Description);
            param.Add("@Priority", task.Priority);
            param.Add("@EstimatedMins", task.EstimatedMins);
            param.Add("@DueDate", task.DueDate);
            param.Add("@IsCompleted", task.IsCompleted);

            DP.ExecuteReturn("TaskUpdate", param);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var param = new DynamicParameters();
            param.Add("@TaskId", id);
            DP.ExecuteReturn("TaskDelete", param);
            return RedirectToAction("Index");
        }




    }
}

