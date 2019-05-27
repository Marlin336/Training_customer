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
	/// Логика взаимодействия для Main_win.xaml
	/// </summary>
	public partial class Main_win : Window
	{
		private bool logout = false;
		public Login_win super { get; }
		public int user_id { get; }
		public NpgsqlConnection conn;
		public Main_win(Login_win super, int id, string login, string pass)
		{
			this.super = super;
			InitializeComponent();
			user_id = id;
			conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;User Id=customer_" + login + ";Password=" + pass + ";Database=Training;");
			try
			{
				NpgsqlCommand comm = new NpgsqlCommand("select deposit from my_own_customer(" + user_id + ")", conn);
				conn.Open();
				l_bal.Content += comm.ExecuteScalar().ToString();
			}
			catch (NpgsqlException ex)
			{
				MessageBox.Show(ex.Message, "Ошибка на сервере", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
			}
			finally { conn.Close(); }
			FillSubTable();
			FillUnsubTable();
		}

		private void FillSubTable()
		{
			try
			{
				string sql = "select gv.id, gv.trainer_id, gv.trainer, gv.min_age, gv.max_age, gv.cost, gv.sub " +
				"from group_view_admin as gv, \"customer-customer_group\" where \"customer-customer_group\".id_group = gv.id and \"customer-customer_group\".id_customer = " + user_id;
				NpgsqlCommand comm = new NpgsqlCommand(sql, conn);
				conn.Open();
				NpgsqlDataReader reader = comm.ExecuteReader();
				for (int i = 0; reader.Read(); i++)
				{
					GroupList item = new GroupList(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetValue(3).ToString(), reader.GetValue(4).ToString(), reader.GetInt32(5), reader.GetInt32(6));
					dg_grlist.Items.Add(item);
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
			finally { conn.Close(); }
		}
		private void FillUnsubTable()
		{
			try
			{
				string sql = "select gv.id, gv.trainer_id, gv.trainer, gv.min_age, gv.max_age, gv.cost, gv.sub from group_view_admin as gv " +
				"except(select gv.id, gv.trainer_id, gv.trainer, gv.min_age, gv.max_age, gv.cost, gv.sub from group_view_admin as gv, \"customer-customer_group\" as ccg " +
				"where ccg.id_group = gv.id and ccg.id_customer = " + user_id + ")";
				NpgsqlCommand comm = new NpgsqlCommand(sql, conn);
				conn.Open();
				NpgsqlDataReader reader = comm.ExecuteReader();
				for (int i = 0; reader.Read(); i++)
				{
					int sub;
					try { sub = reader.GetInt32(6); } catch { sub = 0; }
					GroupList item = new GroupList(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetValue(3).ToString(), reader.GetValue(4).ToString(), reader.GetInt32(5), sub);
					dg_grunlist.Items.Add(item);
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
			finally { conn.Close(); }
		}

		public void UpdateSubTable()
		{
			dg_grlist.Items.Clear();
			FillSubTable();
		}
		public void UpdateUnsubTable()
		{
			dg_grunlist.Items.Clear();
			FillUnsubTable();
		}
		#region events
		private void B_prof_Click(object sender, RoutedEventArgs e)
		{
			Profile_win profile = new Profile_win(this);
			profile.Show();
		}

		private void B_exit_Click(object sender, RoutedEventArgs e)
		{
			logout = true;
			Close();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (logout)
			{
				if (MessageBox.Show("Вы действительно хотите выйти?", "Выйти?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
				{
					super.tb_log.Clear();
					super.tb_pass.Clear();
					super.Show();
				}
				else
				{
					logout = false;
					e.Cancel = true;
				}
			}
			else
			{
				if (MessageBox.Show("Вы действительно хотите закрыть приложение?", "Закрыть приложение?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
					Application.Current.Shutdown();
				else
					e.Cancel = true;
			}
		}

		private void B_log_Click(object sender, RoutedEventArgs e)
		{
			Log_win log = new Log_win(this);
			log.Show();
		}

		private void Dg_grlist_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			b_sub_info.IsEnabled = b_sub_trainer.IsEnabled = b_sub_unsub.IsEnabled = dg_grlist.SelectedCells.Count != 0;
		}

		private void Dg_grunlist_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			b_unsub_info.IsEnabled = b_unsub_trainer.IsEnabled = b_unsub_sub.IsEnabled = dg_grunlist.SelectedCells.Count != 0;
		}
		#endregion

		private void B_unsub_update_Click(object sender, RoutedEventArgs e)
		{
			UpdateUnsubTable();
		}
		private void B_sub_unsub_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				GroupList group = dg_grlist.SelectedItem as GroupList;
				string sql = "DELETE FROM \"customer-customer_group\" WHERE id_group = " + group.id + " and id_customer = " + user_id;
				NpgsqlCommand comm = new NpgsqlCommand(sql, conn);
				conn.Open();
				comm.ExecuteNonQuery();
				conn.Close();
				UpdateSubTable();
				UpdateUnsubTable();
			}
			catch (NpgsqlException ex)
			{
				MessageBox.Show(ex.Message, "Ошибка на сервере", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
			}
			finally { conn.Close(); }
		}
		private void B_unsub_sub_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				GroupList group = dg_grunlist.SelectedItem as GroupList;
				string sql = "INSERT INTO \"customer-customer_group\"(id_group, id_customer) VALUES(" + group.id + ", " + user_id + ");";
				NpgsqlCommand comm = new NpgsqlCommand(sql, conn);
				conn.Open();
				comm.ExecuteNonQuery();
				conn.Close();
				UpdateSubTable();
				UpdateUnsubTable();
			}
			catch (NpgsqlException ex)
			{
				MessageBox.Show(ex.Message, "Ошибка на сервере", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
			}
			finally { conn.Close(); }
		}

		private void B_sub_trainer_Click(object sender, RoutedEventArgs e)
		{
			GroupList group = dg_grlist.SelectedItem as GroupList;
			Trainer_win win = new Trainer_win(this, group.trainer_id);
			win.Show();
		}

		private void B_unsub_trainer_Click(object sender, RoutedEventArgs e)
		{
			GroupList group = dg_grunlist.SelectedItem as GroupList;
			Trainer_win win = new Trainer_win(this, group.trainer_id);
			win.Show();
		}

		private void B_unsub_info_Click(object sender, RoutedEventArgs e)
		{
			Group_win win = new Group_win(this, dg_grunlist.SelectedItem as GroupList);
			win.Show();
		}
	}
}
