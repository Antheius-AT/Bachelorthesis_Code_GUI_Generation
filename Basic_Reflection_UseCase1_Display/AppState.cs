namespace Basic_Reflection_UseCase1_Display_Template
{
	public class AppState
	{
		public bool Visible { get; set; }

		public event EventHandler? ChangedEvent;

		public void RaiseEvent()
		{
			ChangedEvent?.Invoke(this, EventArgs.Empty);
		}
	}
}
