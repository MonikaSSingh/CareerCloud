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
using static CareerCloud.gRPC.Protos.ApplicantProfile;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantProfileService : ApplicantProfileBase
    {
        private readonly ApplicantProfileLogic _logic;

        public ApplicantProfileService()
        {
            var repo = new EFGenericRepository<ApplicantProfilePoco>();
            _logic = new ApplicantProfileLogic(repo);
        }

        public override Task<ApplicantProfilePayLoad> GetApplicantProfile(IdRequestApplicantProfile request, ServerCallContext context)
        {
            ApplicantProfilePoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantProfilePayLoad>(
                               () => new ApplicantProfilePayLoad()
                               {
                                   Id = poco.Id.ToString(),
                                   Login = poco.Login.ToString(),
                                   CurrentSalary = (double)poco.CurrentSalary,
                                   CurrentRate = (double)poco.CurrentRate,
                                   Currency = poco.Currency,
                                   Country = poco.Country,
                                   Province = poco.Province,
                                   Street = poco.Street,
                                   City = poco.City,
                                   PostalCode = poco.PostalCode
                               });
        }

        public override Task<Empty> CreateApplicantProfile(ApplicantProfilePayLoad request, ServerCallContext context)
        {
            _logic.Add(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        private ApplicantProfilePoco[] PerformedMethod(ApplicantProfilePayLoad request)
        {
            ApplicantProfilePoco poco = new ApplicantProfilePoco()
            {
                Id=Guid.Parse(request.Id),
                Login=Guid.Parse(request.Login),
                CurrentSalary=(decimal)request.CurrentSalary,
                CurrentRate=(decimal)request.CurrentRate,
                Currency=request.Currency,
                Country=request.Country,
                Province=request.Province,
                Street=request.Street,
                City=request.City,
                PostalCode=request.PostalCode

            };

            ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[1];
            pocos[1] = poco;

            return pocos;
        }

        public override Task<Empty> UpdatedApplicantProfile(ApplicantProfilePayLoad request, ServerCallContext context)
        {
            _logic.Update(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteApplicantProfile(ApplicantProfilePayLoad request, ServerCallContext context)
        {
            _logic.Delete(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }
    }
}
