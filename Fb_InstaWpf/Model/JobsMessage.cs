using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fb_InstaWpf.Model {
	public class JobsMessage {
	    public int Id {get;set;}
		public string ToUserId {get;set;}
		public string MessageType {get;set;}
		public int Status {get;set;}
		public string ToUrl {get;set;}
		public string Message {get;set;}	
	}
}
