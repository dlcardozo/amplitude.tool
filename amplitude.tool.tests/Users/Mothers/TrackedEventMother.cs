using amplitude.tool.Users.Domain.Model;

namespace amplitude.tool.tests.Users.Mothers
{
    public static class TrackedEventMother
    {
        public static TrackedEvent ATrackedEvent(string withName = null) => 
            new TrackedEvent(withName ?? "");
    }
}