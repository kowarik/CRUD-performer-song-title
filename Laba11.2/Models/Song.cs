//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Laba11._2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Song
    {
        public int IdSong { get; set; }
        public int IdPerformer { get; set; }
        public int IdSongTitle { get; set; }
    
        public virtual Performer Performer { get; set; }
        public virtual SongTitle SongTitle { get; set; }
    }
}
