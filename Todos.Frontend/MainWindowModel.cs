using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TodosFrontend
{
    public class MainWindowModel : ViewModel
    {
        ObservableCollection<TodoViewModel> _Todos;
        public ObservableCollection<TodoViewModel> Todos
        {
            get { return _Todos; }
            set { _Todos = value; NotifyChanged(); }
        }

        TodoViewModel _Ausgewählt;
        public TodoViewModel Ausgewählt
        {
            get { return _Ausgewählt; }
            set { _Ausgewählt = value; NotifyChanged(); }
        }

        public Command Speichern { get; set; }

        public Command Neu { get; set; }

        static List<TodoViewModel> Laden()
        {
            var todosService = new TodoService.TodoServiceClient();
            var todos = todosService.AllAsync().Result;
            return todos.Select(t => new TodoViewModel(t)).ToList();
        }

        public MainWindowModel()
        {
            Todos = new ObservableCollection<TodoViewModel>(Laden());

            Neu = new Command(() =>
            {
                Ausgewählt = new TodoViewModel()
                {
                    Name = "Neue Aufgabe"
                };
                Todos.Add(Ausgewählt);
            });

            Speichern = new Command(async () =>
            {
                var todosService = new TodoService.TodoServiceClient();
                try
                {
                    await todosService.SpeichernAsync(Todos.Union(new[] { Ausgewählt })
                                .Where(t => t != null && t.Dirty)
                                .Select(t => t.AsDto())
                                .ToArray());

                    Todos = new ObservableCollection<TodoViewModel>(Laden());
                }
                catch (Exception)
                {
                    var fe = FehlerEvent;
                    if (fe != null) fe();
                }
            });
        }

        public event Action FehlerEvent;
    }
}
