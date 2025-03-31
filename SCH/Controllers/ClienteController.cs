using Microsoft.AspNetCore.Mvc;
using SCH.Models;
using SCH.Repositories.Interfaces;

namespace SCH.Controllers;

public class ClienteController : Controller
{
    private readonly IUnitOfWork _repository;

    public ClienteController(IUnitOfWork repository)
    {
        _repository = repository;
    }

    public async Task<ActionResult> Index(string filter, int pageindex = 1, string sort = "NomeCliente")
    {
        var result = await _repository.clienteRepository.GetClientePagindo(filter, pageindex, sort);
        return View(result);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            _repository.clienteRepository.Add(cliente);
            _repository.Commit();
            return RedirectToAction(nameof(Index));
        }
        return View(cliente);
    }

    public async Task<ActionResult> Edit(int id)
    {
        var produto = await _repository.clienteRepository.GetByIdAsync(id);
        if (produto == null) return NotFound();
        return View(produto);
    }

    [HttpPost]
    public ActionResult Edit(Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            _repository.clienteRepository.Update(cliente);
            _repository.Commit();
            return RedirectToAction(nameof(Index));
        }
        return View(cliente);
    }

    public async Task<ActionResult> Details(int id)
    {
        var produto = await _repository.clienteRepository.GetByIdAsync(id);
        if (produto == null) return NotFound();
        return View(produto);
    }

    public async Task<ActionResult> Delete(int id)
    {
        var produto = await _repository.clienteRepository.GetByIdAsync(id);
        if (produto == null) return NotFound();
        return View(produto);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        _repository.clienteRepository.Delete(id);
        _repository.Commit();
        return RedirectToAction(nameof(Index));
    }
}
