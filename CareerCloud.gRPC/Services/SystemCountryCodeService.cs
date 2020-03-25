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
using static CareerCloud.gRPC.Protos.SystemCountryCode;

namespace CareerCloud.gRPC.Services
{
    public class SystemCountryCodeService : SystemCountryCodeBase
    {
        private readonly SystemCountryCodeLogic _logic;

        public SystemCountryCodeService()
        {
            var repo = new EFGenericRepository<SystemCountryCodePoco>();
            _logic = new SystemCountryCodeLogic(repo);
        }


        public override Task<SystemCountryCodePayLoad> GetCountryCode(IdRequestCountryCode request, ServerCallContext context)
        {
            SystemCountryCodePoco poco = _logic.Get(request.Code);
            return new Task<SystemCountryCodePayLoad>(
                () => new SystemCountryCodePayLoad()
                {
                    Code = poco.Code,
                    Name = poco.Name
                });
        }

        public override Task<Empty> CreateCountryCode(SystemCountryCodePayLoad request, ServerCallContext context)
        {
            _logic.Add(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        private SystemCountryCodePoco[] PerformedMethod(SystemCountryCodePayLoad request)
        {
            SystemCountryCodePoco poco = new SystemCountryCodePoco()
            {
                Code=request.Code,
                Name=request.Name
            };

            SystemCountryCodePoco[] pocos = new SystemCountryCodePoco[1];
            pocos[1] = poco;

            return pocos;
        }

        public override Task<Empty> UpdateCountryCode(SystemCountryCodePayLoad request, ServerCallContext context)
        {
            _logic.Update(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteCountryCode(SystemCountryCodePayLoad request, ServerCallContext context)
        {
            _logic.Delete(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }
    }
}
