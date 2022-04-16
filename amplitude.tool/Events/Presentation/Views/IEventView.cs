using System.Collections.Generic;
using amplitude.tool.Events.Domain.Model;

namespace amplitude.tool.Events.Presentation.Views
{
    public interface IEventView
    {
        void ShowValidationResult(List<Validation> validations);
        void ShowExpectedEventsAdded();
    }
}