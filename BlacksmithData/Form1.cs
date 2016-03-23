using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace BlacksmithData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            RunDetectFiles(true);
        }

        private void RunDetectFiles(bool startup)
        {
            if (startup)
                loadingLbl.Visible = true;

            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Blacksmith Queries");
            List<string> ext = new List<string> { "XML", "FORM", "QUERY" };
            string[] files = GetFilesWithExtensions(path, ext);

            foreach (string file in files)
            {
                ListViewItem i = new ListViewItem(file);
                listView1.Items.Add(i);
            }

            if (startup)
                loadingLbl.Visible = false;
        }

        private string[] GetFilesWithExtensions(string path, List<string> extensions)
        {
            string[] allFilesInFolder = Directory.GetFiles(path);
            string[] selFiles = allFilesInFolder.Where(f => extensions.Contains(f.ToUpper().Split('.').Last())).ToArray();
            List<string> Files = new List<string>();

            foreach (string s in selFiles)
            {
                string[] splitted = s.Split('\\');
                Files.Add(splitted.Last());
            }

            return Files.ToArray();
        }
    }
}
