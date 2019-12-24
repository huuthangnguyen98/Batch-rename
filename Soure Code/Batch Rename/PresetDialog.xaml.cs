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
using System.Windows.Shapes;

namespace Batch_Rename
{
    /// <summary>
    /// Interaction logic for PresetDialog.xaml
    /// </summary>
    public partial class PresetDialog : Window
    {
        public string namePreset;
        public PresetDialog()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            namePreset = nameTextBox.Text.ToString();
            if (namePreset != null)
            {
                if (File.Exists($"Preset\\{namePreset}.txt"))
                {
                    System.Windows.MessageBox.Show("This name has already exists", "Error");
                }
                else
                {
                    this.DialogResult = true;
                }
            }
            else
            {
                MessageBox.Show("Please enter Name preset.");
            }
        }
    }
}
