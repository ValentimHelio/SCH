using Microsoft.AspNetCore.Mvc;
using SCH.Models;
using SCH.Repositories;
using SCH.Repositories.Interfaces;

namespace SCH.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly IEmpresaRepository _empresaRepository;

        public EmpresaController(IEmpresaRepository EmpresaRepository)
        {
            _empresaRepository = EmpresaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _empresaRepository.GetAllAsync();
            return View(produtos);
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
            var produto = await _empresaRepository.GetByIdAsync(id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _empresaRepository.GetByIdAsync(id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _empresaRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
