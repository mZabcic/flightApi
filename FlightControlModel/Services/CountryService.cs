using FlightControlApi.Models;
using FlightControlModel.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightControlModel.Services
{
    public class CountryService
    {
        private readonly CountryRepo _countryRepository;

        public CountryService(CountryRepo countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public List<Country> GetAll()
        {
            return _countryRepository.GetAll();
        }

        public Country Get(int id)
        {
            return _countryRepository.Get(id);
        }
    }
}
