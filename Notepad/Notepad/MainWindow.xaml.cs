using Microsoft.Win32;
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
using System.IO;

namespace Notepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string directoryPath; // holds directory for the opened file
        private bool textChanged = false;

        private void buttonOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            bool? userClickedOK = dialog.ShowDialog();
            directoryPath = dialog.FileName;

            if (userClickedOK == true)
            {
                Stream fileStream = dialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    textBox.Text = reader.ReadToEnd();
                }
                fileStream.Close();
                textChanged = false;
            }
        }

        private void buttonSaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(directoryPath))
            {
                using (StreamWriter sw = new StreamWriter(directoryPath))
                {
                    sw.WriteAsync(textBox.Text);
                    sw.Close();
                }

                MessageBox.Show("Your file has been saved.");
            }
            else
            {
                buttonSaveAsNew_Click(sender, e);
            }

            textChanged = false;
        }

        private void buttonNewFile_Click(object sender, RoutedEventArgs e)
        {
            directoryPath = null;
            textBox.Text = "";
            MessageBox.Show("New file created!");
            textChanged = false;
        }

        private void buttonSaveAsNew_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            save.ShowDialog();

            if (save.FileName != "")
            {
                directoryPath = save.FileName;

                using (StreamWriter sw = new StreamWriter(directoryPath))
                {
                    sw.WriteAsync(textBox.Text);
                    sw.Close();
                }
                MessageBox.Show("Your file has been saved.");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (textChanged == true)
            {
                ExitPopUp check = new ExitPopUp();
                check.ShowDialog();
                bool okayToSave = ExitPopUp.saveClicked;
                bool dontSave = ExitPopUp.dontSaveClicked;

                if (okayToSave == true)
                {
                    if (File.Exists(directoryPath))
                    {
                        using (StreamWriter sw = new StreamWriter(directoryPath))
                        {
                            sw.WriteAsync(textBox.Text);
                            sw.Close();
                        }

                        MessageBox.Show("Your file has been saved.");
                    }
                    else
                    {
                        SaveFileDialog save = new SaveFileDialog();
                        save.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
                        save.ShowDialog();

                        if (save.FileName != "")
                        {
                            directoryPath = save.FileName;

                            using (StreamWriter sw = new StreamWriter(directoryPath))
                            {
                                sw.WriteAsync(textBox.Text);
                                sw.Close();
                            }
                            MessageBox.Show("Your file has been saved.");
                        }
                    }
                }
                else if (dontSave == true)
                {
                    check.Close();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            textChanged = true;
        }
    }
}
