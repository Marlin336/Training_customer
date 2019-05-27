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
using Npgsql;

namespace Training_customer
{
    /// <summary>
    /// Логика взаимодействия для Trainer_win.xaml
    /// </summary>
    public partial class Trainer_win : Window
    {
		Main_win super { get; }
        public Trainer_win(Main_win super, int id)
        {
            InitializeComponent();
			this.super = super;
			try
			{
				NpgsqlCommand comm = new NpgsqlCommand("select * from trainer_view_cust where id = " + id, super.conn);
				super.conn.Open();
				NpgsqlDataReader reader = comm.ExecuteReader();
				reader.Read();
				l_name.Content = reader.GetString(1) + " " + reader.GetString(2) + " " + reader.GetString(3);
				l_age.Content += reader.GetDouble(4).ToString();
				l_mail.Content += reader.GetValue(5).ToString();
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
    }
}
