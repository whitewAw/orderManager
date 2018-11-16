using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Менеждер_заказов.Event;
using Менеждер_заказов.ModelDB;
using Менеждер_заказов.Project;

namespace Менеждер_заказов.VM.Person.Performers
{
    public class PerformerViewModel : TreeViewItemViewModel, IDisposable
    {
        public readonly Performer _performer;
        private MODBEntities db;

        public PerformerViewModel(Performer performer, PersonLayer parentState, MODBEntities db)
            : base(parentState, true)
        {
            _performer = performer;
            this.db = db;
            EventClass.getInstance().UpdateEvent += Update;
        }
        public string PerformeName => ($"{_performer.Surname} {_performer.Name} {_performer.Patronymic}").Trim();

        protected override void LoadChildren()
        {
            foreach (ModelDB.Task task in db.TaskPerformers.Where(p => p.idPerformer == _performer.Id).Select(p => p.Task))
                base.Children.Add(new VM.Person.Performers.TaskViewModel(task, this));
        }
        private void Update()
        {
            while (base.Children.Count > 0)
                base.Children.RemoveAt(0);
            LoadChildren();
        }
        public void Dispose() => EventClass.getInstance().UpdateEvent -= Update;

    }
}
