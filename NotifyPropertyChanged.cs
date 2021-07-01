using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Lab6
{
    //реализация INotifyPropertyChanged
    abstract class NotifyPropertyChanged : INotifyPropertyChanged
    {
        //событие изменения свойства
        public event PropertyChangedEventHandler PropertyChanged;

        //обертка над PropertyChangedEventHandler
        protected virtual void RisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
