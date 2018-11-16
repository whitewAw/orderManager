using AutoMapper;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Менеждер_заказов.Event;
using Менеждер_заказов.ModelDB;
using Менеждер_заказов.Service;

namespace Менеждер_заказов.VM.Elemaents.TaskEl
{
    class TaskViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ModelDB.Task task;

        private MODBEntities db;
        public TaskViewModel(Менеждер_заказов.ModelDB.Task task, MODBEntities db)
        {
            this.task = task == null ? new ModelDB.Task() : Mapper.Map<ModelDB.Task, ModelDB.Task>(task);
            this.db = db;
            completionStatu = db.CompletionStatus.Local;
            performer = new ObservableCollection<ModelDB.Performer>(task.TaskPerformers.Select(p=>p.Performer));
            LoadCombBox();

            EventClass.getInstance().CancelEvent += Cancel;
            EventClass.getInstance().UpdateEvent += Update;

        }

        private void LoadCombBox()
        {
            if (task.Id != 0)
            {
                completionStatuSelectedItem = task.CompletionStatu;
               
            }
            else
            {
                completionStatuSelectedItem = new CompletionStatu();
            }
        }

        public int Id
        {
            get { return task.Id; }
            set
            {
                if (task.Id != value)
                {
                    task.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public DateTime Deadline
        {
            get { return task.Deadline; }
            set
            {
                if (task.Deadline != value)
                {
                    task.Deadline = value;
                    OnPropertyChanged("Deadline");
                }
            }
        }

        public string Name
        {
            get { return task.Name; }
            set
            {
                if (task.Name != value)
                {
                    task.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public int idStatus
        {
            get { return task.idStatus; }
            set
            {
                if (task.idStatus != value)
                {
                    task.idStatus = value;
                    OnPropertyChanged("idStatus");
                }
            }
        }


        private ObservableCollection<CompletionStatu> completionStatu;
        public ObservableCollection<CompletionStatu> CompletionStatu
        {
            get { return completionStatu; }
            set { completionStatu = value; OnPropertyChanged("CompletionStatu"); }
        }

        private CompletionStatu completionStatuSelectedItem;
        public CompletionStatu CompletionStatuSelectedItem
        {
            get { return completionStatuSelectedItem; }
            set { completionStatuSelectedItem = value; idStatus = value.Id; OnPropertyChanged("CompletionStatuSelectedItem"); }
        }


        private ObservableCollection<Performer> performer;
        public ObservableCollection<Performer> Performer
        {
            get { return performer; }
            set { performer = value; OnPropertyChanged("Performer"); }
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
                if (task.Id == 0)
                    db.Tasks.Add(task);
                else
                    UpdateElementsInDb.Update(task, key => key.Id, db);
                db.SaveChanges();
            }
        }


        public void Cancel()
        {
            this.task = task.Id == 0 ? new ModelDB.Task() : Mapper.Map<ModelDB.Task, ModelDB.Task>(db.Tasks.Where(p => p.Id == task.Id).FirstOrDefault());
            LoadCombBox();
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
