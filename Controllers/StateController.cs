using Sample_CRUD.DAL;
using Sample_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample_CRUD.Controllers
{
    public class StateController : Controller
    {
        private IStateRepository _stateRepository;
        private ICountryRepository _countryRepository;

        public StateController()
        {
            this._stateRepository = new StateRepository(new SampleCrudContext());
            this._countryRepository = new CountryRepository(new SampleCrudContext());
        }

        public ActionResult Index()
        {
            var _states = from states in _stateRepository.GetStates()
                             where (states.IsDeleted == null || states.IsDeleted == false)
                             select states;

            return View(_states);
        }

        public ViewResult Details(int id)
        {
            State state = _stateRepository.GetStateByID(id);
            return View(state);
        }

        public ActionResult Create()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var getCountries = from countries in _countryRepository.GetCountries()
                               where (countries.IsDeleted == null || countries.IsDeleted == false)
                               select countries;

            foreach (var item in getCountries.ToList())
            {
                items.Add(new SelectListItem { Text = item.CountryName, Value = item.CountryId.ToString() });
            }
            ViewBag.CountryList = items;

            return View(new State());
        }

        [HttpPost]
        public ActionResult Create(State state)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    state.CreatedBy = 1; // here we will pass the id of logged in user
                    state.CreatedOn = DateTime.Now;
                    _stateRepository.InsertState(state);
                    _stateRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException ee)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(state);
        }

        public ActionResult Edit(int id)
        {
            State state = _stateRepository.GetStateByID(id);

            List<SelectListItem> items = new List<SelectListItem>();
            var getCountries = from countries in _countryRepository.GetCountries()
                               where (countries.IsDeleted == null || countries.IsDeleted == false)
                               select countries;

            foreach (var item in getCountries.ToList())
            {
                items.Add(new SelectListItem { Text = item.CountryName, Value = item.CountryId.ToString() });
            }
            ViewBag.CountryList = items;
            return View(state);
        }


        [HttpPost]
        public ActionResult Edit(State state)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    state.UpdatedBy = 1; // here we will pass the id of logged in user
                    state.UpdatedOn = DateTime.Now;
                    _stateRepository.UpdateState(state);
                    _stateRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException ee)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(state);
        }

        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes. Try again, and if the problem persists see your system administrator.";
            }
            State state = _stateRepository.GetStateByID(id);
            return View(state);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                State state = _stateRepository.GetStateByID(id);
                state.IsDeleted = true;
                _stateRepository.UpdateState(state);
                _stateRepository.Save();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete",
                    new System.Web.Routing.RouteValueDictionary {
                { "id", id },
                { "saveChangesError", true } });
            }
            return RedirectToAction("Index");
        }
    }
}