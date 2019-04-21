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

namespace Training_customer
{
    /// <summary>
    /// Логика взаимодействия для Groups_win.xaml
    /// </summary>
    public partial class Groups_win : Window
    {
		public Main_win super { get; }
        public Groups_win(Main_win super)
        {
			this.super = super;
            InitializeComponent();
        }
    }
}
