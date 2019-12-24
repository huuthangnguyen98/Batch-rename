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
    /// Interaction logic for MoveDialog.xaml
    /// </summary>
    public partial class MoveDialog : Window
    {
        public int start;
        public int end;
        public int caseId;

        public MoveDialog()
        {
            InitializeComponent();
        }
        public MoveDialog(int _caseId, int _start, int _end)
        {
            InitializeComponent();
            this.moveCombobox.SelectedIndex = _caseId;
            this.start = _start;
            this.end = _end;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            caseId = this.moveCombobox.SelectedIndex;
            if ((Int32.TryParse(this.startTextBox.Text, out start)) &&
                                (Int32.TryParse(this.endTextBox.Text, out end)))
            {
                if (start <= 0 || end <= 0)
                {
                    MessageBox.Show("Positions must be > 0", "Error");
                }
                else
                {
                    if (start <= end)
                    {
                        this.DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Start position must be lesser than End position", "Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("Positions must be numbers", "Error");
            }
                
        }
    }
}
