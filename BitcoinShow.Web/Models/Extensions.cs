using System.ComponentModel;
using System.Linq;

namespace BitcoinShow.Web.Models
{
    /// <summary>
    /// https://stackoverflow.com/questions/34557574/how-to-create-a-table-corresponding-to-enum-in-ef6-code-first
    /// </summary>
    public static class Extensions
    {
        public static string GetEnumDescription<TEnum>(this TEnum item)
            => item.GetType()
                .GetField(item.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault()?.Description ?? string.Empty;   
    }
}