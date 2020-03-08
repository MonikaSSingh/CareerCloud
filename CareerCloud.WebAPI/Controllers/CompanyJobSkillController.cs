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
    public class CompanyJobSkillController : ControllerBase
    {
        private readonly CompanyJobSkillLogic _logic;

        public CompanyJobSkillController()
        {
            var repo = new EFGenericRepository<CompanyJobSkillPoco>();
            _logic = new CompanyJobSkillLogic(repo);
        }

        [HttpGet]
        [Route("JobSkill/{Id}")]
        public ActionResult GetCompanyJobSkill(Guid Id)
        {
            CompanyJobSkillPoco poco = _logic.Get(Id);
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
        [Route("JobSkill")]
        public ActionResult GetAllCompanyJobSkill()
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
        [Route("JobSkill")]
        public ActionResult PostCompanyJobSkill([FromBody] CompanyJobSkillPoco[] poco)
        {
            _logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("JobSkill")]
        public ActionResult PutCompanyJobSkill([FromBody] CompanyJobSkillPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("JobSkill")]
        public ActionResult DeleteCompanyJobSkill([FromBody] CompanyJobSkillPoco[] poco)
        {
            _logic.Delete(poco);
            return Ok();
        }
    }
}