using BasicInfrastructure.Persistence;
using FluentValidation;

namespace BasicInfrastructure.Validator
{
    public class EntityValidator : AbstractValidator<Entity>
    {

        public override void CommonValidations()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
        }

        public override void OnCreate()
        {
            base.OnCreate();
            RuleFor(x => x.Id).Equal((int)0).WithMessage("{PropertyName} deve ser igual a 0 para criação");
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            RuleFor(x => x.Id).GreaterThan((int)0).WithMessage("{PropertyName} deve ser maior que 0 para edição");
        }

        public override void OnDelete()
        {
            base.OnDelete();
            RuleFor(x => x.Id).GreaterThan((int)0).WithMessage("{PropertyName} deve ser maior que 0 para exclusão");
        }
    }
}