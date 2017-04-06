using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Healthy
{
    public class Healthy : RocketPlugin
    {

		public static Healthy Instance;

		protected override void Load ()
		{
			Instance = this;
			UnturnedPlayerEvents.OnPlayerUpdateHealth += HealthChanged;
			UnturnedPlayerEvents.OnPlayerRevive += PlayerRespawn;
		}

		private void HealthChanged (UnturnedPlayer player, byte health)
		{
			if (!player.GetComponent <PlayerComponent>().justHealed)
			{
				player.GetComponent<PlayerComponent> ().justHealed = true;
			}
			player.GetComponent<PlayerComponent> ().justHealed = false;
		}

		private void PlayerRespawn (Rocket.Unturned.Player.UnturnedPlayer player, UnityEngine.Vector3 position, byte angle)
		{
			player.GetComponent<PlayerComponent> ().health = player.GetComponent<PlayerComponent> ().maxHealth;
		}

		protected override void Unload ()
		{
			
		}
	}
}
