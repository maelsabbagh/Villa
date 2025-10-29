using AutoMapper;
using System.Linq.Expressions;
using Villa_VillaAPI.IRepository;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;
using Villa_VillaAPI.Services.IServices;

namespace Villa_VillaAPI.Services
{
    public class VillaNumberService : IVillaNumberService
    {
        private readonly IVillaNumberRepository _villaNumberRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<VillaNumberService> _logger;
        private readonly IVillaRepository _villaRepository;
        public VillaNumberService(ILogger<VillaNumberService> logger,IMapper mapper,IVillaNumberRepository villaNumberRepository,IVillaRepository villaRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _villaNumberRepository = villaNumberRepository;
            _villaRepository = villaRepository;
        }

        public async Task<VillaNumberDTO> AddVillaNumber(VillaNumberCreateDTO villaNumberCreateDTO)
        {
            bool isExists = await _villaNumberRepository.IsVillaNumberExists(villaNumberCreateDTO.VillaNo.Value);

            if(isExists)
            {
                throw new InvalidOperationException($"Villa number {villaNumberCreateDTO.VillaNo} already exists.");
            }

            // check for foreign exists or not, if not throw an exception
            bool isVillaExists = await _villaRepository.IsVillaExists(villaNumberCreateDTO.VillaId.Value);

            if(!isVillaExists)
            {
                throw new KeyNotFoundException($"Villa with Id {villaNumberCreateDTO.VillaId} does not exist.");
            }


            VillaNumber villaNumber = _mapper.Map<VillaNumber>(villaNumberCreateDTO);
            await _villaNumberRepository.Create(villaNumber);

            VillaNumberDTO villaNumberDTO = _mapper.Map<VillaNumberDTO>(villaNumber);

            return villaNumberDTO;


        }

        public async Task<IEnumerable<VillaNumberDTO>> GetAll(Expression<Func<VillaNumber, bool>>? filter = null)
        {
            var villaNumbers =await _villaNumberRepository.GetAll(filter);
            var villaNumbersDTO = _mapper.Map<IEnumerable<VillaNumberDTO>>(villaNumbers);

            return villaNumbersDTO;
        }

        public async Task<VillaNumberDTO> GetVillaNumber(int villaNo)
        {
            VillaNumber villaNumber = await _villaNumberRepository.GetVillaNumber(villaNo);
            VillaNumberDTO villaNumberDTO = _mapper.Map<VillaNumberDTO>(villaNumber);

            return villaNumberDTO;
        }

        public async Task<VillaNumberDTO> GetVillaNumber(Expression<Func<VillaNumber, bool>>? filter = null, bool isTracked = true)
        {
            VillaNumber villaNumber = await _villaNumberRepository.GetVillaNumber(filter, false);

            VillaNumberDTO villaNumberDTO = _mapper.Map<VillaNumberDTO>(villaNumber);

            return villaNumberDTO;

        }

        public async Task<bool> DeleteVillaNumber(int id)
        {
            var villaNumber = await _villaNumberRepository.GetVillaNumber(id);

            if (villaNumber==null) return false;

            await _villaNumberRepository.Delete(villaNumber);

            return true;
        }

       

        public async Task<bool> UpdateVillaNumber(VillaNumberUpdateDTO villaNumberUpdateDTO)
        {


            VillaNumber villaNumber = await _villaNumberRepository.GetVillaNumber(villaNumberUpdateDTO.VillaNo.Value);

            if (villaNumber == null) return false;

            // check for foreign exists or not, if not throw an exception
            bool isVillaExists = await _villaRepository.IsVillaExists(villaNumberUpdateDTO.VillaId.Value);

            if (!isVillaExists)
            {
                throw new KeyNotFoundException($"Villa with Id {villaNumberUpdateDTO.VillaId} does not exist.");
            }


            _mapper.Map(villaNumberUpdateDTO, villaNumber);

            await _villaNumberRepository.Update(villaNumber);
            return true;
        }
    }
}
