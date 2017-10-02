using FluentValidation;
using FluentValidation.Results;

namespace BasicInfrastructure.Validator
{
    public abstract class AbstractValidator<T> : FluentValidation.AbstractValidator<T>, IAbstractValidator<T>
    {
        public void ValidateAndThrowOnCreate(T t)
        {
            OnCreate();
            ValidateAndThrow(t);
        }

        public void ValidateAndThrowOnUpdate(T t)
        {
            OnUpdate();
            ValidateAndThrow(t);
        }

        public void ValidateAndThrowOnDelete(T t)
        {
            OnDelete();
            ValidateAndThrow(t);
        }

        private void ValidateAndThrow(T st)
        {
            var r = this.Validate(new ValidationContext<T>(st));
            if (!r.IsValid)
                throw new ValidationException(r.Errors);
        }

        public override ValidationResult Validate(ValidationContext<T> st)
        {
            CommonValidations();
            return base.Validate(st);
        }

        public virtual void OnCreate()
        {
            CommonValidations();
        }
        public virtual void OnUpdate()
        {
            CommonValidations();
        }
        public virtual void OnDelete()
        {
            CommonValidations();
        }

        public abstract void CommonValidations();

    }
}