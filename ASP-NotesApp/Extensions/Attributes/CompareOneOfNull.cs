using System.ComponentModel.DataAnnotations;

namespace ASP_NotesApp.Extensions.Attributes
{
    public class CompareOneOfNull : CompareAttribute
    {
        public CompareOneOfNull(string otherProperty) : base(otherProperty)
        {
        }

        public override bool IsValid(object? value)
        {
            if (this.OtherProperty == null && value == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
