using Microsoft.AspNetCore.Mvc;
using SCH.Models;
using SCH.Repositories.Interfaces;

namespace SCH.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _clienteRepository.GetAllAsync();
            return View(produtos);
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
