using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPT_Parser
{
    class SavedXMLToFile
    {
        public string FilePath { get; set; }

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
            using (FileStream fileStream = File.Create(FilePath))
            {
                byte[] array = Encoding.Default.GetBytes(text);
                fileStream.Write(array, 0, array.Length);
            }
        }


    }
}
