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
using System.Windows.Threading;

namespace project_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Canvas[] canvases=new Canvas[3];
        Racetrack.UserControlHorse[] horses= new Racetrack.UserControlHorse[3];
        Racetrack.UserControlFinish[] finishes = new Racetrack.UserControlFinish[3];
        Racetrack.UserControlPosition[] positions = new Racetrack.UserControlPosition[3];
        DispatcherTimer timer1 = new DispatcherTimer();
        DispatcherTimer timer2 = new DispatcherTimer();
        int totalDistance = 1100;
        Random random = new Random();
        bool flagStart= false;

        public MainWindow()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            for (int i = 0; i < canvases.Length; i++)
            {
                canvases[i] = new Canvas();
                Grid.SetRow(canvases[i],i);
                grid.Children.Add(canvases[i]);
            }

            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = new TimeSpan(1000);
            

            timer2.Tick += new EventHandler(timer2_Tick);
            timer2.Interval = new TimeSpan(0,0,2);

        }

        private void timer1_Tick(object? sender, EventArgs e)
        {
            for (int i = 0; i < canvases.Length; i++)
            {
                // пропускаем если лошадь достигла финиша
                if (horses[i].IsFinish) continue;

                int position = 0;
                for(int j = 0; j < canvases.Length; j++)
                {
                    if (horses[j].IsFinish || horses[i].XHorse <= horses[j].XHorse) position++;
                }

                if (horses[i].XHorse < totalDistance)
                {
                    horses[i].Position = position;
                    horses[i].XHorse += horses[i].GetSpeed() / 1000f;
                } else {
                    //лошадь финишировала
                    horses[i].Position = position;
                    positions[i].SetPosition(horses[i].Position);
                    horses[i].IsFinish = true;
                }

            }
        }

        private void timer2_Tick(object? sender, EventArgs e)
        {
            for (int i = 0; i < canvases.Length; i++)
            {
                // пропускаем если лошадь достигла финиша
                if (horses[i].IsFinish) continue;
                horses[i].SetSpeed(random.Next(40, 80));
            }
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            if(flagStart == false)
            {
                flagStart = true;
                Start();
            }
            if( timer1.IsEnabled == true ) {
                ((Button)sender).Content = "Start";
                timer1.Stop();
                timer2.Stop();
            } else {
                ((Button)sender).Content = "Pause";
                timer1.Start();
                timer2.Start();
            }
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }

        private void Start()
        {
            for (int i = 0; i < canvases.Length; i++)
            {
                canvases[i].Children.Clear();
                finishes[i] = new Racetrack.UserControlFinish();
                canvases[i].Children.Add(finishes[i]);
                Canvas.SetRight(finishes[i], 0);

                positions[i] = new Racetrack.UserControlPosition();
                canvases[i].Children.Add(positions[i]);

                horses[i] = new Racetrack.UserControlHorse(random.Next(20, 50));
                canvases[i].Children.Add(horses[i]);
            }

            timer1.Start();
            timer2.Start();
        }
    }
}
