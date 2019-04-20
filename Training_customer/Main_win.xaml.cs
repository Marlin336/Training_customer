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
		public Login_win super;
		public Main_win(Login_win super)
		{
			this.super = super;
			InitializeComponent();
		}

		private void B_prof_Click(object sender, RoutedEventArgs e)
		{

		}

		private void B_exit_Click(object sender, RoutedEventArgs e)
		{
			logout = true;
			Close();
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			if (logout)
			{
				super.tb_log.Clear();
				super.tb_pass.Clear();
				super.Show();
			}
			else
			{
				Application.Current.Shutdown();
			}
		}

		private void B_glist_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
