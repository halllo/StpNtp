using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodosFrontendJOP
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			var objects = new System.Collections.ObjectModel.ObservableCollection<object>(Todo.NeuLaden());

			JustObjectsPrototype.Show.With(objects,
				types: new List<Type> { typeof(Todo) },
				settings: JustObjectsPrototype.UI.Settings.New(s =>
				{
					s.AllowDelete[typeof(Todo)] = false;
				}));
		}
	}
}
