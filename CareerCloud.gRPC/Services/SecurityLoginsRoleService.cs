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
using static CareerCloud.gRPC.Protos.SecurityLoginsRole;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsRoleService : SecurityLoginsRoleBase
    {
        private readonly SecurityLoginsRoleLogic _logic;

        public SecurityLoginsRoleService()
        {
            var repo = new EFGenericRepository<SecurityLoginsRolePoco>();
            _logic = new SecurityLoginsRoleLogic(repo);
        }

        public override Task<SecurityLoginsRolePayLoad> GetLoginsRole(IdRequestSecurityLoginsRole request, ServerCallContext context)
        {
            SecurityLoginsRolePoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<SecurityLoginsRolePayLoad>(
                () => new SecurityLoginsRolePayLoad()
                {
                    Id = poco.Id.ToString(),
                    Login = poco.Login.ToString(),
                    Role = poco.Role.ToString()
                });
        }

        public override Task<Empty> CreateLoginsRole(SecurityLoginsRolePayLoad request, ServerCallContext context)
        {
            _logic.Add(PerformRequest(request));
            return new Task<Empty>(() => new Empty());
        }

        private SecurityLoginsRolePoco[] PerformRequest(SecurityLoginsRolePayLoad request)
        {
            SecurityLoginsRolePoco poco = new SecurityLoginsRolePoco()
            {
                Id = Guid.Parse(request.Id),
                Login = Guid.Parse(request.Login),
                Role = Guid.Parse(request.Role)
            };

            SecurityLoginsRolePoco[] pocos = new SecurityLoginsRolePoco[1];
            pocos[1] = poco;
            return pocos;
        }

        public override Task<Empty> UpdateLoginsRole(SecurityLoginsRolePayLoad request, ServerCallContext context)
        {
            _logic.Update(PerformRequest(request));
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteLoginsRole(SecurityLoginsRolePayLoad request, ServerCallContext context)
        {
            _logic.Delete(PerformRequest(request));
            return new Task<Empty>(() => new Empty());
        }
    }
}
