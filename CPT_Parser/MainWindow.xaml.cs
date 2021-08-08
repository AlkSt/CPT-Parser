using CPT_Parser.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;
using System.Xml.Serialization;

namespace CPT_Parser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Data class
        internal DataCadastralSet elementsDataSet;
        public MainWindow()
        {
            elementsDataSet = new DataCadastralSet();
            InitializeComponent();
        }

        /// <summary>
        /// Сохраенние всех выбранных объектов
        /// </summary>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var treeEnumerator = treeElementsView.ItemsSource.GetEnumerator();
            treeEnumerator.MoveNext();
            var rootTreeElement = treeEnumerator.Current as TreeViewModel;
            var selectedNodes = TreeViewModel.GetSelectedChildElements(rootTreeElement);
            SavedXMLToFile saved = new SavedXMLToFile();
            if (selectedNodes.Count > 0 && saved.SaveFileDialog())
            {
                var serializer = new Serializer(elementsDataSet);
                var serializedText = serializer.SerializeElemtnts(selectedNodes);
                saved.SaveElemtnts(serializedText);
            }
        }

        /// <summary>
        /// Вывод идентификаторов у всех выбранных обьектов
        /// </summary>
        private void PresentButton_Click(object sender, RoutedEventArgs e)
        {
            var treeEnumerator = treeElementsView.ItemsSource.GetEnumerator();
            treeEnumerator.MoveNext();
            var rootTreeElement = treeEnumerator.Current as TreeViewModel;
            var selectedNodes = TreeViewModel.GetSelectedChildElements(rootTreeElement);
            string s = "";
            foreach (var group in selectedNodes)
            {
                s += elementsDataSet.GetObjectById(group.First()).GetType().Name + "\r\n";
                foreach (var item in group)
                    s += item + ";   ";
                s += "\r\n";
            }
            s = s.Length > 0 ? s : "Не выбрано ни одного обьекта";
            MessageBox.Show(s);

        }
        /// <summary>
        /// Добавление данных об обьектах в впредставление
        /// </summary>>
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            elementsDataSet = ParseXML.ParsingData();
            treeElementsView.ItemsSource = TreeViewModel.SetTree("All cadastral objects", elementsDataSet);

        }
        /// <summary>
        /// Вывод содержимого узла при двоймно клике
        /// </summary>
        /// <param name="sender">Label</param>
        /// <param name="e"></param>
        private void ObjectElement_DoubleClickSelected(object sender, RoutedEventArgs e)
        {
            var tvItem = (Label)sender;
            try
            {
                DataTextBox.Text = elementsDataSet.GetObjectById(tvItem.Content.ToString()).ToString();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Для вывода информации выберите один объект, а не группу объектов",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
