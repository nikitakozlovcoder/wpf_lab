using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6
{
    //Контейнер двух значений с реализацией INotifyPropertyChanged
    class NotifyingPair<T1, T2> : NotifyPropertyChanged
    {
        private T1 _key;
        public T1 Key { 
            get => _key; 
            set 
            {
                _key = value;
                RisePropertyChanged("Key");
            } 
        }
        private T2 _value;
        public T2 Value
        {
            get => _value;
            set
            {
                _value = value;
                RisePropertyChanged("Value");
            }
        }

        public NotifyingPair(T1 key, T2 value)
        {
            Key = key;
            Value = value;
        }
        public NotifyingPair()
        {         
        }
    }
}
