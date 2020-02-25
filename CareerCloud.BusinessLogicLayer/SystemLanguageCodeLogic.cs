using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemLanguageCodeLogic :SystemLanguageCodePoco
    {
        protected IDataRepository<SystemLanguageCodePoco> _repository;

        public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository)
        {
            _repository = repository;
        }
    
        protected void Verify(SystemLanguageCodePoco[] pocos)
        {
            List<ValidationException> exception = new List<ValidationException>();
            foreach (SystemLanguageCodePoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.LanguageID))
                {
                    exception.Add(new ValidationException(1000, "Cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.Name))
                {
                    exception.Add(new ValidationException(1001, "Cannot be empty"));
                }
                if (string.IsNullOrEmpty(poco.NativeName))
                {
                    exception.Add(new ValidationException(1002, "Cannot be empty"));
                }
                if (exception.Count > 0)
                {
                    throw new AggregateException(exception);
                }
            }
        }

        public void Add(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Add(pocos);
        }

        public void Update(SystemLanguageCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Update(pocos);
        }
        public void Delete(SystemLanguageCodePoco[] pocos)
        {
            _repository.Remove(pocos);
        }

        public SystemLanguageCodePoco Get(string Id)
        {
            return _repository.GetSingle(a => a.LanguageID == Id);
        }

        public List<SystemLanguageCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }
    }
}
