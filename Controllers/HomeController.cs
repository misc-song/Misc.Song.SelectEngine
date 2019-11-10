using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class HomeController : ControllerBase// Controller    其中webapi（网络接口）的controller 继承自ControllerBase        MVC（模型视图应用）的controller 继承自ControllerBase
    {
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return null;
        }
       
        public IActionResult Privacy()
        {
            return null;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return null;
        }
       
    }
}
