using FlightControlApi.Models;
using FlightControlModel.Repos;
using System.Collections.Generic;

namespace FlightControlModel.Services
{
    public class PilotService
    {
        private readonly PilotRepo _pilotRepository;

        public PilotService(PilotRepo pilotRepository)
        {
            _pilotRepository = pilotRepository;
        }

        public List<Pilot> GetAll()
        {
            return _pilotRepository.GetAll();
        }

        public Pilot Get(int id)
        {
            return _pilotRepository.Get(id);
        }

        public int Insert(Pilot p)
        {
            return _pilotRepository.Insert(p);
        }

        public bool Update(int id,Pilot p)
        {
            return _pilotRepository.Update(id,p);
        }

        public bool Delete(int unid)
        {
            return _pilotRepository.Delete(unid);
        }
    }
}