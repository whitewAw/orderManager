using AutoMapper;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Менеждер_заказов.Event;
using Менеждер_заказов.ModelDB;
using Менеждер_заказов.Service;

namespace Менеждер_заказов.VM.Elemaents.ProjectsEl
{
    public class ProjectViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ModelDB.Project project;

        private MODBEntities db;
        public ProjectViewModel(Менеждер_заказов.ModelDB.Project project, MODBEntities db)
        {
            this.project = project == null ? new ModelDB.Project() : Mapper.Map<ModelDB.Project, ModelDB.Project>(project);
            this.db = db;
            order = db.Orders.Local;
            completionStatu = db.CompletionStatus.Local;
            performer = db.Performers.Local;
            LoadCombBox();

            EventClass.getInstance().CancelEvent += Cancel;
            EventClass.getInstance().UpdateEvent += Update;

        }

        private void LoadCombBox()
        {
            if (project.Id != 0)
            {
                orderSelectedItem = project.Order;
                completionStatuSelectedItem = project.CompletionStatu;
                performerSelectedItem = project.Performer;
            }
            else
            {
                orderSelectedItem = new Order();
                completionStatuSelectedItem = new CompletionStatu();
                performerSelectedItem = new Performer();
            }
        }


        public int Id
        {
            get { return project.Id; }
            set
            {
                if (project.Id != value)
                {
                    project.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public string Name
        {
            get { return project.Name; }
            set
            {
                if (project.Name != value)
                {
                    project.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public DateTime? Deadline
        {
            get { return project.Deadline; }
            set
            {
                if (project.Deadline != value)
                {
                    project.Deadline = value;
                    OnPropertyChanged("Deadline");
                }
            }
        }
        public int? PlannedBudget
        {
            get { return project.PlannedBudget; }
            set
            {
                if (project.PlannedBudget != value)
                {
                    project.PlannedBudget = value;
                    OnPropertyChanged("PlannedBudget");
                }
            }
        }

        public int? ActualBudget
        {
            get { return project.ActualBudget; }
            set
            {
                if (project.ActualBudget != value)
                {
                    project.ActualBudget = value;
                    OnPropertyChanged("ActualBudget");
                }
            }
        }
        public int idOrder
        {
            get { return project.idOrder; }
            set
            {
                if (project.idOrder != value)
                {
                    project.idOrder = value;
                    OnPropertyChanged("idOrder");
                }
            }
        }

        public int idStatus
        {
            get { return project.idStatus; }
            set
            {
                if (project.idStatus != value)
                {
                    project.idStatus = value;
                    OnPropertyChanged("idStatus");
                }
            }
        }

        public int? idPerformer
        {
            get { return project.idPerformer; }
            set
            {
                if (project.idPerformer != value)
                {
                    project.idPerformer = value;
                    OnPropertyChanged("idPerformer");
                }
            }
        }

        private ObservableCollection<Order> order;
        public ObservableCollection<Order> Order
        {
            get { return order; }
            set { order = value; OnPropertyChanged("Order"); }
        }

        private Order orderSelectedItem;
        public Order OrderSelectedItem
        {
            get { return orderSelectedItem; }
            set {  orderSelectedItem = value; idOrder = value.Id; OnPropertyChanged("OrderSelectedItem"); }
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

        private Performer performerSelectedItem;
        public Performer PerformerSelectedItem
        {
            get { return performerSelectedItem; }
            set { performerSelectedItem = value; idPerformer = value.Id; OnPropertyChanged("PerformerSelectedItem"); }
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
                if (project.Id == 0)
                    db.Projects.Add(project);
                else
                    UpdateElementsInDb.Update(project, key => key.Id, db);
                db.SaveChanges();
            }
        }


        public void Cancel()
        {
            this.project = project.Id == 0 ? new ModelDB.Project() : Mapper.Map<ModelDB.Project, ModelDB.Project>(db.Projects.Where(p=>p.Id==project.Id).FirstOrDefault());
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
