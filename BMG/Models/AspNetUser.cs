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
    
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            this.AspNetUserClaims = new HashSet<AspNetUserClaim>();
            this.AspNetUserLogins = new HashSet<AspNetUserLogin>();
            this.Messages = new HashSet<Message>();
            this.Messages1 = new HashSet<Message>();
            this.Occasions = new HashSet<Occasion>();
            this.Occasions1 = new HashSet<Occasion>();
            this.Places = new HashSet<Place>();
            this.AspNetRoles = new HashSet<AspNetRole>();
            this.Discussions = new HashSet<Discussion>();
            this.Groups = new HashSet<Group>();
            this.Occasions2 = new HashSet<Occasion>();
            this.Photos = new HashSet<Photo>();
            this.Places1 = new HashSet<Place>();
            this.Travelings = new HashSet<Traveling>();
        }
    
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTime> LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public Nullable<int> IdPhoto { get; set; }
        public Nullable<System.DateTime> DataBirthday { get; set; }
        public Nullable<int> IdCountry { get; set; }
        public Nullable<int> IdCity { get; set; }
    
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual Photo Photo { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Message> Messages1 { get; set; }
        public virtual ICollection<Occasion> Occasions { get; set; }
        public virtual ICollection<Occasion> Occasions1 { get; set; }
        public virtual ICollection<Place> Places { get; set; }
        public virtual ICollection<AspNetRole> AspNetRoles { get; set; }
        public virtual ICollection<Discussion> Discussions { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Occasion> Occasions2 { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<Place> Places1 { get; set; }
        public virtual ICollection<Traveling> Travelings { get; set; }
    }
}
