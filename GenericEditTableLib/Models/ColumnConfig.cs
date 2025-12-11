using System.ComponentModel.DataAnnotations;

namespace GenericEditTableLib.Models
{
    public class ColumnConfig
    {
        public string PropertyName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public bool IsEditable { get; set; }
        public Type PropertyType { get; set; } = typeof(string);
    }
}
