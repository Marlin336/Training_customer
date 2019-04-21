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
	/// Логика взаимодействия для Main_win.xaml
	/// </summary>
	public partial class Main_win : Window
	{
		private bool logout = false;
		public Login_win super { get; }
		public Main_win(Login_win super)
		{
			this.super = super;
			InitializeComponent();
		}

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

		private void B_glist_Click(object sender, RoutedEventArgs e)
		{
			Groups_win groups = new Groups_win(this);
			groups.Show();
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
	}
}
