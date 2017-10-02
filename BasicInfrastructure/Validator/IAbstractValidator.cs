namespace BasicInfrastructure.Validator
{
    public interface IAbstractValidator<T>
    {
        void CommonValidations();
        void OnCreate();
        void OnDelete();
        void OnUpdate();
        void ValidateAndThrowOnCreate(T t);
        void ValidateAndThrowOnUpdate(T t);
        void ValidateAndThrowOnDelete(T t);
    }
}