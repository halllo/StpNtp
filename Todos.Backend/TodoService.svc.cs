using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TodosBackend
{
    [ServiceContract]
    public class TodoService
    {
        [OperationContract]
        public List<Todo> All()
        {
            using (var ctx = new TodoContext())
            {
                return ctx.Todos.ToList();
            }
        }

        [OperationContract]
        public void Speichern(Todo todo)
        {
            using (var ctx = new TodoContext())
            {
                ctx.Todos.Add(todo);
                ctx.SaveChanges();
            }
        }
    }
}
