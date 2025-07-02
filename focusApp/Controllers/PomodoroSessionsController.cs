using focusApp.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

public class PomodoroSessionsController : Controller
{
    public IActionResult Index()
    {
        var list = DP.Listeleme<PomodoroSession>("PomodoroSessionList").ToList();
        return View(list);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(PomodoroSession session)
    {
        var param = new DynamicParameters();
        param.Add("@TaskId", session.TaskId);
        param.Add("@StartedAt", session.StartedAt);
        param.Add("@EndedAt", session.EndedAt);
        param.Add("@IsBreak", session.IsBreak);

        DP.ExecuteReturn("sPomodoroSessionAdd", param);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var param = new DynamicParameters();
        param.Add("@SessionId", id);
        var session = DP.Listeleme<PomodoroSession>("PomodoroSessionGetById", param).FirstOrDefault();
        return View(session);
    }

    [HttpPost]
    public IActionResult Edit(PomodoroSession session)
    {
        var param = new DynamicParameters();
        param.Add("@SessionId", session.SessionId);
        param.Add("@TaskId", session.TaskId);
        param.Add("@StartedAt", session.StartedAt);
        param.Add("@EndedAt", session.EndedAt);
        param.Add("@IsBreak", session.IsBreak);

        DP.ExecuteReturn("PomodoroSessionUpdate", param);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var param = new DynamicParameters();
        param.Add("@SessionId", id);
        DP.ExecuteReturn("PomodoroSessionDelete", param);
        return RedirectToAction("Index");
    }
}
