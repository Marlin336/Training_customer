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
    /// Логика взаимодействия для Group_win.xaml
    /// </summary>
    public partial class Group_win : Window
    {
		public Main_win super { get; }
		private bool[] week = new bool[7];
		private string[] timetable = new string[7];
		public List<ExerciseList> exerciseList = new List<ExerciseList>();
		private GroupList group { get; }

		public Group_win(Main_win super, GroupList group)
        {
            InitializeComponent();
			this.super = super;
			this.group = group;
			Title = "Тренер: " + group.trainer;
			NpgsqlCommand comm = new NpgsqlCommand("select * from customer_group where id = " + group.id, super.conn);
			try
			{
				super.conn.Open();
				NpgsqlDataReader reader = comm.ExecuteReader();
				reader.Read();
				var timestamps = (TimeSpan[])reader.GetValue(5);
				week = (bool[])reader.GetValue(6);
				for (int i = 0; i < timestamps.Length; i++)
				{
					if (week[i])
					{
						timetable[i] = timestamps[i].Hours.ToString() + ":" + (timestamps[i].Minutes.ToString().Length == 1 ? "0" + timestamps[i].Minutes.ToString() : timestamps[i].Minutes.ToString());
					}
				}
				int min_age_db;
				int max_age_db;
				try { min_age_db = reader.GetInt32(3); } catch { min_age_db = 0; }
				try { max_age_db = reader.GetInt32(4); } catch { max_age_db = 0; }
				num_cost.Text = reader.GetInt32(2).ToString();
				num_minage.Text = min_age_db.ToString();
				num_maxage.Text = max_age_db.ToString();
				cb_minage.IsChecked = !num_minage.Text.Equals("0");
				cb_maxage.IsChecked = !num_maxage.Text.Equals("0");

				super.conn.Close();
				comm.CommandText = "select exercise.id, exercise.name " +
				"from exercise, \"customer_group-exercise\" " +
				"where \"customer_group-exercise\".id_exercise = exercise.id and \"customer_group-exercise\".id_group = " + group.id;
				super.conn.Open();
				reader = comm.ExecuteReader();
				for (int i = 0; reader.Read(); i++)
				{
					exerciseList.Add(new ExerciseList(reader.GetInt32(0), reader.GetString(1)));
				}
				super.conn.Close();
				UpdateList();

				cb_mon.IsChecked = week[0];
				cb_tue.IsChecked = week[1];
				cb_wed.IsChecked = week[2];
				cb_thu.IsChecked = week[3];
				cb_fri.IsChecked = week[4];
				cb_sat.IsChecked = week[5];
				cb_sun.IsChecked = week[6];

				tp_mon.Text = timetable[0];
				tp_tue.Text = timetable[1];
				tp_wed.Text = timetable[2];
				tp_thu.Text = timetable[3];
				tp_fri.Text = timetable[4];
				tp_sat.Text = timetable[5];
				tp_sun.Text = timetable[6];
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

		public void UpdateList()
		{
			dg_list.Items.Clear();
			foreach (var item in exerciseList)
				dg_list.Items.Add(item);
		}

		private void Dg_list_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (dg_list.SelectedItem != null)
			{
				Exrc_win win = new Exrc_win(super, dg_list.SelectedItem as ExerciseList);
				win.Show();
			}
		}
	}
}
