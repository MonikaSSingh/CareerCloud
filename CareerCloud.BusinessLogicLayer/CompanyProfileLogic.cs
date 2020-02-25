using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic:BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
        {
        }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exception = new List<ValidationException>();
            string[] requiredextention = new string[] { ".ca",".com",".biz"};
            foreach(CompanyProfilePoco poco in pocos)
            {
                if(string.IsNullOrEmpty(poco.CompanyWebsite))
                {
                    exception.Add(new ValidationException
                        (600, "Valid websites must end with the following extensions – .ca, .com, .biz"));
                }
                else if(!requiredextention.Any(t=>poco.CompanyWebsite.Contains(t)))
                {
                    exception.Add(new ValidationException
                        (600, "Valid websites must end with the following extensions – .ca, .com, .biz"));
                }
               if(string.IsNullOrEmpty(poco.ContactPhone))
                {
                    exception.Add(new ValidationException(601, "Must correspond to a valid phone number(e.g. 416 - 555 - 1234)"));
                }
               else
                {
                    string[] phonenumber = poco.ContactPhone.Split("-");
                    if (phonenumber.Length != 3)
                    {
                        exception.Add(new ValidationException(601, "Must correspond to a valid phone number(e.g. 416 - 555 - 1234)"));
                    }
                    else
                    {
                        if (phonenumber[0].Length != 3)
                        {
                            exception.Add(new ValidationException(601, "Must correspond to a valid phone number(e.g. 416 - 555 - 1234)"));
                        }
                        else if (phonenumber[1].Length != 3)
                        {
                            exception.Add(new ValidationException(601, "Must correspond to a valid phone number(e.g. 416 - 555 - 1234)"));
                        }
                        else if (phonenumber[2].Length != 4)
                        {
                            exception.Add(new ValidationException(601, "Must correspond to a valid phone number(e.g. 416 - 555 - 1234)"));
                        }
                    }
                }
               if(exception.Count>0)
                {
                    throw new AggregateException(exception);
                }
            }
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
    }
}
