using Microsoft.AspNetCore.Mvc;
using SCH.Models;
using SCH.Repositories.Interfaces;

namespace SCH.Controllers
{
    public class MovimentoController : Controller
    {
        private readonly IMovimentoRepository _movimentoRepository;

        public MovimentoController(IMovimentoRepository movimentoRepository)
        {
            _movimentoRepository = movimentoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _movimentoRepository.GetAllAsync();
            return View(produtos);
        }

        public IActionResult Create()
        {
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
            var produto = await _movimentoRepository.GetByIdAsync(id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _movimentoRepository.GetByIdAsync(id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _movimentoRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
