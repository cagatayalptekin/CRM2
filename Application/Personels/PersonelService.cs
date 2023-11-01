using Application.Contracts.Departmans;
using Application.Contracts.Personels;
using AutoMapper.Internal.Mappers;
using Domain;
using Domain.Departmans;
using Domain.Personels;
using Domain.Roles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Application.Contracts.Roles;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace Application.Personels
{
    public class PersonelService: IPersonelService
    {
        const string cacheKey = "personels";
        private IPersonelRepository _personelRepository;
        private IDepartmanRepository _departmanRepository;
        private IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memCache;
        public PersonelService(IPersonelRepository personelRepository, IDepartmanRepository departmanRepository,IRoleRepository roleRepository, IMapper mapper, IMemoryCache memCache)
        {
            _personelRepository = personelRepository;
            _departmanRepository = departmanRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _memCache = memCache;

    }
        public async Task<ActionResult<IEnumerable<PersonelDto>>> getAllPersonels()
        {
            if (!_memCache.TryGetValue(cacheKey, out List<PersonelDto> personeldtos))
            {

            
            var personels = await _personelRepository.GetAll();
            var departmans = await _departmanRepository.GetAll();
            var roles = await _roleRepository.GetAll();

            foreach (var item in personels)
            {
                var dept = departmans.First(x => x.Id == item.DepartmanId);
                var role = roles.First(x => x.Id == item.RoleId);
                item.Role = role;
                item.Departman = dept;

            }
            personeldtos = _mapper.Map<List<Personel>, List<PersonelDto>>(personels);


            var cacheExpOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            };
            _memCache.Set(cacheKey, personeldtos, cacheExpOptions);

             }




            return personeldtos;
        }
        public async Task<ActionResult<PersonelDto>> AddPersonel(PersonelDto personelDto)
        {
            var personel = _mapper.Map<PersonelDto, Personel>(personelDto);
            //await _roleRepository.Add(role);
            //return roleDto;
           var personelList= await _personelRepository.GetAll();
         
          await  _personelRepository.Add(personel);
            _memCache.Remove(cacheKey);
            return personelDto;
        }
        public async Task<ActionResult<Boolean>> isPersonelExist(PersonelDto personelDto)
        {
            var personelList =await _personelRepository.GetAll();
            if(personelList.Any(x=>x.Ad==personelDto.Ad&&x.Soyad==personelDto.Soyad))
            {
                return true;
            }
            else
            {
               
                    return false;
            }
        }

        public async Task<ActionResult<PersonelDto>> getPersonelById(int id)
        {
           var personel=await _personelRepository.Get(id);
            var personeldto = _mapper.Map<Personel, PersonelDto>(personel);
            return personeldto;
        }

        public async Task<ActionResult<PersonelDto>> Guncelle(PersonelDto personelDto)
        {
            var personel = _mapper.Map<PersonelDto, Personel>(personelDto);
            await _personelRepository.Update(personel);
            _memCache.Remove(cacheKey);

            return personelDto; 
        }

        public async Task<ActionResult<PersonelDto>> deletePersonel(int id)
        {
            var personel=await _personelRepository.Delete(id);
           var personeldto= _mapper.Map<Personel, PersonelDto>(personel);
            _memCache.Remove(cacheKey);
            return personeldto;
        }
        public async Task<ActionResult<List<PersonelDto>>> PersonelleriListele(int id)
        {
            var allPersonels = await _personelRepository.GetAll();
            var personels = allPersonels.Where(x => x.RoleId == id).ToList();
            var personeldto = _mapper.Map<List<Personel>, List<PersonelDto>>(personels);

            return personeldto;
        }
    }
}
