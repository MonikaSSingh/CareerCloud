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
using static CareerCloud.gRPC.Protos.ApplicantWorkHistory;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantWorkHistoryService : ApplicantWorkHistoryBase
    {
        private readonly ApplicantWorkHistoryLogic _logic;

        public ApplicantWorkHistoryService()
        {
            var repo = new EFGenericRepository<ApplicantWorkHistoryPoco>();
            _logic = new ApplicantWorkHistoryLogic(repo);
        }


        public override Task<ApplicantWorkHistoryPayLoad> GetApplicantWorkHistory(IdRequestHistory request, ServerCallContext context)
        {

            ApplicantWorkHistoryPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantWorkHistoryPayLoad>(
                () => new ApplicantWorkHistoryPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Applicant = poco.Applicant.ToString(),
                    CompanyName = poco.CompanyName,
                    CountryCode = poco.CountryCode,
                    Location = poco.Location,
                    JobTitle = poco.JobTitle,
                    JobDescription = poco.JobDescription,
                    StartMonth = poco.StartMonth,
                    StartYear = poco.StartYear,
                    EndMonth = poco.EndMonth,
                    EndYear = poco.EndYear
                });
        }

        public override Task<Empty> CreateApplicantWorkHistory(ApplicantWorkHistoryPayLoad request, ServerCallContext context)
        {
            _logic.Add(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        private ApplicantWorkHistoryPoco[] PerformedMethod(ApplicantWorkHistoryPayLoad request)
        {
            ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco()
            {
                Id = Guid.Parse(request.Id),
                Applicant = Guid.Parse(request.Applicant),
                CompanyName = request.CompanyName,
                CountryCode = request.CountryCode,
                Location = request.Location,
                JobTitle = request.JobTitle,
                JobDescription = request.JobDescription,
                StartMonth = (short)request.StartMonth,
                StartYear = request.StartYear,
                EndMonth = (short)request.EndMonth,
                EndYear = request.EndYear
            };

            ApplicantWorkHistoryPoco[] pocos = new ApplicantWorkHistoryPoco[1];
            pocos[1] = poco;
            return pocos;

        }

        public override Task<Empty> UpdateApplicantWorkHistory(ApplicantWorkHistoryPayLoad request, ServerCallContext context)
        {
            _logic.Update(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteApplicantWorkHistory(ApplicantWorkHistoryPayLoad request, ServerCallContext context)
        {
            _logic.Delete(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }
    }
}
