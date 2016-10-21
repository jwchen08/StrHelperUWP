using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class SectionPanel : UserControl
    {
        public static double Fn = 150;
        public static double Mx = 300;
        public static double Vx = 200;
        public SectionPanel()
        {
            this.InitializeComponent();
            TextBox1.Text = Fn.ToString();
            TextBox2.Text = Mx.ToString();
            TextBox3.Text = Vx.ToString();
        }

        //荷载按钮
        private async void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox1.Text = Fn.ToString();
            TextBox2.Text = Mx.ToString();
            TextBox3.Text = Vx.ToString();
            var result=await contentdialog1.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                bool b1 = false;
                bool b2 = false;
                bool b3 = false;
                b1 = double.TryParse(TextBox1.Text, out Fn);
                b2 = double.TryParse(TextBox2.Text, out Mx);
                b3 = double.TryParse(TextBox3.Text, out Vx);
                if (b1 == false || b2 == false || b3 == false)
                {
                    DataErrorToast.Label = "荷载输入有误！";
                    DataErrorToast.Show();                }
                else
                {
                    //contentdialog1.Hide();
                }
            }
            else
            {
                contentdialog1.Hide();
            }

        }
        //矩形截面
        private void CalculateRectButton_Click(object sender, RoutedEventArgs e)
        {
            double bb = 0;
            double hh = 0;
            bool isbb = false;
            bool ishh = false;
            isbb = double.TryParse(RectBTextBox.Text, out bb);
            ishh = double.TryParse(RectHTextBox.Text, out hh);
            if (isbb & ishh)
            {
                double sectA = bb * hh;
                double sectIxx = bb * hh * hh * hh / 12.0;
                double sectIyy = bb * bb * bb * hh / 12.0;
                double sectWx = sectIxx * 2.0 / hh;
                double sectWy = sectIyy * 2.0 / bb;
                double sectS = Fn * 1000.0 / sectA + Mx * 1000000.0 / sectWx;
                double sectV = Vx * 1000.0 * (bb * hh * hh) / (8.0 * sectIxx * bb);

                TextBlockA.Text = "A   = " + sectA.ToString("e3") + "  mm^2";
                TextBlockIxx.Text = "Ixx = " + sectIxx.ToString("e3") + "  mm^4";
                TextBlockIyy.Text = "Iyy = " + sectIyy.ToString("e3") + "  mm^4";
                TextBlockWx.Text = "Wx = " + sectWx.ToString("e3") + "  mm^3";
                TextBlockWy.Text = "Wy = " + sectWy.ToString("e3") + "  mm^3";
                TextBlockS.Text = "σ   = " + sectS.ToString("f3") + "  MPa";
                TextBlockV.Text = "τ   = " + sectV.ToString("f3") + "  MPa";
            }
            else
            {
                DataErrorToast.Label = "截面尺寸数据输入有误！";
                DataErrorToast.Show();
            }
        }

        //工型截面
        private void CalculateHButton_Click(object sender, RoutedEventArgs e)
        {
            double bb = 0;
            double hh = 0;
            double tf1 = 0;
            double tw = 0;
            double tf2 = 0;
            bool isbb = false;
            bool ishh = false;
            bool istf1 = false;
            bool istw = false;
            bool istf2 = false;
            isbb = double.TryParse(HSectBTextBox.Text, out bb);
            ishh = double.TryParse(HsectHTextBox.Text, out hh);
            istf1 = double.TryParse(HSecttf1TextBox.Text, out tf1);
            istw = double.TryParse(HSecttwTextBox.Text, out tw);
            istf2 = double.TryParse(HSecttf2TextBox.Text, out tf2);
            if (isbb & ishh & istf1 & istw & istf2)
            {
                double sectA = bb * tf1 + bb * tf2 + (hh - tf1 - tf2) * tw;
                double sectIxx = tw * Math.Pow((hh - tf1 - tf2), 3) / 12.0 + bb * Math.Pow(tf1, 3) / 12.0 + bb * tf1 * Math.Pow(hh / 2.0 - tf1 / 2.0, 2) +
                    bb * Math.Pow(tf2, 3) / 12 + bb * tf2 * Math.Pow(hh / 2.0 - tf2 / 2.0, 2);
                double sectIyy = tf1 * bb * bb * bb / 12.0 + (hh - tf1 - tf2) * tw * tw * tw / 12.0 + tf2 * bb * bb * bb / 12.0;
                double sectWx = sectIxx * 2.0 / hh;
                double sectWy = sectIyy * 2.0 / bb;
                double sectS = Fn * 1000.0 / sectA + Mx * 1000000.0 / sectWx;
                double SS = bb * hh * hh / 8.0 + (tw - bb) * Math.Pow(hh / 2.0 - tf1, 2) / 2.0;
                double sectV = Vx * 1000.0 * SS / (sectIxx * tw);

                HTextBlockA.Text = "A   = " + sectA.ToString("e3") + "  mm^2";
                HTextBlockIxx.Text = "Ixx = " + sectIxx.ToString("e3") + "  mm^4";
                HTextBlockIyy.Text = "Iyy = " + sectIyy.ToString("e3") + "  mm^4";
                HTextBlockWx.Text = "Wx = " + sectWx.ToString("e3") + "  mm^3";
                HTextBlockWy.Text = "Wy = " + sectWy.ToString("e3") + "  mm^3";
                HTextBlockS.Text = "σ   = " + sectS.ToString("f3") + "  MPa";
                HTextBlockV.Text = "τ   = " + sectV.ToString("f3") + "  MPa";
            }
            else
            {
                DataErrorToast.Label = "截面尺寸数据输入有误！";
                DataErrorToast.Show();
            }

        }

        //箱型截面
        private void CalculateBoxButton_Click(object sender, RoutedEventArgs e)
        {
            double bb = 0;
            double hh = 0;
            double tf1 = 0;
            double tw = 0;
            double tf2 = 0;
            bool isbb = false;
            bool ishh = false;
            bool istf1 = false;
            bool istw = false;
            bool istf2 = false;
            isbb = double.TryParse(BoxBTextBox.Text, out bb);
            ishh = double.TryParse(BoxHTextBox.Text, out hh);
            istf1 = double.TryParse(BoxSecttf1TextBox.Text, out tf1);
            istw = double.TryParse(BoxSecttwTextBox.Text, out tw);
            istf2 = double.TryParse(BoxSecttf2TextBox.Text, out tf2);
            if (isbb & ishh & istf1 & istw & istf2)
            {
                double sectA = bb * (tf1 + tf2) + 2 * tw * (hh - tf1 - tf2);
                double sectIxx = tw * Math.Pow(hh - tf1 - tf2, 3) / 6.0 + bb * Math.Pow(tf1, 3) / 12.0 + bb * tf1 * Math.Pow(hh / 2.0 - tf1 / 2.0, 2)
                    + bb * Math.Pow(tf2, 3) / 12.0 + bb * tf2 * Math.Pow(hh / 2.0 - tf2 / 2.0, 2);
                double sectIyy = (tf1 + tf2) * Math.Pow(bb - 2.0 * tw, 3) / 12.0 + 2.0 * (hh * Math.Pow(tw, 3) / 12 + hh * tw * Math.Pow(bb / 2.0 - tw / 2.0, 2));
                double sectWx = sectIxx * 2.0 / hh;
                double sectWy = sectIyy * 2.0 / bb;
                double sectS = Fn * 1000.0 / sectA + Mx * 1000000.0 / sectWx;
                double SS = bb * hh * hh / 8.0 + (tw - bb / 2.0) * Math.Pow(hh / 2.0 - tf1, 2);
                double sectV = Vx * 1000.0 * SS / (sectIxx * tw);

                BTextBlockA.Text = "A   = " + sectA.ToString("e3") + "  mm^2";
                BTextBlockIxx.Text = "Ixx = " + sectIxx.ToString("e3") + "  mm^4";
                BTextBlockIyy.Text = "Iyy = " + sectIyy.ToString("e3") + "  mm^4";
                BTextBlockWx.Text = "Wx = " + sectWx.ToString("e3") + "  mm^3";
                BTextBlockWy.Text = "Wy = " + sectWy.ToString("e3") + "  mm^3";
                BTextBlockS.Text = "σ   = " + sectS.ToString("f3") + "  MPa";
                BTextBlockV.Text = "τ   = " + sectV.ToString("f3") + "  MPa";
            }
            else
            {
                DataErrorToast.Label = "截面尺寸数据输入有误！";
                DataErrorToast.Show();
            }
        }

        //管型截面
        const double PI = Math.PI;
        private void CalculateCircleButton_Click(object sender, RoutedEventArgs e)
        {
            double RR = 0;
            double rr = 0;
            bool isRR = false;
            bool isrr = false;
            isRR = double.TryParse(CircleRTextBox.Text, out RR);
            isrr = double.TryParse(CirclerTextBox.Text, out rr);
            if (isRR & isrr)
            {
                double sectA = PI * (RR * RR - rr * rr);
                double sectIxx = PI * (RR * RR * RR * RR - rr * rr * rr * rr) / 4.0;
                double sectIyy = PI * (RR * RR * RR * RR - rr * rr * rr * rr) / 4.0;
                double sectWx = sectIxx / RR;
                double sectWy = sectIyy / RR;
                double sectS = Fn * 1000.0 / sectA + Mx * 1000000.0 / sectWx;
                double sectV = Vx * 1000.0 * 2 * (RR * RR * RR - rr * rr * rr) / (3 * sectIxx * (RR - rr));

                CTextBlockA.Text = "A   = " + sectA.ToString("e3") + "  mm^2";
                CTextBlockIxx.Text = "Ixx = " + sectIxx.ToString("e3") + "  mm^4";
                CTextBlockIyy.Text = "Iyy = " + sectIyy.ToString("e3") + "  mm^4";
                CTextBlockWx.Text = "Wx = " + sectWx.ToString("e3") + "  mm^3";
                CTextBlockWy.Text = "Wy = " + sectWy.ToString("e3") + "  mm^3";
                CTextBlockS.Text = "σ   = " + sectS.ToString("f3") + "  MPa";
                CTextBlockV.Text = "τ   = " + sectV.ToString("f3") + "  MPa";
            }
            else
            {
                DataErrorToast.Label = "截面尺寸数据输入有误！";
                DataErrorToast.Show();
            }
        }

    }
}
