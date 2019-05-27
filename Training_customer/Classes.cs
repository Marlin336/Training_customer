using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training_customer
{
    public class GroupList
	{
		public int id { get; set; }
		public int trainer_id { get; set; }
		public string trainer { get; set; }
		public string min_age { get; set; }
		public string max_age { get; set; }
		public int cost { get; set; }
		public int sub { get; set; }
		public GroupList(int id, int trainer_id, string trainer, string min, string max, int cost, int sub)
		{
			this.id = id;
			this.trainer_id = trainer_id;
			this.trainer = trainer;
			this.min_age = min;
			this.max_age = max;
			this.cost = cost;
			this.sub = sub;
		}
	}
}
