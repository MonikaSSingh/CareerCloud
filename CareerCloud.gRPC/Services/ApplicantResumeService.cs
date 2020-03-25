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
using static CareerCloud.gRPC.Protos.ApplicantResume;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantResumeService : ApplicantResumeBase
    {
        private readonly ApplicantResumeLogic _logic;

        public ApplicantResumeService()
        {
            var repo = new EFGenericRepository<ApplicantResumePoco>();
            _logic = new ApplicantResumeLogic(repo);
        }

        public override Task<ApplicantResumePayLoad> GetApplicantResume(IdRequestResume request, ServerCallContext context)
        {

            ApplicantResumePoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantResumePayLoad>(
                () => new ApplicantResumePayLoad()
                {
                    Id = poco.Id.ToString(),
                    Applicant = poco.Applicant.ToString(),
                    Resume = poco.Resume,
                    LastUpdated = poco.LastUpdated == null ? null : Timestamp.FromDateTime((DateTime)poco.LastUpdated)
                });
        }

        public override Task<Empty> CreateApplicantResume(ApplicantResumePayLoad request, ServerCallContext context)
        {
            _logic.Add(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        private ApplicantResumePoco[] PerformedMethod(ApplicantResumePayLoad request)
        {
            ApplicantResumePoco poco = new ApplicantResumePoco()
            {
                Id=Guid.Parse(request.Id),
                Applicant=Guid.Parse(request.Applicant),
                Resume=request.Resume,
                LastUpdated=request.LastUpdated.ToDateTime()
            };

            ApplicantResumePoco[] pocos = new ApplicantResumePoco[1];
            pocos[1] = poco;

            return pocos;
        }

        public override Task<Empty> UpdateApplicantResume(ApplicantResumePayLoad request, ServerCallContext context)
        {
            _logic.Update(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteApplicantResume(ApplicantResumePayLoad request, ServerCallContext context)
        {
            _logic.Delete(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }
    }
}
