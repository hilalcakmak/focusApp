using Dapper;
using focusApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class TagsController : Controller
{
    public IActionResult Index()
    {
        var tags = DP.Listeleme<Tag>("TagList").ToList();
        return View(tags);
    }

    [HttpGet]
    public IActionResult Create()
    {
        // User tablosundaki tüm kullanıcıları çek
        var users = DP.Listeleme<User>("UserList").ToList();
        ViewBag.Users = new SelectList(users, "UserId", "Username");
        return View(new Tag());
    }


    [HttpPost]
    public IActionResult Create(Tag tag)
    {
        var param = new DynamicParameters();
        param.Add("@UserId", tag.UserId);
        param.Add("@Name", tag.Name);

        DP.ExecuteReturn("TagAdd", param);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var param = new DynamicParameters();
        param.Add("@TagId", id);
        var users = DP.Listeleme<User>("UserList").ToList();
        ViewBag.Users = new SelectList(users, "UserId", "Username");
        var tag = DP.Listeleme<Tag>("TagGetById", param).FirstOrDefault();
        return View(tag);
    }

    [HttpPost]
    public IActionResult Edit(Tag tag)
    {
        var param = new DynamicParameters();
        param.Add("@TagId", tag.TagId);
        param.Add("@UserId", tag.UserId);
        param.Add("@Name", tag.Name);

        DP.ExecuteReturn("TagUpdate", param);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var param = new DynamicParameters();
        param.Add("@TagId", id);
        DP.ExecuteReturn("TagDelete", param);
        return RedirectToAction("Index");
    }
}
