using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
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
    class ApplicationViewModel : NotifyPropertyChanged, IDataErrorInfo
    {
        //Существует ли выбранный треугольник
        public bool IsValid
        {
            get 
            {
                if (SelectedTriangle != null && SelectedTriangle.Value != null)
                    return SelectedTriangle.Value.IsValid;
                return false;
            }
        }
        //Произвести сравнение треугольников
        private void MakeComparison()
        {
            for (int i=0; i<Triangles.Count; i++)
            {
                Relation r = SelectedTriangle.Value.CompareSquare(Triangles[i].Value, true);
                
                
                Triangles[i].Key = r;
            }
        }
        //Сбросить результат сравнение треугольников
        private void ClearComparison()
        {
            for (int i = 0; i < Triangles.Count; i++)
            {
               
                              
                Triangles[i].Key = null;
                                               
                

            }
        }

        private double _scaleFactor;
        private double _deltaX;
        private double _deltaY;
        //Значение масштаба
        public double ScaleFactor 
        {
            get => _scaleFactor;
            set
            {
                _scaleFactor = value;
                
                RisePropertyChanged("ScaleFactor");
            }
        }
        //Значение сдвига по x
        public double DeltaX
        {
            get => _deltaX;
            set
            {
                _deltaX = value;

                RisePropertyChanged("DeltaX");
            }
        }
        //Значение сдвига по Y
        public double DeltaY
        {
            get => _deltaY;
            set
            {
                _deltaY = value;

                RisePropertyChanged("DeltaY");
            }
        }
        private Command _addTriangle;
        private Command _deleteTriangle;
        private Command _executeAction;
        private string _calculatedResult;
        //Результат выполнения операции
        public string CalculatedResult 
        {
            get => _calculatedResult;
            set
            {

                _calculatedResult = value;
                RisePropertyChanged("CalculatedResult");
            }
        }
        //Команда, выполяющая операции над треугольником 
        public Command ExecuteAction
        {
            get
            {
                return _executeAction ?? (_executeAction = new Command(_ =>
                {
                    CalculatedResult = string.Empty;
                    switch (SelectedAction.Key) 
                    {                      
                        case Actions.Move:                          
                            SelectedTriangle.Value.Move(DeltaX, DeltaY);
                            break;
                        case Actions.CalcSquare:                            
                            CalculatedResult = SelectedTriangle.Value.Square.ToString();
                            break;
                        case Actions.CalcPerimeter:
                            CalculatedResult = SelectedTriangle.Value.Perimeter.ToString();
                            break;
                        case Actions.GetGravity:
                            CalculatedResult = SelectedTriangle.Value.GravityPoint.ToString();
                            break;
                        case Actions.Scale:
                            try
                            {
                                SelectedTriangle.Value.Scale(ScaleFactor);
                            }
                            catch (Exception) { }
                            
                            break;
                        case Actions.Compare:
                            MakeComparison();
                            break;
                        default:
                            break;
                    }
                    

                }, ()=>SelectedTriangle != null && SelectedTriangle.Value != null && SelectedTriangle.Value.IsValid));
            }
        }
        //Удалить треугольник
        public Command DeleteTriangle
        {
            get
            {
                return _deleteTriangle ?? (_deleteTriangle = new Command(_ =>
                {
                    Triangles.Remove(SelectedTriangle);
                   
                    SelectedTriangle = Triangles.Count > 0 ? Triangles[0] : new NotifyingPair<Relation?, Triangle2D>(null, null);
                }));
            }
        }
        //Добавить треугольник
        public Command AddTriangle
        {
            get
            {
                return _addTriangle ?? (_addTriangle = new Command(_ =>
                {
                    Triangle2D triangle = new Triangle2D();
                    Triangles.Add(new NotifyingPair<Relation?, Triangle2D>(null, triangle));
                    SelectedTriangle = Triangles[Triangles.Count-1];
                    
                }));
            }
        }
        private ObservableCollection<NotifyingPair<Relation?, Triangle2D>> _triangles = new ObservableCollection<NotifyingPair<Relation?, Triangle2D>>();
        private KeyValuePair<Actions, string> _selectedAction;
        private NotifyingPair<Relation?, Triangle2D> _selectedTriangle;
        //Выбранное действие
        public KeyValuePair<Actions, string> SelectedAction
        {
            get => _selectedAction;
            set
            {
                _selectedAction = value;
                RisePropertyChanged("SelectedAction");
            }
        }
        //Выбранный треугольник
        public NotifyingPair<Relation?, Triangle2D> SelectedTriangle
        {
            get => _selectedTriangle;
            set
            {
               
                CalculatedResult = null;
                _selectedTriangle = value;
                if(value != null && value.Value != null)
                {
                    value.Value.PointA.PropertyChanged += OnCoordsChanged;
                    value.Value.PointB.PropertyChanged += OnCoordsChanged;
                    value.Value.PointC.PropertyChanged += OnCoordsChanged;
                }
                                         
                RisePropertyChanged("SelectedTriangle");

            }
        }
        //Вызывается, когда координаты вершин треугольника меняются
        private void OnCoordsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "X" || e.PropertyName == "Y")
            {
                ClearComparison();
                RisePropertyChanged("IsValid");
              
            }
        }

        //Треугольники
        public ObservableCollection<NotifyingPair<Relation?, Triangle2D>> Triangles
        {
            get => _triangles;
            set 
            {
                _triangles = value;
                RisePropertyChanged("Triangles");
            }
        }

        //Реализация IDataErrorInfo
        public string Error => throw new NotImplementedException();

        //Реализация IDataErrorInfo
        //Валидация TextBox
        public string this[string columnName] 
        {
            
            get
            {
                string message = "";
                switch (columnName) 
                {
                    case "ScaleFactor":
                        double s = Convert.ToDouble(ScaleFactor);
                        if(s<0)
                            message = "err";
                        break;
                    default:
                        break;
                }
                return message;
            }
        }

        public ApplicationViewModel()
        {
            
            SelectedAction = new KeyValuePair<Actions, string>( Actions.Move, "Переместить" );
            DeltaX = DeltaY = 0;
            ScaleFactor = 1;
            Triangles.Add(new NotifyingPair<Relation?, Triangle2D>(null, new Triangle2D()));
        }
    }
}
