using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for editPresetList.xaml
    /// </summary>
    public partial class editPresetList : Window
    {
        public BindingList <string> presets;

        public editPresetList(BindingList<string> _presets)
        {
            InitializeComponent();
            presets = _presets;
            
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            presetListView.ItemsSource = presets;
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            
            if (presetListView.SelectedItems.Count > 0)
            {
                var preset = presetListView.SelectedItem.ToString();
                presets.Remove(preset);
                var path = $"Preset\\{preset}.txt";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
           
        }
    }
}
