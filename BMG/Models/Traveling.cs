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
    
    public partial class Traveling
    {
        public Traveling()
        {
            this.Occasions = new HashSet<Occasion>();
            this.AspNetUsers = new HashSet<AspNetUser>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> DateTimeComing { get; set; }
        public Nullable<System.DateTime> DateTimeDeparture { get; set; }
        public string Description { get; set; }
        public int IdCountry { get; set; }
        public int IdCity { get; set; }
        public string IdUserCreate { get; set; }
        public Nullable<int> IdPhoto { get; set; }
        public int IdDiscussion { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual Discussion Discussion { get; set; }
        public virtual ICollection<Occasion> Occasions { get; set; }
        public virtual Photo Photo { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}
