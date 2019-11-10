using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Misc.Song.SelectEngine.Models;

namespace Misc.Song.SelectEngine.Controllers
{
    [EnableCors("AllowAll")]  //全局开启Cors进行跨域访问
    [Route("api/[controller]")]   //路由映射
    [ApiController]
    public class BookDetailController : ControllerBase// Controller
    {
        public BookContext sysDbContext { get; }
        public BookDetailController(BookContext _sysDbContext)
        {
            sysDbContext = _sysDbContext;
        }

        public IActionResult GetDetail(int id)
        {
            var data = sysDbContext.b2.Where(i => i.id == id).FirstOrDefault();
            return new JsonResult(data);
        }
    }
}