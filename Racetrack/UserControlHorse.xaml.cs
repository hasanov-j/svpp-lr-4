﻿using System;
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

namespace Racetrack
{
    /// <summary>
    /// Логика взаимодействия для UserControlHorse.xaml
    /// </summary>
    public partial class UserControlHorse : UserControl
    {
        Horse horse;
        public UserControlHorse(int speed)
        {
            InitializeComponent();
            horse=new Horse(speed);
            this.DataContext = horse;

            Binding bindingSpeed = new Binding("Speed");
            bindingSpeed.Converter = new SpeedToString();
            textBlockSpeed.SetBinding(TextBlock.TextProperty, bindingSpeed);

            Binding bindingPosition = new Binding("Position");
            bindingPosition.Converter = new PositionToString();
            textBlockPosition.SetBinding(TextBlock.TextProperty, bindingPosition);

            this.SetBinding(Canvas.LeftProperty, new Binding("X"));
        }

        public void SetSpeed(int speed) {

            horse.Speed = speed;
        }

        public int GetSpeed() {

            return horse.Speed;
        }

        public float XHorse
        {
            get => horse.X ;
            set => horse.X = value;
        }

        public bool IsFinish
        {
            get => horse.IsFinish;
            set => horse.IsFinish = value;
        }

        public int Position
        {
            get => horse.Position;
            set => horse.Position = value;
        }

        private void image_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(textBlockSpeed.Visibility == Visibility.Visible)
            {
                textBlockSpeed.Visibility = Visibility.Hidden;
            } else {
                textBlockSpeed.Visibility = Visibility.Visible;
            }
        }

        private void image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (textBlockPosition.Visibility == Visibility.Visible)
            {
                textBlockPosition.Visibility = Visibility.Hidden;
            }
            else
            {
                textBlockPosition.Visibility = Visibility.Visible;
            }
        }
    }
}
