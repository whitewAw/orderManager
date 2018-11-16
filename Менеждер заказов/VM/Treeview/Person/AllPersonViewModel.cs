using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Менеждер_заказов.ModelDB;
using Менеждер_заказов.VM.Enum;

namespace Менеждер_заказов.VM.Person
{
    class AllPersonViewModel
    {
        readonly ReadOnlyCollection<PersonLayer> _project;
        public AllPersonViewModel(MODBEntities db)
        {
            _project = new ReadOnlyCollection<PersonLayer>(new[] { new PersonLayer(db, "Заказчики",  PersonEnum.Order), new PersonLayer(db, "Исполнители", PersonEnum.Performers) });
        }
        public ReadOnlyCollection<PersonLayer> Regions => _project;
    }
}
