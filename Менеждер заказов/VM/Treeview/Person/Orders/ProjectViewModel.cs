using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Менеждер_заказов.Project;

namespace Менеждер_заказов.VM.Person.Orders
{
    public class ProjectViewModel : TreeViewItemViewModel
    {
       public readonly ModelDB.Project _project;
        public ProjectViewModel(ModelDB.Project performer, OrderViewModel parentState)
            : base(parentState, false)
        {
            _project = performer;
        }
        public string PerformerName => _project.Name;
    }
}


