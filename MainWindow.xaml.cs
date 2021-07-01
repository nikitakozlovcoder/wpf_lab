using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Lab6
{
    public partial class MainWindow : Window
    {      
        public MainWindow()
        {          
            InitializeComponent();
            //Установка модели представления в качестве контекста для привязок
            DataContext = new ApplicationViewModel();
            //Операции над треугольниками
            Dictionary<Actions, string> actions = new Dictionary<Actions, string>
            {
                    { Actions.Move, "Переместить" },
                    { Actions.CalcSquare, "Подсчет площади" },
                    { Actions.CalcPerimeter, "Подсчет периметра" },
                    { Actions.GetGravity, "Получить центр тяжести" },
                    { Actions.Scale, "Масштабировать" },
                    { Actions.Compare, "Сравнить площадь" },

                };

            ComboBoxActions.ItemsSource = actions;
            ComboBoxActions.DisplayMemberPath = "Value";
            ComboBoxActions.SelectedValuePath = "Key";
        }


    }
}
