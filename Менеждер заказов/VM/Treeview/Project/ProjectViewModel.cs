using System;
using System.Linq;
using Менеждер_заказов.Event;
using Менеждер_заказов.ModelDB;

namespace Менеждер_заказов.Project
{
    public class ProjectViewModel : TreeViewItemViewModel, IDisposable
    {
        public readonly ModelDB.Project _project;
        private MODBEntities db;

        public ProjectViewModel(ModelDB.Project project, ProjectLayer per, MODBEntities db)
            : base(per, true)
        {
            _project = project;
            this.db = db;
            EventClass.getInstance().UpdateEvent += Update;
        }

        public string ProjectName => _project.Name;
   
        protected override void LoadChildren()
        {

            base.Children.Add(new OrderViewModel(_project.Order, this));

            foreach (ModelDB.Task state in db.TasksInTheProjects.Where(p=>p.idProject== _project.Id).Select(p=>p.Task))
                base.Children.Add(new TaskViewModel(state, this, db));
        }
        private void Update()
        {
            //while (base.Children.Count > 0)
            //{
            //    var el = base.Children[0];
            //    if (el is TaskViewModel)
            //        (el as TaskViewModel).Dispose();
            //    base.Children.RemoveAt(0);
            //}

            while (base.Children.Count > 0)
                base.Children.RemoveAt(0);
            LoadChildren();
        }

        public void Dispose() => EventClass.getInstance().UpdateEvent -= Update;
    }
}
