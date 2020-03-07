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
    public class ApplicantEducationController : ControllerBase
    {
        private readonly ApplicantEducationLogic _logic;

        public ApplicantEducationController()
        {
            var repo = new EFGenericRepository<ApplicantEducationPoco>();
            _logic = new ApplicantEducationLogic(repo); 
        }

        [HttpGet]
        [Route("education/{Id}")]
        public ActionResult GetApplicantEducation(Guid Id)
        {
            ApplicantEducationPoco poco = _logic.Get(Id);
            if(poco==null)
            {
                return NotFound();
            }
            else
            {
                return Ok(poco);
            }
            
        }
    }
}