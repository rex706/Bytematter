using System;
using System.IO;
using System.Windows.Forms;

namespace FormatToHex
{
    public partial class MainForm : Form
    {
        private string path;
        private string text;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            //get file path from dialog
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            DialogResult res = openFileDialog1.ShowDialog();

            if (res == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
            }
            else if (res == DialogResult.Cancel)
                return;
            else
            {
                InfoLabel.ForeColor = System.Drawing.Color.Red;
                InfoLabel.Text = "Error opening text file";
                return;
            }

            InfoLabel.ForeColor = System.Drawing.Color.Orange;
            InfoLabel.Text = "Reading...";

            text = File.ReadAllText(path);
            text = text.Replace("\r\n", string.Empty);

            string[] bytes = text.Split(' ');

            if (FormatTypeCheckBox.Checked == true)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    if (i < (bytes.Length - 1))
                        bytes[i] = "0x" + bytes[i] + ", ";
                    else
                        bytes[i] = "0x" + bytes[i];
                }
            }
            else if (FormatTypeCheckBox.Checked == false)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    if (i < (bytes.Length - 1))
                        bytes[i] = bytes[i] + ", ";
                    else
                        bytes[i] = bytes[i];
                }
            }

            //check if hex files already exist
            string hexfilepath;
            int hexcount;

            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\hex.txt"))
            {
                hexcount = 1;

                while (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\hex(" + hexcount + ").txt"))
                {
                    hexcount++;
                }

                hexfilepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\hex(" + hexcount + ").txt";
            }
            else
                hexfilepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\hex.txt";

            //write file
            InfoLabel.Text = "Writing...";

            using (StreamWriter File2 = new StreamWriter(hexfilepath, true))
            {
                int count = 0;

                for (int i = 0; i < bytes.Length; i++)
                {
                    File2.Write(bytes[i]);
                    count++;

                    if (count == 16 && i < bytes.Length - 1)
                    {
                        File2.WriteLine();
                        count = 0;
                    }
                }
            }

            InfoLabel.ForeColor = System.Drawing.Color.Green;
            InfoLabel.Text = "Done!";
        }
    }
}
