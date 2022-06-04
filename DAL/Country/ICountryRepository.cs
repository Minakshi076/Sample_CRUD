using Sample_CRUD.Models;
using System;
using System.Collections.Generic;

namespace Sample_CRUD.DAL
{
    public interface ICountryRepository : IDisposable
    {
        IEnumerable<Country> GetCountries();
        Country GetCountryByID(int CountryId);        
        void InsertCountry(Country country);        
        void DeleteCountry(int CountryID);
        void UpdateCountry(Country country);
        void Save();        
    }
}