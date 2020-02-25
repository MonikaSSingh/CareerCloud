using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic:BaseLogic<ApplicantSkillPoco>
    {
        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base(repository)
        {
        }

        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            List<ValidationException> exception = new List<ValidationException>();
            foreach(ApplicantSkillPoco poco in pocos)
            {
                if(poco.StartMonth > 12)
                {
                    exception.Add(new ValidationException
                       (101, "Cannot be greater than 12"));
                }
                if(poco.EndMonth > 12)
                {
                    exception.Add(new ValidationException
                        (102, "Can not be greater than 12"));
                }
                if(poco.StartYear < 1900)
                {
                    exception.Add(new ValidationException
                       (103, "Cannot be less then 1900"));
                }
                if(poco.EndYear < poco.StartYear)
                {
                    exception.Add(new ValidationException
                       (104, "Cannot be less then StartYear"));
                }
                if(exception.Count>0)
                {
                    throw new AggregateException(exception);
                }
            }
         }
        public override void Add(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
