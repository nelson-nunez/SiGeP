using SiGeP.Bussines.Interfaces;
using SiGeP.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SiGeP.Business.Notifiers
{
    public class Notifier<T> : ISubject<T>
    {
        private readonly List<Bussines.Interfaces.IObserver<T>> _observers = new List<Bussines.Interfaces.IObserver<T>>();

        public void Attach(Bussines.Interfaces.IObserver<T> observer)
        {
            _observers.Add(observer);
        }

        public void Detach(Bussines.Interfaces.IObserver<T> observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(T subject)
        {
            foreach (var observer in _observers)
            {
                observer.Update(subject);
            }
        }
    }
}
