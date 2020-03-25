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
using static CareerCloud.gRPC.Protos.CompanyJobEducation;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobEducationService : CompanyJobEducationBase
    {
        private readonly CompanyJobEducationLogic _logic;

        public CompanyJobEducationService()
        {
            var repo = new EFGenericRepository<CompanyJobEducationPoco>();
            _logic = new CompanyJobEducationLogic(repo);
        }

        public override Task<CompanyJobEducationPayLoad> GetCompanyJobEducation(IdRequestJobEducation request, ServerCallContext context)
        {
            CompanyJobEducationPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyJobEducationPayLoad>(
                () => new CompanyJobEducationPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Job = poco.Job.ToString(),
                    Major = poco.Major,
                    Importance = poco.Importance
                });
        }

        public override Task<Empty> CreateCompanyJobEducation(CompanyJobEducationPayLoad request, ServerCallContext context)
        {
            _logic.Add(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        private CompanyJobEducationPoco[] PerformedMethod(CompanyJobEducationPayLoad request)
        {
            CompanyJobEducationPoco poco = new CompanyJobEducationPoco()
            {
                Id = Guid.Parse(request.Id),
                Job = Guid.Parse(request.Job),
                Major = request.Major,
                Importance = (short)request.Importance
            };

            CompanyJobEducationPoco[] pocos = new CompanyJobEducationPoco[1];
            pocos[1] = poco;

            return pocos;
        }

        public override Task<Empty> UpdateCompanyJobEducation(CompanyJobEducationPayLoad request, ServerCallContext context)
        {
            _logic.Update(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteCompanyJobEducation(CompanyJobEducationPayLoad request, ServerCallContext context)
        {
            _logic.Delete(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }
    }
}
