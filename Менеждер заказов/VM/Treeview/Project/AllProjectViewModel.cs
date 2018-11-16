using System.Collections.ObjectModel;
using System.Linq;
using Менеждер_заказов.ModelDB;

namespace Менеждер_заказов.Project
{
    public class AllProjectViewModel
    {
        readonly ReadOnlyCollection<ProjectLayer> _project;
        public AllProjectViewModel(MODBEntities db) => _project = new ReadOnlyCollection<ProjectLayer>(new[] { new ProjectLayer(db) });
        public ReadOnlyCollection<ProjectLayer> Regions => _project;
    }
}
