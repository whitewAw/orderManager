using System;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Менеждер_заказов.Event;
using Менеждер_заказов.ModelDB;
using Менеждер_заказов.Project;
using Менеждер_заказов.Service;
using Менеждер_заказов.VM.Person;

namespace Менеждер_заказов
{
    public partial class MainWindow : Window, IDisposable
    {
        MODBEntities db;

        private async void preLoadDb()
        {
            await db.Projects.LoadAsync();
            await db.Orders.LoadAsync();
            await db.Performers.LoadAsync();
            await db.CompletionStatus.LoadAsync();
            await db.Tasks.LoadAsync();
            await db.TaskPerformers.LoadAsync();
            await db.TasksInTheProjects.LoadAsync();
        }

        public MainWindow()
        {
            InitializeComponent();
            db = new MODBEntities();
            preLoadDb();
            MyMapper.Initialize();
            EventClass.getInstance().DisableEvent += Disabl;
            EventClass.getInstance().EnableEvent += Enabl;
        }

        public void Dispose()
        {
            EventClass.getInstance().DisableEvent -= Disabl;
            EventClass.getInstance().EnableEvent -= Enabl;
            if (db != null)
            {
                db.Dispose();
                db = null;
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e) => ((Button)sender).ContextMenu.IsOpen = true;

        private void AddBtnMenuProjecItem_Click(object sender, RoutedEventArgs e)
        {
            ProjectTC.Items.Insert(ProjectTC.Items.Count - 1, new TabItem
            {
                Header = createTab((sender as MenuItem).Header.ToString()),
                Content = new LoadOnProjectControl(db)
            });
            ProjectTC.SelectedIndex = ProjectTC.Items.Count - 2;
        }

        private void AddBtnMenuPersonItem_Click(object sender, RoutedEventArgs e)
        {
            ProjectTC.Items.Insert(ProjectTC.Items.Count - 1, new TabItem
            {
                Header = createTab((sender as MenuItem).Header.ToString()),
                Content = new LoadOnPersonControl(db)
            });
            ProjectTC.SelectedIndex = ProjectTC.Items.Count - 2;
        }

        private StackPanel createTab(string name)
        {
            string headerMenu = name;
            StackPanel stPan = new StackPanel { Orientation = Orientation.Horizontal };
            stPan.Children.Add(new TextBlock { Margin = new Thickness(5), Text = headerMenu, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center });
            Button btnTemp = new Button { Background = Brushes.Transparent, BorderBrush = new SolidColorBrush(Colors.Transparent) };
            btnTemp.Click += BtnTemp_Click;
            Grid ClosGread = new Grid();
            ClosGread.Children.Add(new Ellipse { Stroke = new SolidColorBrush { Color = Colors.Blue }, MinWidth = 20, MinHeight = 20 });
            ClosGread.Children.Add(new TextBlock { HorizontalAlignment = HorizontalAlignment.Center, Text = "X", TextAlignment = TextAlignment.Center, VerticalAlignment = VerticalAlignment.Center });
            btnTemp.Content = ClosGread;
            stPan.Children.Add(btnTemp);
            return stPan;
        }
        private void BtnTemp_Click(object sender, RoutedEventArgs e)
        {
            var per = (TabItem)((StackPanel)((Button)sender).Parent).Parent;
            ProjectTC.Items.Remove(per);
            if (per.Content is LoadOnProjectControl)
                (per.Content as LoadOnProjectControl).Dispose();
            if (per.Content is LoadOnPersonControl)
                (per.Content as LoadOnPersonControl).Dispose();

        }

        private void Disabl()
        {
            foreach (var el in ProjectTC.Items)
                (el as TabItem).IsEnabled = false;
        }

        private void Enabl()
        {
            foreach (var el in ProjectTC.Items)
                (el as TabItem).IsEnabled = true;
        }
    }
}
