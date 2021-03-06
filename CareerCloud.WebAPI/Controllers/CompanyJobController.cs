﻿using System;
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
    public class CompanyJobController : ControllerBase
    {
        private readonly CompanyJobLogic _logic;

        public CompanyJobController()
        {
            var repo = new EFGenericRepository<CompanyJobPoco>();
            _logic = new CompanyJobLogic(repo);
        }

        [HttpGet]
        [Route("Job/{Id}")]
        public ActionResult GetCompanyJob(Guid Id)
        {
            CompanyJobPoco poco = _logic.Get(Id);

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
        [Route("Job")]
        public ActionResult GetAllCompanyJob()
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
        [Route("Job")]
        public ActionResult PostCompanyJob([FromBody] CompanyJobPoco[] poco)
        {
            _logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("Job")]
        public ActionResult PutCompanyJob([FromBody] CompanyJobPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("Job")]
        public ActionResult DeleteCompanyJob([FromBody] CompanyJobPoco[] poco)
        {
            _logic.Delete(poco);
            return Ok();
        }
    }
}