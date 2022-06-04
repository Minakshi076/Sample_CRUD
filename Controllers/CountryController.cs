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
    public class CountryController : Controller
    {
        private ICountryRepository _countryRepository;

        public CountryController()
        {
            this._countryRepository = new CountryRepository(new SampleCrudContext());
        }

        public ActionResult Index()
        {
            var _countries = from countries in _countryRepository.GetCountries() where (countries.IsDeleted==null||countries.IsDeleted==false)
                             select countries;
            return View(_countries);
        }

        public ViewResult Details(int id)
        {
            Country country = _countryRepository.GetCountryByID(id);
            return View(country);
        }

        public ActionResult Create()
        {
            return View(new Country());
        }

        [HttpPost]
        public ActionResult Create(Country country)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    country.CreatedBy = 1; // here we will pass the id of logged in user
                    country.CreatedOn = DateTime.Now;
                    _countryRepository.InsertCountry(country);
                    _countryRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException ee)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(country);
        }

        public ActionResult Edit(int id)
        {
            Country country = _countryRepository.GetCountryByID(id);
            return View(country);
        }


        [HttpPost]
        public ActionResult Edit(Country country)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    country.UpdatedBy = 1; // here we will pass the id of logged in user
                    country.UpdatedOn = DateTime.Now;
                    _countryRepository.UpdateCountry(country);
                    _countryRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException ee)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(country);
        }

        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes. Try again, and if the problem persists see your system administrator.";
            }
            Country country = _countryRepository.GetCountryByID(id);
            return View(country);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Country country = _countryRepository.GetCountryByID(id);
                country.IsDeleted = true;
                _countryRepository.UpdateCountry(country);
                _countryRepository.Save();
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