using Менеждер_заказов.ModelDB;

namespace Менеждер_заказов.Project
{
    class OrderViewModel : TreeViewItemViewModel
    {
        public readonly Order _order;
        public OrderViewModel(Order order, ProjectViewModel parentState)
            : base(parentState, false) => _order = order;
        public string OrderName => ($"{_order.Surname} {_order.Name} {_order.Patronymic}").Trim();
    }
}
