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
using static CareerCloud.gRPC.Protos.ApplicantSkill;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantSkillService : ApplicantSkillBase
    {
        private readonly ApplicantSkillLogic _logic;

        public ApplicantSkillService()
        {
            var repo = new EFGenericRepository<ApplicantSkillPoco>();
            _logic = new ApplicantSkillLogic(repo);
        }

        public override Task<ApplicantSkillPayLoad> GetApplicantSkill(IdRequestSkill request, ServerCallContext context)
        {
            ApplicantSkillPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantSkillPayLoad>(
                () => new ApplicantSkillPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Applicant = poco.Applicant.ToString(),
                    Skill = poco.Skill,
                    SkillLevel = poco.SkillLevel,
                    StartMonth = poco.StartMonth,
                    StartYear = poco.StartYear,
                    EndYear = poco.EndYear,
                    EndMonth = poco.EndMonth
                });
        }

        public override Task<Empty> CreateApplicanrSkill(ApplicantSkillPayLoad request, ServerCallContext context)
        {
            _logic.Add(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        private ApplicantSkillPoco[] PerformedMethod(ApplicantSkillPayLoad request)
        {
            ApplicantSkillPoco poco = new ApplicantSkillPoco()
            {
                Id=Guid.Parse(request.Id),
                Applicant=Guid.Parse(request.Applicant),
                Skill=request.Skill,
                SkillLevel=request.SkillLevel,
                StartMonth=(byte)request.StartMonth,
                StartYear=request.StartYear,
                EndMonth=(byte)request.EndMonth,
                EndYear=request.EndYear
            };
            ApplicantSkillPoco[] pocos = new ApplicantSkillPoco[1];
            pocos[1] = poco;
            return pocos;
        }

        public override Task<Empty> UpdateApplicantSkill(ApplicantSkillPayLoad request, ServerCallContext context)
        {
            _logic.Update(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteApplicantSkill(ApplicantSkillPayLoad request, ServerCallContext context)
        {
            _logic.Delete(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }
    }
}
