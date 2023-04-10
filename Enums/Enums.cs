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
    /// The products' categories {Laptops = 1, Tablets = 2, Smartphones = 3}
    /// </summary>
    [TypeConverter]
    public enum Category : short
    {
        Laptops = 1,
        Tablets = 2,
        Smartphones = 3
    }

    /// <summary>
    /// The order by entity {Ascending = 1, Descending = 2}
    /// </summary>
    [TypeConverter]
    public enum OrderBy : short
    {
        Name = 1,
        Price = 2,
        Quantity = 3,
        Rate = 4
    }

    /// <summary>
    /// The order type {Ascending = 1, Descending = 2}
    /// </summary>
    [TypeConverter]
    public enum OrderType : short
    {
        Ascending = 1,
        Descending = 2,
    }

    /// <summary>
    /// The page size {Ten = 10, Twenty = 20, Thirty = 30, Fifty = 50}
    /// </summary>
    [TypeConverter]
    public enum PageSize : short
    {
        Ten = 10,
        Twenty = 20,
        Thirty = 30,
        Fifty = 50
    }

}