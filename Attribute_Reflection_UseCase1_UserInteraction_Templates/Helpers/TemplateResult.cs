namespace Attribute_Reflection_UseCase1_UserInteraction_Templates.Helpers
{
    public class TemplateResult
    {
        public TemplateResult(Type templateType, object value, string label = "")
        {
            TemplateType = templateType;
            Value = value;
            Label = label;
        }

        public Type TemplateType { get; }
        public object Value { get; }

        public string Label { get; }
    }
}
