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

namespace CPT_Parser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal Data elementsDataSet = new Data();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Parseable.ParsingData();
        }

        //public delegate void InitDel(object sender, RoutedEventArgs e);
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            elementsDataSet.UploadData();
            var lannds = elementsDataSet.getParcel();
            foreach (var elemId in lannds.Keys)
                InitItem(elemId, ParcelItem);

            var builds = elementsDataSet.getObjectRealty();
            foreach (var elemId in builds.Keys)
                InitItem(elemId, ObjectRealtyItem);

            var spatial = elementsDataSet.getSpatial();
            foreach (var elemId in spatial.Keys)
                InitItem(elemId, SpatialDataItem);

            var bound = elementsDataSet.getBound();
            foreach (var elemId in bound.Keys)
                InitItem(elemId, BoundItem);

            var zones = elementsDataSet.getZone();
            foreach (var elemId in zones.Keys)
                InitItem(elemId, ZoneItem);
            
        }

        public void InitItem(string header, TreeViewItem viewItem)
        {
            var item = new TreeViewItem(); 
            item.Header = header;
            item.AddHandler(MouseDoubleClickEvent, new RoutedEventHandler(ObjectElement_Selected));
            viewItem.Items.Add(item);
        }

        private void ObjectElement_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem tvItem = (TreeViewItem)sender;
            DataTextBox.Text = elementsDataSet.getObject(tvItem.Header.ToString()).ToString();
        }
    }
}
