using System.ComponentModel;

namespace Enums
{
    public static class Enums
    {
        public static string GetDescription(this Enum genericEnum)
        {
            var genericEnumType = genericEnum.GetType();
            var memberInfo = genericEnumType.GetMember(genericEnum.ToString());
            if (memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Any())
                {
                    return ((DescriptionAttribute) attributes.ElementAt(0)).Description;
                }
            }

            return genericEnum.ToString();
        }
    }

    /// <summary>
    /// The users' types {Admin = 1, User = 2}
    /// </summary>
    [TypeConverter]
    public enum UserType : short
    {
        Admin = 1,
        User = 2
    }

    /// <summary>
    /// The products' categories {Lips = 1, Eyes = 2, Body = 3, Face = 4}
    /// </summary>
    [TypeConverter]
    public enum Category : short
    {
        Lips = 1,
        Eyes = 2,
        Body = 3,
        Face = 4,
    }

}