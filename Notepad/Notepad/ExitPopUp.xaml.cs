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
using System.Windows.Shapes;

namespace Notepad
{
    /// <summary>
    /// Interaction logic for ExitPopUp.xaml
    /// </summary>
    public partial class ExitPopUp : Window
    {
        public ExitPopUp()
        {
            InitializeComponent();
        }

        public static bool saveClicked = false;
        public static bool dontSaveClicked = false;
        public static bool cancelClicked = false;

        public void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            saveClicked = true;
            Close();
        }

        private void buttonDontSave_Click(object sender, RoutedEventArgs e)
        {
            dontSaveClicked = true;
            Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            cancelClicked = true;
            Close();
        }
    }
}
