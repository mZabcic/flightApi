using FlightControlApi.Models;
using FlightControlModel.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightControlModel.Services
{
    public class SeatClassService
    {
        private readonly SeatClassRepo _seatClassRepository;

        public SeatClassService(SeatClassRepo seatClassRepository)
        {
            _seatClassRepository = seatClassRepository;
        }

        public List<SeatClass> GetAll()
        {
            return _seatClassRepository.GetAll();
        }

        public SeatClass Get(int id)
        {
            return _seatClassRepository.Get(id);
        }
    }
}
