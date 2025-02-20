using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using SCH.Context;
using SCH.Models;
using SCH.Repositories.Interfaces;

namespace SCH.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaController(IEmpresaRepository EmpresaRepository, AppDbContext context)
        {
            _context = context;
            _empresaRepository = EmpresaRepository;
        }

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "NomeEmpresa")
        {
            var resultado = _context.Empresas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(p => p.NomeEmpresa.Contains(filter));
            }

            var model = await PagingList.CreateAsync(resultado, 10, pageindex, sort, "NomeEmpresa");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Empresa Empresa)
        {
            if (ModelState.IsValid)
            {
                await _empresaRepository.AddAsync(Empresa);
                return RedirectToAction(nameof(Index));
            }
            return View(Empresa);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var produto = await _empresaRepository.GetByIdAsync(id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Empresa Empresa)
        {
            if (ModelState.IsValid)
            {
                await _empresaRepository.UpdateAsync(Empresa);
                return RedirectToAction(nameof(Index));
            }
            return View(Empresa);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _empresaRepository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _empresaRepository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _empresaRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
