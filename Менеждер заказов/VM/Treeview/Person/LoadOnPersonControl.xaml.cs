using System;
using System.Windows;
using System.Windows.Controls;
using Менеждер_заказов.Event;
using Менеждер_заказов.ModelDB;
using Менеждер_заказов.VM.Elemaents.OrderEl;
using Менеждер_заказов.VM.Elemaents.PerformerEl;
using Менеждер_заказов.VM.Elemaents.ProjectsEl;
using Менеждер_заказов.VM.Elemaents.TaskEl;
using Менеждер_заказов.VM.Enum;
using Менеждер_заказов.VM.Node;

namespace Менеждер_заказов.VM.Person
{
    public partial class LoadOnPersonControl : UserControl, IDisposable
    {
        private MODBEntities db;
        public LoadOnPersonControl(MODBEntities db)
        {
            InitializeComponent();
            this.db = db;
            base.DataContext = new AllPersonViewModel(db);
            TV.SelectedItemChanged += TV_SelectedItemChanged;
            EventClass.getInstance().UpdateEvent += Update;
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
            Object SelEl = ((TreeView)sender).SelectedItem;
            while (InfoPanel.Children.Count > 0)
            {
                var elR = InfoPanel.Children[0];
                InfoPanel.Children.RemoveAt(0);
                if (elR is NodeOrder)
                    (elR as NodeOrder).Dispose();
                if (elR is NodePerformers)
                    (elR as NodePerformers).Dispose();
                else if (elR is ProjectView)
                    (elR as ProjectView).Dispose();
                else if (elR is OrderView)
                    (elR as OrderView).Dispose();
                else if (elR is TaskView)
                    (elR as TaskView).Dispose();
                else if (elR is PerformerView)
                    (elR as PerformerView).Dispose();
            }

            if (SelEl is PersonLayer)
            {
                   PersonLayer el = SelEl as PersonLayer;
                    if (el.type == PersonEnum.Order) { InfoPanel.Children.Add(new NodeOrder(db)); }
                    else if (el.type == PersonEnum.Performers) { InfoPanel.Children.Add(new NodePerformers(db)); }
            }
            else if (SelEl is Orders.OrderViewModel) { InfoPanel.Children.Add(new OrderView(db, (SelEl as Orders.OrderViewModel)._order.Id)); }
            else if (SelEl is Orders.ProjectViewModel) { InfoPanel.Children.Add(new ProjectView(db, (SelEl as Orders.ProjectViewModel)._project.Id)); }
            else if (SelEl is Performers.PerformerViewModel) { InfoPanel.Children.Add(new PerformerView(db, (SelEl as Performers.PerformerViewModel)._performer.Id)); }
            else if (SelEl is Performers.TaskViewModel) { InfoPanel.Children.Add(new TaskView(db, (SelEl as Performers.TaskViewModel)._task.Id)); }
        }
        private void Disabl() => TV.IsEnabled = false;
        private void Enabl() => TV.IsEnabled = true;

    }
}
