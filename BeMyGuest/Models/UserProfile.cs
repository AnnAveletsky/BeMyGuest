//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BeMyGuest.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserProfile
    {
        public UserProfile()
        {
            this.Messages = new HashSet<Message>();
            this.Messages1 = new HashSet<Message>();
            this.Occasions = new HashSet<Occasion>();
            this.Occasions1 = new HashSet<Occasion>();
            this.Places = new HashSet<Place>();
            this.Discussions = new HashSet<Discussion>();
            this.Groups = new HashSet<Group>();
            this.Occasions2 = new HashSet<Occasion>();
            this.Photos = new HashSet<Photo>();
            this.Places1 = new HashSet<Place>();
            this.Travelings = new HashSet<Traveling>();
        }
    
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Country { get; set; }
        public string Sity { get; set; }
        public string NumberPasport { get; set; }
        public string Status { get; set; }
        public string NumberPhote { get; set; }
        public Nullable<int> IdPhoto { get; set; }
    
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Message> Messages1 { get; set; }
        public virtual ICollection<Occasion> Occasions { get; set; }
        public virtual ICollection<Occasion> Occasions1 { get; set; }
        public virtual Photo Photo { get; set; }
        public virtual ICollection<Place> Places { get; set; }
        public virtual ICollection<Discussion> Discussions { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Occasion> Occasions2 { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Place> Places1 { get; set; }
        public virtual ICollection<Traveling> Travelings { get; set; }
    }
}