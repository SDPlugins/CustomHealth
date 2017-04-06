using Rocket.Core;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Healthy
{
	public class PlayerComponent : UnturnedPlayerComponent
	{

		public int health;
		public int maxHealth;

		public byte lastHealth;

		public bool justHealed;

		protected override void Load ()
		{
			maxHealth = getHealth ();
			Rocket.Core.Logging.Logger.Log (((int)Player.Health).ToString ());
			if (Player.Health != 0)
				health = maxHealth / (100 / (int)Player.Health);
			else
				health = maxHealth;
			lastHealth = 100;
			justHealed = false;
		}

		protected override void Unload ()
		{
			
		}

		

		public int getHealth ()
		{
			int ret = 100;

			var groups = R.Permissions.GetGroups (Player, true);
			foreach (var group in groups)
			{
				var healthStrings = group.Permissions.Where (p => p.Name.ToLower ().StartsWith ("health.")).ToList ();
				int cur = 0;
				foreach (var health in healthStrings)
				{
					int h;
					if (!int.TryParse (health.Name.Remove (0, 7), out h))
						continue;
					Rocket.Core.Logging.Logger.Log (health.Name + " | " + h);
					if (h > cur)
						cur = h;
				}
				if (cur > 0)
					ret = cur;
			}

			return ret;
		}
	}
}
