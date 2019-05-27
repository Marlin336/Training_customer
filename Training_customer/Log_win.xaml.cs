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
    /// Логика взаимодействия для Log_win.xaml
    /// </summary>
    public partial class Log_win : Window
    {
		public Main_win super { get; private set; }
        public Log_win(Main_win super)
        {
            InitializeComponent();
			this.super = super;
			FillLogTable();
			FillTransactTable();
        }

		private void FillLogTable()
		{
			string sql = "select id, group_id, date, in_time, out_time, admin, trainer, transact_id from log_view where customer_id = " + super.user_id;
			NpgsqlCommand comm = new NpgsqlCommand(sql, super.conn);
			try
			{
				super.conn.Open();
				NpgsqlDataReader reader = comm.ExecuteReader();
				for (int i = 0; reader.Read(); i++)
				{
					dg_log.Items.Add(new LogList(reader.GetInt32(0), reader.GetInt32(1), reader.GetDate(2).ToString(), reader.GetValue(3).ToString(), reader.GetValue(4).ToString(), reader.GetString(5), reader.GetString(6), reader.GetInt32(7)));
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
			finally { super.conn.Close(); }
		}
		private void FillTransactTable()
		{
			string sql = "select id, admin, addition, date, time from transact_log_view_cust where customer_id = " + super.user_id;
			NpgsqlCommand comm = new NpgsqlCommand(sql, super.conn);
			try
			{
				super.conn.Open();
				NpgsqlDataReader reader = comm.ExecuteReader();
				for (int i = 0; reader.Read(); i++)
				{
					dg_trans.Items.Add(new TransactList(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetDate(3).ToString(), reader.GetValue(4).ToString()));
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
			finally { super.conn.Close(); }
		}

		private void Dg_log_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (dg_log.SelectedItem != null)
			{
				Dispatcher.BeginInvoke((Action)(() => tab_panel.SelectedItem = tab_transact));
				LogList selected = dg_log.SelectedItem as LogList;
				for (int i = 0; i < dg_trans.Items.Count; i++)
				{
					TransactList item = dg_trans.Items[i] as TransactList;
					if (item.id == selected.transact_id)
					{
						dg_trans.SelectedIndex = i;
						dg_trans.ScrollIntoView(dg_trans.Items[i]);
						break;
					}
				}
			}
		}
	}
}
