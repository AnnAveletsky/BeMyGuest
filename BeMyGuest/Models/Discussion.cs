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
    
    public partial class Discussion
    {
        public Discussion()
        {
            this.MessagesInDiscussions = new HashSet<MessagesInDiscussion>();
            this.Groups = new HashSet<Group>();
            this.UserProfiles = new HashSet<UserProfile>();
        }
    
        public int Id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string Country { get; set; }
        public string Sity { get; set; }
        public System.DateTime DateTimeCreate { get; set; }
    
        public virtual ICollection<MessagesInDiscussion> MessagesInDiscussions { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
