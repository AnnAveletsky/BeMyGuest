using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace BeMyGuest.Models
{
    public class OccasionModels:DbContext
    {
        public OccasionModels()
            : base("aspnet-BeMyGuest-20150425021944")
        {
        }

        public DbSet<Occasion> Occasions { get; set; }
        
    }
    [Table("Occasion")]
    public class Occasion
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdUserTraveler { get; set; }
        public int IdUserHost { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}