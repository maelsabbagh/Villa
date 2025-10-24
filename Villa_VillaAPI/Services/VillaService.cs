using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Villa_VillaAPI.Data;
using Villa_VillaAPI.IRepository;
using Villa_VillaAPI.Models;
using Villa_VillaAPI.Models.DTO;

namespace Villa_VillaAPI.Services
{
    public class VillaService : IVillaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IVillaRepository _villaRepository;

        public VillaService(ApplicationDbContext context, IMapper mapper,IVillaRepository villaRepository)
        {
            _context = context;
            _mapper = mapper;
            _villaRepository = villaRepository;
        }

        public async Task<IEnumerable<VillaDTO>> GetVillas()
        {
            var villaEntityList =await _villaRepository.GetVillas();
            var villaListDTO = _mapper.Map<IEnumerable<VillaDTO>>(villaEntityList);

            return villaListDTO;
            
        }

        public async Task<VillaDTO> GetVilla(int id)
        {

            var villaEntity = await _villaRepository.GetVilla(id);
            var villaDTO = _mapper.Map<VillaDTO>(villaEntity);
            return villaDTO;
        }

        public async Task<VillaDTO> AddVilla(VillaCreateDTO villaDTO)
        {
            Villa villa = _mapper.Map<Villa>(villaDTO);
            await _villaRepository.Create(villa);
            var savedVillaDTO = _mapper.Map<VillaDTO>(villa);
            return savedVillaDTO;
        }

        public async Task<bool> DeleteVilla(int id)
        {
            var villa = await _villaRepository.GetVilla(id);
            if (villa == null) return false;


            await _villaRepository.DeleteVilla(villa);
            
            return true;
        }

        public async Task<bool> UpdateVilla(VillaUpdateDTO villaDTO)
        {
            var villa = await _villaRepository.GetVilla(villaDTO.Id);

            if (villa == null) return false;

            

            _mapper.Map(villaDTO, villa);

            await _villaRepository.Update(villa);

            return true;
        }
    }
}
