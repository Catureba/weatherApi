using AutoMapper;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using weatherApi.Models;

namespace weatherApi.Data.Repository
{
    public class MeteorologicalRepository : IMeteorologicalRepository
    {
        private MeteorologicalContext _context;
        private IMapper _mapper;
        public MeteorologicalRepository(MeteorologicalContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        public MeteorologicalModel? FindByID(Guid id)
        {
            return _context.Weathers.FirstOrDefault(x => x.Id == id);
        }


        public MeteorologicalModel AddMeteorologicalRegister(MeteorologicalModel weather)
        {
            var responseEntity = _context.Weathers.Add(weather);
            MeteorologicalModel response = _mapper.Map<MeteorologicalModel>(responseEntity.Entity);
            _context.SaveChanges();
            return response;
        }
        public MeteorologicalModel? EditMeteorologicalRegister(MeteorologicalModel meteorological)
        {
            MeteorologicalModel? meteorologicalToEdit = _context.Weathers.FirstOrDefault(x => x.Id == meteorological.Id);
            if (meteorologicalToEdit == null) return null;
            Console.WriteLine("#######");
            Console.WriteLine(meteorological.City + " From: " + meteorologicalToEdit.City);

            _mapper.Map(meteorologicalToEdit, meteorological);
            //_mapper.Map(meteorological, meteorologicalToEdit);
            _context.SaveChanges();

            return _context.Weathers.FirstOrDefault(x => x.Id == meteorological.Id);
        }

        public MeteorologicalModel DeleteMeteorologicalRegister(MeteorologicalModel meteorological)
        {
            _context.Weathers.Remove(meteorological);
            _context.SaveChanges();
            return meteorological;
        }



        public List<MeteorologicalModel> ListAll()
        {
            return _context.Weathers.ToList();
        }
    }
}
