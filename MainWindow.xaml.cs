using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace fourier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int maxWidth;
        private int maxheight;
        BackgroundWorker bgw = new BackgroundWorker();
        public MainWindow()
        {
            InitializeComponent();
        }

       
        private bool progressBarPaused = false;
        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            progressBarPaused = true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //https://stackoverflow.com/questions/25779182/how-to-make-a-progressbar-run-in-a-different-thread-in-c-sharp
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!progressBarPaused)
            {
                int total = 100; //some number (this is your variable to change)!!

                for (int i = 0; i <= total; i++) //some number (total)
                {
                    System.Threading.Thread.Sleep(100);
                    int percents = (i * 100) / total;
                    bgw.ReportProgress(percents, i);
                    //2 arguments:
                    //1. procenteges (from 0 t0 100) - i do a calcumation 
                    //2. some current value!
                }
            }
        }

        void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(!progressBarPaused)
            {
                progressBar.Value = e.ProgressPercentage;
            }
           
        }

        public Ellipse el;
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           el = draw.circle(100, canvas);
            Ellipse rect = new Ellipse()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 2
            };
            Tuple<double, double> tup = (Tuple<double, double>)dataGrid.Items.GetItemAt(0);
            rect.Width = rect.Height = tup.Item1;
            Canvas.SetLeft(rect, (maxheight - rect.Height) / 2);
            Canvas.SetLeft(rect, (maxWidth - rect.Width) / 2);
            canvas.Children.Add(rect);
        }

        private Ellipse circleMain = new Ellipse();
        class draw
        {
            public static Ellipse circle(int r, Canvas cv)
            {

                Ellipse circle = new Ellipse()
                {
                    Width = r,
                    Height = r,
                    Stroke = Brushes.Black,
                    StrokeThickness = 6
                };
                
                cv.Children.Add(circle);
                double left = (cv.ActualWidth - circle.ActualWidth) / 2;
                double top = (cv.ActualHeight - circle.ActualHeight) / 2;
                circle.SetValue(Canvas.LeftProperty, left);
                circle.SetValue(Canvas.TopProperty, top-40);
                return circle;
            }
        }
        private bool isWorking = false;
        void startButton_Click(object sender, RoutedEventArgs e)
        {
            //if its already in background then we cant invoke it again
            if (!isWorking)
            {
                isWorking = true;
                progressBarPaused = false;
                bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
                bgw.ProgressChanged += new ProgressChangedEventHandler(bgw_ProgressChanged);
                bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
                bgw.WorkerReportsProgress = true;
                bgw.RunWorkerAsync();
            }

            if (isWorking && progressBarPaused)
            {
                progressBarPaused = false;
            }

            

        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            progressBar.Value = 0;
             canvas.Children.Remove(el);
        }
    }
    

        
        
    
}
