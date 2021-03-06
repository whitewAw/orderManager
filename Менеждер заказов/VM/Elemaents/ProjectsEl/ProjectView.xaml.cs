﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Менеждер_заказов.Event;
using Менеждер_заказов.ModelDB;

namespace Менеждер_заказов.VM.Elemaents.ProjectsEl
{
    public partial class ProjectView : UserControl, IDisposable
    {
        private MODBEntities db;
        private ProjectViewModel model;

        public ProjectView(MODBEntities db, int id=0)
        {
            InitializeComponent();
            this.db = db;
            //EventClass.getInstance().CancelEvent += Cancel;
            //EventClass.getInstance().UpdateEvent += Update;
            model = new ProjectViewModel(db.Projects.Where(p => p.Id == id).FirstOrDefault(), db);
            this.DataContext = model;
            updateButton.IsEnabled = false;
            deleteButton.IsEnabled = false;
            EventClass.getInstance().DisableEvent += enableButton;
            EventClass.getInstance().EnableEvent += disableButton;

        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            EventClass.getInstance().RunUpdate();
            EventClass.getInstance().RunEnable();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            EventClass.getInstance().RunCancel();
            EventClass.getInstance().RunEnable();
        }

        private void Update()
        {
            EventClass.getInstance().RunUpdate();
            EventClass.getInstance().RunEnable();
        }

        private void Cancel()
        {
            EventClass.getInstance().RunCancel();
            EventClass.getInstance().RunEnable();
        }
        private void enableButton()
        {
            updateButton.IsEnabled = true;
            deleteButton.IsEnabled = true;
        }
        private void disableButton()
        {
            updateButton.IsEnabled = false;
            deleteButton.IsEnabled = false;
        }
        public void Dispose()
        {
            model.Dispose();
            EventClass.getInstance().DisableEvent -= enableButton;
            EventClass.getInstance().EnableEvent -= disableButton;
        }
    }
}
