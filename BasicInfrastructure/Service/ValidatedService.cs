using BasicInfrastructure.Persistence;
using BasicInfrastructure.Validator;

namespace BasicInfrastructure.Service
{
    public class ValidatedService<T> : BaseService<T>, IValidatedService<T> where T : Entity
    {
        protected IRepository<T> _repository;
        private readonly IAbstractValidator<T> _validator;

        public ValidatedService(IRepository<T> repository, IAbstractValidator<T> validator) : base(repository)
        {
            _repository = repository;
            _validator = validator;
        }

        public bool ValidateOnCreate(T entity)
        {
            _validator.ValidateAndThrowOnCreate(entity);
            return true;
        }

        public bool ValidateOnUpdate(T entity)
        {
            _validator.ValidateAndThrowOnUpdate(entity);
            return true;
        }

        public bool ValidateOnDelete(T entity)
        {
            _validator.ValidateAndThrowOnDelete(entity);
            return true;
        }

        protected override void OnBeforeAdd(T entity)
        {
            BeforeAdd?.Invoke(entity);
            _validator?.ValidateAndThrowOnCreate(entity);
        }

        protected override void OnBeforeUpdate(T entity)
        {
            BeforeUpdate?.Invoke(entity);
            _validator?.ValidateAndThrowOnUpdate(entity);
        }

        protected override void OnBeforeDelete(T entity)
        {
            BeforeDelete?.Invoke(entity);
            _validator?.ValidateAndThrowOnDelete(entity);
        }

        protected new event BeforeHandler BeforeAdd;
        protected new event BeforeHandler BeforeDelete;
        protected new event BeforeHandler BeforeUpdate;
    }
}