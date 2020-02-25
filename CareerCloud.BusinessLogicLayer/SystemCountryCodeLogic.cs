using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemCountryCodeLogic :SystemCountryCodePoco
    {
        protected IDataRepository<SystemCountryCodePoco> _repository;
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository)
        {
            _repository = repository;
        }
        protected void Verify(SystemCountryCodePoco[] pocos)
        {
             List<ValidationException> exception = new List<ValidationException>();
             foreach(SystemCountryCodePoco poco in pocos)
             {
                 if(string.IsNullOrEmpty(poco.Code))
                 {
                     exception.Add(new ValidationException(900, "Cannot be empty"));
                 }
                 if(string.IsNullOrEmpty(poco.Name))
                 {
                     exception.Add(new ValidationException(901, "Cannot be empty"));
                 }
                 if(exception.Count>0)
                 {
                     throw new AggregateException(exception);
                 }
             }
         }
        public void Add(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Add(pocos);
        }

        public void Update(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Update(pocos);
        }

        public void Delete(SystemCountryCodePoco[] pocos)
        {
            _repository.Remove(pocos);
        }

        public List<SystemCountryCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public SystemCountryCodePoco Get(string Id)
        {
            return _repository.GetSingle(a => a.Code == Id);
        }

    }
}
