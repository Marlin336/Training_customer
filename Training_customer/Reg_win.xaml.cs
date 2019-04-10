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
using System.Text.RegularExpressions;

namespace Training_customer
{
	/// <summary>
	/// Логика взаимодействия для Reg_win.xaml
	/// </summary>
	public partial class Reg_win : Window
	{
		string phone = "";
		public Reg_win()
		{
			InitializeComponent();
		}

		private void tb_text_changed(object sender, TextChangedEventArgs e)
		{
			b_reg.IsEnabled = tb_name.Text != "" && tb_sname.Text != "" && tb_pname.Text != "" &&
				tb_login.Text != "" && tb_pass.Password != "" && tb_repass.Password != "" && new Regex(@"^\d{2}.\d{2}.\d{4}$").IsMatch(date_birth.Text);
		}

		private void Tb_pass_PasswordChanged(object sender, RoutedEventArgs e)
		{
			b_reg.IsEnabled = tb_name.Text != "" && tb_sname.Text != "" && tb_pname.Text != "" &&
				tb_login.Text != "" && tb_pass.Password != "" && tb_repass.Password != "" && new Regex(@"^\d{2}.\d{2}.\d{4}$").IsMatch(date_birth.Text);
		}

		private void B_reg_Click(object sender, RoutedEventArgs e)
		{
			if (tb_pass.Password == tb_repass.Password)
			{
				// TODO
				// Попытка регистрации
			}
			else
				MessageBox.Show("Пароли не совпадают", "Ошибка подтверждение пароля", MessageBoxButton.OK, MessageBoxImage.Warning);
		}

		private void Date_birth_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			b_reg.IsEnabled = tb_name.Text != "" && tb_sname.Text != "" && tb_pname.Text != "" &&
				tb_login.Text != "" && tb_pass.Password != "" && tb_repass.Password != "" && new Regex(@"^\d{2}.\d{2}.\d{4}$").IsMatch(date_birth.Text);
		}

		private void Tb_phone_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = "0123456789".IndexOf(e.Text) < 0 || tb_phone.Text.Length > 9;
		}
	}
}
