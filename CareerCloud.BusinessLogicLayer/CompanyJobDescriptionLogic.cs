using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobDescriptionLogic:BaseLogic<CompanyJobDescriptionPoco>
    {
        public CompanyJobDescriptionLogic(IDataRepository<CompanyJobDescriptionPoco> repository) : base(repository)
        {
        }

        protected override void Verify(CompanyJobDescriptionPoco[] pocos)
        {
            List<ValidationException> exception = new List<ValidationException>();
            foreach(CompanyJobDescriptionPoco poco in pocos)
            {
                if(string.IsNullOrEmpty(poco.JobName))
                {
                    exception.Add(new ValidationException(300, "JobName cannot be empty"));
                }
                if(string.IsNullOrEmpty(poco.JobDescriptions))
                {
                    exception.Add(new ValidationException(301, "JobDescriptions cannot be empty"));
                }
                if(exception.Count>0)
                {
                    throw new AggregateException(exception); 
                }
            }
        }

        public override void Add(CompanyJobDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyJobDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
