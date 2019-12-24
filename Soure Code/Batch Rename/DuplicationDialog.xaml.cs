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

namespace Batch_Rename
{
    /// <summary>
    /// Interaction logic for DuplicationDialog.xaml
    /// </summary>
    public partial class DuplicationDialog : Window
    {
        public int caseId;
        public DuplicationDialog()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            caseId = this.DuplicationCombobox.SelectedIndex;
            this.DialogResult = true;
        }
    }
}
