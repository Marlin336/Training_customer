using Npgsql;
using System;
using System.Windows;

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
