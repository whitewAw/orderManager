//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Менеждер_заказов.ModelDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class TasksInTheProject
    {
        public int Id { get; set; }
        public int idProject { get; set; }
        public int idTask { get; set; }
    
        public virtual Project Project { get; set; }
        public virtual Task Task { get; set; }
    }
}
