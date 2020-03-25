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
using static CareerCloud.gRPC.Protos.SystemLanguageCode;

namespace CareerCloud.gRPC.Services
{
    public class SystemLanguageCodeService : SystemLanguageCodeBase
    {
        private readonly SystemLanguageCodeLogic _logic;

        public SystemLanguageCodeService()
        {
            var repo = new EFGenericRepository<SystemLanguageCodePoco>();
            _logic = new SystemLanguageCodeLogic(repo);
        }

        public override Task<SystemLanguageCodePayLoad> GetSystemLanguageCode(IdRequestLanguageCode request, ServerCallContext context)
        {
            SystemLanguageCodePoco poco = _logic.Get(request.LanguageId);
            return new Task<SystemLanguageCodePayLoad>(
                () => new SystemLanguageCodePayLoad()
                {
                    LanguageID = poco.LanguageID,
                    Name = poco.Name,
                    NativeName = poco.NativeName
                });
        }

        public override Task<Empty> CreateSystemLanguageCode(SystemLanguageCodePayLoad request, ServerCallContext context)
        {
            _logic.Add(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        private SystemLanguageCodePoco[] PerformedMethod(SystemLanguageCodePayLoad request)
        {
            SystemLanguageCodePoco poco = new SystemLanguageCodePoco()
            {
                LanguageID=request.LanguageID,
                Name=request.Name,
                NativeName=request.NativeName
            };

            SystemLanguageCodePoco[] pocos = new SystemLanguageCodePoco[1];
            pocos[1] = poco;
            return pocos;

        }

        public override Task<Empty> UpdateSystemLanguageCode(SystemLanguageCodePayLoad request, ServerCallContext context)
        {
            _logic.Update(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteSystemLanguageCode(SystemLanguageCodePayLoad request, ServerCallContext context)
        {
            _logic.Delete(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }
    }
}
