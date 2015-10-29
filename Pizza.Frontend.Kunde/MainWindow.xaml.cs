using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pizza.Frontend.Kunde
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new MainWindowModel();
		}
	}

	public class MainWindowModel : ViewModel
	{
		public MainWindowModel()
		{
			var pizzaService = new PizzaService.PizzaServiceClient();
			
			Neuladen = new Command(() =>
			{
				Pizzen = new ObservableCollection<PizzaViewModel>(pizzaService.Alle().Select(p => new PizzaViewModel(p)));
				NotifyChanged("Pizzen");
				Ausgewählt = null;
			});

			Neuladen.Execute(null);
		}

		public ObservableCollection<PizzaViewModel> Pizzen { get; set; }

		PizzaViewModel _Ausgewählt;
		public PizzaViewModel Ausgewählt { get { return _Ausgewählt; } set { _Ausgewählt = value; NotifyChanged(); } }

		public Command Neuladen { get; set; }
	}

	public class PizzaViewModel : ViewModel
	{
		public int Id { get; set; }

		string _Name;
		public string Name { get { return _Name; } set { _Name = value; NotifyChanged(); } }

		decimal _Preis;
		public decimal Preis { get { return _Preis; } set { _Preis = value; NotifyChanged(); } }

		string[] _Zutaten;
		public string[] Zutaten { get { return _Zutaten; } set { _Zutaten = value; NotifyChanged(); } }

		public PizzaViewModel()
		{
			Id = 0;
			Name = "Neue Pizza";
			Zutaten = new string[0];
		}

		public PizzaViewModel(PizzaService.PizzaEntity pizza)
		{
			Id = pizza.Id;
			Name = pizza.Name;
			Preis = pizza.Preis;
			Zutaten = Arrayify(pizza.Zutaten);
		}

		public PizzaService.PizzaEntity AsEntity()
		{
			return new PizzaService.PizzaEntity
			{
				Id = Id,
				Name = Name,
				Preis = Preis,
				Zutaten = Stringify(Zutaten),
			};
		}

		private static string[] Arrayify(string s)
		{
			return s.Split(new[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries).ToArray();
		}
		private static string Stringify(string[] s)
		{
			return string.Join("\n", s);
		}
	}

	public class ViewModel : INotifyPropertyChanged
	{
		public void NotifyChanged([CallerMemberName]string membername = null)
		{
			var pc = PropertyChanged;
			if (pc != null)
			{
				pc(this, new PropertyChangedEventArgs(membername));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}

	public class Command : ICommand
	{
		Action _action;
		public Command(Action action)
		{
			_action = action;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			_action();
		}
	}
}
