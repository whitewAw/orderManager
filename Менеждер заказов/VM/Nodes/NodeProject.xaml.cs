using System;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using Менеждер_заказов.Event;
using Менеждер_заказов.ModelDB;

namespace Менеждер_заказов.VM.Node
{
    public partial class NodeProject : UserControl, IDisposable
    {
        private MODBEntities db;
        public NodeProject(MODBEntities db)
        {
            this.db = db;
            InitializeComponent();
            db.Projects.Load();
            EventClass.getInstance().CancelEvent += Cancel;
            EventClass.getInstance().UpdateEvent += Update;

            try
            {
                Grid.ItemsSource = db.Projects.Local.ToBindingList();
            }
            catch { EventClass.getInstance().RunCancel(); }  // на случай ввода не коректынх данных, лучше зделать через валидатор
            updateButton.IsEnabled = false;
            deleteButton.IsEnabled = false;
            Grid.CurrentCellChanged += Grid_CurrentCellChanged;
        }

        private void Grid_CurrentCellChanged(object sender, EventArgs e)
        {
            EventClass.getInstance().RunDisable();
            updateButton.IsEnabled = true;
            deleteButton.IsEnabled = true;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            EventClass.getInstance().RunUpdate();
            EventClass.getInstance().RunEnable();
            updateButton.IsEnabled = false;
            deleteButton.IsEnabled = false;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            EventClass.getInstance().RunCancel();
            EventClass.getInstance().RunEnable();
            updateButton.IsEnabled = false;
            deleteButton.IsEnabled = false;
        }

        private void Update()
        {
            try
            {
                db.SaveChanges();
            }
            catch { EventClass.getInstance().RunCancel(); }
        }

        private void Cancel()
        {
            foreach (var entry in db.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
            Grid.ItemsSource = null;
            Grid.ItemsSource = db.Projects.Local.ToBindingList();
        }


        public void Dispose()
        {
            EventClass.getInstance().CancelEvent -= Update;
            EventClass.getInstance().UpdateEvent -= Cancel;
        }
    }
}
