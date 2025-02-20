using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using SCH.Context;
using SCH.Models;
using SCH.Repositories.Interfaces;

namespace SCH.Controllers
{
    public class MovimentoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMovimentoRepository _movimentoRepository;

        public MovimentoController(IMovimentoRepository movimentoRepository, AppDbContext context)
        {
            _context = context;
            _movimentoRepository = movimentoRepository;
        }

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Valor_Hora")
        {
            var resultado = _context.Movimentos.Include(s => s.servico).Include(c => c.cliente).AsQueryable(); 

            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(p => p.cliente.NomeCliente.Contains(filter));
            }

            var model = await PagingList.CreateAsync(resultado, 10, pageindex, sort, "Valor_Hora");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(_context.Clientes, "ClienteId", "NomeCliente");
            ViewBag.ServicoId = new SelectList(_context.Servicos, "ServicoId", "Descricao");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Movimento movimento)
        {
            if (ModelState.IsValid)
            {
                await _movimentoRepository.AddAsync(movimento);
                return RedirectToAction(nameof(Index));
            }
            return View(movimento);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.ClienteId = new SelectList(_context.Clientes, "ClienteId", "NomeCliente");
            ViewBag.ServicoId = new SelectList(_context.Servicos, "ServicoId", "Descricao");

            var produto = await _movimentoRepository.GetByIdAsync(id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Movimento movimento)
        {
            if (ModelState.IsValid)
            {
                await _movimentoRepository.UpdateAsync(movimento);
                return RedirectToAction(nameof(Index));
            }
            return View(movimento);
        }

        public async Task<IActionResult> Details(int id)
        {
            ViewBag.ClienteId = new SelectList(_context.Clientes, "ClienteId", "NomeCliente");
            ViewBag.ServicoId = new SelectList(_context.Servicos, "ServicoId", "Descricao");

            var movimento = await _movimentoRepository.GetMovimentoById(id);
            if (movimento == null) return NotFound();
            return View(movimento);
        }

        public async Task<IActionResult> Delete(int id)
        {
            ViewBag.ClienteId = new SelectList(_context.Clientes, "ClienteId", "NomeCliente");
            ViewBag.ServicoId = new SelectList(_context.Servicos, "ServicoId", "Descricao");

            var result = await _movimentoRepository.GetMovimentoById(id);
            if (result == null) return NotFound();
            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _movimentoRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
