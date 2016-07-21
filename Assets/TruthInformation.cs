using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace DungeonPlayer
{
    public class TruthInformation : MotherForm
    {
        // GUI
        public GameObject groupAttribute;
        public GameObject groupElement;
        public GameObject groupMixElement;
        public GameObject groupArchetype;
        public GameObject groupCommand;
        public GameObject groupCurrent;
        public Button[] AttributeButton;
        public Text[] AttributeButtonText;
        public Button[] ElementButton;
        public Text[] ElementButtonText;
        public Button[] MixElementButton;
        public Text[] MixElementButtonText;
        public Button[] ArchetypeButton;
        public Text[] ArchetypeButtonText;
        public Button[] CommandButton;
        public Text[] CommandButtonText;
        public Image CurrentImage;
        public Text CurrentLabel_JP;
        public Text CurrentLabel_EN;
        public Text CurrentCost;
        public Text CurrentTarget;
        public Text CurrentTiming;
        public GameObject back_CurrentDescription;
        public Text CurrentDescription;
        public GameObject backPanel;
        public Button CloseButton;

        // Use this for initialization
        public override void Start()
        {
            base.Start();

            if (GroundOne.WE2 != null && GroundOne.WE2.AvailableMixSpellSkill == false)
            {
                AttributeButton[2].gameObject.SetActive(false);
                AttributeButton[3].gameObject.SetActive(false);
            }
            if (GroundOne.WE2 != null && GroundOne.WE2.AvailableArcheTypeCommand == false)
            {
                AttributeButton[4].gameObject.SetActive(false);
            }
            tapAttribute(AttributeButtonText[0]);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                tapClose();
            }
        }

        private void SetupCommandButton(Button button, Text txt, string targetName)
        {
            txt.text = targetName;
            if (targetName == "")
            {
                button.GetComponent<Image>().color = Color.clear; button.enabled = false;
            }
            else
            {
                button.GetComponent<Image>().color = Color.white; button.enabled = true;
            }
        }

        private void button1_Click(Text sender)
        {
            Color targetColor = Color.white;
            if (sender.text == "聖")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.FRESH_HEAL);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.PROTECTION);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.HOLY_SHOCK);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], Database.SAINT_POWER);
                SetupCommandButton(CommandButton[4], CommandButtonText[4], Database.GLORY);
                SetupCommandButton(CommandButton[5], CommandButtonText[5], Database.RESURRECTION);
                SetupCommandButton(CommandButton[6], CommandButtonText[6], Database.CELESTIAL_NOVA);
                targetColor = UnityColor.Gold;
            }
            else if (sender.text == "闇")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.DARK_BLAST);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.SHADOW_PACT);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.LIFE_TAP);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], Database.BLACK_CONTRACT);
                SetupCommandButton(CommandButton[4], CommandButtonText[4], Database.DEVOURING_PLAGUE);
                SetupCommandButton(CommandButton[5], CommandButtonText[5], Database.BLOODY_VENGEANCE);
                SetupCommandButton(CommandButton[6], CommandButtonText[6], Database.DAMNATION);
                targetColor = UnityColor.Darkgray;
            }
            else if (sender.text == "火")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.FIRE_BALL);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.FLAME_AURA);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.HEAT_BOOST);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], Database.FLAME_STRIKE);
                SetupCommandButton(CommandButton[4], CommandButtonText[4], Database.VOLCANIC_WAVE);
                SetupCommandButton(CommandButton[5], CommandButtonText[5], Database.IMMORTAL_RAVE);
                SetupCommandButton(CommandButton[6], CommandButtonText[6], Database.LAVA_ANNIHILATION);
                targetColor = UnityColor.OrangeRed;
            }
            else if (sender.text == "水")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.ICE_NEEDLE);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.ABSORB_WATER);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.CLEANSING);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], Database.FROZEN_LANCE);
                SetupCommandButton(CommandButton[4], CommandButtonText[4], Database.MIRROR_IMAGE);
                SetupCommandButton(CommandButton[5], CommandButtonText[5], Database.PROMISED_KNOWLEDGE);
                SetupCommandButton(CommandButton[6], CommandButtonText[6], Database.ABSOLUTE_ZERO);
                targetColor = UnityColor.CornFlowerBlue;
            }
            else if (sender.text == "理")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.WORD_OF_POWER);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.GALE_WIND);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.WORD_OF_LIFE);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], Database.WORD_OF_FORTUNE);
                SetupCommandButton(CommandButton[4], CommandButtonText[4], Database.AETHER_DRIVE);
                SetupCommandButton(CommandButton[5], CommandButtonText[5], Database.GENESIS);
                SetupCommandButton(CommandButton[6], CommandButtonText[6], Database.ETERNAL_PRESENCE);
                targetColor = UnityColor.LimeGreen;
            }
            else if (sender.text == "空")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.DISPEL_MAGIC);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.RISE_OF_IMAGE);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.DEFLECTION);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], Database.TRANQUILITY);
                SetupCommandButton(CommandButton[4], CommandButtonText[4], Database.ONE_IMMUNITY);
                SetupCommandButton(CommandButton[5], CommandButtonText[5], Database.WHITE_OUT);
                SetupCommandButton(CommandButton[6], CommandButtonText[6], Database.TIME_STOP);
                targetColor = UnityColor.White;
            }
            else if (sender.text == "動")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.STRAIGHT_SMASH);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.DOUBLE_SLASH);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.CRUSHING_BLOW);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], Database.SOUL_INFINITY);
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
                targetColor = UnityColor.Gold;
            }
            else if (sender.text == "静")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.COUNTER_ATTACK);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.PURE_PURIFICATION);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.ANTI_STUN);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], Database.STANCE_OF_DEATH);
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
                targetColor = UnityColor.Darkgray;
            }
            else if (sender.text == "柔")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.STANCE_OF_FLOW);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.ENIGMA_SENSE);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.SILENT_RUSH);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], Database.OBORO_IMPACT);
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
                targetColor = UnityColor.OrangeRed;
            }
            else if (sender.text == "剛")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.STANCE_OF_STANDING);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.INNER_INSPIRATION);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.KINETIC_SMASH);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], Database.CATASTROPHE);
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
                targetColor = UnityColor.CornFlowerBlue;
            }
            else if (sender.text == "心眼")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.TRUTH_VISION);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.HIGH_EMOTIONALITY);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.STANCE_OF_EYES);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], Database.PAINFUL_INSANITY);
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
                targetColor = UnityColor.LimeGreen;
            }
            else if (sender.text == "無心")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.NEGATE);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.VOID_EXTRACTION);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.CARNAGE_RUSH);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], Database.NOTHING_OF_NOTHINGNESS);
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
                targetColor = UnityColor.White;
            }
            else if (sender.text == "聖/火")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.FLASH_BLAZE);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.LIGHT_DETONATOR);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.ASCENDANT_METEOR);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "聖/理")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.HOLY_BREAKER);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.EXALTED_FIELD);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.HYMN_CONTRACT);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "火/理")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.ENRAGE_BLAST);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.PIERCING_FLAME);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.SIGIL_OF_HOMURA);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "闇/水")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.BLUE_BULLET);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.DEEP_MIRROR);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.DEATH_DENY);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "闇/空")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.DARKEN_FIELD);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.DOOM_BLADE);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.ECLIPSE_END);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "水/空")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.VANISH_WAVE);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.VORTEX_FIELD);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.BLUE_DRAGON_WILL);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "聖/水")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.SKY_SHIELD);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.SACRED_HEAL);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.EVER_DROPLET);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "聖/空")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.STAR_LIGHTNING);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.ANGEL_BREATH);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.ENDLESS_ANTHEM);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "火/空")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.IMMOLATE);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.PHANTASMAL_WIND);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.RED_DRAGON_WILL);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "闇/火")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.BLACK_FIRE);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.BLAZING_FIELD);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.DEMONIC_IGNITE);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "闇/理")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.WORD_OF_MALICE);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.ABYSS_EYE);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.SIN_FORTUNE);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "水/理")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.WORD_OF_ATTITUDE);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.STATIC_BARRIER);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.AUSTERITY_MATRIX);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "聖/闇")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.PSYCHIC_TRANCE);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.BLIND_JUSTICE);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.TRANSCENDENT_WISH);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "火/水")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.FROZEN_AURA);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.CHILL_BURN);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.ZETA_EXPLOSION);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "理/空")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.SEVENTH_MAGIC);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.PARADOX_IMAGE);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], Database.WARP_GATE);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "動/柔")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.SWIFT_STEP);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.VIGOR_SENSE);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], "");
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "動/心眼")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.RUMBLE_SHOUT);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.ONSLAUGHT_HIT);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], "");
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "柔/心眼")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.PSYCHIC_WAVE);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.NOURISH_SENSE);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], "");
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "静/剛")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.REFLEX_SPIRIT);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.FATAL_BLOW);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], "");
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "静/無心")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.TRUST_SILENCE);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.MIND_KILLING);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], "");
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "剛/無心")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.OUTER_INSPIRATION);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.HARDEST_PARRY);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], "");
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "動/剛")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.CIRCLE_SLASH);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.RISING_AURA);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], "");
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "動/無心")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.COLORLESS_MOVE);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.ASCENSION_AURA);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], "");
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "柔/無心")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.RECOVER);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.IMPULSE_HIT);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], "");
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "静/柔")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.FUTURE_VISION);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.UNKNOWN_SHOCK);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], "");
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "静/心眼")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.SHARP_GLARE);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.CONCUSSIVE_HIT);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], "");
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "剛/心眼")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.VIOLENT_SLASH);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.ONE_AUTHORITY);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], "");
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "動/静")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.NEUTRAL_SMASH);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.STANCE_OF_DOUBLE);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], "");
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "柔/剛")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.SURPRISE_ATTACK);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.STANCE_OF_MYSTIC);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], "");
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "心眼/無心")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.STANCE_OF_SUDDENNESS);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], Database.SOUL_EXECUTION);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], "");
                SetupCommandButton(CommandButton[3], CommandButtonText[3], "");
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }
            else if (sender.text == "元核")
            {
                SetupCommandButton(CommandButton[0], CommandButtonText[0], Database.ARCHETYPE_EIN_JP);
                SetupCommandButton(CommandButton[1], CommandButtonText[1], ""); // Database.ARCHETYPE_RANA_JP);
                SetupCommandButton(CommandButton[2], CommandButtonText[2], ""); // Database.ARCHETYPE_OL_JP);
                SetupCommandButton(CommandButton[3], CommandButtonText[3], ""); // Database.ARCHETYPE_VERZE_JP);
                SetupCommandButton(CommandButton[4], CommandButtonText[4], "");
                SetupCommandButton(CommandButton[5], CommandButtonText[5], "");
                SetupCommandButton(CommandButton[6], CommandButtonText[6], "");
            }

            this.backPanel.GetComponent<Image>().color = targetColor;
            button7_Click(CommandButtonText[0]);
        }

        private void button7_Click(Text sender)
        {
            if (sender.text == "？？？") return;
            if (sender.text == "") return;

            string command = TruthActionCommand.ConvertToEnglish(((Text)sender).text);
            CurrentImage.sprite = Resources.Load<Sprite>(Database.BaseResourceFolder + command);

            switch (TruthActionCommand.GetTargetType(command))
            {
                case TruthActionCommand.TargetType.AllMember:
                    CurrentTarget.text = "敵味方全体";
                    break;
                case TruthActionCommand.TargetType.Ally:
                    CurrentTarget.text = "味方単体";
                    break;
                case TruthActionCommand.TargetType.AllyGroup:
                    CurrentTarget.text = "味方全体";
                    break;
                case TruthActionCommand.TargetType.AllyOrEnemy:
                    CurrentTarget.text = "敵単体 / 味方単体";
                    break;
                case TruthActionCommand.TargetType.Enemy:
                    CurrentTarget.text = "敵単体";
                    break;
                case TruthActionCommand.TargetType.EnemyGroup:
                    CurrentTarget.text = "敵全体";
                    break;
                case TruthActionCommand.TargetType.InstantTarget:
                    CurrentTarget.text = "インスタント対象";
                    break;
                case TruthActionCommand.TargetType.NoTarget:
                    CurrentTarget.text = "なし";
                    break;
                case TruthActionCommand.TargetType.Own:
                    CurrentTarget.text = "自分";
                    break;
            }

            CurrentLabel_JP.text = TruthActionCommand.ConvertToJapanese(command);
            CurrentLabel_EN.text = command;
            CurrentDescription.text = TruthActionCommand.GetDescription(command);
            CurrentCost.text = TruthActionCommand.GetCost(command).ToString();
            if (TruthActionCommand.GetTimingType(command) == TruthActionCommand.TimingType.Instant)
            {
                CurrentTiming.text = "インスタント";
            }
            else if (TruthActionCommand.GetTimingType(command) == TruthActionCommand.TimingType.Normal)
            {
                CurrentTiming.text = "ノーマル";
            }
            else if (TruthActionCommand.GetTimingType(command) == TruthActionCommand.TimingType.Sorcery)
            {
                CurrentTiming.text = "ソーサリー";
            }
        }

        // spell, skill, mixspell, mixskill, architect
        public void tapAttribute(Text sender)
        {
            if (sender.text == "魔法")
            {
                groupElement.SetActive(true);
                groupMixElement.SetActive(false);
                groupArchetype.SetActive(false);
                groupCommand.SetActive(true);
                // 上ボタン、テキスト更新
                ElementButtonText[0].text = "聖";
                ElementButtonText[1].text = "火";
                ElementButtonText[2].text = "理";
                ElementButtonText[3].text = "闇";
                ElementButtonText[4].text = "水";
                ElementButtonText[5].text = "空";
                // 最初の下項目のボタンを選択しておく！
                tapElement(ElementButtonText[0]);
            }
            else if (sender.text == "スキル")
            {
                groupElement.SetActive(true);
                groupMixElement.SetActive(false);
                groupArchetype.SetActive(false);
                groupCommand.SetActive(true);
                // 上ボタン、テキスト更新
                ElementButtonText[0].text = "動";
                ElementButtonText[1].text = "柔";
                ElementButtonText[2].text = "心眼";
                ElementButtonText[3].text = "静";
                ElementButtonText[4].text = "剛";
                ElementButtonText[5].text = "無心";
                // 最初の下項目のボタンを選択しておく！
                tapElement(ElementButtonText[0]);
            }
            else if (sender.text == "複合魔法")
            {
                groupElement.SetActive(false);
                groupMixElement.SetActive(true);
                groupArchetype.SetActive(false);
                groupCommand.SetActive(true);
                // 上ボタン、テキスト更新
                MixElementButtonText[0].text = "聖/火";
                MixElementButtonText[1].text = "聖/理";
                MixElementButtonText[2].text = "火/理";
                MixElementButtonText[3].text = "闇/水";
                MixElementButtonText[4].text = "闇/空";
                MixElementButtonText[5].text = "水/空";
                MixElementButtonText[6].text = "聖/水";
                MixElementButtonText[7].text = "聖/空";
                MixElementButtonText[8].text = "火/空";
                MixElementButtonText[9].text = "闇/火";
                MixElementButtonText[10].text = "闇/理";
                MixElementButtonText[11].text = "水/理";
                MixElementButtonText[12].text = "聖/闇";
                MixElementButtonText[13].text = "火/水";
                MixElementButtonText[14].text = "理/空";
                // 最初の下項目のボタンを選択しておく！
                tapElement(MixElementButtonText[0]);
            }
            else if (sender.text == "複合スキル")
            {
                groupElement.SetActive(false);
                groupMixElement.SetActive(true);
                groupArchetype.SetActive(false);
                groupCommand.SetActive(true);
                // 上ボタン、テキスト更新
                MixElementButtonText[0].text = "動/柔";
                MixElementButtonText[1].text = "動/心眼";
                MixElementButtonText[2].text = "柔/心眼";
                MixElementButtonText[3].text = "静/剛";
                MixElementButtonText[4].text = "静/無心";
                MixElementButtonText[5].text = "剛/無心";
                MixElementButtonText[6].text = "動/剛";
                MixElementButtonText[7].text = "動/無心";
                MixElementButtonText[8].text = "柔/無心";
                MixElementButtonText[9].text = "静/柔";
                MixElementButtonText[10].text = "静/心眼";
                MixElementButtonText[11].text = "剛/心眼";
                MixElementButtonText[12].text = "動/静";
                MixElementButtonText[13].text = "柔/剛";
                MixElementButtonText[14].text = "心眼/無心";
                // 最初の下項目のボタンを選択しておく！
                tapElement(MixElementButtonText[0]);
            }
            else if (sender.text == "元核")
            {
                groupElement.SetActive(false);
                groupMixElement.SetActive(false);
                groupArchetype.SetActive(true);
                groupCommand.SetActive(true);
                // 上ボタン、テキスト更新
                ArchetypeButtonText[0].text = "元核";
                // 最初の下項目のボタンを選択しておく！
                tapElement(ArchetypeButtonText[0]);
            }
        }

        public void tapElement(Text sender)
        {
            button1_Click(sender);
            tapActionCommand(CommandButtonText[0]);
        }

        public void tapActionCommand(Text sender)
        {
            button7_Click(sender);
        }
        public void tapClose()
        {
            SceneDimension.Back(this);
        }
    }
}
