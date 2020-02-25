using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantProfileLogic:BaseLogic<ApplicantProfilePoco>
    {
        public ApplicantProfileLogic(IDataRepository<ApplicantProfilePoco> repository) : base(repository)
        {
        }

        protected override void Verify(ApplicantProfilePoco[] pocos)
        {
            List<ValidationException> exception = new List<ValidationException>();
            foreach (ApplicantProfilePoco poco in pocos)
            {
                if(poco.CurrentSalary < 0)
                {
                    exception.Add(new ValidationException(111, "CurrentSalary cannot be negative"));
                }
                if(poco.CurrentRate < 0)
                {
                    exception.Add(new ValidationException(112, "CurrentRate cannot be negative"));
                }

                if(exception.Count >0 )
                {
                    throw new AggregateException(exception);
                }
            }
        }

        public override void Update(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        public override void Add(ApplicantProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
    }
}
