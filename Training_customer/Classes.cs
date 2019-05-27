﻿using System;
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
	public class LogList
	{
		public int id { get; set; }
		public int group_id { get; set; }
		public string date { get; set; }
		public string in_time { get; set; }
		public string out_time { get; set; }
		public string admin { get; set; }
		public string trainer { get; set; }
		public int transact_id { get; set; }
		public LogList(int id, int group_id, string date, string in_time, string out_time, string admin, string trainer, int transact_id)
		{
			this.id = id;
			this.group_id = group_id;
			this.date = date;
			this.in_time = in_time;
			this.out_time = out_time;
			this.admin = admin;
			this.trainer = trainer;
			this.transact_id = transact_id;
		}
	}
	public class TransactList
	{
		public int id { get; set; }
		public string admin { get; set; }
		public int addition { get; set; }
		public string date { get; set; }
		public string time { get; set; }
		public TransactList(int id, string admin, int addition, string date, string time)
		{
			this.id = id;
			this.admin = admin;
			this.addition = addition;
			this.date = date;
			this.time = time;
		}
	}
}
