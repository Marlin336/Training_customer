using System.Windows;

namespace Training_customer
{
	/// <summary>
	/// Логика взаимодействия для Read_win.xaml
	/// </summary>
	public partial class Read_win : Window
    {
        public Read_win(string text)
        {
            InitializeComponent();
			tb.Text = text;
        }
    }
}
