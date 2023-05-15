using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
public static class ExtensionMethods {
    public static string GetDescription(this Enum e)
        {
            Type eType = e.GetType();
            string? eName = Enum.GetName(eType, e);
            if (eName != null)
            {
                FieldInfo? fieldInfo = eType.GetField(eName);
                if (fieldInfo != null)
                {
                    DescriptionAttribute? descriptionAttribute =
                           Attribute.GetCustomAttribute(fieldInfo,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (descriptionAttribute != null)
                    {
                        return descriptionAttribute.Description;
                    }
                }
            }

            return string.Empty;
        }
}