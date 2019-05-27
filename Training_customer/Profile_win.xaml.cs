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
    /// Логика взаимодействия для Profile_win.xaml
    /// </summary>
    public partial class Profile_win : Window
    {
		public Main_win super { get; }
        public Profile_win(Main_win super)
        {
			this.super = super;
            InitializeComponent();
			try
			{
				NpgsqlCommand comm = new NpgsqlCommand("select sname, fname, pname, birthday, mail, login from my_own_customer(" + super.user_id + ")", super.conn);
				super.conn.Open();
				NpgsqlDataReader reader = comm.ExecuteReader();
				reader.Read();
				l_name.Content = reader.GetString(0) + " " + reader.GetString(1) + " " + reader.GetString(2);
				l_birth.Content += reader.GetDate(3).ToString();
				l_mail.Content += reader.GetValue(4).ToString();
				l_login.Content += reader.GetString(5);
			}
			catch (NpgsqlException ex)
			{
				MessageBox.Show(ex.Message, "Ошибка на сервере", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
			}
			finally { super.conn.Close(); }
		}

		private void B_edit_Click(object sender, RoutedEventArgs e)
		{
			Passreq_win passreq = new Passreq_win(this);
			passreq.Show();
		}
	}
}
