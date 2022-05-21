
namespace Models.Metadata
{
    public class StringLengthAttribute : Attribute
    {
        public StringLengthAttribute(int maxLength)
        {
            MaxLength = maxLength;
        }

        public int MaxLength { get; }
    }
}
