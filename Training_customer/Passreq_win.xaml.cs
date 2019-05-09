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
    /// Логика взаимодействия для Passreq_win.xaml
    /// </summary>
    public partial class Passreq_win : Window
    {
		public Profile_win super { get; private set; }
        public Passreq_win(Profile_win super)
        {
            InitializeComponent();
			this.super = super;
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (tb_pass.Password == "0000")//Тестовый
			{
				ProfileEdit_win edit = new ProfileEdit_win(super);
				Close();
				edit.ShowDialog();
			}
			else
			{
				MessageBox.Show("Неверный пароль!", "Ошибка подтверждения", MessageBoxButton.OK, MessageBoxImage.Error);
				tb_pass.Clear();
			}
		}
	}
}
