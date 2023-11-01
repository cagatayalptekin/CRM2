using Application.Contracts.Departmans;
using Application.Contracts.Personels;
using Application.Contracts.Tasks;
using AutoMapper;
using Domain;
using Domain.Departmans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Departmans
{
    public class DepartmanService:IDepartmanService
    {
        const string cacheKey = "departmans";

        private IDepartmanRepository _departmanRepository;
        private IMapper _mapper;
        private readonly IMemoryCache _memCache;

        public DepartmanService(IDepartmanRepository departmanRepository, IMapper mapper, IMemoryCache memCache)
        {
            _departmanRepository = departmanRepository;
            _mapper = mapper;
            _memCache = memCache;
        }
        public async Task<ActionResult<IEnumerable<DepartmanDto>>> getAllDepartmans()
        {
            if (!_memCache.TryGetValue(cacheKey, out List<DepartmanDto> departmandto))
            {



                var departmans = await _departmanRepository.GetAll();
                //departmandto = departmans.Select(x => new DepartmanDto
                //{
                //    DepartmanName = x.DepartmanName,
                //    Id = x.Id,
                //    Personels = x.Personels

                //}).ToList();
                departmandto = _mapper.Map<List<Departman>, List<DepartmanDto>>(departmans);

                var cacheExpOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                    Priority = CacheItemPriority.Normal
                };
                _memCache.Set(cacheKey, departmandto, cacheExpOptions);
            }
            return departmandto;
        }
        public async Task<ActionResult<DepartmanDto>> AddDepartman(DepartmanDto departmanDto)
        {
            var departman=_mapper.Map<DepartmanDto, Departman>(departmanDto);

           await _departmanRepository.Add(departman);
            _memCache.Remove(cacheKey);
            return departmanDto;
            
        }
        public async Task<ActionResult<DepartmanDto>> getDepartmanById(int id)
        {
            var departman = await _departmanRepository.Get(id);
        //  await  _departmanRepository.Update(departman);
            var departmandto = _mapper.Map<Departman, DepartmanDto>(departman);
            return departmandto;


        }
        public async Task<ActionResult<DepartmanDto>> Guncelle(DepartmanDto departmanDto)
        {


            var departman = _mapper.Map<DepartmanDto, Departman>(departmanDto);
        await _departmanRepository.Update(departman);
            _memCache.Remove(cacheKey);

            return departmanDto;
    

        }
        public async Task<ActionResult<DepartmanDto>> deleteDepartman(int id)
        {
          var deleted=await _departmanRepository.Delete(id);
          var departmandto = _mapper.Map<Departman, DepartmanDto>(deleted);
            _memCache.Remove(cacheKey);
            return departmandto;
            
        }
        public async Task<ActionResult<Boolean>> isDepartmanExist(DepartmanDto departmanDto)
        {
            var personelList = await _departmanRepository.GetAll();
            if (personelList.Any(x => x.DepartmanName == departmanDto.DepartmanName))
            {
                return true;
            }
            else
            {

                return false;
            }
        }
    }
}
