using System;
using System.Windows;
using System.Windows.Controls;
using Менеждер_заказов.Event;
using Менеждер_заказов.ModelDB;
using Менеждер_заказов.VM.Elemaents.OrderEl;
using Менеждер_заказов.VM.Elemaents.PerformerEl;
using Менеждер_заказов.VM.Elemaents.ProjectsEl;
using Менеждер_заказов.VM.Elemaents.TaskEl;
using Менеждер_заказов.VM.Node;

namespace Менеждер_заказов.Project
{

    public partial class LoadOnProjectControl : UserControl, IDisposable
    {
        private MODBEntities db;
        public LoadOnProjectControl(MODBEntities db)
        {
            InitializeComponent();
            this.db = db;
            base.DataContext = new AllProjectViewModel(db); 
            TV.SelectedItemChanged += TV_SelectedItemChanged;
            //EventClass.getInstance().UpdateEvent += Update;
            EventClass.getInstance().DisableEvent += Disabl;
            EventClass.getInstance().EnableEvent += Enabl;
        }

        private void Update() => TV.Items.Refresh();

        public void Dispose()
        {
            EventClass.getInstance().UpdateEvent -= Update;
            EventClass.getInstance().DisableEvent -= Disabl;
            EventClass.getInstance().EnableEvent -= Enabl;
        }

        private void TV_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            while (InfoPanel.Children.Count > 0)
            {
                var el = InfoPanel.Children[0];
                InfoPanel.Children.RemoveAt(0);
                if (el is NodeProject)
                    (el as NodeProject).Dispose();
                else if (el is ProjectView)
                    (el as ProjectView).Dispose();
                else if (el is OrderView)
                    (el as OrderView).Dispose();
                else if (el is TaskView)
                    (el as TaskView).Dispose();
                else if (el is PerformerView)
                    (el as PerformerView).Dispose();
            }





            Object SelEl = ((TreeView)sender).SelectedItem;
            if (SelEl is ProjectLayer){InfoPanel.Children.Add(new NodeProject(db));}
            else if (SelEl is ProjectViewModel) {InfoPanel.Children.Add(new ProjectView(db, (SelEl as ProjectViewModel)._project.Id));}
            else if (SelEl is OrderViewModel) {InfoPanel.Children.Add(new OrderView(db, (SelEl as OrderViewModel)._order.Id));}
            else if (SelEl is TaskViewModel) {InfoPanel.Children.Add(new TaskView(db, (SelEl as TaskViewModel)._task.Id)); }
            else if (SelEl is PerformerViewModel) { InfoPanel.Children.Add(new PerformerView(db, (SelEl as PerformerViewModel)._performer.Id)); }

        }

        private void Disabl() => TV.IsEnabled = false;

        private void Enabl() => TV.IsEnabled = true;
    }
}
