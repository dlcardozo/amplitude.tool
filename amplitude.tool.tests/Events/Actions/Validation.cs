namespace amplitude.tool.tests.Events.Actions
{
    public struct Validation
    {
        public readonly string EventName;
        public readonly bool IsValid;

        public Validation(string eventName, bool isValid)
        {
            EventName = eventName;
            IsValid = isValid;
        }
        
        public override string ToString() => $"Validation: {EventName}, {IsValid}";
    }
}