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
    
    public partial class Discussion
    {
        public Discussion()
        {
            this.Photos = new HashSet<Photo>();
            this.Places = new HashSet<Place>();
            this.Travelings = new HashSet<Traveling>();
            this.UsersDiscussions = new HashSet<UsersDiscussion>();
        }
    
        public int Id { get; set; }
        public string IdUserCreate { get; set; }
        public string Title { get; set; }
        public System.DateTime DateTimeCreate { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Place> Places { get; set; }
        public virtual ICollection<Traveling> Travelings { get; set; }
        public virtual ICollection<UsersDiscussion> UsersDiscussions { get; set; }
        public virtual Place Place { get; set; }
    }
}
