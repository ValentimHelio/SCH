using Microsoft.AspNetCore.Mvc;
using SCH.Models;
using SCH.Repositories.Interfaces;
using SCH.Services;
using SCH.ViewModels;

namespace SCH.Controllers;

public class DemontrativoController : Controller
{
    private readonly IUnitOfWork _repository;
    private readonly RelatorioServices _rel;

    public DemontrativoController(IUnitOfWork repository, RelatorioServices rel)
    {
        _repository = repository;
        _rel = rel;
    }

    public ActionResult Index()
    {
        ViewBag.ClienteId = _repository.clienteRepository.SelectListClientes();
        return View();
    }

    [HttpPost]
    public async Task<ActionResult<RelatorioDemonstrativoViewModel>> RelatorioDemontrativoGeral(Cliente cliente, DateTime? minDate, DateTime? maxDate)
    {
        if (!minDate.HasValue)
        {
            minDate = new DateTime(DateTime.Now.Year, 1, 1);
        }
        if (!maxDate.HasValue)
        {
            maxDate = DateTime.Now;
        }
        ViewData["minDate"] = minDate.Value.ToString("dd-MM-yyyy");
        ViewData["maxDate"] = maxDate.Value.ToString("dd-MM-yyyy");

        var result = await  _rel.ObterRelatorioDemonstrativo(minDate.Value, maxDate.Value);


        //var result = _rel.RelDemonstrativos(cliente, minDate, maxDate);

        return View(result);
    }
}
