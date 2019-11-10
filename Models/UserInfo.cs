using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Misc.Song.SelectEngine.Models
{
    public class UserInfo
    {
        [Key]
        public int uid { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }


    }
}
