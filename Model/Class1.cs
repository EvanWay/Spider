using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	class Class1
	{
		//private void FindLink(string html)
		//{
		//	this.TextBox3.Text = "";
		//	List<string> hrefList = new List<string>();//链接
		//	List<string> nameList = new List<string>();//链接名称

		//	string pattern = @"<a\s*href=(""|')(?<href>[\s\S.]*?)(""|').*?>\s*(?<name>[\s\S.]*?)</a>";
		//	MatchCollection mc = Regex.Matches(html, pattern);
		//	foreach (Match m in mc)
		//	{
		//		if (m.Success)
		//		{
		//			//加入集合数组
		//			hrefList.Add(m.Groups["href"].Value);
		//			nameList.Add(m.Groups["name"].Value);
		//			this.TextBox3.Text += m.Groups["href"].Value + "|" + m.Groups["name"].Value + "\n";
		//		}
		//	}
		//}
		//public string ClearHtml(string text)//过滤html,js,css代码
		//{
		//	text = text.Trim();
		//	if (string.IsNullOrEmpty(text))
		//		return string.Empty;
		//	text = Regex.Replace(text, "<head[^>]*>(?:.|[\r\n])*?</head>", "");
		//	text = Regex.Replace(text, "<script[^>]*>(?:.|[\r\n])*?</script>", "");
		//	text = Regex.Replace(text, "<style[^>]*>(?:.|[\r\n])*?</style>", "");

		//	text = Regex.Replace(text, "(<[b|B][r|R]/*>)+|(<[p|P](.|\\n)*?>)", ""); //<br> 
		//	text = Regex.Replace(text, "\\&[a-zA-Z]{1,10};", "");
		//	text = Regex.Replace(text, "<[^>]*>", "");

		//	text = Regex.Replace(text, "(\\s*&[n|N][b|B][s|S][p|P];\\s*)+", ""); //&nbsp;
		//	text = Regex.Replace(text, "<(.|\\n)*?>", string.Empty); //其它任何标记
		//	text = Regex.Replace(text, "[\\s]{2,}", " "); //两个或多个空格替换为一个

		//	text = text.Replace("'", "''");
		//	text = text.Replace("\r\n", "");
		//	text = text.Replace("  ", "");
		//	text = text.Replace("\t", "");
		//	return text.Trim();
		//}
		//private void IPAddresses(string url)
		//{
		//	url = url.Substring(url.IndexOf("//") + 2);
		//	if (url.IndexOf("/") != -1)
		//	{
		//		url = url.Remove(url.IndexOf("/"));
		//	}
		//	this.Literal1.Text += "<br>" + url;
		//	try
		//	{
		//		System.Text.ASCIIEncoding ASCII = new System.Text.ASCIIEncoding();
		//		IPHostEntry ipHostEntry = Dns.GetHostEntry(url);
		//		System.Net.IPAddress[] ipaddress = ipHostEntry.AddressList;
		//		foreach (IPAddress item in ipaddress)
		//		{
		//			this.Literal1.Text += "<br>IP:" + item;
		//		}
		//	}
		//	catch (Exception e)
		//	{

		//	}
		//}
	}
}
