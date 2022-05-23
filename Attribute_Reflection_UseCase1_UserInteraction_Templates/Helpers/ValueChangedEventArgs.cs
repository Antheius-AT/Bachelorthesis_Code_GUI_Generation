namespace Attribute_Reflection_UseCase1_UserInteraction_Templates.Helpers
{
    public class ValueChangedEventArgs : EventArgs
    {
        public ValueChangedEventArgs(object newValue, string propertyName)
        {
            NewValue = newValue;
            PropertyName = propertyName;
        }

        public object NewValue { get; }
        public string PropertyName { get; }
    }
}
