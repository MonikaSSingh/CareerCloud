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
using static CareerCloud.gRPC.Protos.ApplicantEducation;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantEducationService : ApplicantEducationBase
    {
        private readonly ApplicantEducationLogic _logic;

        public ApplicantEducationService()
        {
            var repo = new EFGenericRepository<ApplicantEducationPoco>();
            _logic = new ApplicantEducationLogic(repo);
        }
        public override Task<ApplicantEducationPayLoad> GetApplicationEducation(IdRequestEducation request, ServerCallContext context)
        {
            ApplicantEducationPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantEducationPayLoad>(
                () => new ApplicantEducationPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Applicant = poco.Applicant.ToString(),
                    CertificateDiploma = poco.CertificateDiploma,
                    CompletionDate = poco.CompletionDate == null ? null : Timestamp.FromDateTime((DateTime)poco.CompletionDate),
                    CompletionPercent = poco.CompletionPercent is null ? 0 : (int)poco.CompletionPercent,
                    Major = poco.Major,
                    StartDate = poco.StartDate == null ? null : Timestamp.FromDateTime((DateTime)poco.StartDate)
                }
                );
        }

        public override Task<Empty> CreateApplicantEducation(ApplicantEducationPayLoad request, ServerCallContext context)
        {
            _logic.Add(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());

        }

        private ApplicantEducationPoco[] PerformedMethod(ApplicantEducationPayLoad request)
        {
            ApplicantEducationPoco poco = new ApplicantEducationPoco()
            {
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                Major = request.Major,
                StartDate = request.StartDate.ToDateTime(),
                CompletionDate = request.CompletionDate.ToDateTime(),
                CompletionPercent = Convert.ToByte(request.CompletionPercent)
            };

            ApplicantEducationPoco[] pocos = new ApplicantEducationPoco[1];
            pocos[1] = poco;

            return pocos;
        }


        public override Task<Empty> UpdateApplicantEducataion(ApplicantEducationPayLoad request, ServerCallContext context)
        {
            _logic.Update(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteApplicantEducation(ApplicantEducationPayLoad request, ServerCallContext context)
        {
            _logic.Delete(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

    }
}
