using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GenericEditTableLib.Models
{
    public class EditableItemWrapper<TItem> : IValidatableObject where TItem : class
    {
        [Required(ErrorMessage = "Item cannot be null")]
        public TItem? Item { get; set; }

        public List<ColumnConfig> Columns { get; set; } = new();

        public EditableItemWrapper(TItem item, List<ColumnConfig> columns)
        {
            Item = item;
            Columns = columns;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            if (Item == null)
            {
                yield return new ValidationResult("Item cannot be null", new[] { nameof(Item) });
                yield break;
            }

            foreach (var col in Columns.Where(c => c.IsEditable))
            {
                var prop = typeof(TItem).GetProperty(col.PropertyName);
                if (prop == null) continue;

                var value = prop.GetValue(Item);

                if (string.IsNullOrWhiteSpace(value?.ToString()))
                {
                    yield return new ValidationResult(
                        $"⚠️ {col.DisplayName} is required and cannot be empty",
                        new[] { col.PropertyName }
                    );
                }
            }
        }
    }
}
