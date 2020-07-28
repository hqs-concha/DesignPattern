using System;
using DesignPattern.Interface;

namespace DesignPattern.Services
{
    public class SingletonService : ISingletonService
    {
        private MyClass _myClass;
        private readonly object _locker = new object();

        private MyClass GetInstance()
        {
            if (_myClass == null)
            {
                lock (_locker)
                {
                    if (_myClass == null)
                        _myClass = new MyClass();
                }
            }

            return _myClass;
        }

        
        public string Get()
        {
            return GetInstance().Now.ToString("s");
        }
    }

    public class MyClass
    {
        public DateTime Now { get; set; } = DateTime.Now;
    }
}
