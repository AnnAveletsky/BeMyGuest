//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BMG.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Occasion
    {
        public int Id { get; set; }
        public string IdUserTraveler { get; set; }
        public string IdUserHost { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public System.DateTime ArrivalDate { get; set; }
        public System.DateTime CheckOut { get; set; }
        public string CommentTreveler { get; set; }
        public string CommentHost { get; set; }
        public Nullable<int> IdTraveling { get; set; }
        public Nullable<int> IdPlace { get; set; }
        public string CommentPlaceHost { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
        public virtual Place Place { get; set; }
        public virtual Traveling Traveling { get; set; }
    }
}
