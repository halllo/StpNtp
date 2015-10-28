using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodosFrontend
{
    public class TodoViewModel : ViewModel
    {
        public int Id { get; set; }
        bool _Erledigt;
        public bool Erledigt
        {
            get { return _Erledigt; }
            set { _Erledigt = value; NotifyChanged(); Dirty = true; }
        }
        string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; NotifyChanged(); Dirty = true; }
        }
        bool _Dirty;
        public bool Dirty
        {
            get { return _Dirty; }
            private set { _Dirty = value; NotifyChanged(); }
        }

        public TodoViewModel()
        {
            Dirty = true;
        }
        public TodoViewModel(TodoService.Todo todo)
        {
            Id = todo.Id;
            Erledigt = todo.Erledigt;
            Name = todo.Name;
            Dirty = false;
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
    }
}
