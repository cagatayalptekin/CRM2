using Application.Contracts.Departmans;
using Application.Contracts.Personels;
using Application.Contracts.Roles;
using AutoMapper;
using Domain;
using Domain.Departmans;
using Domain.Roles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Roles
{
    public class RoleService: IRolesService
    {
        const string cacheKey = "roles";

        private IRoleRepository _roleRepository;
        private IMapper _mapper;
        private readonly IMemoryCache _memCache;

        public RoleService(IRoleRepository roleRepository, IMapper mapper,IMemoryCache memCache)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _memCache = memCache;
        }

        
            

    
        public async Task<ActionResult<IEnumerable<RoleDto>>> getAllRoles()
        {
            if (!_memCache.TryGetValue(cacheKey, out List<RoleDto> roledto))
            {
                var roles = await _roleRepository.GetAll();
                // roledto = roles.Select(x => new RoleDto
                //{
                //    Id = x.Id,
                //    Personels = x.Personels,
                //    RoleName = x.RoleName

                //}).ToList();
                roledto = _mapper.Map<List<Role>, List<RoleDto>>(roles);

            }
            var cacheExpOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(30),
                Priority = CacheItemPriority.Normal
            };
            _memCache.Set(cacheKey, roledto, cacheExpOptions);


            return roledto;
        }

        public async Task<ActionResult<RoleDto>> AddRole(RoleDto roleDto)
        {
            var role = _mapper.Map<RoleDto, Role>(roleDto);
            await _roleRepository.Add(role);
            _memCache.Remove(cacheKey);
            return roleDto;
        }

        public async Task<ActionResult<RoleDto>> deleteRole(int id)
        {
            var deleted = await _roleRepository.Delete(id);
            var roleDto = _mapper.Map<Role, RoleDto>(deleted);
            _memCache.Remove(cacheKey);
            return roleDto;
        }

        public async Task<ActionResult<RoleDto>> getRoleById(int id)
        {
            var role = await _roleRepository.Get(id);
            //  await  _departmanRepository.Update(departman);
            var roleDto = _mapper.Map<Role, RoleDto>(role);
            return roleDto;
        }

        public async Task<ActionResult<RoleDto>> Guncelle(RoleDto roleDto)
        {
            var role = _mapper.Map<RoleDto, Role>(roleDto);
            await _roleRepository.Update(role);
            _memCache.Remove(cacheKey);

            return roleDto;
        }
        public async Task<ActionResult<Boolean>> isRoleExist(RoleDto roleDto)
        {
            var roleList = await _roleRepository.GetAll();
            if (roleList.Any(x => x.RoleName == roleDto.RoleName))
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
