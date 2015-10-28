using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TodosFrontend
{
    public class Command : ICommand
    {
        Action _a;

        public Command(Action a)
        {
            _a = a;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _a();
        }
    }
}
