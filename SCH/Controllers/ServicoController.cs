using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using SCH.Context;
using SCH.Models;
using SCH.Repositories.Interfaces;

namespace SCH.Controllers
{
    public class ServicoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IServicoRepository _servicoRepository;

        public ServicoController(IServicoRepository servicoRepository, AppDbContext context)
        {
            _context = context;
            _servicoRepository = servicoRepository;
        }

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Descricao")
        {
            var resultado = _context.Servicos.Include(e => e.empresa).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(p => p.Descricao.Contains(filter));
            }

            var model = await PagingList.CreateAsync(resultado, 10, pageindex, sort, "Descricao");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(model);

        }

        public IActionResult Create()
        {
            ViewBag.EmpresaId = new SelectList(_context.Empresas, "EmpresaId", "NomeEmpresa");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Descricao,Tipo,EmpresaId")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                await _servicoRepository.AddAsync(servico);
                return RedirectToAction(nameof(Index));
            }
            return View(servico);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.EmpresaId = new SelectList(_context.Empresas, "EmpresaId", "NomeEmpresa");
            var servico = await _servicoRepository.GetByIdAsync(id);
            if (servico == null) return NotFound();
            return View(servico);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Servico servico)
        {
            if (ModelState.IsValid)
            {
                await _servicoRepository.UpdateAsync(servico);
                return RedirectToAction(nameof(Index));
            }
            return View(servico);
        }

        public async Task<IActionResult> Details(int id)
        {
            ViewBag.EmpresaId = new SelectList(_context.Empresas, "EmpresaId", "NomeEmpresa");
            var servico = await _servicoRepository.GetServicoById(id);
            if (servico == null) return NotFound();
            return View(servico);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _servicoRepository.GetServicoById(id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _servicoRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
