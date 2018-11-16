using AutoMapper;
using System;
using System.ComponentModel;
using System.Linq;
using Менеждер_заказов.Event;
using Менеждер_заказов.ModelDB;
using Менеждер_заказов.Service;

namespace Менеждер_заказов.VM.Elemaents.OrderEl
{
    class OrderViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ModelDB.Order order;

        private MODBEntities db;
        public OrderViewModel(ModelDB.Order order, MODBEntities db)
        {
            this.order = order == null ? new ModelDB.Order() : Mapper.Map<ModelDB.Order, ModelDB.Order>(order);
            this.db = db;
            EventClass.getInstance().CancelEvent += Cancel;
            EventClass.getInstance().UpdateEvent += Update;

        }

        public int Id
        {
            get { return order.Id; }
            set
            {
                if (order.Id != value)
                {
                    order.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public string Picture 
        {
            get { return $"/Picture/{order.Picture}"; }
            set
            {
                if (order.Picture != value)
                {
                    order.Picture = value;
                    OnPropertyChanged("Picture");
                }
            }
        }
        public string Surname
        {
            get { return order.Surname; }
            set
            {
                if (order.Surname != value)
                {
                    order.Surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }


        public string Name
        {
            get { return order.Name; }
            set
            {
                if (order.Name != value)
                {
                    order.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Patronymic
        {
            get { return order.Patronymic; }
            set
            {
                if (order.Patronymic != value)
                {
                    order.Patronymic = value;
                    OnPropertyChanged("Patronymic");
                }
            }
        }

        public string Address
        {
            get { return order.Address; }
            set
            {
                if (order.Address != value)
                {
                    order.Address = value;
                    OnPropertyChanged("Address");
                }
            }
        }

        public bool IsValid
        {
            get
            {
                return true;  //валидность
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
                EventClass.getInstance().RunDisable();
            }
        }


        public void Update()
        {
            if (IsValid)
            {
                if (order.Id == 0)
                    db.Orders.Add(order);
                else
                    UpdateElementsInDb.Update(order, key => key.Id, db);
                db.SaveChanges();
            }
        }


        public void Cancel()
        {
            this.order = order.Id == 0 ?new Order() : Mapper.Map<Order, Order>(db.Orders.Where(p => p.Id == order.Id).FirstOrDefault());
            foreach (var el in this.GetType().GetProperties())
            {
                OnPropertyChanged(el.Name);
            }
        }

        public void Dispose()
        {
            EventClass.getInstance().CancelEvent -= Cancel;
            EventClass.getInstance().UpdateEvent -= Update;
        }
    }
}
