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
    public class TaskTagsController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index(int taskId)
        {
            var param = new DynamicParameters();
            param.Add("@TaskId", taskId);
            var taskTags = DP.Listeleme<TaskTag>("TaskTagListByTask", param).ToList();

            // Burası önemli:
            ViewBag.AllTags = DP.Listeleme<Tag>("TagList").ToList();

            ViewBag.TaskId = taskId;
            return View(taskTags);
        }


        [HttpPost]
        public IActionResult Create(int taskId, int tagId)
        {
            var param = new DynamicParameters();
            param.Add("@TaskId", taskId);
            param.Add("@TagId", tagId);

            DP.ExecuteReturn("TaskTagAdd", param);
            return RedirectToAction("Index", new { taskId = taskId });
        }

        public IActionResult Delete(int taskId, int tagId)
        {
            var param = new DynamicParameters();
            param.Add("@TaskId", taskId);
            param.Add("@TagId", tagId);

            DP.ExecuteReturn("TaskTagDelete", param);
            return RedirectToAction("Index", new { taskId = taskId });
        }



    }
}

