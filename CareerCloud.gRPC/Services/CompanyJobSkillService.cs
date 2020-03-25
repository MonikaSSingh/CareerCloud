using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.CompanyJobSkill;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobSkillService : CompanyJobSkillBase
    {
        private readonly CompanyJobSkillLogic _logic;

        public CompanyJobSkillService()
        {
            var repo = new EFGenericRepository<CompanyJobSkillPoco>();
            _logic = new CompanyJobSkillLogic(repo);
        }

        public override Task<CompanyJobSkillPayLoad> GetCompanyJobSkill(IdRequestJobSkill request, ServerCallContext context)
        {
            CompanyJobSkillPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyJobSkillPayLoad>(
                () => new CompanyJobSkillPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Job = poco.Job.ToString(),
                    Skill = poco.Skill,
                    SkillLevel = poco.SkillLevel,
                    Importance = poco.Importance

                });
        }

        public override Task<Empty> CreateCompanyJobSkill(CompanyJobSkillPayLoad request, ServerCallContext context)
        {
            _logic.Add(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        private CompanyJobSkillPoco[] PerformedMethod(CompanyJobSkillPayLoad request)
        {
            CompanyJobSkillPoco poco = new CompanyJobSkillPoco()
            {
                Id = Guid.Parse(request.Id),
                Job = Guid.Parse(request.Job),
                Skill = request.Skill,
                SkillLevel = request.SkillLevel,
                Importance = request.Importance
            };

            CompanyJobSkillPoco[] pocos = new CompanyJobSkillPoco[1];
            pocos[1] = poco;

            return pocos;
        }

        public override Task<Empty> UpdateCompanyJobSkill(CompanyJobSkillPayLoad request, ServerCallContext context)
        {
            _logic.Update(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteCompanyJobSkill(CompanyJobSkillPayLoad request, ServerCallContext context)
        {
            _logic.Delete(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }
    }
}
