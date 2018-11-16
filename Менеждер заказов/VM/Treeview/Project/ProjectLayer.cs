using System;
using Менеждер_заказов.Event;
using Менеждер_заказов.ModelDB;

namespace Менеждер_заказов.Project
{
    public class ProjectLayer : TreeViewItemViewModel, IDisposable
    {
        private MODBEntities db;
        public ProjectLayer(MODBEntities db)
            : base(null, true)
        {
            this.db = db;
            EventClass.getInstance().UpdateEvent += Update;
        }
        public string ProjectLayerName => "Проекты";
        protected override void LoadChildren()
        {
            foreach (ModelDB.Project project in db.Projects)
                base.Children.Add(new ProjectViewModel(project, this, db));
        }
        private void Update()
        {
            while (base.Children.Count > 0)
            {
                var el = base.Children[0];
                if (el is ProjectViewModel)
                    (el as ProjectViewModel).Dispose();
                base.Children.RemoveAt(0);
            }
            //while (base.Children.Count > 0)
            //    base.Children.RemoveAt(0);
            LoadChildren();
        }

        public void Dispose() => EventClass.getInstance().UpdateEvent -= Update;

    }
}
