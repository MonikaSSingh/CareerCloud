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
using static CareerCloud.gRPC.Protos.SecurityLogin;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginService : SecurityLoginBase
    {
        private readonly SecurityLoginLogic _logic;

        public SecurityLoginService()
        {
            var repo = new EFGenericRepository<SecurityLoginPoco>();
            _logic = new SecurityLoginLogic(repo);
        }

        public override Task<SecurityLoginPayLoad> GetSecurityLogin(IdRequestSecurityLogin request, ServerCallContext context)
        {
            SecurityLoginPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<SecurityLoginPayLoad>(
                () => new SecurityLoginPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Login = poco.Login,
                    Password = poco.Password,
                    Created = poco.Created == null ? null : Timestamp.FromDateTime((DateTime)poco.Created),
                    PasswordUpdate = poco.PasswordUpdate == null ? null : Timestamp.FromDateTime((DateTime)poco.PasswordUpdate),
                    AgreementAccepted = poco.AgreementAccepted == null ? null : Timestamp.FromDateTime((DateTime)poco.AgreementAccepted),
                    IsLocked = poco.IsLocked,
                    IsInactive = poco.IsInactive,
                    EmailAddress = poco.EmailAddress,
                    PhoneNumber = poco.PhoneNumber,
                    FullName = poco.FullName,
                    ForceChangePassword = poco.ForceChangePassword,
                    PrefferredLanguage = poco.PrefferredLanguage
                });
        }

        public override Task<Empty> CreateSecurityLogin(SecurityLoginPayLoad request, ServerCallContext context)
        {
            _logic.Add(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        private SecurityLoginPoco[] PerformedMethod(SecurityLoginPayLoad request)
        {
            SecurityLoginPoco poco = new SecurityLoginPoco()
            {
                Id = Guid.Parse(request.Id),
                Login = request.Login,
                Password = request.Password,
                Created = request.Created.ToDateTime(),
                PasswordUpdate = request.PasswordUpdate.ToDateTime(),
                AgreementAccepted = request.AgreementAccepted.ToDateTime(),
                IsLocked = request.IsLocked,
                IsInactive = request.IsInactive,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                FullName = request.FullName,
                ForceChangePassword = request.ForceChangePassword,
                PrefferredLanguage = request.PrefferredLanguage
            };

            SecurityLoginPoco[] pocos = new SecurityLoginPoco[1];
            pocos[1] = poco;

            return pocos;
        }

        public override Task<Empty> UpdateSecurityLogin(SecurityLoginPayLoad request, ServerCallContext context)
        {
            _logic.Update(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteSecurityLogin(SecurityLoginPayLoad request, ServerCallContext context)
        {
            _logic.Delete(PerformedMethod(request));
            return new Task<Empty>(() => new Empty());
        }
    }
}
