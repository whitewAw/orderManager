using AutoMapper;
using System;
using System.ComponentModel;
using System.Linq;
using Менеждер_заказов.Event;
using Менеждер_заказов.ModelDB;
using Менеждер_заказов.Service;

namespace Менеждер_заказов.VM.Elemaents.PerformerEl
{
    class PerformerViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ModelDB.Performer performer;
        private MODBEntities db;
        public PerformerViewModel(ModelDB.Performer performer, MODBEntities db)
        {
            this.performer = performer == null ? new ModelDB.Performer() : Mapper.Map<ModelDB.Performer, ModelDB.Performer>(performer);
            this.db = db;
            EventClass.getInstance().CancelEvent += Cancel;
            EventClass.getInstance().UpdateEvent += Update;
        }

        public int Id
        {
            get { return performer.Id; }
            set
            {
                if (performer.Id != value)
                {
                    performer.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        public string Picture
        {
            get { return $"/Picture/{performer.Picture}"; }
            set
            {
                if (performer.Picture != value)
                {
                    performer.Picture = value;
                    OnPropertyChanged("Picture");
                }
            }
        }
        public string Surname
        {
            get { return performer.Surname; }
            set
            {
                if (performer.Surname != value)
                {
                    performer.Surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }


        public string Name
        {
            get { return performer.Name; }
            set
            {
                if (performer.Name != value)
                {
                    performer.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Patronymic
        {
            get { return performer.Patronymic; }
            set
            {
                if (performer.Patronymic != value)
                {
                    performer.Patronymic = value;
                    OnPropertyChanged("Patronymic");
                }
            }
        }

        public string Contact
        {
            get { return performer.Contact; }
            set
            {
                if (performer.Contact != value)
                {
                    performer.Contact = value;
                    OnPropertyChanged("Contact");
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
                if (performer.Id == 0)
                    db.Performers.Add(performer);
                else
                    UpdateElementsInDb.Update(performer, key => key.Id, db);
                db.SaveChanges();
            }
        }


        public void Cancel()
        {
            this.performer = performer.Id == 0 ? new Performer() : Mapper.Map<Performer, Performer>(db.Performers.Where(p => p.Id == performer.Id).FirstOrDefault());
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
