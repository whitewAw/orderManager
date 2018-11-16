using System;
using System.Linq;
using Менеждер_заказов.Event;
using Менеждер_заказов.ModelDB;
using Менеждер_заказов.Project;

namespace Менеждер_заказов.VM.Person.Orders
{
    public class OrderViewModel : TreeViewItemViewModel, IDisposable
    {
        public readonly Order _order;
        private MODBEntities db;

        public OrderViewModel(Order order, PersonLayer parentState, MODBEntities db)
            : base(parentState, true)
        {
            _order = order;
            this.db = db;
            EventClass.getInstance().UpdateEvent += Update;
        }
        public string OrderName => ($"{_order.Surname} {_order.Name} {_order.Patronymic}").Trim();

        protected override void LoadChildren()
        {
            foreach (ModelDB.Project project in db.Projects.Where(p => p.idOrder == _order.Id))
                base.Children.Add(new VM.Person.Orders.ProjectViewModel(project, this));
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
