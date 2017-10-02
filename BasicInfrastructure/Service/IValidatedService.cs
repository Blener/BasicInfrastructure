using BasicInfrastructure.Persistence;

namespace BasicInfrastructure.Service
{
    public interface IValidatedService<T> where T : Entity
    {
        bool ValidateOnCreate(T entity);
        bool ValidateOnDelete(T entity);
        bool ValidateOnUpdate(T entity);
    }
}