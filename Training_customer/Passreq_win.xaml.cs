using Npgsql;
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
			try
			{
				NpgsqlCommand comm = new NpgsqlCommand("select pass from my_own_customer(" + super.super.user_id + ")", super.super.conn);
				super.super.conn.Open();
				string compare = comm.ExecuteScalar().ToString();
				if (tb_pass.Password == compare)
				{
					comm.CommandText = "select * from my_own_customer(" + super.super.user_id + ")";
					NpgsqlDataReader reader = comm.ExecuteReader();
					reader.Read();
					CustomList cust = new CustomList(reader.GetInt32(0), reader.GetString(2), reader.GetString(1), reader.GetString(3), reader.GetDate(4).ToString(), reader.GetValue(5).ToString(), reader.GetString(7), reader.GetString(8));
					Reg_win edit = new Reg_win(super, cust);
					Close();
					edit.ShowDialog();
				}
				else
				{
					MessageBox.Show("Неверный пароль!", "Ошибка подтверждения", MessageBoxButton.OK, MessageBoxImage.Error);
					tb_pass.Clear();
				}
			}
			catch (NpgsqlException ex)
			{
				MessageBox.Show(ex.Message, "Ошибка на сервере", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
			}
			finally { super.super.conn.Close(); }
		}
	}
}
