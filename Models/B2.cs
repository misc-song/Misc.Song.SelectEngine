using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Misc.Song.SelectEngine.Models
{
    public class B2
    {
        [Key]
        public int id { get; set; }
        [MaxLength(500)]
        public string url { get; set; }
        public string pic_url { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public string publish_time { get; set; }
        public string page_num { get; set; }
        public string publisher { get; set; }
        public string isbn { get; set; }
        public string desc1 { get; set; }
        public string desc2 { get; set; }
        public string price { get; set; }
        [MaxLength(255)]
        public string read_url { get; set; }
        [MaxLength(500)]
        public string t { get; set; }
    }
}
