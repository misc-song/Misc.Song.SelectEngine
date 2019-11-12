using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Misc.Song.SelectEngine.Common;
using Misc.Song.SelectEngine.Models;

namespace Misc.Song.SelectEngine.Controllers
{
    [EnableCors("AllowAll")]  //全局开启Cors进行跨域访问
    [Route("api/[controller]")]   //路由映射
    [ApiController]
    public class SearchController : ControllerBase
    {
        public BookContext sysDbContext { get; }
        //获取依赖注入容器中的数据库上下文
        public SearchController(BookContext _sysDbContext)
        {
            sysDbContext = _sysDbContext;
        }
        [HttpGet("GetResult")]  //指定get请求的方法名称 不使用restfull风格
        public IActionResult GetResult()
        {
            var keyword = HttpContext.Request.Query["keywords"].ToString();//根据query 获取传入的数据 其中httpcontext 是单次Http请求的上下文 使用上下文可以获取到前端传递过来的所有内容
            var pageSize = HttpContext.Request.Query["pageSize"].ToString();
            var pageIndex = HttpContext.Request.Query["pageIndex"].ToString();
            List<string> str = new List<string>();
            str.Add("parameter error");
            if (string.IsNullOrEmpty(pageSize) || string.IsNullOrEmpty(pageIndex))
            {
                return new JsonResult(new { data = str, returnCode = ErrorCode.ParameterError, totalNum = 0 });
            }
            keyword = keyword.Trim();
            int pindex = int.Parse(pageIndex);
            int psize = int.Parse(pageSize);
            if (string.IsNullOrEmpty(keyword))
            {
                var total = sysDbContext.Database.SqlQuery($"select count(*) from book.b2 where 1=1").Rows[0].ItemArray[0];
                var offset = psize * (pindex - 1);
                var temp2 = sysDbContext.b2.FromSql($"select * from ( SELECT * FROM book.b2 as book   where 1=1 limit {offset}, {psize} ) as t  order by(t.id)", offset, psize);
                return new JsonResult(new { result = temp2, total, returnCode = ErrorCode.Sucess }); 
            }
            else
            {
                //var temp = sysDbContext.b2.FromSql($"select * from book.b2 where title like '{ keyword }%' ", keyword);
                //获取总的记录数
                var total = sysDbContext.Database.SqlQuery($"select count(*) from book.b2 where title like '{ keyword }%'").Rows[0].ItemArray[0];
                var offset = psize * (pindex - 1);  //偏移量 跳过数据条数
                                                    //构建分页sql语句 （后期会使用 存储过程）
                var temp2 = sysDbContext.b2.FromSql($"select * from ( SELECT * FROM book.b2 as book   where title like '{keyword}%' limit {offset}, {psize} ) as t  order by(t.id)", keyword, offset, psize);
                //使用sql来计算count 增加查询效率
                //var res = temp.OrderBy(u => u.id).Skip(psize * (pindex - 1)).Take(psize);
                return new JsonResult(new { result = temp2, total, returnCode = ErrorCode.Sucess }); // 返回json数据
            }
            #region MyRegion
            //var temp = from i in sysDbContext.b2 where i.title.StartsWith("广") select i;
            //var temp = sysDbContext.b2.Where(u => u.title.StartsWith(keyword));
            //var dd = sysDbContext.Database.ExecuteSqlCommand($"explain select count(*) from book.b2 where title like '{ keyword }%'", keyword);
            //var totalNum = temp.Count(); 
            //string par = "select * from book.b2 where title like '%" + keyword + "%'";
            #endregion
        }
    }
}