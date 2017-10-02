using System;
using System.ComponentModel;
using System.Reflection;

namespace BasicInfrastructureExtensions.Extensions
{
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        private Type _nameResourceType;

        private PropertyInfo _nameProperty;

        public LocalizedDescriptionAttribute(string descriptionKey)
            : base(descriptionKey)
        {
        }

        public Type NameResourceType
        {
            get
            {
                return _nameResourceType;
            }
            set
            {
                _nameResourceType = value;
                _nameProperty = _nameResourceType.GetProperty(base.Description, BindingFlags.Static | BindingFlags.Public);
            }
        }

        public override string Description
        {
            get
            {
                if (_nameProperty == null)
                    return base.Description;

                return (string)_nameProperty.GetValue(_nameProperty.DeclaringType, null);
            }
        }
    }
}
