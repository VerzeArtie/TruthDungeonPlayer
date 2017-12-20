using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using DungeonPlayer;
using System;
using System.Reflection.Emit;

namespace DungeonPlayer
{
    public class TruthStatusPlayer : MotherForm
    {
        public GameObject btnSpellSkillDescClose;
        public Text txtSpellSkillDescription;
        public GameObject groupParentStatus;
        public GameObject groupParentBackpack;
        public GameObject groupParentSpell;
        public GameObject groupParentResist;
        public Button btnClose;
        public Text txtClose;
        public GameObject groupBtnChara;
        public Button btnFirstChara;
        public Button btnSecondChara;
        public Button btnThirdChara;
        public GameObject groupTxtChara;
        public Text labelFirstPlayerLife;
        public Text labelSecondPlayerLife;
        public Text labelThirdPlayerLife;
        public Text mainMessage;
        public Text txtName;
        public Text txtLevel;
        public Text txtExperience;
        public Image imgExpGauge;
        public Text txtJobClass;
        public Text txtGold;
        public GameObject groupBtnLifeManaSkill;
        public GameObject groupTxtLifeManaSkill;
        public GameObject btnLife;
        public GameObject btnMana;
        public GameObject btnSkill;
        public Text life;
        public Image imgLifeGauge;
        public Text mana;
        public Image imgManaGauge;
        public Text skill;
        public Image imgSkillGauge;
        public Button btnStrength;
        public Button btnAgility;
        public Button btnIntelligence;
        public Button btnStamina;
        public Button btnMind;
        public Text strength;
        public Text addStrength;
        public Text addStrengthFood;
        public Text totalStrength;
        public Text agility;
        public Text addAgility;
        public Text addAgilityFood;
        public Text totalAgility;
        public Text intelligence;
        public Text addIntelligence;
        public Text addIntelligenceFood;
        public Text totalIntelligence;
        public Text stamina;
        public Text addStamina;
        public Text addStaminaFood;
        public Text totalStamina;
        public Text mind;
        public Text addMind;
        public Text addMindFood;
        public Text totalMind;
        public Button plus1;
        public Button plus10;
        public Button plus100;
        public Button plus1000;
        public Button btnUpReset;
        public Text lblRemain;
        public Text txtPhysicalAttack;
        public Text txtPhysicalDefense;
        public Text txtMagicAttack;
        public Text txtMagicDefense;
        public Text txtBattleSpeed;
        public Text txtBattleResponse;
        public Text txtPotential;
        public Text weapon;
        public Image imgMainWeapon;
        public Text subWeapon;
        public Image imgSubWeapon;
        public Text armor;
        public Image imgArmor;
        public Text accessory;
        public Image imgAccessory1;
        public Text accessory2;
        public Image imgAccessory2;
        public GameObject back_weapon;
        public GameObject back_subWeapon;
        public GameObject back_armor;
        public GameObject back_accessory;
        public GameObject back_accessory2;
        public GameObject[] back_Backpack;
        public Text[] backpack;
        public Text[] backpackStack;
        public Image[] backpackIcon;
        public GameObject groupChoice;
        public GameObject groupTarget;
        public GameObject groupWhoTarget;
        public GameObject backpackFilter;
        public Button btnTargetName1;
        public Button btnTargetName2;
        public Button btnTargetName3;
        public Text targetName1;
        public Text targetName2;
        public Text targetName3;
        public Button btnWhoTarget1;
        public Button btnWhoTarget2;
        public Button btnWhoTarget3;
        public Text whoTarget1;
        public Text whoTarget2;
        public Text whoTarget3;
        public GameObject CommandFilter;
        public GameObject groupTargetCommand;
        public GameObject btnTargetNameCommand1;
        public GameObject btnTargetNameCommand2;
        public GameObject btnTargetNameCommand3;
        public Text txtTargetNameCommand1;
        public Text txtTargetNameCommand2;
        public Text txtTargetNameCommand3;
        public GameObject[] back_SpellSkill;
        public Text[] SpellSkill;
        public Text[] ResistLabel;
        public Text[] ResistLabelValue;
        public Text[] ResistAbnormalStatus;
        public Text[] ResistAbnormalStatusValue;
        public GameObject groupLevelup;
        public Text lblLevelUp;
        public Text txtDescription1;
        public Text txtDescription2;
        public Text txtDescription3;
        public Text lblStatus;
        public Text lblBackpack;
        public Text lblSpell;
        public Text lblResist;
        public Text lblLevel;
        public Text lblExp;
        public Text lblGold;
        public Text lblCore;
        public Text lblBasic;
        public Text lblEquip;
        public Text lblFood;
        public Text lblTotal;
        public Text lblLife;
        public Text lblMana;
        public Text lblSkill;
        public Text lblStrength;
        public Text lblAgility;
        public Text lblIntelligence;
        public Text lblStamina;
        public Text lblMind;
        public Text lblMainWeapon;
        public Text lblSubWeapon;
        public Text lblArmor;
        public Text lblAccessory1;
        public Text lblAccessory2;
        public Text lblPhysicalAttack;
        public Text lblPhysicalDefense;
        public Text lblMagicAttack;
        public Text lblMagicDefense;
        public Text lblBattleSpeed;
        public Text lblBattleReaction;
        public Text lblPotential;
        public Text lblClose;

        ItemBackPack[] backpackData = null;


        private bool onlyUseItem = false;
        public bool OnlyUseItem
        {
            get { return onlyUseItem; }
            set { onlyUseItem = value; }
        }

        private bool useOverShifting = false; // オーバーシフティング使用時
        private bool ItemChoiced = false; // アイテム「つかう」で、使用できなかった時のメッセージ表示用
        private bool useTargetedItem = false; // アイテム「つかう」で、ターゲットを選定して使う場合を示すフラグ

        private string currentCommand = string.Empty; // スペルで現在使用しようとしているスペル名称を記憶するための値
 
        // Use this for initialization
        public override void Start()
        {
            base.Start();

            if (GroundOne.Language == GroundOne.GameLanguage.Japanese)
            {
                lblStatus.text = Database.GUI_STATUS_STATUS;
                lblBackpack.text = Database.GUI_STATUS_BACKPACK;
                lblSpell.text = Database.GUI_STATUS_SPELL;
                lblResist.text = Database.GUI_STATUS_RESIST;
                lblLevel.text = Database.GUI_S_BASIC_LEVEL;
                lblExp.text = Database.GUI_S_BASIC_EXP;
                lblGold.text = Database.GUI_S_BASIC_GOLD;
                lblCore.text = Database.GUI_S_BASIC_CORE;
                lblBasic.text = Database.GUI_S_BASIC_BASIC;
                lblEquip.text = Database.GUI_S_BASIC_EQUIP;
                lblFood.text = Database.GUI_S_BASIC_FOOD;
                lblTotal.text = Database.GUI_S_BASIC_TOTAL;
                lblLife.text = Database.GUI_S_BASIC_LIFE;
                lblMana.text = Database.GUI_S_BASIC_MANA;
                lblSkill.text = Database.GUI_S_BASIC_SKILL;
                lblStrength.text = Database.GUI_S_BASIC_STR;
                lblAgility.text = Database.GUI_S_BASIC_AGI;
                lblIntelligence.text = Database.GUI_S_BASIC_INT;
                lblStamina.text = Database.GUI_S_BASIC_STA;
                lblMind.text = Database.GUI_S_BASIC_MIN;
                lblMainWeapon.text = Database.GUI_S_BASIC_MAIN;
                lblSubWeapon.text = Database.GUI_S_BASIC_SUB;
                lblArmor.text = Database.GUI_S_BASIC_ARMOR;
                lblAccessory1.text = Database.GUI_S_BASIC_ACCESSORY1;
                lblAccessory2.text = Database.GUI_S_BASIC_ACCESSORY2;
                lblPhysicalAttack.text = Database.GUI_S_BASIC_PATK;
                lblPhysicalDefense.text = Database.GUI_S_BASIC_PDEF;
                lblMagicAttack.text = Database.GUI_S_BASIC_MATK;
                lblMagicDefense.text = Database.GUI_S_BASIC_MDEF;
                lblBattleSpeed.text = Database.GUI_S_BASIC_BSPD;
                lblBattleReaction.text = Database.GUI_S_BASIC_BRCT;
                lblPotential.text = Database.GUI_S_BASIC_PTCL;
                lblClose.text = Database.GUI_S_BASIC_CLOSE;
            }

            this.txtGold.text = GroundOne.MC.Gold.ToString();

            if (GroundOne.MC.FullName == GroundOne.LevelUpCharacter)
            {
                this.Background.GetComponent<Image>().color = GroundOne.MC.PlayerStatusColor;
            }
            else if (GroundOne.SC.FullName == GroundOne.LevelUpCharacter)
            {
                this.Background.GetComponent<Image>().color = GroundOne.SC.PlayerStatusColor;
            }
            else if (GroundOne.TC.FullName == GroundOne.LevelUpCharacter)
            {
                this.Background.GetComponent<Image>().color = GroundOne.TC.PlayerStatusColor;
            }

            MainCharacter player = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);
            SettingCharacterData(player);
            RefreshPartyMembersBattleStatus(player);

            RefreshPartyMembersLife(labelFirstPlayerLife, labelSecondPlayerLife, labelThirdPlayerLife);

            if (GroundOne.MC == null) { Debug.Log("status MC is null...?"); }
            if (GroundOne.SC == null) { Debug.Log("status SC is null...?"); }
            if (GroundOne.TC == null) { Debug.Log("status TC is null...?"); }
            if (GroundOne.MC != null) { btnFirstChara.GetComponent<Image>().color = GroundOne.MC.PlayerColor; }
            if (GroundOne.SC != null) { btnSecondChara.GetComponent<Image>().color = GroundOne.SC.PlayerColor; }
            if (GroundOne.TC != null) { btnThirdChara.GetComponent<Image>().color = GroundOne.TC.PlayerColor; }

            backpackData = player.GetBackPackInfo();
            Method.UpdateBackPackLabel(player, this.back_Backpack, this.backpack, this.backpackStack, this.backpackIcon);

            // ケースに応じて、キャラクター選択ボタン／ライフの表示を切り替えます。
            if ((!GroundOne.WE.AvailableSecondCharacter && !GroundOne.WE.AvailableThirdCharacter) || (GroundOne.DuelMode == true))
            {
                this.groupBtnChara.SetActive(false);
                this.groupTxtChara.SetActive(false);
            }
            else
            {
                this.groupBtnChara.SetActive(true);
                this.groupTxtChara.SetActive(true);
            }

            if (GroundOne.WE.AvailableSecondCharacter && GroundOne.DuelMode == false)
            {
                btnSecondChara.gameObject.SetActive(true);
                labelSecondPlayerLife.gameObject.SetActive(true);
            }
            else
            {
                btnSecondChara.gameObject.SetActive(false);
                labelSecondPlayerLife.gameObject.SetActive(false);
            }

            if (GroundOne.WE.AvailableThirdCharacter && GroundOne.DuelMode == false)
            {
                btnThirdChara.gameObject.SetActive(true);
                labelThirdPlayerLife.gameObject.SetActive(true);
            }
            else
            {
                btnThirdChara.gameObject.SetActive(false);
                labelThirdPlayerLife.gameObject.SetActive(false);
            }

            btnMana.SetActive(GroundOne.MC.AvailableMana);
            mana.gameObject.SetActive(GroundOne.MC.AvailableMana);

            btnSkill.SetActive(GroundOne.MC.AvailableSkill);
            skill.gameObject.SetActive(GroundOne.MC.AvailableSkill);

            if (!GroundOne.LevelUp)
            {
                mainMessage.text = "";
            }
            else
            {
                GroundOne.PlaySoundEffect(Database.SOUND_LEVEL_UP);
                //btnClose.gameObject.SetActive(false);
                lblRemain.gameObject.SetActive(true);
                groupLevelup.SetActive(true);
                lblRemain.text = "残り　" + GroundOne.UpPoint.ToString();
                txtClose.text = "完了";
                if (GroundOne.CumultiveLvUpValue >= 2)
                {
                    mainMessage.text = GroundOne.CumultiveLvUpValue.ToString() + "レベルアップ！！" + GroundOne.UpPoint.ToString() + "ポイントを割り振ってください。";
                }
                else
                {
                    mainMessage.text = "レベルアップ！！" + GroundOne.UpPoint.ToString() + "ポイントを割り振ってください。";
                }
            }

            if (GroundOne.OnlySelectTrash)
            {
                if (GroundOne.Parent.Count > 0)
                {
                    GroundOne.Parent[GroundOne.Parent.Count - 1].Filter.SetActive(true);
                    GroundOne.Parent[GroundOne.Parent.Count - 1].Filter.GetComponent<Image>().color = player.PlayerStatusColor;
                }
                txtClose.text = "諦める";
                mainMessage.text = "アイン：バックパックがいっぱいみたいだ。何か捨てないとな・・・";
                groupParentStatus.gameObject.SetActive(false);
                groupParentBackpack.gameObject.SetActive(true);
                groupParentSpell.gameObject.SetActive(false);
                groupParentResist.gameObject.SetActive(false);
            }

            if (this.onlyUseItem)
            {
                groupParentStatus.gameObject.SetActive(false);
                groupParentBackpack.gameObject.SetActive(true);
                groupParentSpell.gameObject.SetActive(false);
                groupParentResist.gameObject.SetActive(false);
            }
        }

        bool usingOvershifting = false;
        bool usingOvershiftingFirstSleep = false;
        bool usingOvershiftingSecondSleep = false;
        bool usingToomiBlueSuisyou = false;
        // Update is called once per frame
        public override void Update()
        {
            base.Update();

            if (this.usingToomiBlueSuisyou)
            {
                SceneDimension.Back(this);
            }
            #region "Overshifting"
            if (this.usingOvershifting)
            {
                MainCharacter player = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);
                if (this.usingOvershiftingFirstSleep == false)
                {
                    this.usingOvershiftingFirstSleep = true;
                    return;
                }
                if (this.usingOvershiftingSecondSleep == false)
                {
                    this.usingOvershiftingSecondSleep = true;
                    System.Threading.Thread.Sleep(1000);

                }

                int firstStrength = 1;
                int firstAgility = 1;
                int firstIntelligence = 1;
                int firstStamina = 1;
                int firstMind = 1;
                if (player.Equals(GroundOne.MC))
                {
                    firstStrength = Database.MAINPLAYER_FIRST_STRENGTH;
                    firstAgility = Database.MAINPLAYER_FIRST_AGILITY;
                    firstIntelligence = Database.MAINPLAYER_FIRST_INTELLIGENCE;
                    firstStamina = Database.MAINPLAYER_FIRST_STAMINA;
                    firstMind = Database.MAINPLAYER_FIRST_MIND;
                }
                else if (player.Equals(GroundOne.SC))
                {
                    firstStrength = Database.SECONDPLAYER_FIRST_STRENGTH;
                    firstAgility = Database.SECONDPLAYER_FIRST_AGILITY;
                    firstIntelligence = Database.SECONDPLAYER_FIRST_INTELLIGENCE;
                    firstStamina = Database.SECONDPLAYER_FIRST_STAMINA;
                    firstMind = Database.SECONDPLAYER_FIRST_MIND;
                }
                else if (player.Equals(GroundOne.TC))
                {
                    firstStrength = Database.THIRDPLAYER_FIRST_STRENGTH;
                    firstAgility = Database.THIRDPLAYER_FIRST_AGILITY;
                    firstIntelligence = Database.THIRDPLAYER_FIRST_INTELLIGENCE;
                    firstStamina = Database.THIRDPLAYER_FIRST_STAMINA;
                    firstMind = Database.THIRDPLAYER_FIRST_MIND;
                }
                if (player.Strength <= firstStrength)
                {
                    if (player.Agility <= firstAgility)
                    {
                        if (player.Intelligence <= firstIntelligence)
                        {
                            if (player.Stamina <= firstStamina)
                            {
                                if (player.Mind <= firstMind)
                                {
                                    this.usingOvershifting = false;
                                    //buttonStrength.Enabled = true;
                                    //buttonAgility.Enabled = true;
                                    //buttonIntelligence.Enabled = true;
                                    //buttonStamina.Enabled = true;
                                    //buttonMind.Enabled = true;
                                    ItemBackPack backpackData = new ItemBackPack(currentSelect.text);
                                    player.DeleteBackPack(backpackData, 1, currentNumber);
                                    UsingItemUpdateBackPackLabel(player, backpackData, currentSelect, currentNumber);
                                    SettingCharacterData(player);
                                    RefreshPartyMembersBattleStatus(player);
                                }
                                else
                                {
                                    player.Mind--;
                                    this.mind.text = player.Mind.ToString();
                                }
                            }
                            else
                            {
                                player.Stamina--;
                                this.stamina.text = player.Stamina.ToString();
                            }
                        }
                        else
                        {
                            player.Intelligence--;
                            this.intelligence.text = player.Intelligence.ToString();
                        }
                    }
                    else
                    {
                        player.Agility--;
                        this.agility.text = player.Agility.ToString();
                    }
                }
                else
                {
                    player.Strength--;
                    this.strength.text = player.Strength.ToString();
                }
                GroundOne.UpPoint++;
                System.Threading.Thread.Sleep(1);
            }
            #endregion
        }

        public override void SceneBack()
        {
            base.SceneBack();

            MainCharacter player = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);
            SettingCharacterData(player);
            RefreshPartyMembersBattleStatus(player);
            RefreshPartyMembersLife(labelFirstPlayerLife, labelSecondPlayerLife, labelThirdPlayerLife);

            if (GroundOne.LevelUp)
            {
                SpellSkillDesc_Close_Click();
            }
        }

        public void tapClose()
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_STATUS_CLOSE, String.Empty, String.Empty);
            GroundOne.SQL.UpdateCharacter();
            if (GroundOne.OnlySelectTrash)
            {
                if (GroundOne.CannotSelectTrash != String.Empty)
                {
                    mainMessage.text = "アイン：いや【" + GroundOne.CannotSelectTrash + "】の入手を諦めるわけにはいかねえ。";
                    return;
                }
            }

            if (GroundOne.LevelUp)
            {
                if (GroundOne.UpPoint > 0)
                {
                    mainMessage.text = "【力】【技】【知】【体】【心】のいずれかをタップしてパラメタを割り振ってください。";
                    return;
                }
            }

            SpellSkillDesc_Close_Click();
        }

        public void SpellSkillDesc_Close_Click()
        {
            if (GroundOne.LevelUp && Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color) == GroundOne.MC)
            {
                #region "アイン・レベルアップ習得表"
                if ((GroundOne.MC.Level >= 2) && (!GroundOne.MC.StraightSmash)) { GroundOne.MC.AvailableSkill = true; GroundOne.MC.StraightSmash = true; ShowActiveSkillSpell(GroundOne.MC, Database.STRAIGHT_SMASH); }
                else if ((GroundOne.MC.Level >= 3) && (!GroundOne.MC.FreshHeal)) { GroundOne.MC.AvailableMana = true; GroundOne.MC.FreshHeal = true; ShowActiveSkillSpell(GroundOne.MC, Database.FRESH_HEAL); }
                else if ((GroundOne.MC.Level >= 5) && (!GroundOne.MC.FireBall)) { GroundOne.MC.FireBall = true; ShowActiveSkillSpell(GroundOne.MC, Database.FIRE_BALL); }
                else if ((GroundOne.MC.Level >= 6) && (!GroundOne.MC.Protection)) { GroundOne.MC.Protection = true; ShowActiveSkillSpell(GroundOne.MC, Database.PROTECTION); }
                else if ((GroundOne.MC.Level >= 7) && (!GroundOne.MC.DoubleSlash)) { GroundOne.MC.DoubleSlash = true; ShowActiveSkillSpell(GroundOne.MC, Database.DOUBLE_SLASH); }
                else if ((GroundOne.MC.Level >= 8) && (!GroundOne.MC.FlameAura)) { GroundOne.MC.FlameAura = true; ShowActiveSkillSpell(GroundOne.MC, Database.FLAME_AURA); }
                else if ((GroundOne.MC.Level >= 9) && (!GroundOne.MC.StanceOfStanding)) { GroundOne.MC.StanceOfStanding = true; ShowActiveSkillSpell(GroundOne.MC, Database.STANCE_OF_STANDING); }
                else if ((GroundOne.MC.Level >= 10) && (!GroundOne.MC.WordOfPower)) { GroundOne.MC.WordOfPower = true; ShowActiveSkillSpell(GroundOne.MC, Database.WORD_OF_POWER); }
                else if ((GroundOne.MC.Level >= 11) && (!GroundOne.MC.HolyShock)) { GroundOne.MC.HolyShock = true; ShowActiveSkillSpell(GroundOne.MC, Database.HOLY_SHOCK); }
                else if ((GroundOne.MC.Level >= 12) && (!GroundOne.MC.TruthVision)) { GroundOne.MC.TruthVision = true; ShowActiveSkillSpell(GroundOne.MC, Database.TRUTH_VISION); }
                else if ((GroundOne.MC.Level >= 13) && (!GroundOne.MC.HeatBoost)) { GroundOne.MC.HeatBoost = true; ShowActiveSkillSpell(GroundOne.MC, Database.HEAT_BOOST); }
                else if ((GroundOne.MC.Level >= 14) && (!GroundOne.MC.SaintPower)) { GroundOne.MC.SaintPower = true; ShowActiveSkillSpell(GroundOne.MC, Database.SAINT_POWER); }
                else if ((GroundOne.MC.Level >= 15) && (!GroundOne.MC.GaleWind)) { GroundOne.MC.GaleWind = true; ShowActiveSkillSpell(GroundOne.MC, Database.GALE_WIND); }
                else if ((GroundOne.MC.Level >= 16) && (!GroundOne.MC.InnerInspiration)) { GroundOne.MC.InnerInspiration = true; ShowActiveSkillSpell(GroundOne.MC, Database.INNER_INSPIRATION); }
                else if ((GroundOne.MC.Level >= 17) && (!GroundOne.MC.WordOfLife)) { GroundOne.MC.WordOfLife = true; ShowActiveSkillSpell(GroundOne.MC, Database.WORD_OF_LIFE); }
                else if ((GroundOne.MC.Level >= 18) && (!GroundOne.MC.FlameStrike)) { GroundOne.MC.FlameStrike = true; ShowActiveSkillSpell(GroundOne.MC, Database.FLAME_STRIKE); }
                else if ((GroundOne.MC.Level >= 19) && (!GroundOne.MC.HighEmotionality)) { GroundOne.MC.HighEmotionality = true; ShowActiveSkillSpell(GroundOne.MC, Database.HIGH_EMOTIONALITY); }
                else if ((GroundOne.MC.Level >= 20) && (!GroundOne.MC.WordOfFortune)) { GroundOne.MC.WordOfFortune = true; ShowActiveSkillSpell(GroundOne.MC, Database.WORD_OF_FORTUNE); }
                // [警告] ここで一気にレベルを挙げられると、複合魔法・スキルの習得に違和感が出てしまう。
                // 複合魔法・スキルはガンツ武具屋のテレポート先、カール爵より習得するようにする。
                else if ((GroundOne.MC.Level >= 24) && (!GroundOne.MC.Glory)) { GroundOne.MC.Glory = true; ShowActiveSkillSpell(GroundOne.MC, Database.GLORY); }
                else if ((GroundOne.MC.Level >= 25) && (!GroundOne.MC.VolcanicWave)) { GroundOne.MC.VolcanicWave = true; ShowActiveSkillSpell(GroundOne.MC, Database.VOLCANIC_WAVE); }
                else if ((GroundOne.MC.Level >= 26) && (!GroundOne.MC.AetherDrive)) { GroundOne.MC.AetherDrive = true; ShowActiveSkillSpell(GroundOne.MC, Database.AETHER_DRIVE); }

                else if ((GroundOne.MC.Level >= 36) && (!GroundOne.MC.CrushingBlow)) { GroundOne.MC.CrushingBlow = true; ShowActiveSkillSpell(GroundOne.MC, Database.CRUSHING_BLOW); }
                else if ((GroundOne.MC.Level >= 37) && (!GroundOne.MC.KineticSmash)) { GroundOne.MC.KineticSmash = true; ShowActiveSkillSpell(GroundOne.MC, Database.KINETIC_SMASH); }
                else if ((GroundOne.MC.Level >= 38) && (!GroundOne.MC.StanceOfEyes)) { GroundOne.MC.StanceOfEyes = true; ShowActiveSkillSpell(GroundOne.MC, Database.STANCE_OF_EYES); }
                else if ((GroundOne.MC.Level >= 39) && (!GroundOne.MC.Resurrection)) { GroundOne.MC.Resurrection = true; ShowActiveSkillSpell(GroundOne.MC, Database.RESURRECTION); }
                else if ((GroundOne.MC.Level >= 41) && (!GroundOne.MC.StaticBarrier)) { GroundOne.MC.StaticBarrier = true; ShowActiveSkillSpell(GroundOne.MC, Database.STATIC_BARRIER); }
                else if ((GroundOne.MC.Level >= 42) && (!GroundOne.MC.Genesis)) { GroundOne.MC.Genesis = true; ShowActiveSkillSpell(GroundOne.MC, Database.GENESIS); }
                else if ((GroundOne.MC.Level >= 43) && (!GroundOne.MC.LightDetonator)) { GroundOne.MC.LightDetonator = true; ShowActiveSkillSpell(GroundOne.MC, Database.LIGHT_DETONATOR); }
                else if ((GroundOne.MC.Level >= 44) && (!GroundOne.MC.ImmortalRave)) { GroundOne.MC.ImmortalRave = true; ShowActiveSkillSpell(GroundOne.MC, Database.IMMORTAL_RAVE); }
                else if ((GroundOne.MC.Level >= 45) && (!GroundOne.MC.ExaltedField)) { GroundOne.MC.ExaltedField = true; ShowActiveSkillSpell(GroundOne.MC, Database.EXALTED_FIELD); }
                else if ((GroundOne.MC.Level >= 46) && (!GroundOne.MC.PiercingFlame)) { GroundOne.MC.PiercingFlame = true; ShowActiveSkillSpell(GroundOne.MC, Database.PIERCING_FLAME); }
                else if ((GroundOne.MC.Level >= 47) && (!GroundOne.MC.SacredHeal)) { GroundOne.MC.SacredHeal = true; ShowActiveSkillSpell(GroundOne.MC, Database.SACRED_HEAL); }
                else if ((GroundOne.MC.Level >= 48) && (!GroundOne.MC.RisingAura)) { GroundOne.MC.RisingAura = true; ShowActiveSkillSpell(GroundOne.MC, Database.RISING_AURA); }
                else if ((GroundOne.MC.Level >= 49) && (!GroundOne.MC.ChillBurn)) { GroundOne.MC.ChillBurn = true; ShowActiveSkillSpell(GroundOne.MC, Database.CHILL_BURN); }
                else if ((GroundOne.MC.Level >= 50) && (!GroundOne.MC.SoulInfinity)) { GroundOne.MC.SoulInfinity = true; ShowActiveSkillSpell(GroundOne.MC, Database.SOUL_INFINITY); }

                else if ((GroundOne.MC.Level >= 51) && (!GroundOne.MC.HymnContract)) { GroundOne.MC.HymnContract = true; ShowActiveSkillSpell(GroundOne.MC, Database.HYMN_CONTRACT); }
                else if ((GroundOne.MC.Level >= 52) && (!GroundOne.MC.Catastrophe)) { GroundOne.MC.Catastrophe = true; ShowActiveSkillSpell(GroundOne.MC, Database.CATASTROPHE); }
                else if ((GroundOne.MC.Level >= 53) && (!GroundOne.MC.CelestialNova)) { GroundOne.MC.CelestialNova = true; ShowActiveSkillSpell(GroundOne.MC, Database.CELESTIAL_NOVA); }
                else if ((GroundOne.MC.Level >= 54) && (!GroundOne.MC.OnslaughtHit)) { GroundOne.MC.OnslaughtHit = true; ShowActiveSkillSpell(GroundOne.MC, Database.ONSLAUGHT_HIT); }
                else if ((GroundOne.MC.Level >= 55) && (!GroundOne.MC.PainfulInsanity)) { GroundOne.MC.PainfulInsanity = true; ShowActiveSkillSpell(GroundOne.MC, Database.PAINFUL_INSANITY); }
                else if ((GroundOne.MC.Level >= 56) && (!GroundOne.MC.LavaAnnihilation)) { GroundOne.MC.LavaAnnihilation = true; ShowActiveSkillSpell(GroundOne.MC, Database.LAVA_ANNIHILATION); }
                else if ((GroundOne.MC.Level >= 57) && (!GroundOne.MC.ConcussiveHit)) { GroundOne.MC.ConcussiveHit = true; ShowActiveSkillSpell(GroundOne.MC, Database.CONCUSSIVE_HIT); }
                else if ((GroundOne.MC.Level >= 58) && (!GroundOne.MC.EternalPresence)) { GroundOne.MC.EternalPresence = true; ShowActiveSkillSpell(GroundOne.MC, Database.ETERNAL_PRESENCE); }
                else if ((GroundOne.MC.Level >= 59) && (!GroundOne.MC.AusterityMatrix)) { GroundOne.MC.AusterityMatrix = true; ShowActiveSkillSpell(GroundOne.MC, Database.AUSTERITY_MATRIX); }
                else if ((GroundOne.MC.Level >= 60) && (!GroundOne.MC.SigilOfHomura)) { GroundOne.MC.SigilOfHomura = true; ShowActiveSkillSpell(GroundOne.MC, Database.SIGIL_OF_HOMURA); }

                else if ((GroundOne.MC.Level >= 61) && (!GroundOne.MC.EverDroplet)) { GroundOne.MC.EverDroplet = true; ShowActiveSkillSpell(GroundOne.MC, Database.EVER_DROPLET); }
                else if ((GroundOne.MC.Level >= 62) && (!GroundOne.MC.ONEAuthority)) { GroundOne.MC.ONEAuthority = true; ShowActiveSkillSpell(GroundOne.MC, Database.ONE_AUTHORITY); }
                else if ((GroundOne.MC.Level >= 63) && (!GroundOne.MC.AscendantMeteor)) { GroundOne.MC.AscendantMeteor = true; ShowActiveSkillSpell(GroundOne.MC, Database.ASCENDANT_METEOR); }
                else if ((GroundOne.MC.Level >= 64) && (!GroundOne.MC.FatalBlow)) { GroundOne.MC.FatalBlow = true; ShowActiveSkillSpell(GroundOne.MC, Database.FATAL_BLOW); }
                else if ((GroundOne.MC.Level >= 65) && (!GroundOne.MC.StanceOfDouble)) { GroundOne.MC.StanceOfDouble = true; ShowActiveSkillSpell(GroundOne.MC, Database.STANCE_OF_DOUBLE); }
                else if ((GroundOne.MC.Level >= 66) && (!GroundOne.MC.ZetaExplosion)) { GroundOne.MC.ZetaExplosion = true; ShowActiveSkillSpell(GroundOne.MC, Database.ZETA_EXPLOSION); }
                else
                {
                    GroundOne.CumultiveLvUpValue = 0;
                    GroundOne.LevelUp = false;
                    GroundOne.UpPoint = 0;
                    GroundOne.LevelUpRoutine = true;
                    SceneDimension.Back(this);
                }
                #endregion
            }
            else if (GroundOne.LevelUp && Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color) == GroundOne.SC)
            {
                #region "ラナ・レベルアップ習得表"
                if ((GroundOne.SC.Level >= 2) && (!GroundOne.SC.IceNeedle)) { GroundOne.SC.AvailableMana = true; GroundOne.SC.IceNeedle = true; ShowActiveSkillSpell(GroundOne.SC, Database.ICE_NEEDLE); }
                else if ((GroundOne.SC.Level >= 3) && (!GroundOne.SC.CounterAttack)) { GroundOne.SC.AvailableSkill = true; GroundOne.SC.CounterAttack = true; ShowActiveSkillSpell(GroundOne.SC, Database.COUNTER_ATTACK); }
                else if ((GroundOne.SC.Level >= 5) && (!GroundOne.SC.DarkBlast)) { GroundOne.SC.DarkBlast = true; ShowActiveSkillSpell(GroundOne.SC, Database.DARK_BLAST); }
                else if ((GroundOne.SC.Level >= 6) && (!GroundOne.SC.AbsorbWater)) { GroundOne.SC.AbsorbWater = true; ShowActiveSkillSpell(GroundOne.SC, Database.ABSORB_WATER); }
                else if ((GroundOne.SC.Level >= 7) && (!GroundOne.SC.StanceOfFlow)) { GroundOne.SC.StanceOfFlow = true; ShowActiveSkillSpell(GroundOne.SC, Database.STANCE_OF_FLOW); }
                else if ((GroundOne.SC.Level >= 8) && (!GroundOne.SC.ShadowPact)) { GroundOne.SC.ShadowPact = true; ShowActiveSkillSpell(GroundOne.SC, Database.SHADOW_PACT); }
                else if ((GroundOne.SC.Level >= 9) && (!GroundOne.SC.DispelMagic)) { GroundOne.SC.DispelMagic = true; ShowActiveSkillSpell(GroundOne.SC, Database.DISPEL_MAGIC); }
                else if ((GroundOne.SC.Level >= 10) && (!GroundOne.SC.LifeTap)) { GroundOne.SC.LifeTap = true; ShowActiveSkillSpell(GroundOne.SC, Database.LIFE_TAP); }
                else if ((GroundOne.SC.Level >= 11) && (!GroundOne.SC.PurePurification)) { GroundOne.SC.PurePurification = true; ShowActiveSkillSpell(GroundOne.SC, Database.PURE_PURIFICATION); }
                else if ((GroundOne.SC.Level >= 12) && (!GroundOne.SC.EnigmaSence)) { GroundOne.SC.EnigmaSence = true; ShowActiveSkillSpell(GroundOne.SC, Database.ENIGMA_SENSE); }
                else if ((GroundOne.SC.Level >= 13) && (!GroundOne.SC.BlackContract)) { GroundOne.SC.BlackContract = true; ShowActiveSkillSpell(GroundOne.SC, Database.BLACK_CONTRACT); }
                else if ((GroundOne.SC.Level >= 14) && (!GroundOne.SC.Cleansing)) { GroundOne.SC.Cleansing = true; ShowActiveSkillSpell(GroundOne.SC, Database.CLEANSING); }
                else if ((GroundOne.SC.Level >= 15) && (!GroundOne.SC.Negate)) { GroundOne.SC.Negate = true; ShowActiveSkillSpell(GroundOne.SC, Database.NEGATE); }
                else if ((GroundOne.SC.Level >= 16) && (!GroundOne.SC.FrozenLance)) { GroundOne.SC.FrozenLance = true; ShowActiveSkillSpell(GroundOne.SC, Database.FROZEN_LANCE); }
                else if ((GroundOne.SC.Level >= 17) && (!GroundOne.SC.RiseOfImage)) { GroundOne.SC.RiseOfImage = true; ShowActiveSkillSpell(GroundOne.SC, Database.RISE_OF_IMAGE); }
                else if ((GroundOne.SC.Level >= 18) && (!GroundOne.SC.Deflection)) { GroundOne.SC.Deflection = true; ShowActiveSkillSpell(GroundOne.SC, Database.DEFLECTION); }
                else if ((GroundOne.SC.Level >= 19) && (!GroundOne.SC.Tranquility)) { GroundOne.SC.Tranquility = true; ShowActiveSkillSpell(GroundOne.SC, Database.TRANQUILITY); }
                else if ((GroundOne.SC.Level >= 20) && (!GroundOne.SC.VoidExtraction)) { GroundOne.SC.VoidExtraction = true; ShowActiveSkillSpell(GroundOne.SC, Database.VOID_EXTRACTION); }
                // [警告] ここで一気にレベルを挙げられると、複合魔法・スキルの習得に違和感が出てしまう。
                else if ((GroundOne.SC.Level >= 24) && (!GroundOne.SC.DevouringPlague)) { GroundOne.SC.DevouringPlague = true; ShowActiveSkillSpell(GroundOne.SC, Database.DEVOURING_PLAGUE); }
                else if ((GroundOne.SC.Level >= 25) && (!GroundOne.SC.MirrorImage)) { GroundOne.SC.MirrorImage = true; ShowActiveSkillSpell(GroundOne.SC, Database.MIRROR_IMAGE); }
                else if ((GroundOne.SC.Level >= 26) && (!GroundOne.SC.OneImmunity)) { GroundOne.SC.OneImmunity = true; ShowActiveSkillSpell(GroundOne.SC, Database.ONE_IMMUNITY); }

                else if ((GroundOne.SC.Level >= 36) && (!GroundOne.SC.AntiStun)) { GroundOne.SC.AntiStun = true; ShowActiveSkillSpell(GroundOne.SC, Database.ANTI_STUN); }
                else if ((GroundOne.SC.Level >= 37) && (!GroundOne.SC.SilentRush)) { GroundOne.SC.SilentRush = true; ShowActiveSkillSpell(GroundOne.SC, Database.SILENT_RUSH); }
                else if ((GroundOne.SC.Level >= 38) && (!GroundOne.SC.CarnageRush)) { GroundOne.SC.CarnageRush = true; ShowActiveSkillSpell(GroundOne.SC, Database.CARNAGE_RUSH); }
                else if ((GroundOne.SC.Level >= 39) && (!GroundOne.SC.BloodyVengeance)) { GroundOne.SC.BloodyVengeance = true; ShowActiveSkillSpell(GroundOne.SC, Database.BLOODY_VENGEANCE); }
                else if ((GroundOne.SC.Level >= 41) && (!GroundOne.SC.SacredHeal)) { GroundOne.SC.SacredHeal = true; ShowActiveSkillSpell(GroundOne.SC, Database.SACRED_HEAL); }
                else if ((GroundOne.SC.Level >= 42) && (!GroundOne.SC.WhiteOut)) { GroundOne.SC.WhiteOut = true; ShowActiveSkillSpell(GroundOne.SC, Database.WHITE_OUT); }
                else if ((GroundOne.SC.Level >= 43) && (!GroundOne.SC.DeepMirror)) { GroundOne.SC.DeepMirror = true; ShowActiveSkillSpell(GroundOne.SC, Database.DEEP_MIRROR); }
                else if ((GroundOne.SC.Level >= 44) && (!GroundOne.SC.PromisedKnowledge)) { GroundOne.SC.PromisedKnowledge = true; ShowActiveSkillSpell(GroundOne.SC, Database.PROMISED_KNOWLEDGE); }
                else if ((GroundOne.SC.Level >= 45) && (!GroundOne.SC.DoomBlade)) { GroundOne.SC.DoomBlade = true; ShowActiveSkillSpell(GroundOne.SC, Database.DOOM_BLADE); }
                else if ((GroundOne.SC.Level >= 46) && (!GroundOne.SC.VortexField)) { GroundOne.SC.VortexField = true; ShowActiveSkillSpell(GroundOne.SC, Database.VORTEX_FIELD); }
                else if ((GroundOne.SC.Level >= 47) && (!GroundOne.SC.AngelBreath)) { GroundOne.SC.AngelBreath = true; ShowActiveSkillSpell(GroundOne.SC, Database.ANGEL_BREATH); }
                else if ((GroundOne.SC.Level >= 48) && (!GroundOne.SC.UnknownShock)) { GroundOne.SC.UnknownShock = true; ShowActiveSkillSpell(GroundOne.SC, Database.UNKNOWN_SHOCK); }
                else if ((GroundOne.SC.Level >= 49) && (!GroundOne.SC.BlindJustice)) { GroundOne.SC.BlindJustice = true; ShowActiveSkillSpell(GroundOne.SC, Database.BLIND_JUSTICE); }
                else if ((GroundOne.SC.Level >= 50) && (!GroundOne.SC.StanceOfDeath)) { GroundOne.SC.StanceOfDeath = true; ShowActiveSkillSpell(GroundOne.SC, Database.STANCE_OF_DEATH); }

                else if ((GroundOne.SC.Level >= 51) && (!GroundOne.SC.EclipseEnd)) { GroundOne.SC.EclipseEnd = true; ShowActiveSkillSpell(GroundOne.SC, Database.ECLIPSE_END); }
                else if ((GroundOne.SC.Level >= 52) && (!GroundOne.SC.OboroImpact)) { GroundOne.SC.OboroImpact = true; ShowActiveSkillSpell(GroundOne.SC, Database.OBORO_IMPACT); }
                else if ((GroundOne.SC.Level >= 53) && (!GroundOne.SC.Damnation)) { GroundOne.SC.Damnation = true; ShowActiveSkillSpell(GroundOne.SC, Database.DAMNATION); }
                else if ((GroundOne.SC.Level >= 54) && (!GroundOne.SC.MindKilling)) { GroundOne.SC.MindKilling = true; ShowActiveSkillSpell(GroundOne.SC, Database.MIND_KILLING); }
                else if ((GroundOne.SC.Level >= 55) && (!GroundOne.SC.NothingOfNothingness)) { GroundOne.SC.NothingOfNothingness = true; ShowActiveSkillSpell(GroundOne.SC, Database.NOTHING_OF_NOTHINGNESS); }
                else if ((GroundOne.SC.Level >= 56) && (!GroundOne.SC.AbsoluteZero)) { GroundOne.SC.AbsoluteZero = true; ShowActiveSkillSpell(GroundOne.SC, Database.ABSOLUTE_ZERO); }
                else if ((GroundOne.SC.Level >= 57) && (!GroundOne.SC.NourishSense)) { GroundOne.SC.NourishSense = true; ShowActiveSkillSpell(GroundOne.SC, Database.NOURISH_SENSE); }
                else if ((GroundOne.SC.Level >= 58) && (!GroundOne.SC.TimeStop)) { GroundOne.SC.TimeStop = true; ShowActiveSkillSpell(GroundOne.SC, Database.TIME_STOP); }
                else if ((GroundOne.SC.Level >= 59) && (!GroundOne.SC.EverDroplet)) { GroundOne.SC.EverDroplet = true; ShowActiveSkillSpell(GroundOne.SC, Database.EVER_DROPLET); }
                else if ((GroundOne.SC.Level >= 60) && (!GroundOne.SC.BlueDragonWill)) { GroundOne.SC.BlueDragonWill = true; ShowActiveSkillSpell(GroundOne.SC, Database.BLUE_DRAGON_WILL); }

                else if ((GroundOne.SC.Level >= 61) && (!GroundOne.SC.EndlessAnthem)) { GroundOne.SC.EndlessAnthem = true; ShowActiveSkillSpell(GroundOne.SC, Database.ENDLESS_ANTHEM); }
                else if ((GroundOne.SC.Level >= 62) && (!GroundOne.SC.ImpulseHit)) { GroundOne.SC.ImpulseHit = true; ShowActiveSkillSpell(GroundOne.SC, Database.IMPULSE_HIT); }
                else if ((GroundOne.SC.Level >= 63) && (!GroundOne.SC.DeathDeny)) { GroundOne.SC.DeathDeny = true; ShowActiveSkillSpell(GroundOne.SC, Database.DEATH_DENY); }
                else if ((GroundOne.SC.Level >= 64) && (!GroundOne.SC.ConcussiveHit)) { GroundOne.SC.ConcussiveHit = true; ShowActiveSkillSpell(GroundOne.SC, Database.CONCUSSIVE_HIT); }
                else if ((GroundOne.SC.Level >= 65) && (!GroundOne.SC.SoulExecution)) { GroundOne.SC.SoulExecution = true; ShowActiveSkillSpell(GroundOne.SC, Database.SOUL_EXECUTION); }
                else if ((GroundOne.SC.Level >= 66) && (!GroundOne.SC.TranscendentWish)) { GroundOne.SC.TranscendentWish = true; ShowActiveSkillSpell(GroundOne.SC, Database.TRANSCENDENT_WISH); }
                else
                {
                    GroundOne.CumultiveLvUpValue = 0;
                    GroundOne.LevelUp = false;
                    GroundOne.UpPoint = 0;
                    GroundOne.LevelUpRoutine = true;
                    SceneDimension.Back(this);
                }
                #endregion
            }
            else if (GroundOne.LevelUp && Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color) == GroundOne.TC)
            {
                // ランディスはレベル上限MAX35からスタートのため、習得はない。
                #region "ヴェルゼ・レベルアップ習得表"
                if ((GroundOne.TC.Level >= 36) && (!GroundOne.TC.StanceOfEyes)) { GroundOne.TC.StanceOfEyes = true; ShowActiveSkillSpell(GroundOne.TC, Database.STANCE_OF_EYES); }
                else if ((GroundOne.TC.Level >= 37) && (!GroundOne.TC.SwiftStep)) { GroundOne.TC.SwiftStep = true; ShowActiveSkillSpell(GroundOne.TC, Database.SWIFT_STEP); }
                else if ((GroundOne.TC.Level >= 38) && (!GroundOne.TC.Resurrection)) { GroundOne.TC.Resurrection = true; ShowActiveSkillSpell(GroundOne.TC, Database.RESURRECTION); }
                else if ((GroundOne.TC.Level >= 39) && (!GroundOne.TC.BlindJustice)) { GroundOne.TC.BlindJustice = true; ShowActiveSkillSpell(GroundOne.TC, Database.BLIND_JUSTICE); }
                else if ((GroundOne.TC.Level >= 41) && (!GroundOne.TC.Genesis)) { GroundOne.TC.Genesis = true; ShowActiveSkillSpell(GroundOne.TC, Database.GENESIS); }
                else if ((GroundOne.TC.Level >= 42) && (!GroundOne.TC.DeepMirror)) { GroundOne.TC.DeepMirror = true; ShowActiveSkillSpell(GroundOne.TC, Database.DEEP_MIRROR); }
                else if ((GroundOne.TC.Level >= 43) && (!GroundOne.TC.ImmortalRave)) { GroundOne.TC.ImmortalRave = true; ShowActiveSkillSpell(GroundOne.TC, Database.IMMORTAL_RAVE); }
                else if ((GroundOne.TC.Level >= 44) && (!GroundOne.TC.DoomBlade)) { GroundOne.TC.DoomBlade = true; ShowActiveSkillSpell(GroundOne.TC, Database.DOOM_BLADE); }
                else if ((GroundOne.TC.Level >= 45) && (!GroundOne.TC.CarnageRush)) { GroundOne.TC.CarnageRush = true; ShowActiveSkillSpell(GroundOne.TC, Database.CARNAGE_RUSH); }
                else if ((GroundOne.TC.Level >= 46) && (!GroundOne.TC.ChillBurn)) { GroundOne.TC.ChillBurn = true; ShowActiveSkillSpell(GroundOne.TC, Database.CHILL_BURN); }
                else if ((GroundOne.TC.Level >= 47) && (!GroundOne.TC.WhiteOut)) { GroundOne.TC.WhiteOut = true; ShowActiveSkillSpell(GroundOne.TC, Database.WHITE_OUT); }
                else if ((GroundOne.TC.Level >= 48) && (!GroundOne.TC.PhantasmalWind)) { GroundOne.TC.PhantasmalWind = true; ShowActiveSkillSpell(GroundOne.TC, Database.PHANTASMAL_WIND); }
                else if ((GroundOne.TC.Level >= 49) && (!GroundOne.TC.PainfulInsanity)) { GroundOne.TC.PainfulInsanity = true; ShowActiveSkillSpell(GroundOne.TC, Database.PAINFUL_INSANITY); }
                else if ((GroundOne.TC.Level >= 50) && (!GroundOne.TC.FatalBlow)) { GroundOne.TC.FatalBlow = true; ShowActiveSkillSpell(GroundOne.TC, Database.FATAL_BLOW); }

                else if ((GroundOne.TC.Level >= 51) && (!GroundOne.TC.StaticBarrier)) { GroundOne.TC.StaticBarrier = true; ShowActiveSkillSpell(GroundOne.TC, Database.STATIC_BARRIER); }
                else if ((GroundOne.TC.Level >= 52) && (!GroundOne.TC.StanceOfDeath)) { GroundOne.TC.StanceOfDeath = true; ShowActiveSkillSpell(GroundOne.TC, Database.STANCE_OF_DEATH); }
                else if ((GroundOne.TC.Level >= 53) && (!GroundOne.TC.EverDroplet)) { GroundOne.TC.EverDroplet = true; ShowActiveSkillSpell(GroundOne.TC, Database.EVER_DROPLET); }
                else if ((GroundOne.TC.Level >= 54) && (!GroundOne.TC.Catastrophe)) { GroundOne.TC.Catastrophe = true; ShowActiveSkillSpell(GroundOne.TC, Database.CATASTROPHE); }
                else if ((GroundOne.TC.Level >= 55) && (!GroundOne.TC.CelestialNova)) { GroundOne.TC.CelestialNova = true; ShowActiveSkillSpell(GroundOne.TC, Database.CELESTIAL_NOVA); }
                else if ((GroundOne.TC.Level >= 56) && (!GroundOne.TC.MindKilling)) { GroundOne.TC.MindKilling = true; ShowActiveSkillSpell(GroundOne.TC, Database.MIND_KILLING); }
                else if ((GroundOne.TC.Level >= 57) && (!GroundOne.TC.NothingOfNothingness)) { GroundOne.TC.NothingOfNothingness = true; ShowActiveSkillSpell(GroundOne.TC, Database.NOTHING_OF_NOTHINGNESS); }
                else if ((GroundOne.TC.Level >= 58) && (!GroundOne.TC.AbsoluteZero)) { GroundOne.TC.AbsoluteZero = true; ShowActiveSkillSpell(GroundOne.TC, Database.ABSOLUTE_ZERO); }
                else if ((GroundOne.TC.Level >= 59) && (!GroundOne.TC.AusterityMatrix)) { GroundOne.TC.AusterityMatrix = true; ShowActiveSkillSpell(GroundOne.TC, Database.AUSTERITY_MATRIX); }
                else if ((GroundOne.TC.Level >= 60) && (!GroundOne.TC.VigorSense)) { GroundOne.TC.VigorSense = true; ShowActiveSkillSpell(GroundOne.TC, Database.VIGOR_SENSE); }

                else if ((GroundOne.TC.Level >= 61) && (!GroundOne.TC.LavaAnnihilation)) { GroundOne.TC.LavaAnnihilation = true; ShowActiveSkillSpell(GroundOne.TC, Database.LAVA_ANNIHILATION); }
                else if ((GroundOne.TC.Level >= 62) && (!GroundOne.TC.EclipseEnd)) { GroundOne.TC.EclipseEnd = true; ShowActiveSkillSpell(GroundOne.TC, Database.ECLIPSE_END); }
                else if ((GroundOne.TC.Level >= 63) && (!GroundOne.TC.TimeStop)) { GroundOne.TC.TimeStop = true; ShowActiveSkillSpell(GroundOne.TC, Database.TIME_STOP); }
                else if ((GroundOne.TC.Level >= 64) && (!GroundOne.TC.SinFortune)) { GroundOne.TC.SinFortune = true; ShowActiveSkillSpell(GroundOne.TC, Database.SIN_FORTUNE); }
                else if ((GroundOne.TC.Level >= 65) && (!GroundOne.TC.DemonicIgnite)) { GroundOne.TC.DemonicIgnite = true; ShowActiveSkillSpell(GroundOne.TC, Database.DEMONIC_IGNITE); }
                else if ((GroundOne.TC.Level >= 66) && (!GroundOne.TC.StanceOfDouble)) { GroundOne.TC.StanceOfDouble = true; ShowActiveSkillSpell(GroundOne.TC, Database.STANCE_OF_DOUBLE); }
                else if ((GroundOne.TC.Level >= 67) && (!GroundOne.TC.WarpGate)) { GroundOne.TC.WarpGate = true; ShowActiveSkillSpell(GroundOne.TC, Database.WARP_GATE); }
                else if ((GroundOne.TC.Level >= 68) && (!GroundOne.TC.StanceOfMystic)) { GroundOne.TC.StanceOfMystic = true; ShowActiveSkillSpell(GroundOne.TC, Database.STANCE_OF_MYSTIC); }
                else if ((GroundOne.TC.Level >= 69) && (!GroundOne.TC.SoulExecution)) { GroundOne.TC.SoulExecution = true; ShowActiveSkillSpell(GroundOne.TC, Database.SOUL_EXECUTION); }
                else if ((GroundOne.TC.Level >= 70) && (!GroundOne.TC.ZetaExplosion)) { GroundOne.TC.ZetaExplosion = true; ShowActiveSkillSpell(GroundOne.TC, Database.ZETA_EXPLOSION); }
                else
                {
                    GroundOne.CumultiveLvUpValue = 0;
                    GroundOne.LevelUp = false;
                    GroundOne.UpPoint = 0;
                    GroundOne.LevelUpRoutine = true;
                    SceneDimension.Back(this);
                }
                #endregion
            }
            else
            {
                SceneDimension.Back(this);
            }
        }

        private void ShowActiveSkillSpell(MainCharacter player, string skillSpellName)
        {
            for (int ii = 0; ii < player.BattleActionCommandList.Length; ii++)
            {
                if (player.BattleActionCommandList[ii] == "")
                {
                    player.BattleActionCommandList[ii] = skillSpellName;
                    break;
                }
            }

            SceneDimension.CallTruthSkillSpellDesc(this, player.FirstName, skillSpellName);
        }


        private void RefreshPartyMembersLife(Text player1Life, Text player2Life, Text player3Life)
        {
            if (GroundOne.WE.AvailableFirstCharacter)
            {
                player1Life.text = GroundOne.MC.CurrentLife.ToString() + "/" + GroundOne.MC.MaxLife.ToString();
            }
            if (GroundOne.WE.AvailableSecondCharacter && GroundOne.DuelMode == false)
            {
                player2Life.text = GroundOne.SC.CurrentLife.ToString() + "/" + GroundOne.SC.MaxLife.ToString();
            }
            if (GroundOne.WE.AvailableThirdCharacter && GroundOne.DuelMode == false)
            {
                player3Life.text = GroundOne.TC.CurrentLife.ToString() + "/" + GroundOne.TC.MaxLife.ToString();
            }
        }

        private void UsingItemUpdateBackPackLabel(MainCharacter player, ItemBackPack backpackData, Text sender, int currentNumber)
        {
            int stackValue = player.CheckBackPackExist(backpackData, currentNumber);
            if (stackValue <= 0)
            {
                backpack[currentNumber].text = "";
                backpackStack[currentNumber].text = "";
                backpackIcon[currentNumber].sprite = null;
                Method.UpdateRareColor(null, backpack[currentNumber], back_Backpack[currentNumber], null);
                //back_Backpack[currentNumber].SetActive(false);
            }
            else
            {
                backpackStack[currentNumber].text = "x" + stackValue.ToString();
            }
        }

        Text currentSelect = null;
        int currentNumber = 0;
        Vector3 currentPosition;

        public void Use_Click()
        {
            groupChoice.SetActive(false);
            backpackFilter.SetActive(false);

            this.ItemChoiced = true;

            MainCharacter player = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);
            ItemBackPack backpackData = new ItemBackPack(currentSelect.text);

            if (player.Dead)
            {
                mainMessage.text = "【" + player.FirstName + "は死んでしまっているため、アイテムが使えない。】";
                return;
            }

            switch (backpackData.Name)
            {
                case Database.POOR_SMALL_RED_POTION:
                case Database.COMMON_NORMAL_RED_POTION:
                case Database.COMMON_LARGE_RED_POTION:
                case Database.COMMON_HUGE_RED_POTION:
                case Database.COMMON_GORGEOUS_RED_POTION:
                case Database.RARE_PERFECT_RED_POTION:
                case "名前がとても長いわりにはまったく役に立たず、何の効果も発揮しない役立たずであるにもかかわらずデコレーションが長い超豪華なスーパーミラクルポーション":
                    int effect = backpackData.UseIt();
                    if (player.CurrentNourishSense > 0)
                    {
                        effect = (int)((double)effect * PrimaryLogic.NourishSenseValue(player));
                    }
                    player.CurrentLife += effect;
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    this.life.text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();
                    mainMessage.text = String.Format(player.GetCharacterSentence(2001), effect);
                    UsingItemUpdateBackPackLabel(player, backpackData, currentSelect, currentNumber);
                    RefreshPartyMembersLife(labelFirstPlayerLife, labelSecondPlayerLife, labelThirdPlayerLife);
                    break;

                case Database.POOR_SMALL_BLUE_POTION:
                case Database.COMMON_NORMAL_BLUE_POTION:
                case Database.COMMON_LARGE_BLUE_POTION:
                case Database.COMMON_HUGE_BLUE_POTION:
                case Database.COMMON_GORGEOUS_BLUE_POTION:
                case Database.RARE_PERFECT_BLUE_POTION:
                    effect = backpackData.UseIt();
                    player.CurrentMana += effect;
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    this.mana.text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
                    mainMessage.text = String.Format(player.GetCharacterSentence(2001), effect);
                    UsingItemUpdateBackPackLabel(player, backpackData, currentSelect, currentNumber);
                    RefreshPartyMembersLife(labelFirstPlayerLife, labelSecondPlayerLife, labelThirdPlayerLife);
                    break;

                case Database.POOR_SMALL_GREEN_POTION:
                case Database.COMMON_NORMAL_GREEN_POTION:
                case Database.COMMON_LARGE_GREEN_POTION:
                case Database.COMMON_HUGE_GREEN_POTION:
                case Database.COMMON_GORGEOUS_GREEN_POTION:
                case Database.RARE_PERFECT_GREEN_POTION:
                    effect = backpackData.UseIt();
                    player.CurrentSkillPoint += effect;
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    this.skill.text = player.CurrentSkillPoint.ToString() + " / " + player.MaxSkillPoint.ToString();
                    mainMessage.text = String.Format(player.GetCharacterSentence(2001), effect);
                    UsingItemUpdateBackPackLabel(player, backpackData, currentSelect, currentNumber);
                    RefreshPartyMembersLife(labelFirstPlayerLife, labelSecondPlayerLife, labelThirdPlayerLife);
                    break;

                case Database.COMMON_REVIVE_POTION_MINI:
                    this.useTargetedItem = true;
                    WhoTarget_View();
                    break;

                case Database.COMMON_POTION_RESIST_FIRE:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.CurrentResistFireUp = Database.INFINITY;
                        player.CurrentResistFireUpValue = 50;
                        player.ActivateBuff(player.pbResistFireUp, Database.BaseResourceFolder + "ResistFireUp", Database.INFINITY);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.COMMON_POTION_MAGIC_SEAL:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.AmplifyMagicAttack = 1.05f;
                        player.ActivateBuff(player.pbMagicAttackUp, Database.BaseResourceFolder + "BuffMagicAttackUp.bmp", Database.INFINITY);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.COMMON_POTION_ATTACK_SEAL:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.AmplifyPhysicalAttack = 1.05f;
                        player.ActivateBuff(player.pbPhysicalAttackUp, Database.BaseResourceFolder + "BuffPhysicalAttackUp.bmp", Database.INFINITY);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;


                case Database.POOR_POTION_CURE_POISON:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.CurrentPoison = 0;
                        player.CurrentPoisonValue = 0;
                        player.DeBuff(player.pbPoison);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.COMMON_POTION_NATURALIZE:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.CurrentPoison = 0;
                        player.CurrentPoisonValue = 0;
                        player.DeBuff(player.pbPoison);
                        player.CurrentSlow = 0;
                        player.DeBuff(player.pbSlow);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.COMMON_POTION_CURE_BLIND:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.CurrentBlind = 0;
                        player.DeBuff(player.pbBlind);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.RARE_POTION_MOSSGREEN_DREAM:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.RemoveSlow();
                        player.RemovePoison();
                        player.RemoveBlind();
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.RARE_DRYAD_SAGE_POTION:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.AmplifyBattleSpeed = 1.05f;
                        player.AmplifyBattleResponse = 1.05f;
                        player.ActivateBuff(player.pbSpeedUp, Database.BaseResourceFolder + "BuffSpeedUp", Database.INFINITY);
                        player.ActivateBuff(player.pbReactionUp, Database.BaseResourceFolder + "BuffReactionUp", Database.INFINITY);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.COMMON_RESIST_POISON:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.CurrentPoison = 0;
                        player.CurrentPoisonValue = 0;
                        player.DeBuff(player.pbPoison);
                        player.ResistPoison = true;
                        player.ActivateBuff(player.pbResistPoison, Database.BaseResourceFolder + "ResistPoison.bmp", Database.INFINITY);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.COMMON_POTION_OVER_GROWTH:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.CurrentStaminaUp = Database.INFINITY;
                        player.CurrentStaminaUpValue = 100; // スタミナUPは内部処理で10倍されてるため、ここでは1000/10で100
                        player.ActivateBuff(player.pbStaminaUp, Database.BaseResourceFolder + "BuffStaminaUp", Database.INFINITY);
                        player.labelCurrentLifePoint.text = player.CurrentLife.ToString();
                        if (player.CurrentLife >= player.MaxLife)
                        {
                            player.labelCurrentLifePoint.color = Color.green;
                        }
                        else
                        {
                            player.labelCurrentLifePoint.color = Color.black;
                        }
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.COMMON_POTION_RAINBOW_IMPACT:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.RemovePhysicalAttackDown();
                        player.RemoveMagicAttackDown();
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.COMMON_POTION_BLACK_GAST:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.AmplifyMagicAttack = 1.07f;
                        player.AmplifyPhysicalAttack = 1.07f;
                        player.ActivateBuff(player.pbMagicAttackUp, Database.BaseResourceFolder + "BuffMagicAttackUp", Database.INFINITY);
                        player.ActivateBuff(player.pbPhysicalAttackUp, Database.BaseResourceFolder + "BuffPhysicalAttackUp", Database.INFINITY);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.COMMON_SOUKAI_DRINK_SS:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.AmplifyMagicAttack = 1.07f;
                        player.AmplifyBattleSpeed = 1.07f;
                        player.ActivateBuff(player.pbMagicAttackUp, Database.BaseResourceFolder + "BuffMagicAttackUp", Database.INFINITY);
                        player.ActivateBuff(player.pbSpeedUp, Database.BaseResourceFolder + "BuffSpeedUp", Database.INFINITY);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.COMMON_TUUKAI_DRINK_DD:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.AmplifyPhysicalAttack = 1.07f;
                        player.AmplifyBattleSpeed = 1.07f;
                        player.ActivateBuff(player.pbPhysicalAttackUp, Database.BaseResourceFolder + "BuffPhysicalAttackUp", Database.INFINITY);
                        player.ActivateBuff(player.pbSpeedUp, Database.BaseResourceFolder + "BuffSpeedUp", Database.INFINITY);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.COMMON_FAIRY_BREATH:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.RemoveSilence();
                        player.ResistSilence = true;
                        player.ActivateBuff(player.pbResistSilence, Database.BaseResourceFolder + "ResistSilence.bmp", Database.INFINITY);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.COMMON_HEART_ACCELERATION:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.RemoveParalyze();
                        player.ResistParalyze = true;
                        player.ActivateBuff(player.pbResistParalyze, Database.BaseResourceFolder + "ResistParalyze.bmp", Database.INFINITY);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.RARE_SAGE_POTION_MINI:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.RemoveDebuffEffect();
                        player.RemoveDebuffParam();
                        player.RemoveDebuffSpell();
                        player.RemoveDebuffSkill();
                        player.CurrentSagePotionMini = Database.INFINITY;
                        player.CurrentNoResurrection = Database.INFINITY;
                        player.ActivateBuff(player.pbNoResurrection, Database.BaseResourceFolder + "NoResurrection.bmp", Database.INFINITY);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.RARE_POWER_SURGE:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.BuffUpStrength(600);
                        player.BuffUpStamina(400);
                        player.BuffUpAmplifyPhysicalAttack(1.20f);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.RARE_ZEPHER_BREATH:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.BuffUpAgility(600);
                        player.BuffUpIntelligence(400);
                        player.BuffUpAmplifyBattleSpeed(1.20f);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.RARE_GENSEI_MAGIC_BOTTLE:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.BuffUpIntelligence(600);
                        player.BuffUpMind(400);
                        player.BuffUpAmplifyMagicAttack(1.20f);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.RARE_ZETTAI_STAMINAUP:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.BuffUpStrength(200);
                        player.BuffUpIntelligence(200);
                        player.BuffUpStamina(600);
                        player.BuffUpAmplifyPhysicalDefence(1.10f);
                        player.BuffUpAmplifyMagicDefense(1.10f);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.RARE_MIND_ILLUSION:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.BuffUpStrength(100);
                        player.BuffUpAgility(100);
                        player.BuffUpIntelligence(100);
                        player.BuffUpStamina(100);
                        player.BuffUpMind(600);
                        player.BuffUpAmplifyPotential(1.20f);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.RARE_GENSEI_TAIMA_KUSURI:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.CurrentGenseiTaima = Database.INFINITY;
                        player.ActivateBuff(player.pbGenseiTaima, Database.BaseResourceFolder + Database.ITEMCOMMAND_GENSEI_TAIMA, Database.INFINITY);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.RARE_SHINING_AETHER:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.CurrentShiningAether = 2; // 次のターンまで有効
                        player.ActivateBuff(player.pbShiningAether, Database.BaseResourceFolder + Database.ITEMCOMMAND_SHINING_AETHER, 2);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.RARE_BLACK_ELIXIR:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.CurrentBlackElixir = Database.INFINITY;
                        player.CurrentBlackElixirValue = player.MaxLife / 2;
                        player.CurrentLife += player.CurrentBlackElixirValue;
                        player.ActivateBuff(player.pbBlackElixir, Database.BaseResourceFolder + Database.ITEMCOMMAND_BLACK_ELIXIR, Database.INFINITY);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.RARE_ELEMENTAL_SEAL:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.RemoveDebuffEffect();
                        player.CurrentElementalSeal = Database.INFINITY;
                        player.ActivateBuff(player.pbElementalSeal, Database.BaseResourceFolder + Database.ITEMCOMMAND_ELEMENTAL_SEAL, Database.INFINITY);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.RARE_COLORLESS_ANTIDOTE:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.RemoveDebuffParam();
                        player.CurrentColorlessAntidote = Database.INFINITY;
                        player.ActivateBuff(player.pbColorlessAntidote, Database.BaseResourceFolder + Database.ITEMCOMMAND_COLORLESS_ANTIDOTE, Database.INFINITY);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case Database.RARE_TOTAL_HIYAKU_KASSEI:
                    if (this.onlyUseItem)
                    {
                        int maxValue = Math.Max(player.Strength,
                                        Math.Max(player.Agility,
                                                player.Intelligence));
                        if (maxValue == player.Strength)
                        {
                            player.BuffStrength_Hiyaku_Kassei = maxValue;
                        }
                        else if (maxValue == player.Agility)
                        {
                            player.BuffAgility_Hiyaku_Kassei = maxValue;
                        }
                        else if (maxValue == player.Intelligence)
                        {
                            player.BuffIntelligence_Hiyaku_Kassei = maxValue;
                        }
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        this.skill.text = player.CurrentSkillPoint.ToString() + " / " + player.MaxSkillPoint.ToString();
                        UsingItemUpdateBackPackLabel(player, backpackData, currentSelect, currentNumber);
                        RefreshPartyMembersLife(labelFirstPlayerLife, labelSecondPlayerLife, labelThirdPlayerLife);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2011);
                    }
                    break;

                case "神聖水": // ２階アイテム
                    if (!GroundOne.WE.AlreadyUseSyperSaintWater)
                    {
                        GroundOne.WE.AlreadyUseSyperSaintWater = true;
                        player.CurrentLife += (int)((double)player.MaxLife * 0.3F);
                        player.CurrentMana += (int)((double)player.MaxMana * 0.3F);
                        player.CurrentSkillPoint += (int)((double)player.MaxSkillPoint * 0.3F);
                        this.life.text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();
                        this.mana.text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
                        this.skill.text = player.CurrentSkillPoint.ToString() + " / " + player.MaxSkillPoint.ToString();
                        RefreshPartyMembersLife(labelFirstPlayerLife, labelSecondPlayerLife, labelThirdPlayerLife);
                        mainMessage.text = player.GetCharacterSentence(2009);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2010);
                    }
                    break;

                case Database.RARE_PURE_WATER:
                    if (!GroundOne.WE.AlreadyUsePureWater)
                    {
                        GroundOne.WE.AlreadyUsePureWater = true;
                        player.CurrentLife = (int)((double)player.MaxLife);
                        this.life.text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();
                        mainMessage.text = player.GetCharacterSentence(2027);
                        RefreshPartyMembersLife(labelFirstPlayerLife, labelSecondPlayerLife, labelThirdPlayerLife);
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2028);
                    }
                    break;

                case Database.RARE_REVIVE_POTION:
                    if (!GroundOne.WE.AlreadyUseRevivePotion)
                    {
                        this.useTargetedItem = true;
                        WhoTarget_View();
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2010);
                    }
                    break;

                case Database.EPIC_OVER_SHIFTING:
                    StatusButton1_Click();
                    btnClose.gameObject.SetActive(false);
                    mainMessage.text = player.GetCharacterSentence(2022);
                    this.useOverShifting = true;
                    this.usingOvershifting = true;
                    break;

                case Database.GROWTH_LIQUID_STRENGTH:
                case Database.GROWTH_LIQUID2_STRENGTH:
                case Database.GROWTH_LIQUID3_STRENGTH:
                case Database.GROWTH_LIQUID4_STRENGTH:
                case Database.GROWTH_LIQUID5_STRENGTH:
                    int effectValue = backpackData.MinValue + AP.Math.RandomInteger(backpackData.MaxValue - backpackData.MinValue + 1);
                    player.Strength += effectValue;
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    SettingCharacterData(player);
                    RefreshPartyMembersBattleStatus(player);
                    mainMessage.text = String.Format(player.GetCharacterSentence(2024), "力", effectValue.ToString());
                    break;
                case Database.GROWTH_LIQUID_AGILITY:
                case Database.GROWTH_LIQUID2_AGILITY:
                case Database.GROWTH_LIQUID3_AGILITY:
                case Database.GROWTH_LIQUID4_AGILITY:
                case Database.GROWTH_LIQUID5_AGILITY:
                    effectValue = backpackData.MinValue + AP.Math.RandomInteger(backpackData.MaxValue - backpackData.MinValue + 1);
                    player.Agility += effectValue;
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    SettingCharacterData(player);
                    RefreshPartyMembersBattleStatus(player);
                    mainMessage.text = String.Format(player.GetCharacterSentence(2024), "技", effectValue.ToString());
                    break;
                case Database.GROWTH_LIQUID_INTELLIGENCE:
                case Database.GROWTH_LIQUID2_INTELLIGENCE:
                case Database.GROWTH_LIQUID3_INTELLIGENCE:
                case Database.GROWTH_LIQUID4_INTELLIGENCE:
                case Database.GROWTH_LIQUID5_INTELLIGENCE:
                    effectValue = backpackData.MinValue + AP.Math.RandomInteger(backpackData.MaxValue - backpackData.MinValue + 1);
                    player.Intelligence += effectValue;
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    SettingCharacterData(player);
                    RefreshPartyMembersBattleStatus(player);
                    mainMessage.text = String.Format(player.GetCharacterSentence(2024), "知", effectValue.ToString());
                    break;
                case Database.GROWTH_LIQUID_STAMINA:
                case Database.GROWTH_LIQUID2_STAMINA:
                case Database.GROWTH_LIQUID3_STAMINA:
                case Database.GROWTH_LIQUID4_STAMINA:
                case Database.GROWTH_LIQUID5_STAMINA:
                    effectValue = backpackData.MinValue + AP.Math.RandomInteger(backpackData.MaxValue - backpackData.MinValue + 1);
                    player.Stamina += effectValue;
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    SettingCharacterData(player);
                    RefreshPartyMembersBattleStatus(player);
                    mainMessage.text = String.Format(player.GetCharacterSentence(2024), "体", effectValue.ToString());
                    break;
                case Database.GROWTH_LIQUID_MIND:
                case Database.GROWTH_LIQUID2_MIND:
                case Database.GROWTH_LIQUID3_MIND:
                case Database.GROWTH_LIQUID4_MIND:
                case Database.GROWTH_LIQUID5_MIND:
                    effectValue = backpackData.MinValue + AP.Math.RandomInteger(backpackData.MaxValue - backpackData.MinValue + 1);
                    player.Mind += effectValue;
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    SettingCharacterData(player);
                    RefreshPartyMembersBattleStatus(player);
                    mainMessage.text = String.Format(player.GetCharacterSentence(2024), "心", effectValue.ToString());
                    break;

                // 装備品：武器
                case "練習用の剣": // アイン初期装備
                case "ナックル": // ラナ初期装備
                case "白銀の剣（レプリカ）": // ヴェルゼ初期装備
                case "ショートソード": // ガンツの武具屋販売（ダンジョン１階）
                case "洗練されたロングソード": // ガンツの武具屋販売（ダンジョン１階）
                case "青銅の剣": // ガンツの武具屋販売（ダンジョン２階）
                case "メタルフィスト": // ガンツの武具屋販売（ダンジョン２階）
                case "プラチナソード": // ガンツの武具屋販売（ダンジョン３階）
                case "ファルシオン": // ガンツの武具屋販売（ダンジョン３階）
                case "アイアンクロー": // ガンツの武具屋販売（ダンジョン３階）
                case "シャムシール": // ３階アイテム
                case "ライトプラズマブレード": // ガンツの武具屋販売（ダンジョン４階）
                case "イスリアルフィスト": // ガンツの武具屋販売（ダンジョン４階）
                case "エスパダス": // ダンジョン４階のアイテム
                case "ソード・オブ・ブルールージュ": // ダンジョン４階のアイテム
                case "ルナ・エグゼキュージョナー": // ダンジョン５階
                case "蒼黒・氷大蛇の爪": // ダンジョン５階
                case "ファージル・ジ・エスペランザ": // ダンジョン５階
                case "神剣  フェルトゥーシュ":
                case "双剣  ジュノセレステ":
                case "極剣  ゼムルギアス":
                case "クロノス・ロマティッド・ソード":
                // 装備品：防具
                case "黒真空の鎧（レプリカ）": // ヴェルゼ初期装備
                case "コート・オブ・プレート": // アイン初期装備
                case "ライト・クロス": // ラナ初期装備
                case "冒険者用の鎖かたびら": // ガンツの武具屋販売（ダンジョン１階）
                case "青銅の鎧": // ガンツの武具屋販売（ダンジョン１階）
                case "真鍮の鎧": // ２階アイテム
                case "光沢のある鉄のプレート": // ガンツの武具屋販売（ダンジョン２階）
                case "シルクの武道衣": // ガンツの武具屋販売（ダンジョン２階）
                case "シルバーアーマー": // ガンツの武具屋販売（ダンジョン３階）
                case "獣皮製の舞踏衣": // ガンツの武具屋販売（ダンジョン３階）
                case "フィスト・クロス": // ガンツの武具屋販売（ダンジョン３階）
                case "プレート・アーマー": // ３階アイテム
                case "ラメラ・アーマー": // ３階アイテム
                case "プリズマティックアーマー": // ガンツの武具屋販売（ダンジョン４階）
                case "極薄合金製の羽衣": // ガンツの武具屋販売（ダンジョン４階）
                case "アヴォイド・クロス": // ダンジョン４階のアイテム
                case "ブリガンダィン": // ダンジョン４階のアイテム
                case "ロリカ・セグメンタータ": // ダンジョン４階のアイテム
                case "ヘパイストス・パナッサロイニ":
                // 装備品：アクセサリ
                case "珊瑚のブレスレット": // ラナ初期装備
                case "天空の翼（レプリカ）": // ヴェルゼ初期装備
                case Database.COMMON_CHARM_OF_FIRE_ANGEL: // １階アイテム e 後編編集
                case "チャクラオーブ": // １階アイテム
                case "些細なパワーリング": // ガンツの武具屋販売（ダンジョン１階）
                case "紺碧のスターエムブレム": // ガンツの武具屋販売（ダンジョン２階）
                case "闘魂バンド": // ガンツの武具屋販売（ダンジョン２階）
                case "鷹の刻印": // ２階アイテム
                case "身かわしのマント": // ２階アイテム
                case "ライオンハート": // ３階アイテム
                case "オーガの腕章": // ３階アイテム
                case "鋼鉄の石像": // ３階アイテム
                case "ファラ様信仰のシール": // ３階アイテム
                case "ウェルニッケの腕輪": // ガンツの武具屋販売（ダンジョン３階）
                case "賢者の眼鏡": // ガンツの武具屋販売（ダンジョン３階）
                case "七色プリズムバンド": // ガンツの武具屋販売（ダンジョン４階）
                case "再生の紋章": // ガンツの武具屋販売（ダンジョン４階）
                case "シールオブアクア＆ファイア": // ガンツの武具屋販売（ダンジョン４階）
                case "ドラゴンのベルト": // ガンツの武具屋販売（ダンジョン４階）
                case "剣紋章ペンダント": // ラナレベルアップ時でもらえるアイテム
                case "夢見の印章": // ダンジョン４階のアイテム
                case "天使の契約書": // ダンジョン４階のアイテム
                case "ラナのイヤリング": // ダンジョン５階（ラナのイベント） // ラナ専用
                case "レジェンド・レッドホース": // ダンジョン５階のアイテム
                case "エルミ・ジョルジュ　ファージル王家の刻印":
                case "ファラ・フローレ　天使のペンダント":
                case "シニキア・カールハンツ　魔道デビルアイ":
                case "オル・ランディス　炎神グローブ":
                case "ヴェルゼ・アーティ　天空の翼":
                // s 後編追加
                case Database.POOR_PRACTICE_SHILED:

                case Database.POOR_HINJAKU_ARMRING:
                case Database.POOR_USUYOGORETA_FEATHER:
                case Database.POOR_NON_BRIGHT_ORB:
                case Database.POOR_KUKEI_BANGLE:
                case Database.POOR_SUTERARESHI_EMBLEM:
                case Database.POOR_ARIFURETA_STATUE:
                case Database.POOR_NON_ADJUST_BELT:
                case Database.POOR_SIMPLE_EARRING:
                case Database.POOR_KATAKUZURESHITA_FINGERRING:
                case Database.POOR_IROASETA_CHOKER:
                case Database.POOR_YOREYORE_MANTLE:
                case Database.POOR_NON_HINSEI_CROWN:
                case Database.POOR_TUKAIFURUSARETA_SWORD:
                case Database.POOR_TUKAINIKUI_LONGSWORD:
                case Database.POOR_GATAGAKITERU_ARMOR:
                case Database.POOR_FESTERING_ARMOR:
                case Database.POOR_HINSO_SHIELD:
                case Database.POOR_MUDANIOOKII_SHIELD:

                case Database.COMMON_RED_PENDANT:
                case Database.COMMON_BLUE_PENDANT:
                case Database.COMMON_PURPLE_PENDANT:
                case Database.COMMON_GREEN_PENDANT:
                case Database.COMMON_YELLOW_PENDANT:
                case Database.COMMON_SISSO_ARMRING:
                case Database.COMMON_FINE_FEATHER:
                case Database.COMMON_KIREINA_ORB:
                case Database.COMMON_FIT_BANGLE:
                case Database.COMMON_PRISM_EMBLEM:
                case Database.COMMON_FINE_SWORD:
                case Database.COMMON_TWEI_SWORD:
                case Database.COMMON_FINE_ARMOR:
                case Database.COMMON_GOTHIC_PLATE:
                case Database.COMMON_FINE_SHIELD:
                case Database.COMMON_GRIPPING_SHIELD:

                case Database.RARE_JOUSITU_BLUE_POWERRING:
                case Database.RARE_KOUJOUSINYADORU_RED_ORB:
                case Database.RARE_MAGICIANS_MANTLE:
                case Database.RARE_BEATRUSH_BANGLE:
                case Database.RARE_AERO_BLADE:
                case Database.RARE_SUN_BRAVE_ARMOR:
                case Database.RARE_ESMERALDA_SHIELD:

                case Database.EPIC_RING_OF_OSCURETE:
                case Database.EPIC_MERGIZD_SOL_BLADE:

                case Database.COMMON_SIMPLE_BRACELET:
                case Database.POOR_HARD_SHOES:
                case Database.COMMON_SEAL_OF_POSION:
                    // e 後編追加
                    if (((backpackData.Type == ItemBackPack.ItemType.Weapon_Heavy) && (player == GroundOne.SC || player == GroundOne.TC)) // アイン専用
                        || ((backpackData.Type == ItemBackPack.ItemType.Weapon_Light) && (player == GroundOne.MC || player == GroundOne.TC)) // ラナ専用
                        || ((backpackData.Type == ItemBackPack.ItemType.Weapon_Middle) && (player == GroundOne.MC || player == GroundOne.SC)) // ヴェルゼ専用
                        || ((backpackData.Type == ItemBackPack.ItemType.Armor_Heavy) && (player == GroundOne.SC || player == GroundOne.TC)) // アイン専用
                        || ((backpackData.Type == ItemBackPack.ItemType.Armor_Light) && (player == GroundOne.MC || player == GroundOne.TC)) // ラナ専用
                        || ((backpackData.Type == ItemBackPack.ItemType.Armor_Middle) && (player == GroundOne.MC || player == GroundOne.SC)) // ヴェルゼ専用
                        || ((backpackData.EquipablePerson == ItemBackPack.Equipable.Ein) && (player == GroundOne.SC || player == GroundOne.TC)) // アイン専用
                        || ((backpackData.EquipablePerson == ItemBackPack.Equipable.Lana) && (player == GroundOne.MC || player == GroundOne.TC)) // ラナ専用
                        || ((backpackData.EquipablePerson == ItemBackPack.Equipable.Verze) && (player == GroundOne.MC || player == GroundOne.SC))) // ヴェルゼ専用
                    {
                        mainMessage.text = player.GetCharacterSentence(2019);
                        return;
                    }
                    EquipDecision(player, backpackData, currentSelect, currentNumber);
                    break;

                // その他
                case "ブラックマテリアル": // １階ドロップアイテム
                case "ブルーマテリアル": // １階アイテム
                case "レッドマテリアル": // ３階アイテム
                case "グリーンマテリアル": // ダンジョン４階のアイテム
                case "タイム・オブ・ルーセ": // ダンジョン５階の隠しアイテム
                    mainMessage.text = player.GetCharacterSentence(2007);
                    break;

                case "リーベストランクポーション":
                case "アカシジアの実":
                    mainMessage.text = player.GetCharacterSentence(2011);
                    break;

                case Database.RARE_TOOMI_BLUE_SUISYOU: // 初期ラナ会話イベントで入手アイテム
                    if (GroundOne.WE.dungeonEvent4_SlayBoss3)
                    {
                        mainMessage.text = "アイン：（ダメだ。ラナが囚われたままだ。助けるまではもう街へは帰らねえ）";
                        return;
                    }
                    if (GroundOne.WE.CompleteSlayBoss5)
                    {
                        mainMessage.text = player.GetCharacterSentence(2020);
                        return;
                    }
                    if ((GroundOne.WE.dungeonEvent329 && !GroundOne.WE.dungeonEvent328) ||
                        (GroundOne.WE.dungeonEvent318 && !GroundOne.WE.dungeonEvent312))
                    {
                        mainMessage.text = player.GetCharacterSentence(2020);
                        return;
                    }
                    if (GroundOne.WE.SaveByDungeon == false)
                    {
                        mainMessage.text = player.GetCharacterSentence(2012);
                        return;
                    }

                    mainMessage.text = player.GetCharacterSentence(2006);
                    this.usingToomiBlueSuisyou = true;
                    break;
            }
        }

        public void Handover_Click()
        {
            groupChoice.SetActive(false);
            //backpackFilter.SetActive(false); // ExecHandOverの続きがある。

            MainCharacter player = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);
            if (!GroundOne.WE.AvailableSecondCharacter && !GroundOne.WE.AvailableThirdCharacter) // 1人しかいない場合、「わたす」コマンドは対象外。
            {
                mainMessage.text = player.GetCharacterSentence(2037);
                return;
            }
            else // ここからが「わたす」コマンドである
            {
                targetName1.text = GroundOne.MC.FirstName;
                targetName2.text = GroundOne.SC.FirstName;
                targetName3.text = GroundOne.TC.FirstName;

                btnTargetName2.gameObject.SetActive(GroundOne.WE.AvailableSecondCharacter);
                btnTargetName3.gameObject.SetActive(GroundOne.WE.AvailableThirdCharacter);

                groupTarget.gameObject.transform.position = this.currentPosition;
                groupTarget.SetActive(true);
                return;
            }
        }

        public void Trash_Click()
        {
            this.ItemChoiced = true;

            groupChoice.SetActive(false);
            backpackFilter.SetActive(false);
            MainCharacter player = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);
            ItemBackPack backpackData = new ItemBackPack(currentSelect.text);
            if (TruthItemAttribute.CheckImportantItem(backpackData.Name) == TruthItemAttribute.Transfer.Any)
            {
                int exchangeValue = CallBackPackExchangeValue(player, backpackData, this.currentNumber);
                if (exchangeValue <= -1) return;

                player.DeleteBackPack(backpackData, exchangeValue, this.currentNumber);
                UsingItemUpdateBackPackLabel(player, backpackData, currentSelect, this.currentNumber);
                currentSelect = null;

                if (GroundOne.OnlySelectTrash)
                {
                    GetNewItem(new ItemBackPack(GroundOne.OnlySelectTrashNewItem));
                    SceneDimension.JumpToTruthDungeon(false);
                }
            }
            else
            {
                mainMessage.text = player.GetCharacterSentence(2013);
            }
        }

        public void WhoTarget_View()
        {
            groupChoice.SetActive(false);

            whoTarget1.text = GroundOne.MC.FirstName;
            whoTarget2.text = GroundOne.SC.FirstName;
            whoTarget3.text = GroundOne.TC.FirstName;

            btnWhoTarget2.gameObject.SetActive(GroundOne.WE.AvailableSecondCharacter);
            btnWhoTarget3.gameObject.SetActive(GroundOne.WE.AvailableThirdCharacter);

            groupWhoTarget.gameObject.transform.position = this.currentPosition;
            groupWhoTarget.SetActive(true);
            return;
        }

        public void ExecWhoTarget(Text sender)
        {
            if (sender.GetComponent<Mask>().showMaskGraphic == false) { HideAllChild(); return; }

            this.ItemChoiced = true;

            groupWhoTarget.SetActive(false);
            backpackFilter.SetActive(false);
            MainCharacter player = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);
            ItemBackPack backpackData = new ItemBackPack(currentSelect.text);

            MainCharacter target = null;
            if (sender.text == GroundOne.MC.FirstName)
            {
                target = GroundOne.MC;
            }
            else if (sender.text == GroundOne.SC.FirstName)
            {
                target = GroundOne.SC;
            }
            else if (sender.text == GroundOne.TC.FirstName)
            {
                target = GroundOne.TC;
            }

            this.useTargetedItem = false;

            switch (currentSelect.text)
            {
                case Database.COMMON_REVIVE_POTION_MINI:
                    if (target.Dead)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        target.ResurrectPlayer(1);
                        UsingItemUpdateBackPackLabel(player, backpackData, currentSelect, currentNumber);
                        RefreshPartyMembersLife(labelFirstPlayerLife, labelSecondPlayerLife, labelThirdPlayerLife);
                        mainMessage.text = target.GetCharacterSentence(2016);
                    }
                    else if (target == player)
                    {
                        mainMessage.text = player.GetCharacterSentence(2018);
                    }
                    else
                    {
                        mainMessage.text = String.Format(player.GetCharacterSentence(2017), target.FirstName);
                    }
                    break;

                case Database.RARE_REVIVE_POTION:
                    if (target.Dead)
                    {
                        GroundOne.WE.AlreadyUseRevivePotion = true;
                        target.ResurrectPlayer(target.MaxLife / 2);
                        UsingItemUpdateBackPackLabel(player, backpackData, currentSelect, currentNumber);
                        RefreshPartyMembersLife(labelFirstPlayerLife, labelSecondPlayerLife, labelThirdPlayerLife);
                        mainMessage.text = target.GetCharacterSentence(2016);
                    }
                    else if (target == player)
                    {
                        mainMessage.text = player.GetCharacterSentence(2018);
                    }
                    else
                    {
                        mainMessage.text = String.Format(player.GetCharacterSentence(2017), target.FirstName);
                    }
                    break;
            }
        }

        public void ExecHandover_Click(Text sender)
        {
            if (sender.GetComponent<Mask>().showMaskGraphic == false) { HideAllChild(); return; }
            this.ItemChoiced = true;

            groupTarget.SetActive(false);
            backpackFilter.SetActive(false);
            MainCharacter player = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);
            ItemBackPack backpackData = new ItemBackPack(currentSelect.text);

            MainCharacter target = null;
            if (sender.text == GroundOne.MC.FirstName)
            {
                target = GroundOne.MC;
            }
            else if (sender.text == GroundOne.SC.FirstName)
            {
                target = GroundOne.SC;
            }
            else if (sender.text == GroundOne.TC.FirstName)
            {
                target = GroundOne.TC;
            }
            
            int exchangeValue = CallBackPackExchangeValue(player, backpackData, this.currentNumber);
            if (exchangeValue <= -1) return;

            if (player == target)
            {
                return;
            }

            if (backpackData.Name == Database.RARE_EARRING_OF_LANA && (target == GroundOne.SC || target == GroundOne.TC))
            {
                mainMessage.text = "アイン：（いや・・・これは渡さないでおこう。)";
                return;
            }
            if ((backpackData.Name == Database.POOR_PRACTICE_SWORD_ZERO && (target == GroundOne.SC || target == GroundOne.TC)) ||
                (backpackData.Name == Database.POOR_PRACTICE_SWORD_1 && (target == GroundOne.SC || target == GroundOne.TC)) ||
                (backpackData.Name == Database.POOR_PRACTICE_SWORD_2 && (target == GroundOne.SC || target == GroundOne.TC)) ||
                (backpackData.Name == Database.COMMON_PRACTICE_SWORD_3 && (target == GroundOne.SC || target == GroundOne.TC)) ||
                (backpackData.Name == Database.COMMON_PRACTICE_SWORD_4 && (target == GroundOne.SC || target == GroundOne.TC)) ||
                (backpackData.Name == Database.RARE_PRACTICE_SWORD_5 && (target == GroundOne.SC || target == GroundOne.TC)) ||
                (backpackData.Name == Database.RARE_PRACTICE_SWORD_6 && (target == GroundOne.SC || target == GroundOne.TC)) ||
                (backpackData.Name == Database.EPIC_PRACTICE_SWORD_7 && (target == GroundOne.SC || target == GroundOne.TC)) ||
                (backpackData.Name == Database.LEGENDARY_FELTUS && (target == GroundOne.SC || target == GroundOne.TC)))
            {
                mainMessage.text = "アイン：（いや・・・これは渡さないでおこう。)";
                return;
            }

            bool success = target.AddBackPack(backpackData, exchangeValue);
            if (success)
            {
                player.DeleteBackPack(backpackData, exchangeValue, this.currentNumber);
                UsingItemUpdateBackPackLabel(player, backpackData, sender, this.currentNumber);
            }
            else
            {
                mainMessage.text = String.Format(player.GetCharacterSentence(2003), target.FirstName);
            }
        }

        public void StatusPlayer_Click(Text sender)
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_STATUS_SELECTPLAYER, String.Empty, String.Empty);
            MainCharacter player = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);

            if (sender.text == "")
            {
                return;
            }
            if (GroundOne.LevelUp)
            {
                mainMessage.text = player.GetCharacterSentence(2002);
                return;
            }

            if (this.useOverShifting)
            {
                mainMessage.text = player.GetCharacterSentence(2023);
                return;
            }

            this.currentSelect = sender;
            for (int ii = 0; ii < backpack.Length; ii++)
            {
                if (backpack[ii].Equals(sender))
                {
                    this.currentNumber = ii;
                    Debug.Log("currentNumber is " + this.currentNumber.ToString());
                    break;
                }
            }
            groupChoice.gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            this.currentPosition = Input.mousePosition;
            groupChoice.SetActive(true);
            backpackFilter.SetActive(true);

            //    for (int currentNumber = 0; currentNumber < Database.MAX_BACKPACK_SIZE; currentNumber++)
            //    {
            //        if (((Label)sender).Name == "backpack" + currentNumber.ToString())
            //        {

            //            ItemBackPack backpackData = new ItemBackPack(((Label)sender).Text);

            //            if (GroundOne.OnlySelectTrash)
            //            {
            //                mainMessage.Text = string.Format(player.GetCharacterSentence(2030), ((Label)sender).Text); // mc.Name + "：" + ((Label)sender).Text + "を捨てて新しいアイテムを入手するか？";
            //                using (YesNoRequest yesno = new YesNoRequest())
            //                {
            //                    yesno.StartPosition = FormStartPosition.CenterParent;
            //                    yesno.ShowDialog();
            //                    if (yesno.DialogResult == DialogResult.Yes)
            //                    {
            //                        if (TruthItemAttribute.CheckImportantItem(backpackData.Name) == TruthItemAttribute.Transfer.Any)
            //                        {
            //                            player.DeleteBackPack(backpackData, player.CheckBackPackExist(backpackData, currentNumber), currentNumber);
            //                            ((Label)sender).Text = "";
            //                            ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
            //                            this.DialogResult = DialogResult.OK;
            //                        }
            //                        else
            //                        {
            //                            mainMessage.Text = player.GetCharacterSentence(2013);
            //                            this.DialogResult = DialogResult.None;
            //                        }
            //                        return;
            //                    }
            //                    else
            //                    {
            //                        return;
            //                    }
            //                }
            //            }

            //            if (backpackData.Name == "") return;

            //            using (SelectAction sa = new SelectAction())
            //            {
            //                if (this.onlyUseItem) // 戦闘中は「つかう」しかさせない仕様で動作させる。「わたす」戦闘の最中でとっさの渡しは出来ない。「すてる」戦闘後に捨てるのが普通。
            //                {
            //                    if ((backpackData.Type == ItemBackPack.ItemType.Use_Potion) ||
            //                        (backpackData.Type == ItemBackPack.ItemType.Use_Any))
            //                    {
            //                        // 使用品限り、使用可能とする。装備品は選択不可
            //                    }
            //                    else
            //                    {
            //                        mainMessage.Text = player.GetCharacterSentence(2032);
            //                        return;
            //                    }

            //                    sa.TargetNum = 0;
            //                }
            //                else
            //                {
            //                    sa.StartPosition = FormStartPosition.Manual;
            //                    if ((this.Location.X + this.Size.Width - this.mousePosX) <= sa.Width) this.mousePosX = this.Location.X + this.Size.Width - sa.Width;
            //                    if ((this.Location.Y + this.Size.Height - this.mousePosY) <= sa.Height) this.mousePosY = this.Location.Y + this.Size.Height - sa.Height;
            //                    sa.Location = new Point(this.mousePosX, this.mousePosY);

            //                    if (backpackData.Type == ItemBackPack.ItemType.Armor_Middle
            //                        || backpackData.Type == ItemBackPack.ItemType.Armor_Light
            //                        || backpackData.Type == ItemBackPack.ItemType.Armor_Heavy
            //                        || backpackData.Type == ItemBackPack.ItemType.Accessory
            //                        || backpackData.Type == ItemBackPack.ItemType.Weapon_Heavy
            //                        || backpackData.Type == ItemBackPack.ItemType.Weapon_Light
            //                        || backpackData.Type == ItemBackPack.ItemType.Weapon_Middle)
            //                    {
            //                        sa.ElementA = "そうび";
            //                    }
            //                    else
            //                    {
            //                        sa.ElementA = "つかう";
            //                    }

            //                    if (!we.AvailableSecondCharacter && !we.AvailableThirdCharacter) // 1人しかいない場合、「わたす」コマンドではなく、「すてる」である。
            //                    {
            //                        sa.ElementB = "すてる";
            //                    }
            //                    else
            //                    {
            //                        sa.ElementB = "わたす";
            //                        sa.ElementC = "すてる";
            //                    }
            // if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) { IsShift = true; }
            //                    sa.IsShift = this.IsShift;
            //                    sa.ShowDialog();
            //                    this.IsShift = sa.IsShift;
            //                }

            //                if (sa.TargetNum == 0) // つかう / そうび
            //                {
            //                    // switch-case に進みます。
            //                }
            //                else if (sa.TargetNum == 1) // わたす
            //                {

            //                }
            //                else if (sa.TargetNum == 2) // すてる
            //                {
            //                }
            //                else
            //                {
            //                    // ESCキーキャンセルは何もしません。
            //                    return;
            //                }
            //            }

            //        }
            //    }
            //    if (this.onlyUseItem)
            //    {
            //        if (player.labelLife != null)
            //        {
            //            player.labelLife.Text = player.CurrentLife.ToString();
            //            if (player.CurrentLife >= player.MaxLife)
            //            {
            //                player.labelLife.ForeColor = Color.Green;
            //            }
            //            else
            //            {
            //                player.labelLife.ForeColor = Color.Black;
            //            }
            //            player.labelLife.Update();
            //        }

            //        this.DialogResult = DialogResult.OK;
            //    }
        }

        private int CallBackPackExchangeValue(MainCharacter player, ItemBackPack backpack, int currentNumber)
        {
            int exchangeValue = player.CheckBackPackExist(backpack, currentNumber); // [警告] backpackData.StackValueでは無い事は分かりにくい。
            // after
            //if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            //{
            //    using (SelectValue sv = new SelectValue())
            //    {
            //        sv.StartPosition = FormStartPosition.Manual;
            //        sv.Location = new Point(this.mousePosX, this.mousePosY);
            //        sv.MaxValue = exchangeValue;
            //        sv.ShowDialog();
            //        IsShift = false; // ShowDialog表示先で、Shiftキーは外された場合検知できないため、ココでリセット。
            //        if (sv.DialogResult == DialogResult.Cancel) return -1; // ESCキャンセルは中断とみなす。
            //        exchangeValue = sv.CurrentValue;
            //    }
            //}
            return exchangeValue;
        }

        private void EquipDecision(MainCharacter player, ItemBackPack backpackData, Text sender, int currentNumber)
        {
            //    player.GetCharacterSentence(2004);
            //    using (YesNoRequest yesno = new YesNoRequest())
            //    {
            //        yesno.StartPosition = FormStartPosition.CenterParent;
            //        yesno.ShowDialog();
            //        if (yesno.DialogResult == DialogResult.Yes)
            //        {
            //            // [警告]：武器・防具・アクセサリが違うだけなので、関数統一化を実施してください。
            //            ItemBackPack tempItem = null;
            //            if (backpackData.Type == ItemBackPack.ItemType.Weapon_Heavy
            //                || backpackData.Type == ItemBackPack.ItemType.Weapon_Light
            //                || backpackData.Type == ItemBackPack.ItemType.Weapon_Middle)
            //            {
            //                if (player.MainWeapon != null)
            //                {
            //                    tempItem = new ItemBackPack(player.MainWeapon.Name);
            //                }
            //                player.MainWeapon = backpackData;
            //                weapon.Text = player.MainWeapon.Name;
            //                UpdateLabelColorForRare(ref this.weapon, player.MainWeapon.Rare);
            //            }
            //            else if (backpackData.Type == ItemBackPack.ItemType.Armor_Heavy
            //                || backpackData.Type == ItemBackPack.ItemType.Armor_Light
            //                || backpackData.Type == ItemBackPack.ItemType.Armor_Middle)
            //            {
            //                if (player.MainArmor != null)
            //                {
            //                    tempItem = new ItemBackPack(player.MainArmor.Name);
            //                }
            //                player.MainArmor = backpackData;
            //                armor.Text = player.MainArmor.Name;
            //                UpdateLabelColorForRare(ref this.armor, player.MainArmor.Rare);
            //            }
            //            else if (backpackData.Type == ItemBackPack.ItemType.Shield)
            //            {
            //                if (player.SubWeapon != null)
            //                {
            //                    tempItem = new ItemBackPack(player.SubWeapon.Name);
            //                }
            //                player.SubWeapon = backpackData;
            //                subWeapon.Text = player.SubWeapon.Name;
            //                UpdateLabelColorForRare(ref this.subWeapon, player.SubWeapon.Rare);
            //            }
            //            else if (backpackData.Type == ItemBackPack.ItemType.Accessory)
            //            {
            //                if (player.Accessory == null)
            //                {
            //                    if (player.Accessory2 == null)
            //                    {
            //                        // 両方とも空の場合は、Accessoryを対象とする。
            //                        player.Accessory = backpackData;
            //                        accessory.Text = player.Accessory.Name;
            //                    }
            //                    else
            //                    {
            //                        // Accessory2があってもAccessoryが空なら、Accessoryを対象とする。
            //                        player.Accessory = backpackData;
            //                        accessory.Text = player.Accessory.Name;
            //                    }
            //                }
            //                else
            //                {
            //                    if (player.Accessory2 == null)
            //                    {
            //                        // Accessoryが埋まっていてAccessory2が空なら、Accessory2を対象とする。
            //                        player.Accessory2 = backpackData;
            //                        accessory2.Text = player.Accessory2.Name;
            //                    }
            //                    else
            //                    {
            //                        // 両方とも埋まっている場合は、Accessoryを対象とする。
            //                        tempItem = new ItemBackPack(player.Accessory.Name);
            //                        player.Accessory = backpackData;
            //                        accessory.Text = player.Accessory.Name;
            //                    }
            //                }

            //                // UpdateLabelColorForRare(ref accessory, player.Accessory.Rare); // 後編削除                   
            //            }
            //            else
            //            {
            //                // [警告] 将来装備する箇所が増えた場合、随時対応してください。
            //                mainMessage.Text = "";
            //                return;
            //            }

            //            player.DeleteBackPack(backpackData, 1, currentNumber);
            //            // [警告]：nullオブジェクトなのか、Name空文字なのかハッキリ修正してください。
            //            if (tempItem != null)
            //            {
            //                if (tempItem.Name != String.Empty)
            //                {
            //                    player.AddBackPack(tempItem);
            //                }
            //                else
            //                {
            //                    ((Label)sender).Text = "";
            //                    ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
            //                    backpackStack[currentNumber].Text = "";
            //                    backpackIcon[currentNumber].Image = null;
            //                    backpackIcon[currentNumber].Update();
            //                }
            //            }
            //            else
            //            {
            //                ((Label)sender).Text = "";
            //                ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
            //                backpackStack[currentNumber].Text = "";
            //                backpackIcon[currentNumber].Image = null;
            //                backpackIcon[currentNumber].Update();
            //            }
            //            UpdateBackPackLabel(player);
            //            SettingCharacterData(player);
            //            RefreshPartyMembersBattleStatus(player);
            //            mainMessage.Text = player.GetCharacterSentence(2005);
            //        }
            //        else
            //        {
            //            mainMessage.Text = "";
            //        }
            //    }
        }


        //void StatusPlayer_MouseLeave(object sender, EventArgs e)
        //{
        //    if (GroundOne.LevelUp)
        //    {
        //        mainMessage.Text = "レベルアップ！！" + GroundOne.UpPoint.ToString() + "ポイントを割り振ってください。";
        //    }
        //    else if (this.useOverShifting)
        //    {
        //        mainMessage.Text = "オーバーシフティング使用中、" + GroundOne.UpPoint.ToString() + "ポイントを割り振ってください。";
        //    }
        //    else
        //    {
        //        //mainMessage.Text = "";
        //    }
        //}

        public void StatusPlayer_MouseLeave()
        {
            if (this.ItemChoiced == false)
            {
                mainMessage.text = "";
            }
        }
        public void StatusPlayer_MouseEnter(Text sender)
        {

            //    if (((Label)sender).Name == "weapon")
            //    {
            //        ItemBackPack temp = new ItemBackPack(weapon.Text);
            //        if (temp.Description != "")
            //        {
            //            mainMessage.Text = temp.Description;
            //            return;
            //        }
            //    }

            //    // s 後編追加
            //    if (((Label)sender).Name == "subWeapon")
            //    {
            //        ItemBackPack temp = new ItemBackPack(subWeapon.Text);
            //        if (temp.Description != "")
            //        {
            //            mainMessage.Text = temp.Description;
            //            return;
            //        }
            //    }
            //    // e 後編追加

            //    if (((Label)sender).Name == "armor")
            //    {
            //        ItemBackPack temp = new ItemBackPack(armor.Text);
            //        if (temp.Description != "")
            //        {
            //            mainMessage.Text = temp.Description;
            //            return;
            //        }
            //    }

            //    if (((Label)sender).Name == "accessory")
            //    {
            //        ItemBackPack temp = new ItemBackPack(accessory.Text);
            //        if (temp.Description != "")
            //        {
            //            mainMessage.Text = temp.Description;
            //            return;
            //        }
            //    }

            //    // s 後編追加
            //    if (((Label)sender).Name == "accessory2")
            //    {
            //        ItemBackPack temp = new ItemBackPack(accessory2.Text);
            //        if (temp.Description != "")
            //        {
            //            mainMessage.Text = temp.Description;
            //            return;
            //        }
            //    }
            //    // e 後編追加

            if (this.ItemChoiced)
            {
                this.ItemChoiced = false;
            }
            else
            {
                if (sender.text == "")
                {
                    mainMessage.text = "";
                }
                else
                {
                    ItemBackPack temp = new ItemBackPack(sender.text);
                    mainMessage.text = temp.Description;
                }
            }
        }
        
        public void HideAllChild()
        {
            groupChoice.SetActive(false);
            groupTarget.SetActive(false);
            groupWhoTarget.SetActive(false);
            backpackFilter.SetActive(false);
            groupTargetCommand.SetActive(false);
            CommandFilter.SetActive(false);
        }

        private enum CoreType
        {
            Strength,
            Agility,
            Intelligence,
            Stamina,
            Mind
        }

        private void SettingCoreParameter(CoreType coreType, int basicValue, int addAccessoryValue, int addFoodValue, Text txtBasic, Text txtBuff, Text txtFood, Text txtTotal)
        {
            int totalValue = 0;

            totalValue = basicValue;
            txtBasic.text = basicValue.ToString();

            txtBuff.text = "+" + addAccessoryValue.ToString();
            totalValue += addAccessoryValue;

            txtFood.text = "+" + addFoodValue.ToString();
            totalValue += addFoodValue;

            txtTotal.text = "" + totalValue.ToString();
        }

        private void SettingCharacterData(MainCharacter chara)
        {
            this.txtName.text = chara.FullName;
            this.txtLevel.text = chara.Level.ToString();
            if (chara.Level < Method.GetMaxLevel())
            {
                this.txtExperience.text = chara.Exp.ToString() + " / " + chara.NextLevelBorder.ToString();
                float dx = (float)chara.Exp / (float)chara.NextLevelBorder;
                imgExpGauge.rectTransform.localScale = new Vector2(dx, 1.0f);
            }
            else
            {
                this.txtExperience.text = "-----" + " / " + "-----";
                imgExpGauge.rectTransform.localScale = new Vector2(0.0f, 1.0f);
            }


            SettingCoreParameter(CoreType.Strength, chara.Strength, chara.BuffStrength_Accessory, chara.BuffStrength_Food, this.strength,  this.addStrength, this.addStrengthFood, this.totalStrength);
            SettingCoreParameter(CoreType.Intelligence, chara.Intelligence, chara.BuffIntelligence_Accessory, chara.BuffIntelligence_Food, this.intelligence, this.addIntelligence, this.addIntelligenceFood, this.totalIntelligence);
            SettingCoreParameter(CoreType.Agility, chara.Agility, chara.BuffAgility_Accessory, chara.BuffAgility_Food, this.agility, this.addAgility, this.addAgilityFood, this.totalAgility);
            SettingCoreParameter(CoreType.Stamina, chara.Stamina, chara.BuffStamina_Accessory, chara.BuffStamina_Food, this.stamina, this.addStamina, this.addStaminaFood, this.totalStamina);
            SettingCoreParameter(CoreType.Mind, chara.Mind, chara.BuffMind_Accessory, chara.BuffMind_Food, this.mind, this.addMind, this.addMindFood, this.totalMind);

            // over shifting
            if (this.useOverShifting)
            {
                plus1.gameObject.SetActive(true);
                plus10.gameObject.SetActive(true);
                plus100.gameObject.SetActive(true);
                plus1000.gameObject.SetActive(true);
                btnUpReset.gameObject.SetActive(true);
                lblRemain.gameObject.SetActive(true);
                lblRemain.text = "残り　" + GroundOne.UpPoint.ToString();
            }
            else
            {
                plus1.gameObject.SetActive(false);
                plus10.gameObject.SetActive(false);
                plus100.gameObject.SetActive(false);
                plus1000.gameObject.SetActive(false);
                btnUpReset.gameObject.SetActive(false);
                lblRemain.gameObject.SetActive(false);
            }

            RefreshPartyMembersLife(labelFirstPlayerLife, labelSecondPlayerLife, labelThirdPlayerLife);
            this.life.text = chara.CurrentLife.ToString() + " / " + chara.MaxLife.ToString();
            float dxLife = (float)chara.CurrentLife / (float)chara.MaxLife;
            imgLifeGauge.rectTransform.localScale = new Vector2(dxLife, 1.0f);

            if (chara.AvailableSkill)
            {
                if (chara.CurrentSkillPoint > chara.MaxSkillPoint)
                {
                    chara.CurrentSkillPoint = chara.MaxSkillPoint;
                }
                skill.text = chara.CurrentSkillPoint.ToString() + " / " + chara.MaxSkillPoint.ToString();
                float dx = (float)chara.CurrentSkillPoint / (float)chara.MaxSkillPoint;
                imgSkillGauge.rectTransform.localScale = new Vector2(dx, 1.0f);
            }

            if (chara.AvailableMana)
            {
                mana.text = chara.CurrentMana.ToString() + " / " + chara.MaxMana.ToString();
                float dx = (float)chara.CurrentMana / (float)chara.MaxMana;
                imgManaGauge.rectTransform.localScale = new Vector2(dx, 1.0f);
            }

            this.weapon.text = "";
            this.imgMainWeapon.sprite = null;
            this.subWeapon.text = "";
            this.imgSubWeapon.sprite = null;
            this.armor.text = "";
            this.imgArmor.sprite = null;
            this.accessory.text = "";
            this.imgAccessory1.sprite = null;
            this.accessory2.text = "";
            this.imgAccessory2.sprite = null;
            if (chara.MainWeapon != null)
            {
                this.weapon.text = chara.MainWeapon.Name;
                Method.UpdateItemImage(chara.MainWeapon, this.imgMainWeapon);
            }
            else
            {
                this.weapon.text = "";
            }
            Method.UpdateRareColor(chara.MainWeapon, weapon, back_weapon, null);

            if (chara.SubWeapon != null)
            {
                this.subWeapon.text = chara.SubWeapon.Name;
                Method.UpdateItemImage(chara.SubWeapon, this.imgSubWeapon);
            }
            else
            {
                this.subWeapon.text = "";
            }
            Method.UpdateRareColor(chara.SubWeapon, subWeapon, back_subWeapon, null);

            if (chara.MainArmor != null)
            {
                this.armor.text = chara.MainArmor.Name;
                Method.UpdateItemImage(chara.MainArmor, this.imgArmor);
            }
            else
            {
                this.armor.text = "";
            }
            Method.UpdateRareColor(chara.MainArmor, armor, back_armor, null);

            if (chara.Accessory != null)
            {
                this.accessory.text = chara.Accessory.Name;
                Method.UpdateItemImage(chara.Accessory, this.imgAccessory1);
            }
            else
            {
                this.accessory.text = "";
            }
            Method.UpdateRareColor(chara.Accessory, accessory, back_accessory, null);

            if (chara.Accessory2 != null)
            {
                this.accessory2.text = chara.Accessory2.Name;
                Method.UpdateItemImage(chara.Accessory2, this.imgAccessory2);
            }
            else
            {
                this.accessory2.text = "";
            }
            Method.UpdateRareColor(chara.Accessory2, accessory2, back_accessory2, null);

            txtGold.text = GroundOne.MC.Gold.ToString() + "[G]";

            Method.UpdateBackPackLabel(chara, this.back_Backpack, this.backpack, this.backpackStack, this.backpackIcon);
            UpdateSpellSkillLabel(chara);
            UpdateResistStatus(chara);
        }

        private void UpdateSpellSkillLabel(MainCharacter target)
        {
            // コマンド習得に応じて、ボタンを見せる。
            back_SpellSkill[0].SetActive(target.FreshHeal);
            back_SpellSkill[1].SetActive(target.LifeTap);
            back_SpellSkill[2].SetActive(target.Resurrection);
            back_SpellSkill[3].SetActive(target.CelestialNova);
            back_SpellSkill[4].SetActive(target.SacredHeal);
        }

        private void UpdateResistStatus(MainCharacter player)
        {
            // 【後編必須】％レジスト増強も実装してください。
            if (GroundOne.Language == GroundOne.GameLanguage.Japanese)
            {
                ResistLabel[0].text = Database.STRING_LIGHT;
                ResistLabel[1].text = Database.STRING_SHADOW;
                ResistLabel[2].text = Database.STRING_FIRE;
                ResistLabel[3].text = Database.STRING_ICE;
                ResistLabel[4].text = Database.STRING_FORCE;
                ResistLabel[5].text = Database.STRING_WILL;
            }
            ResistLabelValue[0].text = "+" + player.TotalResistLight.ToString() + " (0%)";
            ResistLabelValue[1].text = "+" + player.TotalResistShadow.ToString() + " (0%)";
            ResistLabelValue[2].text = "+" + player.TotalResistFire.ToString() + " (0%)";
            ResistLabelValue[3].text = "+" + player.TotalResistIce.ToString() + " (0%)";
            ResistLabelValue[4].text = "+" + player.TotalResistForce.ToString() + " (0%)";
            ResistLabelValue[5].text = "+" + player.TotalResistWill.ToString() + " (0%)";

            if (GroundOne.Language == GroundOne.GameLanguage.Japanese)
            {
                ResistAbnormalStatus[0].text = Database.STRING_STUNNING;
                ResistAbnormalStatus[1].text = Database.STRING_SILENCE;
                ResistAbnormalStatus[2].text = Database.STRING_POISON;
                ResistAbnormalStatus[3].text = Database.STRING_TEMPTATION;
                ResistAbnormalStatus[4].text = Database.STRING_FROZEN;
                ResistAbnormalStatus[5].text = Database.STRING_PARALYZE;
                ResistAbnormalStatus[6].text = Database.STRING_SLOW;
                ResistAbnormalStatus[7].text = Database.STRING_BLIND;
                ResistAbnormalStatus[8].text = Database.STRING_SLIP;
            }

            if (player.CheckResistStun) ResistAbnormalStatusValue[0].text = "○";
            else ResistAbnormalStatusValue[0].text = "--";
            if (player.CheckResistSilence) ResistAbnormalStatusValue[1].text = "○";
            else ResistAbnormalStatusValue[1].text = "--";
            if (player.CheckResistPoison) ResistAbnormalStatusValue[2].text = "○";
            else ResistAbnormalStatusValue[2].text = "--";
            if (player.CheckResistTemptation) ResistAbnormalStatusValue[3].text = "○";
            else ResistAbnormalStatusValue[3].text = "--";
            if (player.CheckResistFrozen) ResistAbnormalStatusValue[4].text = "○";
            else ResistAbnormalStatusValue[4].text = "--";
            if (player.CheckResistParalyze) ResistAbnormalStatusValue[5].text = "○";
            else ResistAbnormalStatusValue[5].text = "--";
            if (player.CheckResistSlow) ResistAbnormalStatusValue[6].text = "○";
            else ResistAbnormalStatusValue[6].text = "--";
            if (player.CheckResistBlind) ResistAbnormalStatusValue[7].text = "○";
            else ResistAbnormalStatusValue[7].text = "--";
            if (player.CheckResistSlip) ResistAbnormalStatusValue[8].text = "○";
            else ResistAbnormalStatusValue[8].text = "--";
            // [コメント] 蘇生不可は特殊なので、ステータスとして見せない。
            //ResistAbnormalStatus[9].Text = Database.STRING_NORESURRECTION;
            //if (player.CheckResistNoResurrection) ResistAbnormalStatusValue[9].Text += "　○";
        }

        //Point basePhysicalLocation;
        private void RefreshPartyMembersBattleStatus(MainCharacter player)
        {
            double temp1 = 0;
            double temp2 = 0;
            temp1 = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PrimaryLogic.SpellSkillType.Standard, false);
            temp2 = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Max, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PrimaryLogic.SpellSkillType.Standard, false);
            txtPhysicalAttack.text = temp1.ToString("F2");
            txtPhysicalAttack.text += " - " + temp2.ToString("F2");

            temp1 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, false);
            temp2 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, false);
            if (temp1 > 0)
            {
                txtPhysicalAttack.text += "\r\n" + temp1.ToString("F2");
                txtPhysicalAttack.text += " - " + temp2.ToString("F2");
            }

            temp1 = PrimaryLogic.PhysicalDefenseValue(player, PrimaryLogic.NeedType.Min, false);
            temp2 = PrimaryLogic.PhysicalDefenseValue(player, PrimaryLogic.NeedType.Max, false);
            txtPhysicalDefense.text = temp1.ToString("F2");
            txtPhysicalDefense.text += " - " + temp2.ToString("F2");

            temp1 = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Min, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, false);
            temp2 = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Max, 1.0f, 0.0f, PrimaryLogic.SpellSkillType.Standard, false, false);
            txtMagicAttack.text = temp1.ToString("F2");
            txtMagicAttack.text += " - " + temp2.ToString("F2");

            temp1 = PrimaryLogic.MagicDefenseValue(player, PrimaryLogic.NeedType.Min, false);
            temp2 = PrimaryLogic.MagicDefenseValue(player, PrimaryLogic.NeedType.Max, false);
            txtMagicDefense.text = temp1.ToString("F2");
            txtMagicDefense.text += " - " + temp2.ToString("F2");

            temp1 = PrimaryLogic.BattleSpeedValue(player, false);
            txtBattleSpeed.text = temp1.ToString("F2");

            temp1 = PrimaryLogic.BattleResponseValue(player, false);
            txtBattleResponse.text = temp1.ToString("F2");

            temp1 = PrimaryLogic.PotentialValue(player, false);
            txtPotential.text = temp1.ToString("F2");
        }

        public void btnSomeSpellSkill_Click(Text sender)
        {
            MainCharacter player = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);

            #region "使用不可のブロック"
            if (player.Dead)
            {
                mainMessage.text = "【" + player.FirstName + "は死んでしまっているため、魔法詠唱ができない。】";
                return;
            }

            if (GroundOne.LevelUp)
            {
                mainMessage.text = player.GetCharacterSentence(2002);
                return;
            }

            if (this.useOverShifting)
            {
                mainMessage.text = player.GetCharacterSentence(2023);
                return;
            }

            if (GroundOne.OnlySelectTrash)
            {
                mainMessage.text = player.GetCharacterSentence(2021);
                return;
            }
            #endregion

            // 味方全体の場合(SACRED_HEALのみなら以下を直接処理、将来他のコマンドが増えるなら、ロジック変更が必要）
            if (sender.text == Database.SACRED_HEAL_JP || sender.text == Database.SACRED_HEAL)
            {
                int lifeGain = 0;
                if (player.CurrentMana < Database.SACRED_HEAL_COST)
                {
                    mainMessage.text = player.GetCharacterSentence(2008);
                    return;
                }
                player.CurrentMana -= Database.SACRED_HEAL_COST;
                lifeGain = (int)PrimaryLogic.SacredHealValue(player, false);

                List<MainCharacter> group = new List<MainCharacter>();
                if (GroundOne.WE.AvailableFirstCharacter && GroundOne.MC != null && !GroundOne.MC.Dead) { group.Add(GroundOne.MC); }
                if (GroundOne.WE.AvailableSecondCharacter && GroundOne.SC != null && !GroundOne.SC.Dead) { group.Add(GroundOne.SC); }
                if (GroundOne.WE.AvailableThirdCharacter && GroundOne.TC != null && !GroundOne.TC.Dead) { group.Add(GroundOne.TC); }
                for (int currentNumber = 0; currentNumber < group.Count; currentNumber++)
                {
                    group[currentNumber].CurrentLife += lifeGain;
                    mainMessage.text = String.Format(player.GetCharacterSentence(2035), lifeGain.ToString());
                }
                this.life.text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();
                this.mana.text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
                RefreshPartyMembersLife(labelFirstPlayerLife, labelSecondPlayerLife, labelThirdPlayerLife);
            }
            // 単体対象の場合
            else
            {
                MainCharacter target = null;
                if (!GroundOne.WE.AvailableSecondCharacter && !GroundOne.WE.AvailableThirdCharacter)
                {
                    target = GroundOne.MC;
                    ExecSomeSpellSkill(sender, target, sender.text);
                }
                else if (GroundOne.WE.AvailableSecondCharacter || GroundOne.WE.AvailableThirdCharacter)
                {
                    this.groupTargetCommand.gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
                    this.currentPosition = Input.mousePosition;
                    this.txtTargetNameCommand1.text = GroundOne.MC.FirstName;
                    this.txtTargetNameCommand2.text = GroundOne.SC.FirstName;
                    this.txtTargetNameCommand3.text = GroundOne.TC.FirstName;
                    btnTargetNameCommand2.gameObject.SetActive(GroundOne.WE.AvailableSecondCharacter);
                    btnTargetNameCommand3.gameObject.SetActive(GroundOne.WE.AvailableThirdCharacter);
                    this.currentCommand = sender.text;
                    this.groupTargetCommand.SetActive(true);
                    this.CommandFilter.SetActive(true);
                }
            }
        }

        public void ExecSomeSpellSkill(Text sender)
        {
            groupTargetCommand.SetActive(false);
            CommandFilter.SetActive(false);

            MainCharacter target = null;
            if (sender.text == GroundOne.MC.FirstName)
            {
                target = GroundOne.MC;
            }
            else if (sender.text == GroundOne.SC.FirstName)
            {
                target = GroundOne.SC;
            }
            else if (sender.text == GroundOne.TC.FirstName)
            {
                target = GroundOne.TC;
            }
            ExecSomeSpellSkill(sender, target, this.currentCommand);
        }

        private void ExecSomeSpellSkill(Text sender, MainCharacter target, string commandName)
        {
            MainCharacter player = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);

            if ((commandName == Database.FRESH_HEAL_JP) || (commandName == Database.FRESH_HEAL) ||
                (commandName == Database.LIFE_TAP_JP) || (commandName == Database.LIFE_TAP) ||
                (commandName == Database.CELESTIAL_NOVA_JP) || (commandName == Database.CELESTIAL_NOVA))
            {
                if (target.Dead)
                {
                    mainMessage.text = "【" + target.FirstName + "は死んでしまっているため、効果がない。】";
                    return;
                }

                int lifeGain = 0;
                if (commandName == Database.FRESH_HEAL_JP || commandName == Database.FRESH_HEAL)
                {
                    if (player.CurrentMana < Database.FRESH_HEAL_COST)
                    {
                        mainMessage.text = player.GetCharacterSentence(2008);
                        return;
                    }
                    player.CurrentMana -= Database.FRESH_HEAL_COST;
                    lifeGain = (int)PrimaryLogic.FreshHealValue(player, false);
                }
                else if (commandName == Database.LIFE_TAP_JP || commandName == Database.LIFE_TAP)
                {
                    if (player.CurrentMana < Database.LIFE_TAP_COST)
                    {
                        mainMessage.text = player.GetCharacterSentence(2008);
                        return;
                    }
                    player.CurrentMana -= Database.LIFE_TAP_COST;
                    lifeGain = (int)PrimaryLogic.LifeTapValue(player, false);
                }
                else if (commandName == Database.CELESTIAL_NOVA_JP || commandName == Database.CELESTIAL_NOVA)
                {
                    if (player.CurrentMana < Database.CELESTIAL_NOVA_COST)
                    {
                        mainMessage.text = player.GetCharacterSentence(2008);
                        return;
                    }
                    player.CurrentMana -= Database.CELESTIAL_NOVA_COST;
                    lifeGain = (int)(PrimaryLogic.CelestialNovaValue_B(player, false));
                }

                target.CurrentLife += lifeGain;
                mainMessage.text = String.Format(player.GetCharacterSentence(2001), lifeGain.ToString());
            }
            else // resurrection
            {
                if (player.CurrentMana < Database.RESURRECTION_COST)
                {
                    mainMessage.text = player.GetCharacterSentence(2008);
                    return;
                }

                if (target.Dead)
                {
                    player.CurrentMana -= Database.RESURRECTION_COST;

                    target.ResurrectPlayer((int)PrimaryLogic.ResurrectionValue(target));
                    mainMessage.text = String.Format(target.GetCharacterSentence(2016));
                }
                else if (target == player)
                {
                    mainMessage.text = String.Format(player.GetCharacterSentence(2018));
                }
                else if (!target.Dead)
                {
                    mainMessage.text = String.Format(player.GetCharacterSentence(2017), target.FirstName);
                }
            }
            this.life.text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();
            this.mana.text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
            RefreshPartyMembersLife(labelFirstPlayerLife, labelSecondPlayerLife, labelThirdPlayerLife);
        }

        private void ViewItemDescription(Text sender)
        {
            if (this.ItemChoiced)
            {
                this.ItemChoiced = false;
            }
            else
            {
                if (sender.text == "")
                {
                    mainMessage.text = "";
                }
                else
                {
                    ItemBackPack temp = new ItemBackPack(sender.text);
                    mainMessage.text = temp.Description;
                }
            }
        }

        public void weapon_Click(Text sender)
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_STATUS_MAINWEAPON, String.Empty, String.Empty);
            ViewItemDescription(sender);
            ChangeEquipment(0);
        }
        public void subWeapon_Click(Text sender)
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_STATUS_SUBWEAPON, String.Empty, String.Empty);
            MainCharacter targetPlayer = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);
            if (targetPlayer.MainWeapon != null)
            {
                if ((targetPlayer.MainWeapon.Type == ItemBackPack.ItemType.Weapon_Rod) ||
                    (targetPlayer.MainWeapon.Type == ItemBackPack.ItemType.Weapon_TwoHand))
                {
                    mainMessage.text = targetPlayer.GetCharacterSentence(2025);
                    return;
                }
                if (targetPlayer.MainWeapon.Name == "")
                {
                    mainMessage.text = targetPlayer.GetCharacterSentence(2026);
                    return;
                }
            }
            if (targetPlayer.MainWeapon == null)
            {
                mainMessage.text = targetPlayer.GetCharacterSentence(2026);
                return;
            }
            ViewItemDescription(sender);
            ChangeEquipment(1);
        }
        public void armor_Click(Text sender)
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_STATUS_ARMOR, String.Empty, String.Empty);
            ViewItemDescription(sender);
            ChangeEquipment(2);
        }
        public void accessory_Click(Text sender)
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_STATUS_ACCESSORY1, String.Empty, String.Empty);
            ViewItemDescription(sender);
            ChangeEquipment(3);
        }
        public void accessory2_Click(Text sender)
        {
            GroundOne.SQL.UpdateOwner(Database.LOG_STATUS_ACCESSORY2, String.Empty, String.Empty);
            ViewItemDescription(sender);
            ChangeEquipment(4);
        }

        // equipType: 0:Weapon  1:SubWeapon  2:Armor  3:Accessory  4:Accessory2
        private void ChangeEquipment(int equipType)
        {
            MainCharacter targetPlayer = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);
            if (GroundOne.LevelUp)
            {
                mainMessage.text = targetPlayer.GetCharacterSentence(2002);
                return;
            }

            if (this.useOverShifting)
            {
                mainMessage.text = targetPlayer.GetCharacterSentence(2023);
                return;
            }

            SceneDimension.CallTruthSelectEquipment(this, equipType, targetPlayer);
        }
        
        public void FirstChara_Click()
        {
            if (GroundOne.LevelUp)
            {
                mainMessage.text = GroundOne.MC.GetCharacterSentence(2002);
                return;
            }
            else if (this.useOverShifting)
            {
                mainMessage.text = GroundOne.MC.GetCharacterSentence(2023);
                return;
            }
            else if (GroundOne.OnlySelectTrash)
            {
                mainMessage.text = GroundOne.MC.GetCharacterSentence(2021);
            }
            else
            {
                this.Background.GetComponent<Image>().color = GroundOne.MC.PlayerStatusColor;
                SettingCharacterData(GroundOne.MC);
                RefreshPartyMembersBattleStatus(GroundOne.MC);
            }
        }

        public void SecondChara_Click()
        {
            if (GroundOne.LevelUp)
            {
                mainMessage.text = GroundOne.SC.GetCharacterSentence(2002);
                return;
            }
            else if (this.useOverShifting)
            {
                mainMessage.text = GroundOne.SC.GetCharacterSentence(2023);
                return;
            }
            else if (GroundOne.OnlySelectTrash)
            {
                mainMessage.text = GroundOne.SC.GetCharacterSentence(2021);
            }
            else
            {
                this.Background.GetComponent<Image>().color = GroundOne.SC.PlayerStatusColor;
                SettingCharacterData(GroundOne.SC);
                RefreshPartyMembersBattleStatus(GroundOne.SC);
            }
        }

        public void ThirdChara_Click()
        {
            if (GroundOne.LevelUp)
            {
                mainMessage.text = GroundOne.TC.GetCharacterSentence(2002);
                return;
            }
            else if (this.useOverShifting)
            {
                mainMessage.text = GroundOne.TC.GetCharacterSentence(2023);
                return;
            }
            else if (GroundOne.OnlySelectTrash)
            {
                mainMessage.text = GroundOne.TC.GetCharacterSentence(2021);
            }
            else
            {
                this.Background.GetComponent<Image>().color = GroundOne.TC.PlayerStatusColor;
                SettingCharacterData(GroundOne.TC);
                RefreshPartyMembersBattleStatus(GroundOne.TC);
            }
        }

        public void ChangeViewButton_Click(int viewNumber)
        {
            MainCharacter targetPlayer = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);
            if (GroundOne.LevelUp)
            {
                mainMessage.text = targetPlayer.GetCharacterSentence(2002);
                return;
            }

            if (this.useOverShifting)
            {
                mainMessage.text = targetPlayer.GetCharacterSentence(2023);
                return;
            }
            if (GroundOne.OnlySelectTrash)
            {
                mainMessage.text = targetPlayer.GetCharacterSentence(2021);
                return;
            }
            if (this.onlyUseItem)
            {
                mainMessage.text = targetPlayer.GetCharacterSentence(2031);
                return;
            }

            if (viewNumber == 0)
            {
                groupParentStatus.gameObject.SetActive(true);
                groupParentBackpack.gameObject.SetActive(false);
                groupParentSpell.gameObject.SetActive(false);
                groupParentResist.gameObject.SetActive(false);
            }
            else if (viewNumber == 1)
            {
                groupParentStatus.gameObject.SetActive(false);
                groupParentBackpack.gameObject.SetActive(true);
                groupParentSpell.gameObject.SetActive(false);
                groupParentResist.gameObject.SetActive(false);
            }
            else if (viewNumber == 2)
            {
                groupParentStatus.gameObject.SetActive(false);
                groupParentBackpack.gameObject.SetActive(false);
                groupParentSpell.gameObject.SetActive(true);
                groupParentResist.gameObject.SetActive(false);
            }
            else if (viewNumber == 3)
            {
                groupParentStatus.gameObject.SetActive(false);
                groupParentBackpack.gameObject.SetActive(false);
                groupParentSpell.gameObject.SetActive(false);
                groupParentResist.gameObject.SetActive(true);
            }
        }

        public void StatusButton1_Click()
        {
            ChangeViewButton_Click(0);
        }
        public void StatusButton2_Click()
        {
            ChangeViewButton_Click(1);
        }
        public void StatusButton3_Click()
        {
            ChangeViewButton_Click(2);
        }
        public void StatusButton4_Click()
        {
            ChangeViewButton_Click(3);
        }

        // [警告] 以下、TruthSelectCharacterと重複する記述です。統一化を行ってください。
        private int addStrSC = 0;
        private int addAglSC = 0;
        private int addIntSC = 0;
        private int addStmSC = 0;
        private int addMndSC = 0;
        upType number = upType.Strength;

        enum upType
        {
            Strength,
            Agility,
            Intelligence,
            Stamina,
            Mind
        }
        
        private void CheckUpPoint()
        {
            GroundOne.UpPoint--;
            if (GroundOne.UpPoint <= 0)
            {
                mainMessage.text = "ポイント割り振り完了！";
                txtClose.text = "完了";
                btnClose.gameObject.SetActive(true);
                this.useOverShifting = false;
            }
            else
            {
                mainMessage.text = "あと" + GroundOne.UpPoint.ToString() + "ポイントを割り振ってください。";
            }
            lblRemain.text = "残り　" + GroundOne.UpPoint.ToString();
        }

        private MainCharacter SelectCurrentPlayer()
        {
            if (GroundOne.MC != null && GroundOne.MC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                return GroundOne.MC;
            }
            else if (GroundOne.SC != null && GroundOne.SC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                return GroundOne.SC;
            }
            else if (GroundOne.TC != null && GroundOne.TC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                return GroundOne.TC;
            }
            else
            {
                return GroundOne.MC;
            }
        }

        public void buttonStrength_Click()
        {
            if (GroundOne.UpPoint <= 0) { return; } // add unity

            // 通常レベルアップ＋１のロジック
            if (GroundOne.LevelUp)
            {
                MainCharacter chara = SelectCurrentPlayer();
                chara.Strength++;
                strength.text = chara.Strength.ToString();
                SettingCoreParameter(CoreType.Strength, chara.Strength, chara.BuffStrength_Accessory, chara.BuffStrength_Food, this.strength, this.addStrength, this.addStrengthFood, this.totalStrength);
                RefreshPartyMembersBattleStatus(chara);
                CheckUpPoint();
            }
            // オーバーシフティング
            else if (this.useOverShifting)
            {
                this.number = upType.Strength;
            }
        }

        public void buttonAgility_Click()
        {
            if (GroundOne.UpPoint <= 0) { return; } // add unity

            // 通常レベルアップ＋１のロジック
            if (GroundOne.LevelUp)
            {
                MainCharacter chara = SelectCurrentPlayer();
                chara.Agility++;
                agility.text = chara.Agility.ToString();
                RefreshPartyMembersBattleStatus(chara);
                SettingCoreParameter(CoreType.Agility, chara.Agility, chara.BuffAgility_Accessory, chara.BuffAgility_Food, this.agility, this.addAgility, this.addAgilityFood, this.totalAgility);
                CheckUpPoint();
            }
            // オーバーシフティング
            else if (this.useOverShifting)
            {
                this.number = upType.Agility;
            }
        }

        public void buttonIntelligence_Click()
        {
            if (GroundOne.UpPoint <= 0) { return; } // add unity

            // 通常レベルアップ＋１のロジック
            if (GroundOne.LevelUp)
            {
                MainCharacter chara = SelectCurrentPlayer();
                chara.Intelligence++;
                intelligence.text = chara.Intelligence.ToString();
                if (chara.AvailableMana)
                {
                    this.mana.text = chara.CurrentMana.ToString() + " / " + chara.MaxMana.ToString();
                }
                RefreshPartyMembersBattleStatus(chara);
                SettingCoreParameter(CoreType.Intelligence, chara.Intelligence, chara.BuffIntelligence_Accessory, chara.BuffIntelligence_Food, this.intelligence, this.addIntelligence, this.addIntelligenceFood, this.totalIntelligence);
                CheckUpPoint();
            }
            // オーバーシフティング
            else if (this.useOverShifting)
            {
                this.number = upType.Intelligence;
            }
        }

        public void buttonStamina_Click()
        {
            if (GroundOne.UpPoint <= 0) { return; } // add unity

            // 通常レベルアップ＋１のロジック
            if (GroundOne.LevelUp)
            {
                MainCharacter chara = SelectCurrentPlayer();
                chara.Stamina++;
                stamina.text = chara.Stamina.ToString();
                this.life.text = chara.CurrentLife.ToString() + " / " + chara.MaxLife.ToString();
                RefreshPartyMembersBattleStatus(chara);
                SettingCoreParameter(CoreType.Stamina, chara.Stamina, chara.BuffStamina_Accessory, chara.BuffStamina_Food, this.stamina, this.addStamina, this.addStaminaFood, this.totalStamina);
                RefreshPartyMembersLife(labelFirstPlayerLife, labelSecondPlayerLife, labelThirdPlayerLife);
                CheckUpPoint();
            }
            // オーバーシフティング
            else if (this.useOverShifting)
            {
                this.number = upType.Stamina;
            }
        }

        public void buttonMind_Click()
        {
            if (GroundOne.UpPoint <= 0) { return; } // add unity

            // 通常レベルアップ＋１のロジック
            if (GroundOne.LevelUp)
            {
                MainCharacter chara = SelectCurrentPlayer();
                chara.Mind++;
                mind.text = chara.Mind.ToString();
                RefreshPartyMembersBattleStatus(chara);
                SettingCoreParameter(CoreType.Mind, chara.Mind, chara.BuffMind_Accessory, chara.BuffMind_Food, this.mind, this.addMind, this.addMindFood, this.totalMind);
                CheckUpPoint();
            }
            // オーバーシフティング
            else if (this.useOverShifting)
            {
                this.number = upType.Mind;
            }
        }

        private void GoLevelUpPoint(upType type, int plus, ref MainCharacter player, ref int remain, ref int addStr, ref int addAgl, ref int addInt, ref int addStm, ref int addMnd)
        {
            if (remain > 0 && remain >= plus)
            {
                remain -= plus;
                if (type == upType.Strength)
                {
                    addStr += plus;
                    player.Strength += plus;
                    strength.text = player.Strength.ToString();
                }
                else if (type == upType.Agility)
                {
                    addAgl += plus;
                    player.Agility += plus;
                    agility.text = player.Agility.ToString();
                }
                else if (type == upType.Intelligence)
                {
                    addInt += plus;
                    player.Intelligence += plus;
                    intelligence.text = player.Intelligence.ToString();
                }
                else if (type == upType.Stamina)
                {
                    addStm += plus;
                    player.Stamina += plus;
                    stamina.text = player.Stamina.ToString();
                }
                else if (type == upType.Mind)
                {
                    addMnd += plus;
                    player.Mind += plus;
                    mind.text = player.Mind.ToString();
                }
                RefreshPartyMembersBattleStatus(player);
                RefreshPartyMembersLife(labelFirstPlayerLife, labelSecondPlayerLife, labelThirdPlayerLife);
                lblRemain.text = "残り " + remain.ToString();
            }
        }

        public void plus1_Click(Text sender)
        {
            MainCharacter player = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);
            int plus = 0;
            if (sender.text == "+1") { plus = 1; }
            else if (sender.text == "+10") { plus = 10; }
            else if (sender.text == "+100") { plus = 100; }
            else if (sender.text == "+1000") { plus = 1000; }

            GoLevelUpPoint(this.number, plus, ref player, ref GroundOne.UpPoint, ref this.addStrSC, ref this.addAglSC, ref this.addIntSC, ref this.addStmSC, ref this.addMndSC);
            if (GroundOne.UpPoint <= 0)
            {
                plus1.gameObject.SetActive(false);
                plus10.gameObject.SetActive(false);
                plus100.gameObject.SetActive(false);
                plus1000.gameObject.SetActive(false);
                btnUpReset.gameObject.SetActive(false);
                lblRemain.gameObject.SetActive(false);
                useOverShifting = false;
                btnClose.gameObject.SetActive(true);
                mainMessage.text = player.GetCharacterSentence(2036);
                player.CurrentLife = player.MaxLife;
                player.CurrentSkillPoint = player.MaxSkillPoint;
                player.CurrentMana = player.MaxMana;
                SettingCharacterData(player);
            }
        }

        private void ResetParameter(ref MainCharacter player, ref int remain, ref int addStr, ref int addAgl, ref int addInt, ref int addStm, ref int addMnd)
        {
            remain += addStr; player.Strength -= addStr; addStr = 0;
            remain += addAgl; player.Agility -= addAgl; addAgl = 0;
            remain += addInt; player.Intelligence -= addInt; addInt = 0;
            remain += addStm; player.Stamina -= addStm; addStm = 0;
            remain += addMnd; player.Mind -= addMnd; addMnd = 0;
            lblRemain.text = "残り　" + remain.ToString();
        }

        public void btnUpReset_Click()
        {
            MainCharacter player = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);
            ResetParameter(ref player, ref GroundOne.UpPoint, ref this.addStrSC, ref this.addAglSC, ref this.addIntSC, ref this.addStmSC, ref this.addMndSC);
            SettingCharacterData(player);
            RefreshPartyMembersBattleStatus(player);
            RefreshPartyMembersLife(labelFirstPlayerLife, labelSecondPlayerLife, labelThirdPlayerLife);
        }

        //PopUpMini popupInfo = null;
        //private void buttonStrength_MouseEnter(object sender, EventArgs e)
        //{
        //}

        //private void buttonStrength_MouseLeave(object sender, EventArgs e)
        //{
        //    if (popupInfo != null)
        //    {
        //        popupInfo.Close();
        //        popupInfo = null;
        //    }
        //}
        //private void buttonStrength_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (popupInfo == null)
        //    {
        //        popupInfo = new PopUpMini();
        //    }

        //    popupInfo.StartPosition = FormStartPosition.Manual;

        //    Button currentBtn = (Button)sender;
        //    popupInfo.Location = new Point(this.Location.X + currentBtn.Location.X + ((GroupBox)currentBtn.Parent).Location.X + e.X + 5,
        //        this.Location.Y + currentBtn.Location.Y + ((GroupBox)currentBtn.Parent).Location.Y + e.Y - 18);
        //    popupInfo.PopupColor = Color.Black;
        //    System.OperatingSystem os = System.Environment.OSVersion;
        //    int osNumber = os.Version.Major;
        //    if (osNumber != 5)
        //    {
        //        popupInfo.Opacity = 0.7f;
        //    }
        //    if (currentBtn.Equals(buttonStrength)) { popupInfo.CurrentInfo = "【力】パラメタ\r\n物理攻撃／物理防御に影響する。"; }
        //    else if (currentBtn.Equals(buttonAgility)) { popupInfo.CurrentInfo = "【技】パラメタ\r\n戦闘速度／戦闘反応に影響する。"; }
        //    else if (currentBtn.Equals(buttonIntelligence)) { popupInfo.CurrentInfo = "【知】パラメタ\r\n魔法攻撃／魔法防御に影響する。"; }
        //    else if (currentBtn.Equals(buttonStamina)) { popupInfo.CurrentInfo = "【体】パラメタ\r\n最大ライフに影響する。"; }
        //    else if (currentBtn.Equals(buttonMind)) { popupInfo.CurrentInfo = "【心】パラメタ\r\n潜在能力に影響する。\r\n物理攻撃／物理防御／魔法攻撃／魔法防御／戦闘速度／戦闘反応のベース値に影響する。"; }
        //    popupInfo.Show();
        //}
    }
}
