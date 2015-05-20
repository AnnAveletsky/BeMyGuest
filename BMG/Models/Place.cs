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
    
    public partial class Place
    {
        public Place()
        {
            this.Occasions = new HashSet<Occasion>();
            this.UsersPlaces = new HashSet<UsersPlace>();
            this.Discussions = new HashSet<Discussion>();
            this.Photos = new HashSet<Photo>();
        }
    
        public int Id { get; set; }
        public string IdUser { get; set; }
        public string Adress { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int IdCountry { get; set; }
        public int IdCity { get; set; }
        public string Status { get; set; }
        public bool Main { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Occasion> Occasions { get; set; }
        public virtual ICollection<UsersPlace> UsersPlaces { get; set; }
        public virtual ICollection<Discussion> Discussions { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
