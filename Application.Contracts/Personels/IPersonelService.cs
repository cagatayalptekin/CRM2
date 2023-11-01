using Application.Contracts.Roles;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Personels
{
    public interface IPersonelService
    {
        public Task<ActionResult<IEnumerable<PersonelDto>>> getAllPersonels();
        public Task<ActionResult<PersonelDto>> AddPersonel(PersonelDto PersonelDto);
        public Task<ActionResult<PersonelDto>> getPersonelById(int id);
        public Task<ActionResult<PersonelDto>> Guncelle(PersonelDto PersonelDto);
        public Task<ActionResult<PersonelDto>> deletePersonel(int id);
        public Task<ActionResult<List<PersonelDto>>> PersonelleriListele(int id);
        public Task<ActionResult<Boolean>> isPersonelExist(PersonelDto personelDto);



    }
}
