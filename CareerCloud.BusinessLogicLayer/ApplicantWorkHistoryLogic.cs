using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantWorkHistoryLogic:BaseLogic<ApplicantWorkHistoryPoco>
    {
        public ApplicantWorkHistoryLogic(IDataRepository<ApplicantWorkHistoryPoco> repository) : base(repository)
        {
        }

        protected override void Verify(ApplicantWorkHistoryPoco[] pocos)
        {
            List<ValidationException> exception = new List<ValidationException>();
            foreach(ApplicantWorkHistoryPoco poco in pocos)
            {
                if(string.IsNullOrEmpty(poco.CompanyName))
                {
                    exception.Add(new ValidationException(105, "Must be greater then 2 characters"));
                }
                else if(poco.CompanyName.Length <= 2)
                {
                    exception.Add(new ValidationException(105, "Must be greater then 2 characters"));
                }
                if(exception.Count>0)
                {
                    throw new AggregateException(exception);
                }
            }
        }

        public override void Add(ApplicantWorkHistoryPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantWorkHistoryPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
