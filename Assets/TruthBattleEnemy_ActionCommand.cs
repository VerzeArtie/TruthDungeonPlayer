using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

namespace DungeonPlayer
{
    public partial class TruthBattleEnemy : MotherForm
    {
        // フレッシュヒール
        private void PlayerSpellFreshHeal(MainCharacter player, MainCharacter target)
        {
            double lifeGain = PrimaryLogic.FreshHealValue(player, this.DuelMode);
            if (player.CurrentNourishSense > 0)
            {
                lifeGain = lifeGain * 1.3f;
            }
            PlayerAbstractLifeGain(player, player, 0, lifeGain, 0, Database.SOUND_FRESH_HEAL, 9);
        }
    }
}
