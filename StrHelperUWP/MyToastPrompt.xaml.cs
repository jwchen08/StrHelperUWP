﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class MyToastPrompt : UserControl
    {
        //这段代码是通用的依赖属性定义代码,为控件自定义了一个名为Label的属性。
        public DependencyProperty LabelProperty =
                DependencyProperty.Register("Label", typeof(string), typeof(MyToastPrompt), null);

        //这一段代码将变量Label和Label依赖属性绑定在了一起，从而可以通过Binding到Label变量来间接绑定到依赖属性LabelProperty。
        public string Label
        {
            get { return GetValue(LabelProperty) as string; }
            set { SetValue(LabelProperty, value); }
        }


        public MyToastPrompt()
        {
            this.InitializeComponent();
            this.DataContext = this;

        }

        public async Task Show()
        {
            this.Toast.IsOpen = false;
            this.StoryboardHiddenPopup.Stop();
            this.StoryboardShowPopup.Stop();
            //这三步是为了清除上一次动画的效果
            this.Toast.IsOpen = true;
            this.StoryboardShowPopup.Begin();
            //内容提示停留1.2s后开始隐藏
            await Task.Delay(1200);
            this.StoryboardHiddenPopup.Begin();
        }

        //当隐藏动画播放结束后会自动调用StoryboardHiddenPopup_Completed
        private void StoryboardHiddenPopup_Completed(object sender, object e)
        {
            this.Toast.IsOpen = false;
        }


    }
}
