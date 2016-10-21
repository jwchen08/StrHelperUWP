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
    public sealed partial class EarthquakePanel : UserControl
    {
        private ObservableCollection<LieduItem> LieduItemList = new ObservableCollection<LieduItem>();
        private ObservableCollection<EffectIndexItem> EffectIndexItemList = new ObservableCollection<EffectIndexItem>();
        private ObservableCollection<EigenPeriodItem> EigenPeriodItemList = new ObservableCollection<EigenPeriodItem>();
        private ObservableCollection<EffectIndexItem> JsdShichengItemList = new ObservableCollection<EffectIndexItem>();
        private ObservableCollection<WyjItem> ElasticWyjItemList = new ObservableCollection<WyjItem>();
        private ObservableCollection<WyjItem> PlasticWyjItemList = new ObservableCollection<WyjItem>();
        public EarthquakePanel()
        {
            this.InitializeComponent();
            this.Loaded += EarthquakePanel_Loaded;
        }

        private void EarthquakePanel_Loaded(object sender, RoutedEventArgs e)
        {
            LieduItemList.Add(new LieduItem("6度", "0.05g"));
            LieduItemList.Add(new LieduItem("7度", "0.10g"));
            LieduItemList.Add(new LieduItem("7.5度", "0.15g"));
            LieduItemList.Add(new LieduItem("8度", "0.20g"));
            LieduItemList.Add(new LieduItem("8.5度", "0.30g"));
            LieduItemList.Add(new LieduItem("9度", "0.40g"));
            LieduListBox.ItemsSource = LieduItemList;

            EffectIndexItemList.Add(new EffectIndexItem("6度", "0.04", "0.28"));
            EffectIndexItemList.Add(new EffectIndexItem("7度", "0.08", "0.50"));
            EffectIndexItemList.Add(new EffectIndexItem("7.5度", "0.12", "0.72"));
            EffectIndexItemList.Add(new EffectIndexItem("8度", "0.16", "0.90"));
            EffectIndexItemList.Add(new EffectIndexItem("8.5度", "0.24", "1.20"));
            EffectIndexItemList.Add(new EffectIndexItem("9度", "0.32", "1.40"));
            EffectIndexListBox.ItemsSource = EffectIndexItemList;

            EigenPeriodItemList.Add(new EigenPeriodItem("第一组", "0.20", "0.25", "0.35", "0.45", "0.65"));
            EigenPeriodItemList.Add(new EigenPeriodItem("第二组", "0.25", "0.30", "0.40", "0.55", "0.75"));
            EigenPeriodItemList.Add(new EigenPeriodItem("第三组", "0.30", "0.35", "0.45", "0.65", "0.90"));
            EigenPeriodListBox.ItemsSource = EigenPeriodItemList;

            JsdShichengItemList.Add(new EffectIndexItem("6度", "18", "125"));
            JsdShichengItemList.Add(new EffectIndexItem("7度", "35", "220"));
            JsdShichengItemList.Add(new EffectIndexItem("7.5度", "55", "310"));
            JsdShichengItemList.Add(new EffectIndexItem("8度", "70", "400"));
            JsdShichengItemList.Add(new EffectIndexItem("8.5度", "110", "510"));
            JsdShichengItemList.Add(new EffectIndexItem("9度", "140", "620"));
            JsdShichengListBox.ItemsSource = JsdShichengItemList;

            ElasticWyjItemList.Add(new WyjItem("钢筋混凝土框架", "1/550"));
            ElasticWyjItemList.Add(new WyjItem("钢筋混凝土框架-抗震墙、板柱-抗震墙、框架-核心筒", "1/800"));
            ElasticWyjItemList.Add(new WyjItem("钢筋混凝土抗震墙、筒中筒", "1/1000"));
            ElasticWyjItemList.Add(new WyjItem("钢筋混凝土框支层", "1/1000"));
            ElasticWyjItemList.Add(new WyjItem("多、高层钢结构", "1/250"));
            ElasticWyjListBox.ItemsSource = ElasticWyjItemList;

            PlasticWyjItemList.Add(new WyjItem("单层钢筋混凝土柱排架", "1/30"));
            PlasticWyjItemList.Add(new WyjItem("钢筋混凝土框架", "1/50"));
            PlasticWyjItemList.Add(new WyjItem("底部框架砌体房屋中的框架-抗震墙", "1/100"));
            PlasticWyjItemList.Add(new WyjItem("钢筋混凝土框架-抗震墙、板柱-抗震墙、框架-核心筒", "1/100"));
            PlasticWyjItemList.Add(new WyjItem("钢筋混凝土抗震墙、筒中筒", "1/120"));
            PlasticWyjItemList.Add(new WyjItem("多、高层钢结构", "1/50"));
            PlasticWyjListBox.ItemsSource = PlasticWyjItemList;
        }
    }

    //抗震设防烈度目类
    public class LieduItem
    {

        public LieduItem(string liedu, string acc)
        {
            Liedu = liedu;
            Acc = acc;
        }
        public string Liedu { get; set; }
        public string Acc { get; set; }
    }

    //水平地震力影响系数目类
    public class EffectIndexItem
    {

        public EffectIndexItem(string liedu, string duoyu, string hanyu)
        {
            Liedu = liedu;
            Duoyu = duoyu;
            Hanyu = hanyu;
        }
        public string Liedu { get; set; }
        public string Duoyu { get; set; }
        public string Hanyu { get; set; }
    }

    //特征周期目类
    public class EigenPeriodItem
    {

        public EigenPeriodItem(string groupNO, string p1, string p2, string p3, string p4, string p5)
        {
            GroupNO = groupNO;
            Period1 = p1;
            Period2 = p2;
            Period3 = p3;
            Period4 = p4;
            Period5 = p5;
        }
        public string GroupNO { get; set; }
        public string Period1 { get; set; }
        public string Period2 { get; set; }
        public string Period3 { get; set; }
        public string Period4 { get; set; }
        public string Period5 { get; set; }
    }

    //层间位移角目类
    public class WyjItem
    {

        public WyjItem(string type, string limit)
        {
            Type = type;
            Limit = limit;
        }
        public string Type { get; set; }
        public string Limit { get; set; }
    }

}
