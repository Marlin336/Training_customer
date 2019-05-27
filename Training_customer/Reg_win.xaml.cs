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
using Npgsql;

namespace Training_customer
{
	/// <summary>
	/// Логика взаимодействия для Reg_win.xaml
	/// </summary>
	public partial class Reg_win : Window
	{
		private bool edit { get; }
		public Reg_win(bool editing)
		{
			InitializeComponent();
			edit = editing;
			if (edit)
			{
				Title = "Редактирование";
				b_reg.Click += B_edit_Click;
			}
			else
				b_reg.Click += B_reg_Click;
		}

		private void B_reg_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				NpgsqlConnection conn = new NpgsqlConnection("Server = 127.0.0.1; Port = 5432; User Id = Training_login; Password = 0000; Database = Training; ");
				if (tb_pass.Password == tb_repass.Password)
				{
					string fname = tb_name.Text.Trim();
					string sname = tb_sname.Text.Trim();
					string pname = tb_pname.Text.Trim();
					string birth = date_birth.Text;
					string mail = tb_mail.Text.Trim();
					string login = tb_login.Text;
					string pass = tb_pass.Password;
					if (fname.Length == 0 || sname.Length == 0 || pname.Length == 0 || birth.Length == 0 || login.Length == 0 || pass.Length == 0)
					{
						throw new ArgumentNullException();
					}
					NpgsqlCommand comm = new NpgsqlCommand("INSERT INTO customer(first_name, second_name, parent_name, birthday, e_mail, login, pass)" +
					"VALUES('" + fname + "', '" + sname + "', '" + pname + "', '" + birth + "', '" + mail + "', '" + login + "', '" + pass + "'); ", conn);
					conn.Open();
					comm.ExecuteNonQuery();
					comm = new NpgsqlCommand("CREATE USER \"customer_" + login + "\" WITH LOGIN NOSUPERUSER NOCREATEDB NOCREATEROLE INHERIT NOREPLICATION CONNECTION LIMIT - 1 PASSWORD '" + pass + "';" +
					"GRANT \"Customer\" TO \"customer_" + login + "\"; ", conn);
					comm.ExecuteNonQuery();
					Close();
					conn.Close();
				}
				else
					MessageBox.Show("Пароли не совпадают", "Ошибка подтверждение пароля", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
			catch (ArgumentNullException)
			{
				MessageBox.Show("Необходимые поля не заполнены", null, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
			}
			catch (NpgsqlException ex)
			{
				MessageBox.Show(ex.Message, "Ошибка на сервере", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
			}
		}

		private void B_edit_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
