using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BlobStorageRepositoryDemo.Models;
using BlobStorageRepositoryDemo.Service;

namespace BlobStorageRepositoryDemo.Web.Controllers;

public class TodosController : Controller
{
    private readonly ITodoService _todoService;

    public TodosController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var todos = await _todoService.GetAllAsync();

        return View(todos);
    }

    [HttpGet]
    public async Task<IActionResult> Details([FromRoute] string id)
    {
        var todo = await _todoService.GetOneAsync(id);

        return View(todo);
    }

    [HttpGet]
    public async Task<IActionResult> Edit([FromRoute] string id)
    {
        var todo = await _todoService.GetOneAsync(id);

        return View(todo);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromForm] Todo todoItem)
    {
        var results = await _todoService.UpsertAsync(todoItem);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Create()
    {
        var newTodo = new Todo();

        return View(newTodo);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] Todo todoItem)
    {
        var results = await _todoService.UpsertAsync(todoItem);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        var todoItem = await _todoService.GetOneAsync(id);

        return View(todoItem);
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmDelete(Todo todoItem)
    {
        await _todoService.DeletAsync(todoItem.Id);

        return RedirectToAction("Index");
    }

}
