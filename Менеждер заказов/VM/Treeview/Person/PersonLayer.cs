using System;
using Менеждер_заказов.Event;
using Менеждер_заказов.ModelDB;
using Менеждер_заказов.Project;
using Менеждер_заказов.VM.Enum;

namespace Менеждер_заказов.VM.Person
{
    public class PersonLayer : TreeViewItemViewModel, IDisposable
    {
        private MODBEntities db;
        string name;
        public PersonEnum type{get; private set;}
        public PersonLayer(MODBEntities db, string name, PersonEnum type)
            : base(null, true)
        {
            this.name = name;
            this.db = db;
            this.type = type;
            EventClass.getInstance().UpdateEvent += Update;
        }
        public string PersonLayerName => name;

        protected override void LoadChildren()
        {
            if(type == PersonEnum.Order)
            {
                foreach (ModelDB.Order order in db.Orders)
                    base.Children.Add(new Orders.OrderViewModel(order, this, db));
            }

            if (type == PersonEnum.Performers)
            {
                foreach (ModelDB.Performer perfom in db.Performers)
                    base.Children.Add(new Performers.PerformerViewModel(perfom, this, db));
            }
        }

        private void Update()
        {
            while (base.Children.Count > 0)
            {
                var el = base.Children[0];
                if (el is Orders.OrderViewModel)
                    (el as Orders.OrderViewModel).Dispose();
                else if (el is Performers.PerformerViewModel)
                    (el as Performers.PerformerViewModel).Dispose();
                base.Children.RemoveAt(0);
            }
            //while (base.Children.Count > 0)
            //    base.Children.RemoveAt(0);
            LoadChildren();
        }

        public void Dispose() => EventClass.getInstance().UpdateEvent -= Update;
    }
}
