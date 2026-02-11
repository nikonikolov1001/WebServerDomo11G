using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServerDomo11G.Server.Common;

namespace WebServerDomo11G.Server.HTTP
{
	public class Header
	{
		public Header(string name,string value)
		{
			Guard.AgainstNull(name,nameof(name));
            Guard.AgainstNull(name, nameof(value));
            Name = name; 
		    Value = value;	
		}

		public string Name { get; init; }
		public string Value { get; set; }
        public int MyProperty { get; set; }
    }
}
