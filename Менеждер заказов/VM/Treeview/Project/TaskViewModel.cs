using System;
using System.Linq;
using Менеждер_заказов.Event;
using Менеждер_заказов.ModelDB;

namespace Менеждер_заказов.Project
{
    public class TaskViewModel : TreeViewItemViewModel, IDisposable
    {
        public readonly Task _task;
        private MODBEntities db;
        public TaskViewModel(Task task, ProjectViewModel parentRegion, MODBEntities db)
            : base(parentRegion, true)
        {
            this.db = db;
            _task = task;
            EventClass.getInstance().UpdateEvent += Update;
        }
        public string TaskName => _task.Name;
        protected override void LoadChildren()
        {
            foreach (Performer city in db.TaskPerformers.Where(p=>p.idTask==_task.Id).Select(p=>p.Performer))
                 base.Children.Add(new PerformerViewModel(city, this));
        }

        private void Update()
        {
            while (base.Children.Count > 0)
            {
                var el = base.Children[0];
                if (el is PerformerViewModel)
                    (el as PerformerViewModel).Dispose();
                base.Children.RemoveAt(0);
            }
            //while (base.Children.Count > 0)
            //    base.Children.RemoveAt(0);
            LoadChildren();
        }
        public void Dispose() => EventClass.getInstance().UpdateEvent -= Update;
    }
}
