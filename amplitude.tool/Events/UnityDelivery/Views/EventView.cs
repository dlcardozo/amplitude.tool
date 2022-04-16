using amplitude.tool.Events.Presentation.Views;

namespace amplitude.tool.Events.UnityDelivery.Views
{
    public class EventView : IEventView
    {
        EventPresenter presenter;

        public EventView()
        {
            presenter = new EventPresenter(this);
        }
    }

    public class EventPresenter
    {
        readonly IEventView view;

        public EventPresenter(IEventView view)
        {
            this.view = view;
        }
    }
}