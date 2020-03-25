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
using static CareerCloud.gRPC.Protos.CompanyLocation;

namespace CareerCloud.gRPC.Services
{
    public class CompanyLocationService : CompanyLocationBase
    {
        private readonly CompanyLocationLogic _logic;

        public CompanyLocationService()
        {
            var repo = new EFGenericRepository<CompanyLocationPoco>();
            _logic = new CompanyLocationLogic(repo);
        }

        public override Task<CompanyLocationPayLoad> GetCompanyLocation(IdRequestLocation request, ServerCallContext context)
        {
            CompanyLocationPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyLocationPayLoad>(
                () => new CompanyLocationPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Company = poco.Company.ToString(),
                    CountryCode = poco.CountryCode,
                    Province = poco.Province,
                    Street = poco.Street,
                    City = poco.City,
                    PostalCode = poco.PostalCode

                });
        }

        public override Task<Empty> CreateCompanyLocation(CompanyLocationPayLoad request, ServerCallContext context)
        {
            _logic.Add(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        private CompanyLocationPoco[] PerformedMethod(CompanyLocationPayLoad request)
        {
            CompanyLocationPoco poco = new CompanyLocationPoco()
            {
                Id=Guid.Parse(request.Id),
                Company=Guid.Parse(request.Company),
                CountryCode=request.CountryCode,
                Province=request.Province,
                Street=request.Street,
                City=request.City,
                PostalCode=request.PostalCode
            };

            CompanyLocationPoco[] pocos = new CompanyLocationPoco[1];
            pocos[1] = poco;

            return pocos;
        }

        public override Task<Empty> UpdateCompanyLocation(CompanyLocationPayLoad request, ServerCallContext context)
        {
            _logic.Update(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteCompanyLocation(CompanyLocationPayLoad request, ServerCallContext context)
        {
            _logic.Delete(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }
    }
}
