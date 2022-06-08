using System.Collections.Generic;

namespace ElasticCollision.Logic
{
    public class Observable<T>
    {
        private List<Observer> _observers = new List<Observer>();

        public delegate void Observer(T thingie);

        public void Add(Observer del) => _observers.Add(del);

        public void Notify(T thingie) => _observers.ForEach(x => x.Invoke(thingie));
    }
}
