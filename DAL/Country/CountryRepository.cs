using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Sample_CRUD.Models;
using System.Data.Entity;

namespace Sample_CRUD.DAL
{
    public class CountryRepository : ICountryRepository
    {
        private SampleCrudContext _context;

        public CountryRepository(SampleCrudContext dbContext)
        {
            this._context = dbContext;
        }

        public IEnumerable<Country> GetCountries()
        {
            return _context.Country.ToList();
        }

        public Country GetCountryByID(int id)
        {
            return _context.Country.Find(id);
        }

        public void InsertCountry(Country country)
        {
            _context.Country.Add(country);
        }

        public void DeleteCountry(int countryID)
        {
            Country country = _context.Country.Find(countryID);
            _context.Country.Remove(country);
        }

        public void UpdateCountry(Country country)
        {
            _context.Entry(country).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
