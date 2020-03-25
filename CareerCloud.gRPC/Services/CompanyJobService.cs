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
using static CareerCloud.gRPC.Protos.CompanyJob;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobService : CompanyJobBase
    {
        private readonly CompanyJobLogic _logic;

        public CompanyJobService()
        {
            var repo = new EFGenericRepository<CompanyJobPoco>();
            _logic = new CompanyJobLogic(repo);
        }

        public override Task<CompanyJobPayLoad> GetCompanyJob(IdRequestCompanyJob request, ServerCallContext context)
        {
            CompanyJobPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyJobPayLoad>(
                () => new CompanyJobPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Company = poco.Company.ToString(),
                    ProfileCreated = poco.ProfileCreated == null ? null : Timestamp.FromDateTime((DateTime)poco.ProfileCreated),
                    IsInactive = poco.IsInactive,
                    IsCompanyHidden = poco.IsCompanyHidden
                });
        }

        public override Task<Empty> CreateCompanyJob(CompanyJobPayLoad request, ServerCallContext context)
        {
            _logic.Add(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        private CompanyJobPoco[] PerformedMethod(CompanyJobPayLoad request)
        {
            CompanyJobPoco poco = new CompanyJobPoco()
            {
                Id=Guid.Parse(request.Id),
                Company=Guid.Parse(request.Company),
                ProfileCreated=request.ProfileCreated.ToDateTime(),
                IsInactive=request.IsInactive,
                IsCompanyHidden=request.IsCompanyHidden
            };

            CompanyJobPoco[] pocos = new CompanyJobPoco[1];
            pocos[1] = poco;
            return pocos;
        }

        public override Task<Empty> UpdateCompanyJob(CompanyJobPayLoad request, ServerCallContext context)
        {
            _logic.Update(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteCompanyJob(CompanyJobPayLoad request, ServerCallContext context)
        {
            _logic.Delete(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }
    }
}
