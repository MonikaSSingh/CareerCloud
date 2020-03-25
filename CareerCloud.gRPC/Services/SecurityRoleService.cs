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
using static CareerCloud.gRPC.Protos.SecurityRole;

namespace CareerCloud.gRPC.Services
{
    public class SecurityRoleService : SecurityRoleBase
    {
        private readonly SecurityRoleLogic _logic;

        public SecurityRoleService()
        {
            var repo = new EFGenericRepository<SecurityRolePoco>();
            _logic = new SecurityRoleLogic(repo);
        }

        public override Task<SecurityRolePayLoad> GetSecurityRole(IdRequestRole request, ServerCallContext context)
        {
            SecurityRolePoco poco = _logic.Get(Guid.Parse(request.Id));

            return new Task<SecurityRolePayLoad>(
                () => new SecurityRolePayLoad()
                {
                    Id = poco.Id.ToString(),
                    Role = poco.Role,
                    IsInactive = poco.IsInactive
                }
                );
        }

        public override Task<Empty> CreateSecurityRole(SecurityRolePayLoad request, ServerCallContext context)
        {
            _logic.Add(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        private SecurityRolePoco[] PerformedMethod(SecurityRolePayLoad request)
        {
            SecurityRolePoco poco = new SecurityRolePoco()
            {
                Id = Guid.Parse(request.Id),
                Role = request.Role,
                IsInactive = request.IsInactive,
            };
            SecurityRolePoco[] pocos = new SecurityRolePoco[1];
            pocos[1] = poco;

            return pocos;
        }

        public override Task<Empty> UpdateSecurityRole(SecurityRolePayLoad request, ServerCallContext context)
        {
            _logic.Update(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteSecurityRole(SecurityRolePayLoad request, ServerCallContext context)
        {
            _logic.Delete(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }
    }
}
