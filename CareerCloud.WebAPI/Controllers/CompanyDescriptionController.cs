using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyDescriptionController : ControllerBase
    {
        private readonly CompanyDescriptionLogic _logic;

        public CompanyDescriptionController()
        {
            var repo = new EFGenericRepository<CompanyDescriptionPoco>();
            _logic = new CompanyDescriptionLogic(repo);
        }

        [HttpGet]
        [Route("Description/{Id}")]
        public ActionResult GetCompanyDescription(Guid Id)
        {
            CompanyDescriptionPoco poco = _logic.Get(Id);
            if (poco == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(poco);
            }
        }

        [HttpGet]
        [Route("Description")]
        public ActionResult GetAllCompanyDescription()
        {
            var companies = _logic.GetAll();
            if (companies == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(companies);
            }
        }

        [HttpPost]
        [Route("Description")]
        public ActionResult PostCompanyDescription([FromBody] CompanyDescriptionPoco[] poco)
        {
            _logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("Description")]
        public ActionResult PutCompanyDescription([FromBody] CompanyDescriptionPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("Description")]
        public ActionResult DeleteCompanyDescription([FromBody] CompanyDescriptionPoco[] poco)
        {
            _logic.Delete(poco);
            return Ok();
        }
    }
}