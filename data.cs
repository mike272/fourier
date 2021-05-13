using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fourier
{
    public class Circle:INotifyPropertyChanged
    {
        private double frequency;
        public double Frequency
        {
            get { return frequency; }
            set { this.frequency = Frequency; }
        }
        public double radius;

        public event PropertyChangedEventHandler PropertyChanged;

        public Circle(double r, double f)
        {
            frequency = f;
            radius = r;
        }
    }

    public class Circles : ObservableCollection<Circle>
    {
        public Circles()
        {
            Add(new Circle(100, 2));
            Add(new Circle(200, 4));
        }
    }
    public class data : ObservableCollection<Tuple<double, double>>
    {
        public data()
        {
            Add(new Tuple<double, double>(100, 1));
            Add(new Tuple<double, double>(200, 1));
            Add(new Tuple<double, double>(150, 2));
            Add(new Tuple<double, double>(100, 3));
        }
    }
}
