using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class AboutPanel : UserControl
    {
        public AboutPanel()
        {
            this.InitializeComponent();
        }

        private async void FeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            
            var mailto = new Uri($"mailto:{"jwchen08@qq.com"}?subject={"结构小助手v1.6.0版反馈"}&body={""}");

            await Windows.System.Launcher.LaunchUriAsync(mailto);
        }
    }
}
