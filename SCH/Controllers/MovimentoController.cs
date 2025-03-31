using Microsoft.AspNetCore.Mvc;
using SCH.Models;
using SCH.Repositories.Interfaces;

namespace SCH.Controllers
{
    public class MovimentoController : Controller
    {

        private readonly IUnitOfWork _repository;

        public MovimentoController(IUnitOfWork repository)
        {
            _repository = repository;
        }

        private void CarregarViewBag()
        {
            ViewBag.ClienteId = _repository.clienteRepository.SelectListClientes();
            ViewBag.ServicoId = _repository.servicoRepository.SelectListServicos();
        }

        public async Task<ActionResult> Index(string filter, int pageindex = 1, string sort = "Valor_Hora")
        {
            var result = await _repository.movimentoRepository.GetMovimentoPagindo(filter, pageindex, sort);
            return View(result);
        }

        public ActionResult Create()
        {
            CarregarViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Movimento movimento)
        {
            if (ModelState.IsValid)
            {
                _repository.movimentoRepository.Add(movimento);
                _repository.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(movimento);
        }

        public async Task<ActionResult> Edit(int id)
        {
            CarregarViewBag();
            var produto = await _repository.movimentoRepository.GetByIdAsync(id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        [HttpPost]
        public ActionResult Edit(Movimento movimento)
        {
            if (ModelState.IsValid)
            {
                _repository.movimentoRepository.Update(movimento);
                _repository.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(movimento);
        }

        public async Task<ActionResult> Details(int id)
        {
            CarregarViewBag();

            var movimento = await _repository.movimentoRepository.GetMovimentoById(id);
            if (movimento == null) return NotFound();
            return View(movimento);
        }

        public async Task<ActionResult> Delete(int id)
        {
            CarregarViewBag();
            var result = await _repository.movimentoRepository.GetMovimentoById(id);
            if (result == null) return NotFound();
            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.movimentoRepository.Delete(id);
            _repository.Commit();
            return RedirectToAction(nameof(Index));
        }
    }
}
