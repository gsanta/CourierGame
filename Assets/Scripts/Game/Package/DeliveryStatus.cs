using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public enum DeliveryStatus
{
    [Description("Free")]
    UNASSIGNED,
    [Description("Reserved")]
    RESERVED,
    [Description("Assigned")]
    ASSIGNED,
    [Description("Delivered")]
    DELIVERED,
}

public static class DeliveryStatusExtensions
{
    public static string GetDescription(this DeliveryStatus value)
    {
        Type type = value.GetType();
        string name = Enum.GetName(type, value);
        if (name != null)
        {
            FieldInfo field = type.GetField(name);
            if (field != null)
            {
                DescriptionAttribute attr =
                       Attribute.GetCustomAttribute(field,
                         typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attr != null)
                {
                    return attr.Description;
                }
            }
        }
        return null;
    }
}

