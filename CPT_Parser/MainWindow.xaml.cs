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
            var treeEnumerator = treeElementsView.ItemsSource.GetEnumerator();
            treeEnumerator.MoveNext();
            var rootTreeElement = treeEnumerator.Current as TreeViewModel;
            var selectedNode = TreeViewModel.GetSelectedChildElements(rootTreeElement);
            string s = "Выбраны\r\n";
            foreach (var item in selectedNode)
                s += item + "\r\n";
            DataTextBox.Text = s;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            treeElementsView.ItemsSource = TreeViewModel.SetTree("Top Level" , elementsDataSet);           
            
        }

        private void ObjectElement_Selected(object sender, RoutedEventArgs e)
        {
            var tvItem = (Label)sender;
            DataTextBox.Text = elementsDataSet.getObject(tvItem.Content.ToString()).ToString();
        }
    }
}
