using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using SCH.Context;
using SCH.Models;
using SCH.Repositories.Interfaces;

namespace SCH.Controllers
{
    public class ClienteController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository, AppDbContext context)
        {
            _clienteRepository = clienteRepository;
            _context = context;
        }

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "NomeCliente")
        {
            var resultado = _context.Clientes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(p => p.NomeCliente.Contains(filter));
            }

            var model = await PagingList.CreateAsync(resultado, 10, pageindex, sort, "NomeCliente");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(model);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                await _clienteRepository.AddAsync(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var produto = await _clienteRepository.GetByIdAsync(id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                await _clienteRepository.UpdateAsync(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        public async Task<IActionResult> Details(int id)
        {
            var produto = await _clienteRepository.GetByIdAsync(id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _clienteRepository.GetByIdAsync(id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _clienteRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
