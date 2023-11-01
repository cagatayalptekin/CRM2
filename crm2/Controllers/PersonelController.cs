using Application.Contracts.Departmans;
using Application.Contracts.Personels;
using Application.Contracts.Roles;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace crm2.Controllers
{
    public class PersonelController : Controller
    {
        private IPersonelService _personelService;
        private IRolesService _rolesService;
        private IDepartmanService _departmanService;
        private IMapper _mapper;
        public PersonelController(IPersonelService personelService, IRolesService rolesService, IDepartmanService departmanService,IMapper mapper)
        {
            _personelService = personelService;
            _rolesService = rolesService;
            _departmanService = departmanService;
            _mapper = mapper;
        }
        //navbar, authorize,groupby,search,order
        public async Task<IActionResult> Index()
        {
           var personels= await _personelService.getAllPersonels();
            return View("~/Views/Personel/GetAll.cshtml",personels.Value.ToList());

        }
        public async Task<IActionResult> Yeni()
        {
            var departmans =await _departmanService.getAllDepartmans();
            var depslist = departmans.Value.ToList();
            depslist.Count();
            var roles=await _rolesService.getAllRoles();
            var rolelist=roles.Value.ToList();
            
                var model = new PersonelFormViewModel
                {
                    Departmans = depslist,
                    Roles = rolelist,
                    PersonelDto = new PersonelDto()
                };
                return View("Yeni", model);
           


            
        }
        public async Task<IActionResult> Guncelle(int id)
        {
            var departmans = await _departmanService.getAllDepartmans();
            var depslist = departmans.Value.ToList();
            var roles = await _rolesService.getAllRoles();
            var rolelist = roles.Value.ToList();
            var persDto =await _personelService.getPersonelById(id);
            var pers = persDto.Value;

            var model = new PersonelFormViewModel
            {
                Departmans = depslist,
                Roles = rolelist,
                PersonelDto = pers
            };
            return View("Yeni", model);
        }
        public async Task<IActionResult> Kaydet(PersonelDto PersonelDto)
        {
            if (!ModelState.IsValid)
            {
                var departmans = await _departmanService.getAllDepartmans();
                var depslist = departmans.Value.ToList();
                depslist.Count();
                var roles = await _rolesService.getAllRoles();
                var rolelist = roles.Value.ToList();

                var model = new PersonelFormViewModel
                {
                    Departmans = depslist,
                    Roles = rolelist,
                    PersonelDto = new PersonelDto()
                };
                return View("Yeni", model);
            }
            if (PersonelDto.Id == 0)
            {
                var result=await _personelService.isPersonelExist(PersonelDto);
                
                if(result.Value==true)
                {
                    return View("Eklenemedi");
                }
                else
                {
                    await _personelService.AddPersonel(PersonelDto);
                }

                
            }
            else
            {
                await _personelService.Guncelle(PersonelDto);
            }

            return RedirectToAction("Index");


        }
        public async Task<ActionResult<PersonelDto>> deletePersonel(int id)
        {
            var deleted = await _personelService.deletePersonel(id);

            return RedirectToAction("Index");
        }
        public async Task<ActionResult<List<PersonelDto>>> PersonelleriListele(int id)
        {
            var personels = await _personelService.PersonelleriListele(id);

            return PartialView(personels.Value.ToList());
        }

    }
}
