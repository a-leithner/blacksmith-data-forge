using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace BlacksmithData
{
    public partial class QueryHost : Form
    {
        public QueryHost(string path)
        {
            InitializeComponent();

            Text = "Blacksmith Query Host - " + path;
            RunReadXML(path);
        }

        private void RunReadXML(string XMLFileToParse)
        {
            List<string> Keys = null;
            List<string> Values = null;

            XmlTextReader txtReader = new XmlTextReader(XMLFileToParse);
            while (txtReader.Read())
            {
                XmlNodeType nType = txtReader.NodeType;

                if (nType == XmlNodeType.Element)
                {
                    string name = txtReader.Name.ToString();
                    if (name == "control")
                    {
                        Keys = new List<string>();
                        Values = new List<string>();

                        Keys.Add("name");
                        Values.Add(txtReader.GetAttribute("name"));

                        Keys.Add("type");
                        Values.Add(txtReader.GetAttribute("type"));

                        Keys.Add("text");
                        Values.Add(txtReader.GetAttribute("text"));

                        Keys.Add("font");
                        Values.Add(txtReader.GetAttribute("font"));

                        Keys.Add("weight");
                        Values.Add(txtReader.GetAttribute("weight"));

                        Keys.Add("fontsize");
                        Values.Add(txtReader.GetAttribute("fontsize"));

                        Keys.Add("data-binding");
                        Values.Add(txtReader.GetAttribute("binding"));

                        Keys.Add("size");
                        Values.Add(txtReader.GetAttribute("size"));

                        Keys.Add("position");
                        Values.Add(txtReader.GetAttribute("position"));
                    }
                }
                else if (nType == XmlNodeType.Attribute)
                {
                    MessageBox.Show("ATTRIBUTE!");

                    if (Keys != null)
                    {
                        MessageBox.Show("KEYS != null");
                        Keys.Add(txtReader.Name.ToString());
                        Values.Add(txtReader.Value.ToString());
                    }
                }
                else if (nType == XmlNodeType.EndElement)
                {
                    if (txtReader.Name.ToString() == "control")
                    {
                        RunCreateControlViaKeyAndValueList(Keys, Values);
                        Keys = null;
                        Values = null;
                    }
                }
            }
        }

        private void RunCreateControlViaKeyAndValueList(List<string> keys, List<string> values)
        {
            string name = values[keys.IndexOf("name")];
            string type = values[keys.IndexOf("type")];
            string text = values[keys.IndexOf("text")];
            string font = values[keys.IndexOf("font")];
            string weig = values[keys.IndexOf("weight")];
            string fsiz = values[keys.IndexOf("fontsize")];
            string bind = values[keys.IndexOf("data-binding")];
            string size = values[keys.IndexOf("size")];
            string posi = values[keys.IndexOf("position")];
            string[] SIZE = size.Split(',');
            string[] POSI = posi.Split(',');
            FontStyle fs = new FontStyle();

            Control ctrl = null;

            switch (type)
            {
                case "Button":
                    ctrl = new Button();
                    break;
                case "Entry":
                    ctrl = new TextBox();
                    break;
                case "Label":
                    ctrl = new Label();
                    break;
                default:
                    MessageBox.Show("No recognized Type found in Object \"" + name + "\"!", "Parser Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    break;
            }

            switch (weig)
            {
                case "Regular":
                    fs = FontStyle.Regular;
                    break;
                case "Bold":
                    fs = FontStyle.Bold;
                    break;
                case "Italic":
                    fs = FontStyle.Italic;
                    break;
                case "Strikeout":
                    fs = FontStyle.Strikeout;
                    break;
                case "Underline":
                    fs = FontStyle.Underline;
                    break;
                default:
                    MessageBox.Show("No recognized font weight found in Object \"" + name + "\"!", "Parser Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            ctrl.Font = new Font(font, Convert.ToInt16(fsiz), fs);
            ctrl.Text = text;
            ctrl.Location = new Point(Convert.ToInt16(POSI[0]), Convert.ToInt16(POSI[1]));
            ctrl.Size = new Size(Convert.ToInt16(SIZE[0]), Convert.ToInt16(SIZE[1]));

            Controls.Add(ctrl);
        }
    }
}
