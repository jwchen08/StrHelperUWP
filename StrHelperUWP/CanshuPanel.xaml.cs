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
using System.Collections.ObjectModel;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace StrHelperUWP
{
    public sealed partial class CanshuPanel : UserControl
    {
        private ObservableCollection<ConcreteItem> ConcreteItemList = new ObservableCollection<ConcreteItem>();
        private ObservableCollection<RebarItem> RebarItemList = new ObservableCollection<RebarItem>();
        private ObservableCollection<SteelItem> SteelItemList = new ObservableCollection<SteelItem>();
        public CanshuPanel()
        {
            this.InitializeComponent();
            this.Loaded += CanshuPanel_Loaded;
        }

        private void CanshuPanel_Loaded(object sender, RoutedEventArgs e)
        {
            ConcreteItemList.Add(new ConcreteItem("C15", "7.2", "0.91", "1.0", "0.8"));
            ConcreteItemList.Add(new ConcreteItem("C20", "9.6", "1.10", "1.0", "0.8"));
            ConcreteItemList.Add(new ConcreteItem("C25", "11.9", "1.27", "1.0", "0.8"));
            ConcreteItemList.Add(new ConcreteItem("C30", "14.3", "1.43", "1.0", "0.8"));
            ConcreteItemList.Add(new ConcreteItem("C35", "16.7", "1.57", "1.0", "0.8"));
            ConcreteItemList.Add(new ConcreteItem("C40", "19.1", "1.71", "1.0", "0.8"));
            ConcreteItemList.Add(new ConcreteItem("C45", "21.2", "1.80", "1.0", "0.8"));
            ConcreteItemList.Add(new ConcreteItem("C50", "23.1", "1.89", "1.0", "0.8"));
            ConcreteItemList.Add(new ConcreteItem("C55", "25.3", "1.96", "0.99", "0.79"));
            ConcreteItemList.Add(new ConcreteItem("C60", "27.5", "2.04", "0.98", "0.78"));
            ConcreteItemList.Add(new ConcreteItem("C65", "29.7", "2.09", "0.97", "0.77"));
            ConcreteItemList.Add(new ConcreteItem("C70", "31.8", "2.14", "0.96", "0.76"));
            ConcreteItemList.Add(new ConcreteItem("C75", "33.8", "2.18", "0.95", "0.75"));
            ConcreteItemList.Add(new ConcreteItem("C80", "35.9", "2.22", "0.94", "0.74"));
            ConcreteListBox.ItemsSource = ConcreteItemList;

            RebarItemList.Add(new RebarItem("HPB235", "8-20", "210", "210", "2.1e11"));
            RebarItemList.Add(new RebarItem("HPB300", "6-22", "270", "270", "2.1e11"));
            RebarItemList.Add(new RebarItem("HRB335", "6-50", "300", "300", "2.0e11"));
            RebarItemList.Add(new RebarItem("HRB400", "6-50", "360", "360", "2.0e11"));
            RebarItemList.Add(new RebarItem("HRB500", "6-50", "435", "410", "2.0e11"));
            RebarListBox.ItemsSource = RebarItemList;

            SteelItemList.Add(new SteelItem("Q235", "<=16", "215", "125", "325"));
            SteelItemList.Add(new SteelItem("Q235", ">16-40", "205", "120", "325"));
            SteelItemList.Add(new SteelItem("Q235", ">40-60", "200", "115", "325"));
            SteelItemList.Add(new SteelItem("Q235", ">60-100", "190", "110", "325"));
            SteelItemList.Add(new SteelItem("", "", "", "", ""));
            SteelItemList.Add(new SteelItem("Q345", "<=16", "310", "180", "400"));
            SteelItemList.Add(new SteelItem("Q345", ">16-35", "295", "170", "400"));
            SteelItemList.Add(new SteelItem("Q345", ">35-50", "265", "155", "400"));
            SteelItemList.Add(new SteelItem("Q345", ">50-100", "250", "145", "400"));
            SteelItemList.Add(new SteelItem("", "", "", "", ""));
            SteelItemList.Add(new SteelItem("Q390", "<=16", "350", "205", "415"));
            SteelItemList.Add(new SteelItem("Q390", ">16-35", "335", "190", "415"));
            SteelItemList.Add(new SteelItem("Q390", ">35-50", "315", "180", "415"));
            SteelItemList.Add(new SteelItem("Q390", ">50-100", "295", "170", "415"));
            SteelItemList.Add(new SteelItem("", "", "", "", ""));
            SteelItemList.Add(new SteelItem("Q420", "<=16", "380", "220", "440"));
            SteelItemList.Add(new SteelItem("Q420", ">16-35", "360", "210", "440"));
            SteelItemList.Add(new SteelItem("Q420", ">35-50", "340", "195", "440"));
            SteelItemList.Add(new SteelItem("Q420", ">50-100", "325", "185", "440"));
            SteelListBox.ItemsSource = SteelItemList;
        }
    }

    //混凝土条目类
    public class ConcreteItem
    {

        public ConcreteItem(string type, string fc, string ft, string alpha, string beta)
        {
            Type = type;
            Fc = fc;
            Ft = ft;
            Alpha = alpha;
            Beta = beta;
        }
        public string Type { get; set; }
        public string Fc { get; set; }
        public string Ft { get; set; }
        public string Alpha { get; set; }
        public string Beta { get; set; }
    }

    //钢筋条目类
    public class RebarItem
    {

        public RebarItem(string type, string dia, string fy, string ft, string es)
        {
            Type = type;
            Dia = dia;
            Fy = fy;
            Ft = ft;
            Es = es;
        }
        public string Type { get; set; }
        public string Dia { get; set; }
        public string Fy { get; set; }
        public string Ft { get; set; }
        public string Es { get; set; }
    }

    //钢材条目类
    public class SteelItem
    {

        public SteelItem(string type, string dia, string fy, string fv, string fce)
        {
            Type = type;
            Dia = dia;
            Fy = fy;
            Fv = fv;
            Fce = fce;
        }
        public string Type { get; set; }
        public string Dia { get; set; }
        public string Fy { get; set; }
        public string Fv { get; set; }
        public string Fce { get; set; }
    }

}
