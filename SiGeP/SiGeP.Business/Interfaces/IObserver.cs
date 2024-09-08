using SiGeP.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGeP.Bussines.Interfaces
{
    public interface IObserver<T>
    {
        void Update(T subject);
    }
}
