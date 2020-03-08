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
    public class CompanyProfileController : ControllerBase
    {
        private readonly CompanyProfileLogic _logic;

        public CompanyProfileController()
        {
            var repo = new EFGenericRepository<CompanyProfilePoco>();
            _logic = new CompanyProfileLogic(repo);
        }

        [HttpGet]
        [Route("ComapanyProfile/{Id}")]
        public ActionResult GetCompanyProfile(Guid Id)
        {
            CompanyProfilePoco poco = _logic.Get(Id);

            if(poco== null)
            {
                return NotFound();
            }
            else
            {
                return Ok(poco);
            }
        }

        [HttpGet]
        [Route("ComapanyProfile")]
        public ActionResult GetAllCompanyProfile()
        {
            var companies = _logic.GetAll();
            if(companies==null)
            {
                return NotFound();
            }
            else
            {
                return Ok(companies);
            }
        }

        [HttpPost]
        [Route("ComapanyProfile")]
        public ActionResult PostCompanyProfile([FromBody] CompanyProfilePoco[] poco)
        {
            _logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("ComapanyProfile")]
        public ActionResult PutCompanyProfile([FromBody] CompanyProfilePoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("ComapanyProfile")]
        public ActionResult DeleteCompanyProfile([FromBody] CompanyProfilePoco[] poco)
        {
            _logic.Delete(poco);
            return Ok();
        }
    }
}