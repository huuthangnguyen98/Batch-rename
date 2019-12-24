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
    /// Interaction logic for NewCaseDialog.xaml
    /// </summary>
    public partial class NewCaseDialog : Window
    {
        public int caseId;
        public NewCaseDialog()
        {
            InitializeComponent();
        }
        public NewCaseDialog(int _caseId)
        {
            InitializeComponent();
            _caseId = newCaseCombobox.SelectedIndex;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            caseId = newCaseCombobox.SelectedIndex;
            this.DialogResult = true;
        }
    }
}
