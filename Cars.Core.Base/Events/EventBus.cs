using Cars.SharedKernel.Events;
using Cars.SharedKernel.Log;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Cars.Core.Base.Events
{
    public class EventBus : IEventBus
    {
        private readonly IAppLogger logger;

        public EventBus(IAppLogger logger)
        {
            this.logger = logger;
        }

        private readonly ISubject<object> eventBus = new Subject<object>();

        public IDisposable Subscribe<T>(Action<T> action) where T : IEvent
        {
            var disposable = this.eventBus.OfType<T>().Subscribe(action);
            this.logger.Trace($"{GetType().Name} => {typeof(T).Name} was subscribed by {action.Target.GetType().Name}");
            return disposable;
        }

        public void Publish<T>(T item) where T : IEvent
        {
            this.eventBus.OnNext(item);
            this.logger.Trace($"{GetType().Name} => published event {item.GetType().Name}");
        }
    }
}
