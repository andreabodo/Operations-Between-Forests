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

namespace OperationsBetweenForests.Output
{
    /// <summary>
    /// Logica di interazione per InteractiveOutput.xaml
    /// </summary>
    public partial class InteractiveOutput : UserControl
    {
        public InteractiveOutput()
        {
            InitializeComponent();
        }

        private void Browser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key is Key.R)
            {
                ((InteractiveOutput)sender).Browser.Refresh();
            }
        }
    }
}
