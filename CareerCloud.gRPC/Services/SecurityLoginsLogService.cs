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
using static CareerCloud.gRPC.Protos.SecurityLoginsLog;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsLogService : SecurityLoginsLogBase
    {
        private readonly SecurityLoginsLogLogic _logic;

        public SecurityLoginsLogService()
        {
            var repo = new EFGenericRepository<SecurityLoginsLogPoco>();
            _logic = new SecurityLoginsLogLogic(repo);
        }

        public override Task<SecurityLoginsLogPayLoad> GetSecurityLoginsLog(IdRequestLoginsLog request, ServerCallContext context)
        {
            SecurityLoginsLogPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<SecurityLoginsLogPayLoad>(
                () => new SecurityLoginsLogPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Login = poco.Login.ToString(),
                    SourceIP = poco.SourceIP,
                    LogonDate = poco.LogonDate == null ? null : Timestamp.FromDateTime((DateTime)poco.LogonDate)
                    //string Id = 1;
                    //string Login = 2;
                    //string SourceIP = 3;
                    //google.protobuf.Timestamp LogonDate = 4;
                    //bool IsSuccesful = 5;
                }); 
        }

        public override Task<Empty> CreateSecurityLoginsLog(SecurityLoginsLogPayLoad request, ServerCallContext context)
        {
            _logic.Add(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        private SecurityLoginsLogPoco[] PerformedMethod(SecurityLoginsLogPayLoad request)
        {
            SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco()
            {
                Id = Guid.Parse(request.Id),
                Login = Guid.Parse(request.Login),
                SourceIP = request.SourceIP,
                LogonDate = request.LogonDate.ToDateTime()
            };
            SecurityLoginsLogPoco[] pocos = new SecurityLoginsLogPoco[1];
            pocos[1] = poco;

            return pocos;

        }

        public override Task<Empty> UpdateSecurityLoginsLog(SecurityLoginsLogPayLoad request, ServerCallContext context)
        {
            _logic.Update(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteSecurityLoginsLog(SecurityLoginsLogPayLoad request, ServerCallContext context)
        {
            _logic.Delete(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }
    }
}
