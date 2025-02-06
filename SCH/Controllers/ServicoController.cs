using Microsoft.AspNetCore.Mvc;
using SCH.Models;
using SCH.Repositories;
using SCH.Repositories.Interfaces;

namespace SCH.Controllers
{
    public class ServicoController : Controller
    {
        private readonly IServicoRepository _servicoRepository;

        public ServicoController(IServicoRepository servicoRepository)
        {
            _servicoRepository = servicoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _servicoRepository.GetAllAsync();
            return View(produtos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Servico servico)
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
            var produto = await _servicoRepository.GetByIdAsync(id);
            if (produto == null) return NotFound();
            return View(produto);
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
            var produto = await _servicoRepository.GetByIdAsync(id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _servicoRepository.GetByIdAsync(id);
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
