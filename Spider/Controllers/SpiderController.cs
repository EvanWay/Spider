using Ivony.Html;
using Ivony.Html.Parser;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Spider.Controllers
{
    public class SpiderController : Controller
    {
        // GET: Spider
        public ActionResult Index(string keywork,string page,string Radios)
        {
			ViewBag.keywork = keywork;
			if (string.IsNullOrEmpty(page))
			{
				page = "1";
			}
			if (string.IsNullOrEmpty(keywork))
			{
				ViewData["JD"] = new List<JDProduct>();
				return View();
			}
			//中文编码
			string encodingkeywork = System.Web.HttpUtility.UrlEncode(keywork);
			if (Radios == "1")//京东
			{
				//页码2n-1
				string url = "https://search.jd.com/Search?keyword=" + encodingkeywork + "&enc=utf-8" + "&page=" + (2 * (Convert.ToInt32(page)) - 1);
				//爬取
				Tuple<List<JDProduct>, int> t = JD_AnalyticsHtml(url);
				ViewData["currentpage"] = page;
				ViewData["totalpage"] = t.Item2;

				ViewData["pageHtml"] = GetPageHtml(url);
				ViewData["JD"] = t.Item1;

				return View();
			}
			else if (Radios == "2")//苏宁
			{
				//页码N-1
				string url = "https://search.suning.com/" + encodingkeywork + "/&cp=" + ((Convert.ToInt32(page)) - 1);

				return RedirectToAction("SN", new { keywork = keywork, page = page, Radios = Radios });
			}
			return View();
			//苏宁https://search.suning.com/固态硬盘/&cp=1
		}

		public ActionResult SN(string keywork, string page, string Radios)
		{
			ViewData["currentpage"] = 1;
			ViewData["totalpage"] = 10;

			ViewData["SN"] = new List<SNProduct>();
			return View();
		}

		/// <summary>
		/// 获取页面html源码
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		private string GetPageHtml(string url)
		{
			HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
			request.Method = "GET";
			request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";

			HttpWebResponse response = request.GetResponse() as HttpWebResponse;
			using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
			{
				string result = reader.ReadToEnd();
				return result;
			}
		}

		/// <summary>
		/// 爬取JD商品list
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		private Tuple<List<JDProduct>,int> JD_AnalyticsHtml(string url)
		{
			List<JDProduct> jdlist = new List<JDProduct>();
			HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
			request.Method = "GET";
			//伪装浏览器
			request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36";
			HttpWebResponse response = request.GetResponse() as HttpWebResponse;

			var parser = new JumonyParser();
			var doc = parser.LoadDocument(response.GetResponseStream(), Encoding.UTF8, new Uri(url));
			var list = doc.Find("#J_goodsList > ul > .gl-item > .gl-i-wrap");
			foreach (var li in list)
			{
				JDProduct pro = new JDProduct();

				var price = li.FindFirst(".p-price > strong > i");
				var name = li.FindFirst(".p-name > a > em");
				var commit = li.FindFirst(".p-commit");
				var shop = li.FindFirst(".p-shop");

				pro.link = li.FindFirst(".p-img > a").Attribute("href").Value();
				pro.price = Convert.ToDouble(price.InnerText());
				pro.name = name.InnerText();
				pro.commit = commit.InnerText();
				pro.shop = shop.InnerText();
				pro.IsSelf = li.FindFirst(".p-shop").Attribute("data-selfware").Value().Equals("1");

				jdlist.Add(pro);
			}
			//获取总页数
			var totalPage = doc.FindFirst("#J_topPage > .fp-text > i");
			int totalPage_num = Convert.ToInt32(totalPage.InnerText());

			return Tuple.Create(jdlist, totalPage_num);
		}


		/// <summary>
		/// 爬取苏宁商品list
		/// </summary>
		/// <param name="url"></param>
		/// <returns></returns>
		private Tuple<List<JDProduct>, int> SN_AnalyticsHtml(string url)
		{
			return null;
		}

	}
}