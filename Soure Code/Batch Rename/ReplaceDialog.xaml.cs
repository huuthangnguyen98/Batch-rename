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
    /// Interaction logic for ReplaceDialog.xaml
    /// </summary>
    public partial class ReplaceDialog : Window
    {
        public string wordFrom;
        public string wordTo;
        public int caseId;

        public ReplaceDialog()
        {
            InitializeComponent();
        }

        public ReplaceDialog(string _wordFrom, string _wordTo, int _caseId)
        {
            InitializeComponent();
            this.replaceCombobox.SelectedIndex = _caseId;
            this.wordFromTextBox.Text = _wordFrom;
            this.wordToTextBox.Text = _wordTo;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            wordFrom = this.wordFromTextBox.Text;
            wordTo = this.wordToTextBox.Text;
            caseId = this.replaceCombobox.SelectedIndex;

            if (wordFrom == "")
            {
                MessageBox.Show("Word from can not be empty", "Error");
            }
            else
            {
                this.DialogResult = true;
            }
        }
    }
}
