using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantJobApplicationLogic : BaseLogic<ApplicantJobApplicationPoco>
    {
        public ApplicantJobApplicationLogic(IDataRepository<ApplicantJobApplicationPoco> repository) : base(repository)
        {
        }

        protected override void Verify(ApplicantJobApplicationPoco[] pocos)
        {
            List<ValidationException> exception = new List<ValidationException>();
            foreach(ApplicantJobApplicationPoco poco in pocos)
            {
                if(poco.ApplicationDate > DateTime.Now)
                {
                    exception.Add(new ValidationException
                        (110, "ApplicationDate cannot be greater than today"));
                }
            }
            if(exception.Count>0)
            {
                throw new AggregateException(exception);
            }
        }

        public override void Add(ApplicantJobApplicationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantJobApplicationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
