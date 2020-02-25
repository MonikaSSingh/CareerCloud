using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic:BaseLogic<ApplicantEducationPoco>
    {
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repository) : base(repository)
        {
        }

        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            List<ValidationException> exception = new List<ValidationException>();
            foreach(ApplicantEducationPoco poco in pocos)
            {
                if(string.IsNullOrEmpty(poco.Major))
                {
                    exception.Add(new ValidationException
                        (107, "Cannot be empty or less than 3 characters"));
                }
                else if(poco.Major.Length <3)
                {
                    exception.Add(new ValidationException
                       (107, "Cannot be empty or less than 3 characters"));
                }
                if(poco.StartDate>DateTime.Now)
                {
                    exception.Add(new ValidationException
                       (108, "Cannot be greater than today"));
                }
                if(poco.CompletionDate <poco.StartDate)
                {
                    exception.Add(new ValidationException
                       (109, "CompletionDate cannot be earlier than StartDate"));
                }
            }

            if(exception.Count > 0)
            {
                throw new AggregateException(exception);
            }
        }

        public override void Add(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
