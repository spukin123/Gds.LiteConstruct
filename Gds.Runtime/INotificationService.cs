using System;

namespace Gds.Runtime
{
    public interface INotificationService
    {
        void RiseEvent(string eventKey, object sender, EventArgs e);
        void Subscribe(string eventKey, EventHandler eventHandler);
        void Unsubscribe(string eventKey, EventHandler eventHandler);
    }
}
