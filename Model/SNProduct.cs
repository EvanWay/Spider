using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
	public class SNProduct : Product
	{
		public string shop { get; set; }
		public bool IsSelf { get; set; }
		public string commit { get; set; }
		public string link { get; set; }
	}
}
