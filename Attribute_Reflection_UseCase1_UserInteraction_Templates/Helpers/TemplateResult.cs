namespace Attribute_Reflection_UseCase1_UserInteraction_Templates.Helpers
{
    public class TemplateResult
    {
        public TemplateResult(Type templateType, object value)
        {
            TemplateType = templateType;
            Value = value;
        }

        public Type TemplateType { get; }
        public object Value { get; }
    }
}
