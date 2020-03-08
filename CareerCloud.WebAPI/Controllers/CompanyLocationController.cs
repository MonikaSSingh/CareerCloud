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
    public class CompanyLocationController : ControllerBase
    {
        private readonly CompanyLocationLogic _logic;

        public CompanyLocationController()
        {
            var repo = new EFGenericRepository<CompanyLocationPoco>();
            _logic = new CompanyLocationLogic(repo);
        }

        [HttpGet]
        [Route("Location/{Id}")]
        public ActionResult GetCompanyLocation(Guid Id)
        {
            CompanyLocationPoco poco = _logic.Get(Id);
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
        [Route("Location")]
        public ActionResult GetAllCompanyLocation()
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
        [Route("Location")]
        public ActionResult PostCompanyLocation([FromBody] CompanyLocationPoco[] poco)
        {
            _logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("Location")]
        public ActionResult PutCompanyLocation([FromBody] CompanyLocationPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("Location")]
        public ActionResult DeleteCompanyLocation([FromBody] CompanyLocationPoco[] poco)
        {
            _logic.Delete(poco);
            return Ok();
        }
    }
}