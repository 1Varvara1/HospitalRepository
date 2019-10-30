using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;

namespace HospitalBLL.Infrastructure
{
    public class FileOpender
    {
        private Word.Application wordApp;

        public FileOpender()
        {
            wordApp = new Word.Application();
        
        }

        public void OpenFile(string path)
        {
            // Open template
            var wordDoc = wordApp.Documents.Open(path);
          //  wordDoc.Activate();
             wordApp.Visible = true;

        }
    }
}
