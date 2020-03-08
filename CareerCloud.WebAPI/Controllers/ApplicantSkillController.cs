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
    [Route("api/careercloud/applicant/v1")]
    [ApiController]
    public class ApplicantSkillController : ControllerBase
    {
        private readonly ApplicantSkillLogic _logic;

        public ApplicantSkillController()
        {
            var repo = new EFGenericRepository<ApplicantSkillPoco>();
            _logic = new ApplicantSkillLogic(repo);
        }

        [HttpGet]
        [Route("Skill/{Id}")]
        public ActionResult GetApplicantSkill(Guid Id)
        {
            ApplicantSkillPoco poco = _logic.Get(Id);
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
        [Route("Skill")]
        public ActionResult GetAllApplicantSkill()
        {
            var applicants = _logic.GetAll();
            if (applicants == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(applicants);
            }
        }

        [HttpPost]
        [Route("Skill")]
        public ActionResult PostApplicantSkill([FromBody] ApplicantSkillPoco[] poco)
        {
            _logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("Skill")]
        public ActionResult PutApplicantSkill([FromBody] ApplicantSkillPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("Skill")]
        public ActionResult DeleteApplicantSkill([FromBody] ApplicantSkillPoco[] poco)
        {
            _logic.Delete(poco);
            return Ok();
        }
    }
}