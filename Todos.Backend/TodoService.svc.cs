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
        public void Speichern(List<Todo> todos)
        {
            using (var ctx = new TodoContext())
            {
                foreach (var todo in todos)
                {
                    var entity = ctx.Entry(todo);
                    entity.State = todo.Id == 0 ? System.Data.Entity.EntityState.Added : System.Data.Entity.EntityState.Modified;
                }
                ctx.SaveChanges();
            }
        }
    }
}
