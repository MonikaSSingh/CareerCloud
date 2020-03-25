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
using static CareerCloud.gRPC.Protos.CompanyDescription;

namespace CareerCloud.gRPC.Services
{
    public class CompanyDescriptionService : CompanyDescriptionBase
    {
        private readonly CompanyDescriptionLogic _logic;

        public CompanyDescriptionService()
        {
            var repo = new EFGenericRepository<CompanyDescriptionPoco>();
            _logic = new CompanyDescriptionLogic(repo);
        }

        public override Task<CompanyDescriptionPayLoad> GetCompanyDescription(IdRequestDescription request, ServerCallContext context)
        {
            CompanyDescriptionPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyDescriptionPayLoad>(
                () => new CompanyDescriptionPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Company = poco.Company.ToString(),
                    LanguageId = poco.LanguageId,
                    CompanyName = poco.CompanyName,
                    CompanyDescription = poco.CompanyDescription
                });
        }

        public override Task<Empty> CreateCompanyDescription(CompanyDescriptionPayLoad request, ServerCallContext context)
        {
            _logic.Add(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        private CompanyDescriptionPoco[] PerformedMethod(CompanyDescriptionPayLoad request)
        {
            CompanyDescriptionPoco poco = new CompanyDescriptionPoco()
            {
                Id=Guid.Parse(request.Id),
                Company=Guid.Parse(request.Company),
                LanguageId=request.LanguageId,
                CompanyName=request.CompanyName,
                CompanyDescription=request.CompanyDescription
            };

            CompanyDescriptionPoco[] pocos = new CompanyDescriptionPoco[1];
            pocos[1] = poco;
            return pocos;
        }

        public override Task<Empty> UpdateCompanyDescription(CompanyDescriptionPayLoad request, ServerCallContext context)
        {
            _logic.Update(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteCompanyDescription(CompanyDescriptionPayLoad request, ServerCallContext context)
        {
            _logic.Delete(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }
    }
}
