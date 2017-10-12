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
        public ActionResult Index(string keywork)
        {
			if (string.IsNullOrEmpty(keywork))
			{
				ViewData["JD"] = new List<JDProduct>();
				return View();
			}
			else
			{
				//keywork = "固态硬盘";
				//中文编码
				string encodingkeywork = System.Web.HttpUtility.UrlEncode(keywork);
				string url = "https://search.jd.com/Search?keyword=" + encodingkeywork + "&enc=utf-8";
				ViewData["pageHtml"] = GetPageHtml(url);
				ViewData["JD"] = AnalyticsHtml(url);
				return View();
			}
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
		private List<JDProduct> AnalyticsHtml(string url)
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
			return jdlist;
		}
	}
}