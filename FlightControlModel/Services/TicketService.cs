using FlightControlApi.Models;
using FlightControlModel.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightControlModel.Services
{
    public class TicketService
    {
        private readonly TicketRepo _storeRepository;

        public TicketService(TicketRepo storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public List<Ticket> GetAll()
        {
            return _storeRepository.GetAll();
        }

        public Ticket Get(int id)
        {
            return _storeRepository.Get(id);
        }

        public int Insert(Ticket p)
        {
            return _storeRepository.Insert(p);
        }

        public bool Update(int id, Ticket p)
        {
            return _storeRepository.Update(id, p);
        }

        public bool Delete(int unid)
        {
            return _storeRepository.Delete(unid);
        }

    }
}
