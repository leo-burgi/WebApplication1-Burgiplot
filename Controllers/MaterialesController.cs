using Microsoft.AspNetCore.Mvc;
namespace WebApplication1.Controllers

    public class MaterialesController : Controller
    {

    private IActionResult Wip() => View("~/Views/Shared/Wip.cshtml");

public IActionResult Index() => Wip();
public IActionResult Details(int id) => Wip();
public IActionResult Create() => Wip();
[HttpPost] public IActionResult Create(object _) { return Wip(); }
public IActionResult Edit(int id) => Wip();
[HttpPost] public IActionResult Edit(int id, object _) { return Wip(); }
public IActionResult Delete(int id) => Wip();
[HttpPost, ActionName("Delete")] public IActionResult DeleteConfirmed(int id) { return Wip(); }
}
}
