using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Менеждер_заказов.Project;

namespace Менеждер_заказов.VM.Person.Performers
{
    public class TaskViewModel : TreeViewItemViewModel
    {
        public readonly ModelDB.Task _task;
        public TaskViewModel(ModelDB.Task task, PerformerViewModel parentRegion)
            : base(parentRegion, false)
        {
            _task = task;
        }
        public string TaskName => _task.Name;
   
    }
}
