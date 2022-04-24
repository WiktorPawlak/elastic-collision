using System.Collections.Generic;

namespace ElasticCollision.Logic
{
    public abstract class Observable<T>
    {
        public delegate void Observer(T thingie);

        private List<Observer> _observers = new List<Observer>();

        public void AddObserver(Observer del) => _observers.Add(del);

        public void NotifyObservers(T thingie) => _observers.ForEach(x => x.Invoke(thingie));

    }
}
