using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyDescriptionLogic:BaseLogic<CompanyDescriptionPoco>
    {
        public CompanyDescriptionLogic(IDataRepository<CompanyDescriptionPoco> repository) : base(repository)
        {
        }

        protected override void Verify(CompanyDescriptionPoco[] pocos)
        {
            List<ValidationException> exception = new List<ValidationException>();
            foreach(CompanyDescriptionPoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CompanyDescription))
                {
                    exception.Add(new ValidationException
                        (107, "CompanyDescription must be greater than 2 characters"));
                }
                else if(poco.CompanyDescription.Length <= 2)
                {
                    exception.Add(new ValidationException
                        (107, "CompanyDescription must be greater than 2 characters"));
                }
                if(string.IsNullOrEmpty(poco.CompanyName))
                {
                    exception.Add(new ValidationException
                        (106, "CompanyName must be greater than 2 characters"));
                }
                else if (poco.CompanyName.Length <= 2)
                {
                    exception.Add(new ValidationException
                        (106, "CompanyName must be greater than 2 characters"));
                }
                if(exception.Count>0)
                {
                    throw new AggregateException(exception);
                }
            }            
        }

        public override void Update(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        public override void Add(CompanyDescriptionPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
    }
}
