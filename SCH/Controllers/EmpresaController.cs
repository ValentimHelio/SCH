using Microsoft.AspNetCore.Mvc;
using SCH.Models;
using SCH.Repositories.Interfaces;

namespace SCH.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly IUnitOfWork _repository;
        public EmpresaController(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<ActionResult> Index(string filter, int pageindex = 1, string sort = "NomeEmpresa")
        {
            var result = await _repository.empresaRepository.GetEmpresaPagindo(filter, pageindex, sort);
            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                _repository.empresaRepository.Add(empresa);
                _repository.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(empresa);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var produto = await _repository.empresaRepository.GetByIdAsync(id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        [HttpPost]
        public ActionResult Edit(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                _repository.empresaRepository.Update(empresa);
                _repository.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(empresa);
        }

        public async Task<ActionResult> Details(int id)
        {
            var result = await _repository.empresaRepository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        public async Task<ActionResult> Delete(int id)
        {
            var result = await _repository.empresaRepository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.empresaRepository.Delete(id);
            _repository.Commit();
            return RedirectToAction(nameof(Index));
        }
    }
}
