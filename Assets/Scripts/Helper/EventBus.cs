using Enums;

namespace Helper
{
    using System;
    using System.Collections.Generic;

    public static class EventBus
    {
        private static readonly Dictionary<Events, Delegate> EventHandlers = new();

        public static void Register<T>(Events eventName, Action<T> handler)
        {
            if (EventHandlers.ContainsKey(eventName))
                EventHandlers[eventName] = Delegate.Combine(EventHandlers[eventName], handler);
            else
                EventHandlers[eventName] = handler;
        }
        
        public static void Register(Events eventName, Action handler)
        {
            if (EventHandlers.ContainsKey(eventName))
                EventHandlers[eventName] = Delegate.Combine(EventHandlers[eventName], handler);
            else
                EventHandlers[eventName] = handler;
        }

        public static void Unregister<T>(Events eventName, Action<T> handler)
        {
            if (EventHandlers.ContainsKey(eventName))
                EventHandlers[eventName] = Delegate.Remove(EventHandlers[eventName], handler);
        }
        
        public static void Unregister(Events eventName, Action handler)
        {
            if (EventHandlers.ContainsKey(eventName))
                EventHandlers[eventName] = Delegate.Remove(EventHandlers[eventName], handler);
        }

        public static void Trigger<T>(Events eventName, T arg)
        {
            if (EventHandlers.TryGetValue(eventName, out var handler))
                (handler as Action<T>)?.Invoke(arg);
        }
        
        public static void Trigger(Events eventName)
        {
            if (EventHandlers.TryGetValue(eventName, out var handler))
                (handler as Action)?.Invoke();
        }
    }
}