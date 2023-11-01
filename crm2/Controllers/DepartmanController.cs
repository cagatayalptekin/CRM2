using Application.Contracts.Departmans;
using Application.Contracts.Personels;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;

namespace crm2.Controllers
{
    public class DepartmanController : Controller
    {
        private IDepartmanService _departmanService;
        public DepartmanController(IDepartmanService departmanService)
        {
            _departmanService = departmanService;
        }
        public async Task<IActionResult> Index()
        {
           var departmans=await _departmanService.getAllDepartmans();
          var departmanList= departmans.Value.ToList();

            return View("~/Views/Departman/GetAll.cshtml", departmanList);
        }
        public async  Task<IActionResult> Kaydet(DepartmanDto departmanDto)
        {
            if(!ModelState.IsValid)
            {
                return View("Yeni", new DepartmanDto());
            }
            if(departmanDto.Id==0)
            {
                var result = await _departmanService.isDepartmanExist(departmanDto);

                if (result.Value == true)
                {
                    return View("Eklenemedi");
                }
                else
                {
                    await _departmanService.AddDepartman(departmanDto);
                }
            }
          else
            {
              await  _departmanService.Guncelle(departmanDto);
            }
           
            return RedirectToAction("Index");
           

        }
        public IActionResult Yeni()
        {
            return View("Yeni", new DepartmanDto());
        }
        public async Task<IActionResult> Guncelle(int id)
        {
           var departmendto=await _departmanService.getDepartmanById(id);
        // var deptdto=  departmendto.Value;
           

            return View("Yeni",departmendto.Value);
        }
        public async Task<IActionResult> Delete(int id)
        {
           await _departmanService.deleteDepartman(id);

            return RedirectToAction("Index");
            
        }


    }
}
