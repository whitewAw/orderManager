using System.Windows;

namespace Менеждер_заказов.Event
{
    class EventClass
    {
        private static EventClass instance;

        private EventClass()
        { }

        public static EventClass getInstance()
        {
            if (instance == null)
                instance = new EventClass();
            return instance;
        }

        public delegate void EventHandler();

        public event EventHandler UpdateEvent;

        public event EventHandler CancelEvent;


        public event EventHandler DisableEvent;

        public event EventHandler EnableEvent;

        public void RunUpdate() => UpdateEvent?.Invoke();
        public void RunCancel() => CancelEvent?.Invoke();

        public void RunDisable() => DisableEvent?.Invoke();
        public void RunEnable() => EnableEvent?.Invoke();

    }
}
