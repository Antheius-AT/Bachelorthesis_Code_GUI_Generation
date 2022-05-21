namespace Attribute_Reflection_UseCase1_Display_Templates
{
    public class AppState
    {
        public bool Visible { get; set; }

        public event EventHandler? ChangedEvent;
    
        public void RaiseEvent()
        {
            this.ChangedEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
