using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misc.Song.SelectEngine.Filter;
using Misc.Song.SelectEngine.Models;

namespace Misc.Song.SelectEngine.Controllers
{
    [EnableCors("AllowAll")]  //全局开启Cors进行跨域访问
    [Route("api/[controller]")]   //路由映射
    [ApiController]
    public class AdminController : ControllerBase // Controller 
    {
        public BookContext sysDbContext { get; }
        public AdminController(BookContext _sysDbContext)
        {
            sysDbContext = _sysDbContext;
        }
        public IActionResult Index()
        {
            return null;
        }

        public IActionResult CheckLogin()
        {
            var account = HttpContext.Request.Form["account"].ToString();
            var pwd = HttpContext.Request.Form["pwd"].ToString();
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(pwd))
            {
                return new JsonResult(new { res = "用户名或密码不能为空" });
            }
            var i = sysDbContext.UserInfos.Where(u => u.Account == account && u.Password == pwd).FirstOrDefault();
            if (i == null)
            {
                return new JsonResult(new { res = "用户名或者密码错误" });
            }
            else
            {
                HttpContext.Session.SetString("uid", i.Account);
                HttpContext.Session.SetString("upwd", i.Password);
                return new JsonResult(new { res = "ok" });
            }
        }

        //过滤器
        [AdminFilter]
        public IActionResult AdminPage()
        {
            return  null;
        }
        public IActionResult LoadB2Data(string page, string rows)
        {
            int pageIndex = page != null ? int.Parse(page) : 1;
            int pageSize = rows != null ? int.Parse(rows) : 5;


            var res = sysDbContext.b2.Where(u => true).OrderBy(u => u.id).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
            int totalCount = res.Count();
            return new JsonResult(new { rows = res, total = totalCount });
        }

        //public IActionResult AddB2(string word, string board, string xaxis, string yaxis)
        //{
        //    if (string.IsNullOrEmpty(word) || string.IsNullOrEmpty(board) || string.IsNullOrEmpty(xaxis) || string.IsNullOrEmpty(yaxis))
        //    {
        //        return new JsonResult(new { res = "参数错误，参数不能为空" });
        //    }
        //    B2 B2 = new B2()
        //    {
        //        board = int.Parse(board),
        //        name = word,
        //        Xaxis = int.Parse(xaxis),
        //        Yaxis = int.Parse(yaxis),
        //    };
        //    sysDbContext.B2.Add(B2);
        //    if (sysDbContext.SaveChanges() > 0)
        //    {
        //        return new JsonResult(new { res = "ok" });
        //    }
        //    else
        //    {
        //        return new JsonResult(new { res = "no" });
        //    }
        //}
        //public IActionResult DeleteB2()
        //{
        //    string strId = HttpContext.Request.Form["strId"].ToString();
        //    string[] strIds = strId.Split(',');
        //    List<B2> list = new List<B2>();
        //    foreach (string id in strIds)
        //    {
        //        //list.Add(int.Parse(id));
        //        list.Add(sysDbContext.B2.Where(u => u.id == int.Parse(id)).FirstOrDefault());
        //    }
        //    sysDbContext.B2.RemoveRange(list.ToArray());
        //    var ok = sysDbContext.SaveChanges() > 0;
        //    if (ok)
        //    {
        //        return Content("ok");
        //    }
        //    else
        //    {
        //        return Content("no");
        //    }
        //}
        //public IActionResult GetB2ById(string id)
        //{
        //    if (string.IsNullOrEmpty(id))
        //    {
        //        return new JsonResult(new { res = "参数错误，参数不能为空" });
        //    }
        //    var res = sysDbContext.B2.Where(u => u.id == int.Parse(id)).FirstOrDefault();
        //    return new JsonResult(res);
        //}

        //public IActionResult UpdateB2(string id, string word, string board, string xaxis, string yaxis)
        //{

        //    if (string.IsNullOrEmpty(word) || string.IsNullOrEmpty(board) || string.IsNullOrEmpty(xaxis) || string.IsNullOrEmpty(yaxis))
        //    {
        //        return new JsonResult(new { res = "参数错误，参数不能为空" });
        //    }
        //    B2 B2 = new B2()
        //    {
        //        id = int.Parse(id),
        //        board = int.Parse(board),
        //        name = word,
        //        Xaxis = int.Parse(xaxis),
        //        Yaxis = int.Parse(yaxis),
        //    };
        //    sysDbContext.B2.Update(B2);
        //    if (sysDbContext.SaveChanges() > 0)
        //    {
        //        return new JsonResult(new { res = "ok" });
        //    }
        //    else
        //    {
        //        return new JsonResult(new { res = "no" });
        //    }
        //}
        ////批量添加数据 重复的数据将(更新或者不做修改) 根据用户的勾选状态确定
        //public IActionResult AddRangeByCSVFile()
        //{
        //    var httpRequestFile = Request.Form.Files;
        //    string checkbox = Request.Form["checkBox"].ToString();
        //    string fileName = System.AppDomain.CurrentDomain.BaseDirectory + "Uploads\\imgs\\" + System.Guid.NewGuid().ToString() + ".csv";//拼接文件名称
        //    using (FileStream fs = System.IO.File.Create(fileName))
        //    {
        //        httpRequestFile[0].CopyTo(fs);
        //        fs.Flush();
        //    }
        //    List<B2> B2 = new List<B2>();


        //    StreamReader reader = new StreamReader(fileName, Encoding.Default);
        //    var line = reader.ReadLine();
        //    while (line != null)
        //    {
        //        var temp = line.Split(',');
        //        B2 B2 = new B2()
        //        {
        //            name = temp[0],
        //            board = int.Parse(temp[1]),
        //            Xaxis = int.Parse(temp[2]),
        //            Yaxis = int.Parse(temp[3]),
        //        };
        //        B2.Add(B2);
        //        line = reader.ReadLine();
        //    }
        //    if (checkbox == "true")
        //    {
        //        var AllData = sysDbContext.B2.Where(u => true).ToList();//全部数据


        //        //重复数据
        //        var repeatData = from res in AllData
        //                         where (from i in B2
        //                                where (from pp in AllData select pp.name).Contains(i.name)
        //                                select i.name).Contains(res.name)
        //                         select res;

        //        //新数据 
        //        var newData = from i in B2
        //                      where !(from pp in AllData select pp.name).Contains(i.name)
        //                      select i;


        //        sysDbContext.B2.AddRange(newData.ToArray());//添加数据（没有重复的项）
        //        sysDbContext.B2.UpdateRange(repeatData.ToArray());//更新(覆盖)数据
        //    }
        //    else
        //    {
        //        sysDbContext.B2.AddRange(B2.ToArray());//追加数据
        //    }


        //    reader.Close();
        //    if (sysDbContext.SaveChanges() > 0)
        //    {
        //        return Content("ok");
        //    }
        //    else
        //    {
        //        return Content("no");
        //    }

        //}

    }
}