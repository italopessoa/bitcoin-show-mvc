using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BitcoinShow.Web.Models
{
    /// <summary>
    /// https://stackoverflow.com/questions/34557574/how-to-create-a-table-corresponding-to-enum-in-ef6-code-first
    /// </summary>
    public static class Extensions
    {
        public static string GetEnumDisplayName<TEnum>(this TEnum item)
        {
            return item.GetType()
                .GetField(item.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .Cast<DisplayAttribute>()
                .FirstOrDefault()?.Name ?? string.Empty;
        }
    }
}
