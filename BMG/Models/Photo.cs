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
    
    public partial class Photo
    {
        public Photo()
        {
            this.Groups = new HashSet<Group>();
            this.Places = new HashSet<Place>();
            this.Travelings = new HashSet<Traveling>();
        }
    
        public int Id { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public string IdUserCreate { get; set; }
        public bool ContainsPhotoAlbum { get; set; }
        public int IdDiscussion { get; set; }
        public bool Main { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Discussion Discussion { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Place> Places { get; set; }
        public virtual ICollection<Traveling> Travelings { get; set; }
        public virtual Group Group { get; set; }
        public virtual Place Place { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
    }
}
