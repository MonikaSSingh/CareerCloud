using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyLocationLogic:BaseLogic<CompanyLocationPoco>
    {
        public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> repository) : base(repository)
        {
        }

        protected override void Verify(CompanyLocationPoco[] pocos)
        {
            List<ValidationException> exception = new List<ValidationException>();
            foreach (CompanyLocationPoco poco in pocos)
            {
                if(string.IsNullOrEmpty(poco.CountryCode))
                {
                    exception.Add(new ValidationException(500, "CountryCode cannot be empty"));
                }
                if(string.IsNullOrEmpty(poco.Province))
                {
                    exception.Add(new ValidationException(501, "Province cannot be empty"));
                }
                if(string.IsNullOrEmpty(poco.Street))
                {
                    exception.Add(new ValidationException(502, "Street cannot be empty"));
                }
                if(string.IsNullOrEmpty(poco.City))
                {
                    exception.Add(new ValidationException(503, "City cannot be empty"));
                }
                if(string.IsNullOrEmpty(poco.PostalCode))
                {
                    exception.Add(new ValidationException(504, "PostalCode cannot be empty"));
                }
                if(exception.Count>0)
                {
                    throw new AggregateException(exception);
                }
            }               
        }

        public override void Add(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
