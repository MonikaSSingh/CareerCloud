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
using static CareerCloud.gRPC.Protos.ApplicantJobApplication;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantJobApplicationService : ApplicantJobApplicationBase
    {
        private readonly ApplicantJobApplicationLogic _logic;

        public ApplicantJobApplicationService()
        {
            var repo = new EFGenericRepository<ApplicantJobApplicationPoco>();
            _logic = new ApplicantJobApplicationLogic(repo);
        }

        public override Task<ApplicantJobApplicationPayLoad> GetApplicantJobApplication(IdRequestJobApplication request, ServerCallContext context)
        {

            ApplicantJobApplicationPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantJobApplicationPayLoad>(

                () => new ApplicantJobApplicationPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Applicant = poco.Applicant.ToString(),
                    Job = poco.Job.ToString(),
                    ApplicationDate = poco.ApplicationDate == null ? null : Timestamp.FromDateTime((DateTime)poco.ApplicationDate)
                }
                );
        }

        public override Task<Empty> CreateApplicantJobApplication(ApplicantJobApplicationPayLoad request, ServerCallContext context)
        {
            _logic.Add(PeroformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        private ApplicantJobApplicationPoco[] PeroformedMethod(ApplicantJobApplicationPayLoad request)
        {
            ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco()
            {
                Id=Guid.Parse(request.Id),
                Applicant=Guid.Parse(request.Applicant),
                Job =Guid.Parse(request.Job),
                ApplicationDate=request.ApplicationDate.ToDateTime()
            };

            ApplicantJobApplicationPoco[] pocos = new ApplicantJobApplicationPoco[1];
            pocos[1] = poco;

            return pocos;
        }

        public override Task<Empty> UpdateApplicantJobApplication(ApplicantJobApplicationPayLoad request, ServerCallContext context)
        {
            _logic.Update(PeroformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteApplicantJobApplication(ApplicantJobApplicationPayLoad request, ServerCallContext context)
        {
            _logic.Delete(PeroformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }
    }
}
