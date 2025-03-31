using Microsoft.AspNetCore.Mvc;
using SCH.Models;
using SCH.Repositories.Interfaces;

namespace SCH.Controllers;

public class ServicoController : Controller
{
    private readonly IUnitOfWork _repository;
    public ServicoController(IUnitOfWork repository)
    {
        _repository = repository;
    }

    private void CarregarViewBag()
    {
        ViewBag.EmpresaId = _repository.empresaRepository.SelectListEmpresas();
    }

    public async Task<ActionResult> Index(string filter, int pageindex = 1, string sort = "Descricao")
    {
        var result = await _repository.servicoRepository.GetServicoPagindo(filter, pageindex, sort);
        return View(result);
    }

    public ActionResult Create()
    {
        CarregarViewBag();
        return View();
    }

    [HttpPost]
    public ActionResult Create([Bind("Descricao,Tipo,EmpresaId")] Servico servico)
    {
        if (ModelState.IsValid)
        {
            _repository.servicoRepository.Add(servico);
            _repository.Commit();
            return RedirectToAction(nameof(Index));
        }
        return View(servico);
    }

    public async Task<IActionResult> Edit(int id)
    {
        CarregarViewBag();
        var servico = await _repository.servicoRepository.GetByIdAsync(id);
        if (servico == null) return NotFound();
        return View(servico);
    }

    [HttpPost]
    public ActionResult Edit(Servico servico)
    {
        if (ModelState.IsValid)
        {
            _repository.servicoRepository.Update(servico);
            _repository.Commit();
            return RedirectToAction(nameof(Index));
        }
        return View(servico);
    }

    public async Task<IActionResult> Details(int id)
    {
        CarregarViewBag();
        var servico = await _repository.servicoRepository.GetServicoById(id);
        if (servico == null) return NotFound();
        return View(servico);
    }

    public async Task<ActionResult> Delete(int id)
    {
        var produto = await _repository.servicoRepository.GetServicoById(id);
        if (produto == null) return NotFound();
        return View(produto);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        _repository.servicoRepository.Delete(id);
        _repository.Commit();
        return RedirectToAction(nameof(Index));
    }
}
