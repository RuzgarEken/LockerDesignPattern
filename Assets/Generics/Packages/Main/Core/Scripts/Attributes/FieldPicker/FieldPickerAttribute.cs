using System;
using System.Linq;

namespace Generics.Attributes
{

    [AttributeUsage(AttributeTargets.Field)]
    public class FieldPickerAttribute : Attribute
    {
        public Type Type;
        public string[] Fields;

        public FieldPickerAttribute(Type type)
        {
            Type = type;
            Fields = Type.GetFields().Select(f => f.Name).ToArray();
        }

        public int GetIndex(string current)
        {
            if (string.IsNullOrEmpty(current))
            {
                return 0;
            }

            for (int i = 0; i < Fields.Length; i++)
            {
                if(current == Fields[i])
                {
                    return i;
                }
            }

            return 0;
        }

    }

}