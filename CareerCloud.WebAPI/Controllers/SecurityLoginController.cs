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
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginController : ControllerBase
    {
        private readonly SecurityLoginLogic _logic;

        public SecurityLoginController()
        {
            var repo = new EFGenericRepository<SecurityLoginPoco>();
            _logic = new SecurityLoginLogic(repo);
        }

        [HttpGet]
        [Route("Login/{Id}")]
        public ActionResult GetSecurityLogin(Guid Id)
        {
            SecurityLoginPoco poco = _logic.Get(Id);
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
        [Route("Login")]
        public ActionResult GetAllSecurityLogin()
        {
            var logins = _logic.GetAll();
            if(logins == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(logins);
            }
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult PostSecurityLogin([FromBody] SecurityLoginPoco[] poco)
        {
            _logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("Login")]
        public ActionResult PutSecurityLogin([FromBody] SecurityLoginPoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("Login")]
        public ActionResult DeleteSecurityLogin([FromBody] SecurityLoginPoco[] poco)
        {
            _logic.Delete(poco);
            return Ok();
        }
    }
}