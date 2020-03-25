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
using static CareerCloud.gRPC.Protos.CompanyProfile;

namespace CareerCloud.gRPC.Services
{
    public class CompanyProfileService : CompanyProfileBase
    {
        private readonly CompanyProfileLogic _logic;

        public CompanyProfileService()
        {
            var repo = new EFGenericRepository<CompanyProfilePoco>();
            _logic = new CompanyProfileLogic(repo);
        }

        public override Task<CompanyProfilePayLoad> GetCompanyProfile(IdRequestCompanyProfile request, ServerCallContext context)
        {
            CompanyProfilePoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyProfilePayLoad>(
                () => new CompanyProfilePayLoad()
                {
                    Id = poco.Id.ToString(),
                    RegistrationDate = poco.RegistrationDate == null ? null : Timestamp.FromDateTime((DateTime)poco.RegistrationDate),
                    CompanyWebsite = poco.CompanyWebsite,
                    ContactPhone = poco.ContactPhone,
                    ContactName = poco.ContactName,
                    CompanyLogo = Google.Protobuf.ByteString.CopyFrom(poco.CompanyLogo)
                });
        }

        public override Task<Empty> CreateCompanyProfile(CompanyProfilePayLoad request, ServerCallContext context)
        {
            _logic.Add(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        private CompanyProfilePoco[] PerformedMethod(CompanyProfilePayLoad request)
        {
            CompanyProfilePoco poco = new CompanyProfilePoco()
            {
                Id = Guid.Parse(request.Id),
                RegistrationDate = request.RegistrationDate.ToDateTime(),
                CompanyWebsite = request.CompanyWebsite,
                ContactName = request.ContactName,
                ContactPhone = request.ContactPhone,
                CompanyLogo = request.CompanyLogo.ToByteArray()

            };
            CompanyProfilePoco[] pocos = new CompanyProfilePoco[1];
            pocos[1] = poco;

            return pocos;
        }

        public override Task<Empty> UpdateCompanyProfile(CompanyProfilePayLoad request, ServerCallContext context)
        {
            _logic.Update(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteCompanyProfile(CompanyProfilePayLoad request, ServerCallContext context)
        {
            _logic.Delete(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }
    }
}
