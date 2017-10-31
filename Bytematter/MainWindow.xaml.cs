using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Bytematter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string versionURL = "http://textuploader.com/dlkk3/raw";
        private static string[] compare;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Verion number from assembly
            var AssemblyVer = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            MenuItem ver = new MenuItem();
            MenuItem newExistMenuItem = (MenuItem)this.FileMenu.Items[2];
            ver.Header = "v" + AssemblyVer;
            ver.IsEnabled = false;
            newExistMenuItem.Items.Add(ver);

            // Check for a new version.
            if (await UpdateCheck.CheckForUpdate(versionURL) == 1)
            {
                // An update is available, but user has chosen not to update.
                ver.Header = "Update Available!";
                ver.Click += Ver_Click;
                ver.IsEnabled = true;
            }
        }

        private void LoadFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "txt files (*.txt)|*.txt";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            string filePath;

            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
            }
            else
            {
                return;
            }

            ConsoleBox.Text = File.ReadAllText(filePath);
        }

        private void SaveFile()
        {
            if (ConsoleBox.Text.Length > 0)
            {
                using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
                {
                    System.Windows.Forms.DialogResult result = dialog.ShowDialog();

                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        int fileCounter = 0;
                        string savePath = dialog.SelectedPath + "\\hex.txt";

                        while (File.Exists(savePath))
                        {
                            fileCounter++;
                            savePath = dialog.SelectedPath + "\\hex(" + fileCounter + ").txt";
                        }

                        File.WriteAllText(savePath, ConsoleBox.Text);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private void Convert()
        {
            if (ConsoleBox.Text.Length > 0)
            {
                string[] bytes = ConsoleBox.Text.Replace(Environment.NewLine, string.Empty).Split(' ');
                StringBuilder mask = new StringBuilder();

                ConsoleBox.Text = "";

                for (int i = 0; i < bytes.Length; i++)
                {
                    if ((bytes[i].Length != 2 || Int64.TryParse(bytes[i], System.Globalization.NumberStyles.HexNumber, null, out var n) == false))
                    {
                        MessageBox.Show("Invalid format detected: #" + (i + 1) + " '" + bytes[i] + "'");
                        ResetWindow();
                        return;
                    }

                    if (compare != null && compare[i].Length == 2 && Int64.TryParse(compare[i], System.Globalization.NumberStyles.HexNumber, null, out var k) == true && bytes[i] != compare[i])
                    {
                        mask.Append("?");
                    }
                    else if (compare != null && (compare[i].Length != 2 || Int64.TryParse(compare[i], System.Globalization.NumberStyles.HexNumber, null, out var m) == false))
                    {
                        MessageBox.Show("Invalid compare format detected: #" + (i + 1) + " '" + compare[i] + "'");
                        ResetWindow();
                        return;
                    }
                    else
                    {
                        mask.Append("x");
                    }

                    if (i < (bytes.Length - 1))
                    {
                        bytes[i] = "0x" + bytes[i] + ", ";

                        if ((i + 1) % Int64.Parse(NewLineTextBox.Text) == 0)
                        {
                            bytes[i] = bytes[i] + "\n";
                        }
                    }
                    else
                    {
                        bytes[i] = "0x" + bytes[i];
                    }

                    ConsoleBox.AppendText(bytes[i]);
                }

                if (MaskCheckBox.IsChecked == true)
                {
                    ConsoleBox.AppendText("\n\n" + mask.ToString());
                }
            }

            MaskCheckBox.IsChecked = false;
        }

        private void ResetWindow()
        {
            ConsoleBox.Text = "";
            compare = null;
            MaskCheckBox.IsChecked = false;
        }

        private void NewLineTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(NewLineTextBox.Text, out var n) == false || n < 1)
            {
                NewLineTextBox.Text = "1";
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadFile();
        }

        private void LoadMenuItem_Click(object sender, RoutedEventArgs e)
        {
            LoadFile();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFile();
        }

        private void SaveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SaveFile();
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            Convert();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/rex706/Bytematter");
        }

        private async void Ver_Click(object sender, RoutedEventArgs e)
        {
            await UpdateCheck.CheckForUpdate(versionURL);
        }

        private void MaskCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ComplexMaskCheckBox.IsEnabled = true;
        }

        private void MaskCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ComplexMaskCheckBox.IsChecked = false;
            ComplexMaskCheckBox.IsEnabled = false;
        }

        private void ComplexMaskCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            dialog.Filter = "txt files (*.txt)|*.txt";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;

            string filePath;

            if (dialog.ShowDialog() == true)
            {
                filePath = dialog.FileName;
            }
            else
            {
                ComplexMaskCheckBox.IsChecked = false;
                compare = null;
                return;
            }

            compare = File.ReadAllText(filePath).Split(' ');
        }

        private void ComplexMaskCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            compare = null;
        }
    }
}
