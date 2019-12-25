using System;
using RAGE;
using RAGE.Ui;

namespace PhantomCopsClient
{
    public class Main : Events.Script
    {
        public void OnPlayerJoin(RAGE.Elements.Player player)
        {
            RAGE.Chat.Output($"Player {player.Name} has joined");
        }
    }
}
