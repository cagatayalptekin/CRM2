using Application.Contracts.Departmans;
using Application.Contracts.Personels;
using Application.Contracts.Roles;
using Application.Departmans;
using Microsoft.AspNetCore.Mvc;

namespace crm2.Controllers
{
    public class RoleController : Controller
    {
        
        private IRolesService _rolesService;
        public RoleController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _rolesService.getAllRoles();
            var roleList = roles.Value.ToList();

            return View("~/Views/Role/GetAll.cshtml", roleList);
        }
        public async Task<IActionResult> Kaydet(RoleDto roleDto)
        {
            if (!ModelState.IsValid)
            {
                return View("Yeni", new RoleDto());
            }
            if (roleDto.Id == 0)
            {
                var result = await _rolesService.isRoleExist(roleDto);

                if (result.Value == true)
                {
                    return View("Eklenemedi");
                }
                else
                {
                    await _rolesService.AddRole(roleDto);
                }
            }
            else
            {
                await _rolesService.Guncelle(roleDto);
            }

            return RedirectToAction("Index");


        }
        public IActionResult Yeni()
        {
            return View("Yeni", new RoleDto());
        }
        public async Task<IActionResult> Guncelle(int id)
        {
            var roleDto = await _rolesService.getRoleById(id);
            // var deptdto=  departmendto.Value;


            return View("Yeni", roleDto.Value);
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _rolesService.deleteRole(id);

            return RedirectToAction("Index");

        }
    }
}
