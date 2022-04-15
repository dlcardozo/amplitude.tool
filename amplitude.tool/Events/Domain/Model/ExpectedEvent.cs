namespace amplitude.tool.Events.Domain.Model
{
    public struct ExpectedEvent
    {
        public string EventName { get; }

        public ExpectedEvent(string eventName)
        {
            EventName = eventName;
        }
    }
}