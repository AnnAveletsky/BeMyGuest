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
    
    public partial class MessagesInDiscussion
    {
        public int Id { get; set; }
        public int IdDiscussion { get; set; }
        public string Messages { get; set; }
        public System.DateTime DataTimeCreate { get; set; }
    
        public virtual Discussion Discussion { get; set; }
    }
}