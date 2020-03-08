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
    public class SecurityRoleController : ControllerBase
    {
        private readonly SecurityRoleLogic _logic;

        public SecurityRoleController()
        {
            var repo = new EFGenericRepository<SecurityRolePoco>();
            _logic = new SecurityRoleLogic(repo);
        }
        [HttpGet]
        [Route("Role/{Id}")]
        public ActionResult GetSecurityRole(Guid Id)
        {
            SecurityRolePoco poco = _logic.Get(Id);
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
        [Route("Role")]
        public ActionResult GetAllSecurityRole()
        {
            var role = _logic.GetAll();
            if (role == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(role);
            }
        }

        [HttpPost]
        [Route("Role")]
        public ActionResult PostSecurityRole([FromBody] SecurityRolePoco[] poco)
        {
            _logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("Role")]
        public ActionResult PutSecurityRole([FromBody] SecurityRolePoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("Role")]
        public ActionResult DeleteSecurityRole([FromBody] SecurityRolePoco[] poco)
        {
            _logic.Delete(poco);
            return Ok();
        }
    }
}