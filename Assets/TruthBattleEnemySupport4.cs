using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DungeonPlayer
{
    public partial class TruthBattleEnemy : MotherForm
    {
        protected void CheckChaosDesperate(MainCharacter player)
        {
            // ChaosDesperateの効果が解除された時、即死する
            if (player.CurrentChaosDesperate > 0 && player.CurrentChaosDesperateValue <= 0)
            {
                player.CurrentLife = 0;
                UpdateLife(player, 0, false, false, 0, false);
                player.DeadPlayer();
                player.RemoveChaosDesperate();
                AnimationDamage(0, player, 0, Color.black, false, false, "死亡");
            }
        }

        protected double GainIsZero(double damage, MainCharacter player)
        {
            if (player.CurrentAbsoluteZero > 0) { damage = 0; }
            if (player.CurrentVoiceOfAbyss > 0) { damage = 0; }
            if (player.CurrentNoGainLife > 0) { damage = 0; }
            return damage;
        }

        protected double DamageIsZero(double damage, MainCharacter player)
        {
            if (player.CurrentEclipseEnd > 0) { damage = 0; }
            if (player.CurrentDetachmentOrb > 0) { damage = 0; }
            if (player.CurrentLightAndShadow > 0) { damage = 0; }
            if (player.CurrentOneImmunity > 0 && player.PA == DungeonPlayer.MainCharacter.PlayerAction.Defense) { damage = 0; }
            return damage;
        }

        protected void LifeDamage(double damage, MainCharacter player, int interval = 0, bool detectCritical = false)
        {
            if (player.CurrentShiningAether > 0)
            {
                AnimationDamage(0, player, 0, Color.black, false, false, Database.IMMUNE_DAMAGE);
            }
            // 防壁がある場合、マナダメージへ変換する。
            else if (player.CurrentTheAbyssWall <= 0)
            {
                player.CurrentLife -= (int)damage;
                UpdateLife(player, (int)damage, false, true, interval, detectCritical);
            }
            else
            {
                player.CurrentMana -= (int)damage;
                UpdateMana(player, (int)damage, false, true, interval);
                if (player.CurrentMana <= 0)
                {
                    player.RemoveTheAbyssWall();
                }
            }
        }

        protected void ManaDamage(double damage, MainCharacter player, int interval = 0, bool detectCritical = false)
        {
            player.CurrentMana -= (int)damage;
            UpdateMana(player, (int)damage, false, true, interval);
            if (player.CurrentMana <= 0)
            {
                player.RemoveTheAbyssWall();
            }
        }
    }
}
