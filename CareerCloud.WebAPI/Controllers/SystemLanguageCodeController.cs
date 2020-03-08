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
    [Route("api/careercloud/system/v1")]
    [ApiController]
    public class SystemLanguageCodeController : ControllerBase
    {
        private readonly SystemLanguageCodeLogic _logic;

        public SystemLanguageCodeController()
        {
            var repo = new EFGenericRepository<SystemLanguageCodePoco>();
            _logic = new SystemLanguageCodeLogic(repo);
        }

        [HttpGet]
        [Route("LanguageCode/{Id}")]
        public ActionResult GetSystemLanguageCode(string Id)
        {
            SystemLanguageCodePoco poco = _logic.Get(Id);
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
        [Route("LanguageCode")]
        public ActionResult GetAllSystemLanguageCode()
        {
            var systems = _logic.GetAll();
            if (systems == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(systems);
            }
        }

        [HttpPost]
        [Route("LanguageCode")]
        public ActionResult PostSystemLanguageCode([FromBody] SystemLanguageCodePoco[] poco)
        {
            _logic.Add(poco);
            return Ok();
        }

        [HttpPut]
        [Route("LanguageCode")]
        public ActionResult PutSystemLanguageCode([FromBody] SystemLanguageCodePoco[] poco)
        {
            _logic.Update(poco);
            return Ok();
        }

        [HttpDelete]
        [Route("LanguageCode")]
        public ActionResult DeleteSystemLanguageCode([FromBody] SystemLanguageCodePoco[] poco)
        {
            _logic.Delete(poco);
            return Ok();
        }
    }
}