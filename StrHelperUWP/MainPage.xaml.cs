using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.ViewManagement;
using Windows.UI;
using UmengSDK;
using UmengSDK.Business;
using Windows.UI.Popups;
using System.Text;
using System.IO.IsolatedStorage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace StrHelperUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        ObservableCollection<ViewItem> m_items = null;
        public MainPage()
        {
            this.InitializeComponent();

            //标题栏颜色
            var titleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            titleBar.BackgroundColor = Colors.DeepSkyBlue;
            titleBar.InactiveBackgroundColor = Colors.SkyBlue;
            //titleBar.ForegroundColor = Colors.Silver;
            //titleBar.ButtonHoverBackgroundColor = Colors.LightBlue;
            titleBar.ButtonBackgroundColor = Colors.DeepSkyBlue;
            titleBar.ButtonInactiveBackgroundColor = Colors.SkyBlue;
            //titleBar.ButtonForegroundColor = Colors.White;

            m_items = new ObservableCollection<ViewItem>();
            this.lvPrev.ItemsSource = m_items;

            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                
                StatusBar statusBar = StatusBar.GetForCurrentView();
                statusBar.ForegroundColor = Colors.Black;
                statusBar.BackgroundColor = Colors.DeepSkyBlue;
                statusBar.BackgroundOpacity = 1;
                // 显示StatusBar
                statusBar.ShowAsync();
                // 隐藏StatusBar
                // await statusBar.HideAsync();
                // 设置ProgressIndicator
                //statusBar.ProgressIndicator.Text = "test...";
                //await statusBar.ProgressIndicator.ShowAsync();
            }

            this.Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            splitViewGrid.Children.Clear();
            splitViewGrid.RowDefinitions.Clear();
            splitViewGrid.ColumnDefinitions.Clear();
            RebarPanel rp1 = new RebarPanel();
            ScrollViewer sv1 = new ScrollViewer();
            sv1.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            sv1.HorizontalScrollMode = ScrollMode.Auto;
            sv1.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            sv1.Content = rp1;
            splitViewGrid.Children.Add(sv1);

            //调用接口获取在线参数
            var res = await UmengAnalytics.UpdateOnlineParamAsync();
            //res.Result包含获取到的在线参数
            OnUpdateOnlineParamCompleted(res);
        }

        async void OnUpdateOnlineParamCompleted(OnlineParamArgs e)
        {
            if (e.Config != null && e.Config.Params != null)
            {
                //StringBuilder param = new StringBuilder();
                //foreach (var item in e.Config.Params)
                //{
                //    param.AppendLine(string.Format("{0}:{1}", item.Key, item.Value));
                //}
                string ReadedInfo = "";
                //首先读取已查看过的消息列表
                using (IsolatedStorageFile appStorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (appStorage.FileExists("UmengInfo.txt"))
                    {
                        using (var file = appStorage.OpenFile("UmengInfo.txt", FileMode.Open))
                        {
                            using (StreamReader sr = new StreamReader(file))
                            {
                                //读取全部信息
                                ReadedInfo = sr.ReadToEnd();

                            }
                        }
                    }
                }
                //判断服务器上的消息是否已读
                for (int i = 0; i < e.Config.Params.Count; i++)
                {
                    var item = e.Config.Params.ElementAt(i);
                    //存在未读消息
                    if (!ReadedInfo.Contains(item.Key + item.Value))
                    {
                        MessageDialog dialog = new MessageDialog(item.Value,item.Key);
                        await dialog.ShowAsync();
                        //将该消息加进已读列表
                        using (IsolatedStorageFile appStorage = IsolatedStorageFile.GetUserStoreForApplication())
                        {
                            using (var file = appStorage.OpenFile("UmengInfo.txt", System.IO.FileMode.Append))
                            {
                                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(file))
                                {
                                    //WriteLine其实是在字符串后面加上\r\n
                                    sw.WriteLine(item.Key + item.Value);

                                }
                            }
                        }
                        //此次不再获取新消息
                        break;
                    }
                }
                
            }
            else
            {
                //MessageDialog dialog = new MessageDialog("No online params!");
                //await dialog.ShowAsync();
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            m_items.Clear();
            // 添加项列表
            m_items.Add(new ViewItem { Text = "配筋查询", Uri = new Uri("ms-appx:///Pictures/Search.png") });
            m_items.Add(new ViewItem { Text = "设计参数", Uri = new Uri("ms-appx:///Pictures/Books.png") });
            m_items.Add(new ViewItem { Text = "截面特性", Uri = new Uri("ms-appx:///Pictures/Section.png") });
            m_items.Add(new ViewItem { Text = "抗震规范", Uri = new Uri("ms-appx:///Pictures/Earth.png") });
            //m_items.Add(new ViewItem { Text = "结构求解", Uri = new Uri("ms-appx:///Pictures/Calculator.png") });
            m_items.Add(new ViewItem { Text = "关于软件", Uri = new Uri("ms-appx:///Pictures/about.png") });
        }

        private void OnClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (splitView.DisplayMode != SplitViewDisplayMode.Inline)
            {
                this.splitView.IsPaneOpen = !this.splitView.IsPaneOpen;
            }
        }

        private void lvPrev_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            splitViewGrid.Children.Clear();
            splitViewGrid.RowDefinitions.Clear();
            splitViewGrid.ColumnDefinitions.Clear();
            //TextBlock tb1 = new TextBlock();
            //tb1.Text = "Hello World!";
            if(lvPrev.SelectedIndex==0)
            {
                RebarPanel rp1 = new RebarPanel();
                ScrollViewer sv1 = new ScrollViewer();
                sv1.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                sv1.HorizontalScrollMode = ScrollMode.Auto;
                sv1.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                sv1.Content = rp1;
                splitViewGrid.Children.Add(sv1);
            }
            else if (lvPrev.SelectedIndex==1)
            {
                CanshuPanel cp1 = new CanshuPanel();
                splitViewGrid.Children.Add(cp1);
            }
            else if(lvPrev.SelectedIndex==2)
            {
                SectionPanel sp1 = new SectionPanel();
                splitViewGrid.Children.Add(sp1);
            }
            else if(lvPrev.SelectedIndex==3)
            {
                EarthquakePanel ep1 = new EarthquakePanel();
                splitViewGrid.Children.Add(ep1);
            }
            //结构求解
            //else if (lvPrev.SelectedIndex==4)
            //{
                
            //}
            else if (lvPrev.SelectedIndex == 4)
            {
                AboutPanel ap1 = new AboutPanel();
                splitViewGrid.Children.Add(ap1);
            }
            else
            {

            }
            
        }

    }

    public class ViewItem
    {
        public string Text { get; set; }
        public Uri Uri { get; set; }
    }
}
