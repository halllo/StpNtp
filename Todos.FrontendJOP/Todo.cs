using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodosFrontendJOP
{
	public class Todo
	{
		public int Id { get; private set; }
		public bool LokalGeändert { get; private set; }

		bool _Erledigt;
		public bool Erledigt
		{
			get { return _Erledigt; }
			set { _Erledigt = value; LokalGeändert = true; }
		}

		string _Name;
		public string Name
		{
			get { return _Name; }
			set { _Name = value; LokalGeändert = true; }
		}



		public Todo()
		{
			Name = "neue Aufgabe";

			LokalGeändert = true;
		}
		public Todo(TodoService.Todo todo)
		{
			Id = todo.Id;
			Erledigt = todo.Erledigt;
			Name = todo.Name;

			LokalGeändert = false;
		}
		public TodoService.Todo AsDto()
		{
			return new TodoService.Todo
			{
				Id = this.Id,
				Erledigt = this.Erledigt,
				Name = this.Name
			};
		}



		public static IEnumerable<Todo> NeuLaden(System.Collections.ObjectModel.ObservableCollection<Todo> todos = null)
		{
			if (todos != null) todos.Clear();

			var todoService = new TodoService.TodoServiceClient();
			var loadedTodos = todoService.All().Select(t => new Todo(t)).ToList();

			return loadedTodos;
		}

		public static IEnumerable<Todo> AllesSpeichern(System.Collections.ObjectModel.ObservableCollection<Todo> todos)
		{
			var savingTodos = todos.Where(t => t.LokalGeändert);

			var todoService = new TodoService.TodoServiceClient();
			todoService.Speichern(savingTodos.Select(t => t.AsDto()).ToArray());

			return NeuLaden(todos);
		}
	}
}
