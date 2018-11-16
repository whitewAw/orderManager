using System;
using Менеждер_заказов.ModelDB;

namespace Менеждер_заказов.Project
{
    public class PerformerViewModel : TreeViewItemViewModel, IDisposable
    {
        public readonly Performer _performer;
        public PerformerViewModel(Performer performer, TaskViewModel parentState)
            : base(parentState, false) => _performer = performer;
        public string PerformerName => ($"{_performer.Surname} {_performer.Name} {_performer.Patronymic}").Trim();

        public void Dispose(){}
    }
}
