using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobEducationLogic:BaseLogic<CompanyJobEducationPoco>
    {
        public CompanyJobEducationLogic(IDataRepository<CompanyJobEducationPoco> repository) : base(repository)
        {
        }
        protected override void Verify(CompanyJobEducationPoco[] pocos)
        {
            List<ValidationException> exception = new List<ValidationException>();
            foreach(CompanyJobEducationPoco poco in pocos)
            {
                if(string.IsNullOrEmpty(poco.Major))
                {
                    exception.Add(new ValidationException(200, "Major must be at least 2 characters"));
                }
                else if(poco.Major.Length<=2)
                {
                    exception.Add(new ValidationException(200, "Major must be at least 2 characters"));
                }
                if (poco.Importance < 0)
                {
                    exception.Add(new ValidationException(201, "Importance cannot be less than 0"));
                }
                if(exception.Count >0)
                {
                    throw new AggregateException(exception);
                }
            }           
        }
        public override void Update(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        public override void Add(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
    }
}
