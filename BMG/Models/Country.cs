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
    
    public partial class Country
    {
        public Country()
        {
            this.Cities = new HashSet<City>();
            this.Places = new HashSet<Place>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Place> Places { get; set; }
    }
}
