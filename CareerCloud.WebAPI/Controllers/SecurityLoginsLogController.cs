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
    [Route("api/carrercloud/security/v1")]
    [ApiController]
    public class SecurityLoginsLogController : ControllerBase
    {
        private readonly SecurityLoginsLogLogic _logic;

        public SecurityLoginsLogController()
        {
            var repo = new EFGenericRepository<SecurityLoginsLogPoco>();
            _logic = new SecurityLoginsLogLogic(repo);
        }

        [HttpGet]
        [Route("LoginsLog/{Id}")]
        public ActionResult GetSecurityLoginLog(Guid Id)
        {
            SecurityLoginsLogPoco poco = _logic.Get(Id);
            if(poco == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(poco);
            }
        }

        [HttpGet]
        [Route("LoginsLog")]
        public ActionResult GetAllSecurityLoginLog()
        {
            var logins = _logic.GetAll();
            if(logins== null)
            {
                return NotFound();
            }
            else
            {
                return Ok(logins);
            }
        }

        [HttpPost]
        [Route("LoginsLog")] 
        public ActionResult PostSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] poco)
        {
            _logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("LoginsLog")]
        public ActionResult PutSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("LoginsLog")]
        public ActionResult DeleteSecurityLoginLog([FromBody] SecurityLoginsLogPoco[] poco)
        {
            _logic.Delete(poco);
            return Ok();
        }
    }
}