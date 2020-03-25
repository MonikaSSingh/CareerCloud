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
using static CareerCloud.gRPC.Protos.CompanyJobDescription;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobDescriptionService : CompanyJobDescriptionBase
    {
        private readonly CompanyJobDescriptionLogic _logic;

        public CompanyJobDescriptionService()
        {
            var repo = new EFGenericRepository<CompanyJobDescriptionPoco>();
            _logic = new CompanyJobDescriptionLogic(repo);
        }

        public override Task<CompanyJobDescriptionPayLoad> GetCompanyJobDescription(IdRequestJobDescription request, ServerCallContext context)
        {
            CompanyJobDescriptionPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyJobDescriptionPayLoad>(
                () => new CompanyJobDescriptionPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Job = poco.Job.ToString(),
                    JobName = poco.JobName,
                    JobDescriptions = poco.JobDescriptions
                });
        }

        public override Task<Empty> CreateCompanyJobDescription(CompanyJobDescriptionPayLoad request, ServerCallContext context)
        {
            _logic.Add(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        private CompanyJobDescriptionPoco[] PerformedMethod(CompanyJobDescriptionPayLoad request)
        {
            CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco()
            {
                Id=Guid.Parse(request.Id),
                Job=Guid.Parse(request.Job),
                JobName=request.JobName,
                JobDescriptions=request.JobDescriptions
            };

            CompanyJobDescriptionPoco[] pocos = new CompanyJobDescriptionPoco[1];
            pocos[1] = poco;

            return pocos;
        }

        public override Task<Empty> UpdateCompanyJobDescription(CompanyJobDescriptionPayLoad request, ServerCallContext context)
        {
            _logic.Update(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteCompanyJobDescription(CompanyJobDescriptionPayLoad request, ServerCallContext context)
        {
            _logic.Delete(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }
    }

}
