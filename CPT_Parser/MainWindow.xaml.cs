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
            var selectedNodes = TreeViewModel.GetSelectedChildElements(rootTreeElement);
            string s = "Выбраны\r\n";
            foreach (var item in selectedNodes)
                s += item + "\r\n";
            DataTextBox.Text = s;

            SavedXMLFileDialog saved = new SavedXMLFileDialog();
            if (selectedNodes.Count>0 && saved.SaveFileDialog())
            {
                SaveElemtnts(saved.FilePath, selectedNodes);
            }
        }

        /// <summary>
        /// Сериализация и сохраннеие обьекта
        /// </summary>
        /// <param name="filePath">путь сохранения</param>
        /// <param name="selectedNodes">идентификаторы выбранных элементов</param>
        private void SaveElemtnts(string filePath, List<List<string>> selectedNodes)
        {
            // получаем поток, куда будем записывать сериализованный объект
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.ConformanceLevel = ConformanceLevel.Auto;
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(filePath, settings))
            {
                writer.WriteStartElement("CadastralObjects");
                foreach (var cadastralElements in selectedNodes)
                {
                    Type elementsType = elementsDataSet.getObject(cadastralElements.First()).GetType();
                    writer.WriteStartElement(elementsType.Name + "Objects");
                    foreach (var elementId in cadastralElements)
                    {
                        var element = elementsDataSet.getObject(elementId);
                        XmlSerializer formatter = new XmlSerializer(element.GetType());
                        formatter.Serialize(writer, element);
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            treeElementsView.ItemsSource = TreeViewModel.SetTree("All cadastral objects" , elementsDataSet);           
            
        }
        /// <summary>
        /// Вывод содержимого узла при двоймно клике
        /// </summary>
        /// <param name="sender">лейбл</param>
        /// <param name="e"></param>
        private void ObjectElement_DoubleClickSelected(object sender, RoutedEventArgs e)
        {
            var tvItem = (Label)sender;
            try
            {
                DataTextBox.Text = elementsDataSet.getObject(tvItem.Content.ToString()).ToString();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Для вывода информации выберите один объект, а не группу объектов", 
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
