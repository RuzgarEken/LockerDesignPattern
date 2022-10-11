using System;

namespace Generics.Attributes
{

    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentOptionAttribute : Attribute
    {
        public Type BaseComponent;

        public ComponentOptionAttribute(Type optionFor)
        {
            BaseComponent = optionFor;
        }

    }

}