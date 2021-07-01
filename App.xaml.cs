using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Lab6
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //С версии NetFramework 4.5 стало невозможным ввести разделитель "." в TextBox при UpdateSourceTrigger = PropertyChanged
            //Открепляем текстовое значение переменной от значения TextBox, решаем проблему
            System.Windows.FrameworkCompatibilityPreferences.KeepTextBoxDisplaySynchronizedWithTextProperty = false;
        }
        
    }
}
