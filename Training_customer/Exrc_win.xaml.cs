using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Training_customer
{
	/// <summary>
	/// Логика взаимодействия для Exrc_win.xaml
	/// </summary>
	public partial class Exrc_win : Window
    {
		private class XTreeViewItem : TreeViewItem
		{
			public int id { get; }
			public XTreeViewItem(int id, string header)
			{
				this.id = id;
				Header = header;
			}
		}

		Main_win super { get; }

        public Exrc_win(Main_win super, ExerciseList exercise)
        {
            InitializeComponent();
			this.super = super;
			tb_name.Text = exercise.name;
			Title = "Информация об упражнении";
			this.super = super;
			List<XTreeViewItem> groups = new List<XTreeViewItem>();
			string sql = "select group_id, group_name from muscle_view, \"exercise-muscle\" where \"exercise-muscle\".id_muscle = muscle_view.muscle_id and \"exercise-muscle\".id_exercise = " + exercise.id + " group by group_id, group_name";
			NpgsqlCommand comm = new NpgsqlCommand(sql, super.conn);
			try
			{
				super.conn.Open();
				NpgsqlDataReader reader = comm.ExecuteReader();
				for (int i = 0; reader.Read(); i++)
					groups.Add(new XTreeViewItem(reader.GetInt32(0), reader.GetString(1)));
				super.conn.Close();
				comm.CommandText = "Select group_id, muscle_id, muscle_name from muscle_view, \"exercise-muscle\" where \"exercise-muscle\".id_muscle = muscle_view.muscle_id and \"exercise-muscle\".id_exercise = " + exercise.id;
				super.conn.Open();
				reader = comm.ExecuteReader();
				for (int i = 0; reader.Read(); i++)
					groups.Find(gr => gr.id == reader.GetInt32(0)).Items.Add(new XTreeViewItem(reader.GetInt32(1), reader.GetString(2)));
				super.conn.Close();
				foreach (var item in groups)
					tv_main.Items.Add(item);
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
