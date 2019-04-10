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

namespace Training_customer
{
	/// <summary>
	/// Логика взаимодействия для Login_win.xaml
	/// </summary>
	public partial class Login_win : Window
	{
		public Login_win()
		{
			InitializeComponent();
		}

		private void Tb_log_TextChanged(object sender, TextChangedEventArgs e)
		{
			b_ent.IsEnabled = tb_log.Text != "" && tb_pass.Password != "";
		}

		private void Tb_pass_PasswordChanged(object sender, RoutedEventArgs e)
		{
			b_ent.IsEnabled = tb_log.Text != "" && tb_pass.Password != "";
		}

		private void B_reg_Click(object sender, RoutedEventArgs e)
		{
			Reg_win reg_Win = new Reg_win();
			reg_Win.Show();
		}

		private void B_ent_Click(object sender, RoutedEventArgs e)
		{
			if (/*Неверный логин*/false)
			{
				MessageBox.Show("Пользователя с таким логином не существует", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			else
			{
				if (/*Неверный пароль*/false)
				{
					MessageBox.Show("Пара логин-пароль не совпадают", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
				else
				{
					/*Переход к главной форме*/
				}
			}
		}
	}
}
