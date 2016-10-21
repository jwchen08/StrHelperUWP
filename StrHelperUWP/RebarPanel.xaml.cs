using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace StrHelperUWP
{
    public sealed partial class RebarPanel : UserControl
    {
        int[] diameters = { 6, 8, 10, 12, 14, 16, 18, 20, 22, 25, 28, 30, 32,36 };
        const double PI = 3.14159;
        public RebarPanel()
        {
            this.InitializeComponent();

            for (int i=0;i<diameters.Count();i++)
            {
                RowDefinition rd1 = new RowDefinition();
                rd1.Height = new GridLength(30);
                RebarGrid.RowDefinitions.Add(rd1);

                Button button = new Button();
                button.HorizontalAlignment = HorizontalAlignment.Stretch;
                button.FontWeight = Windows.UI.Text.FontWeights.Bold;
                button.Background = new SolidColorBrush(Windows.UI.Colors.Transparent); 
                button.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
                button.BorderThickness = new Thickness(1);
                button.Content = diameters[i].ToString();
                RebarGrid.Children.Add(button);
                Grid.SetColumn(button, 0);
                Grid.SetRow(button, i +1);

                Button button2 = new Button();
                button2.HorizontalAlignment = HorizontalAlignment.Stretch;
                button2.FontWeight = Windows.UI.Text.FontWeights.Bold;
                button2.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
                button2.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
                button2.BorderThickness = new Thickness(1);
                button2.Content = diameters[i].ToString();
                RebarGrid.Children.Add(button2);
                Grid.SetColumn(button2, 11);
                Grid.SetRow(button2, i + 1);

                for (int j=1;j<11;j++)
                {
                    Button b1 = new Button();
                    b1.HorizontalAlignment = HorizontalAlignment.Stretch;
                    b1.Content =string.Format("{0:f0}", j*PI * diameters[i] * diameters[i] / 4);
                    RebarGrid.Children.Add(b1);
                    Grid.SetColumn(b1, j);
                    Grid.SetRow(b1, i+1);

                }
            }

        }
    }
}
