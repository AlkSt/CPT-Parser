using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CPT_Parser
{
    class SavedXMLToFile
    {
        public string FilePath { get; set; }

        private bool savedStatus;
        public SavedXMLToFile()
        {
            savedStatus = false;
        }
        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Cadastral objects";
            saveFileDialog.DefaultExt = ".xml";
            saveFileDialog.Filter = "XML-File | *.xml";
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }

        public void SaveElemtnts(string text)
        {
            try
            {
                using (FileStream fileStream = File.Create(FilePath))
                {
                    byte[] array = Encoding.Default.GetBytes(text);
                    fileStream.Write(array, 0, array.Length);
                }
                savedStatus = true;
            }
            catch
            {
                savedStatus = false;
            }
            StatusMesage();
        }

        private void StatusMesage()
        {
            if (savedStatus)
                MessageBox.Show("Файл успешно сохранен", "Сохранение",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Неизвестная ошибка. Обьекты не были сохранены.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
