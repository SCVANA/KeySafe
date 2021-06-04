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

namespace KeySafe.View
{
    public partial class PwdGeneratorWindow : Window
    {
        public PwdGeneratorWindow()
        {
            InitializeComponent();
        }
        public List<bool> getPwd()
        {
            List<bool> pwdConfig = new List<bool>();
            pwdConfig.Add(upper.IsChecked == true ? true : false);
            pwdConfig.Add(lower.IsChecked == true ? true : false);
            pwdConfig.Add(special.IsChecked == true ? true : false);
            pwdConfig.Add(numbers.IsChecked == true ? true : false);
            pwdConfig.Add(spaces.IsChecked == true ? true : false);
            pwdConfig.Add(wishes.IsChecked == true ? true : false);
            return pwdConfig; 
        }
        private void acceptBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
