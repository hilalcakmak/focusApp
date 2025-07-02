using focusApp.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class RemindersController : Controller
{
    public IActionResult Index()
    {
        var list = DP.Listeleme<Reminder>("ReminderList").ToList();
        return View(list);
    }

    [HttpGet]
    public IActionResult Create()
    {
        var tasks = DP.Listeleme<FocusTask>("TaskList").ToList();
        ViewBag.Tasks = new SelectList(tasks, "TaskId", "Title");
        return View(new Reminder());
    }


    [HttpPost]
    public IActionResult Create(Reminder reminder)
    {
        var param = new DynamicParameters();
        param.Add("@TaskId", reminder.TaskId);
        param.Add("@RemindAt", reminder.RemindAt);
        param.Add("@IsSent", reminder.IsSent);
        param.Add("@CreatedAt", DateTime.Now); // <-- DÜZELTİLDİ

        DP.ExecuteReturn("ReminderAdd", param);
        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult Edit(int id)
    {
        var param = new DynamicParameters();
        param.Add("@ReminderId", id);
        var reminder = DP.Listeleme<Reminder>("ReminderGetById", param).FirstOrDefault();
        var tasks = DP.Listeleme<FocusTask>("TaskList").ToList();
        ViewBag.Tasks = new SelectList(tasks, "TaskId", "Title");
        return View(reminder);
    }

    [HttpPost]
    public IActionResult Edit(Reminder reminder)
    {
        var param = new DynamicParameters();
        param.Add("@ReminderId", reminder.ReminderId);
        param.Add("@TaskId", reminder.TaskId);
        param.Add("@RemindAt", reminder.RemindAt);
        param.Add("@IsSent", reminder.IsSent);
        param.Add("@CreatedAt", reminder.CreatedAt);

        DP.ExecuteReturn("ReminderUpdate", param);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var param = new DynamicParameters();
        param.Add("@ReminderId", id);
        DP.ExecuteReturn("ReminderDelete", param);
        return RedirectToAction("Index");
    }
}
