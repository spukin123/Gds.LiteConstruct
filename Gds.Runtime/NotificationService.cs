using System;
using System.Collections.Generic;
using System.Text;

namespace Gds.Runtime
{
    public class NotificationService : INotificationService
    {
        private Dictionary<string, EventHandler> handlers = new Dictionary<string, EventHandler>();

        public void Subscribe(string eventKey, EventHandler eventHandler)
        {
            if (handlers.ContainsKey(eventKey))
            {
                handlers[eventKey] += eventHandler;
            }
            else
            {
                handlers.Add(eventKey, eventHandler);
            }
        }

        public void Unsubscribe(string eventKey, EventHandler eventHandler)
        {
            EventHandler handlerList;
            if (handlers.TryGetValue(eventKey, out handlerList))
            {
                handlerList -= eventHandler;
                if (handlerList == null)
                {
                    handlers.Remove(eventKey);
                }
            }
        }

        public void RiseEvent(string eventKey, object sender, EventArgs e)
        {
            if (handlers.ContainsKey(eventKey))
            {
                handlers[eventKey](sender, e);
            }
        }
    }
}
