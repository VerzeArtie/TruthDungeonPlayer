using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace DungeonPlayer
{
    public static class JobClass
    {
        public enum Job
        {
            None,
            Fighter,
            Archer,
            Lancer,
            Rouge,
            Warrior,
            Breaker,
            Sorcerer,
            Conjurer,
            Priest,
            Warlock,
            Druid,
            Enchanter,
        }

        public static string GetSpecialCommand(MainCharacter player)
        {
            if (player.Job == Job.Fighter) { return "QuickParry"; }
            if (player.Job == Job.Archer) { return "ShootingStar"; }
            if (player.Job == Job.Lancer) { return "PenetrateShot"; }
            if (player.Job == Job.Rouge) { return "DeceitStep"; }
            if (player.Job == Job.Warrior) { return "SelfRestoration"; }
            if (player.Job == Job.Breaker) { return "ShockCharge"; }
            if (player.Job == Job.Sorcerer) { return "MagicBolt"; }
            if (player.Job == Job.Conjurer) { return "SpellDemise"; }
            if (player.Job == Job.Priest) { return "SplashHeal"; }
            if (player.Job == Job.Warlock) { return "MindPoison"; }
            if (player.Job == Job.Druid) { return "NaturePresence"; }
            if (player.Job == Job.Enchanter) { return "AuraBlade"; }
            return "";
        }
    }
}