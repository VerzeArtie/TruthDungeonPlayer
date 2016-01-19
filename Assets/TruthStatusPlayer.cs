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
    public class TruthStatusPlayer : MonoBehaviour
    {
        public GameObject groupParentStatus;
        public GameObject groupParentBackpack;
        public GameObject groupParentSpell;
        public GameObject groupParentResist;
        public GameObject groupBtnChara;
        public GameObject groupTxtChara;
        public Button btnClose;
        public Text txtClose;
        public Button btnFirstChara;
        public Button btnSecondChara;
        public Button btnThirdChara;
        public Text labelFirstPlayerLife;
        public Text labelSecondPlayerLife;
        public Text labelThirdPlayerLife;
        public Camera cam;
        public Text mainMessage;
        public Text txtName;
        public Text txtLevel;
        public Text txtExperience;
        public Text txtJobClass;
        public Text txtGold;
        public GameObject groupBtnLifeManaSkill;
        public GameObject groupTxtLifeManaSkill;
        public GameObject btnLife;
        public GameObject btnMana;
        public GameObject btnSkill;
        public Text life;
        public Text mana;
        public Text skill;
        public Button btnStrength;
        public Button btnAgility;
        public Button btnIntelligence;
        public Button btnStamina;
        public Button btnMind;
        public Text strength;
        public Text addStrength;
        public Text agility;
        public Text addAgility;
        public Text intelligence;
        public Text addIntelligence;
        public Text stamina;
        public Text addStamina;
        public Text mind;
        public Text addMind;
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
        public Text subWeapon;
        public Text armor;
        public Text accessory;
        public Text accessory2;
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
        public GameObject backpackFilter;
        public Button btnTargetName1;
        public Button btnTargetName2;
        public Button btnTargetName3;
        public Text targetName1;
        public Text targetName2;
        public Text targetName3;
        public GameObject[] back_SpellSkill;
        public Text[] SpellSkill;
        public Text[] ResistLabel;
        public Text[] ResistLabelValue;
        public Text[] ResistAbnormalStatus;
        public Text[] ResistAbnormalStatusValue;

        ItemBackPack[] backpackData = null;

        private Color currentStatusView;
        public Color CurrentStatusView
        {
            get { return currentStatusView; }
            set { currentStatusView = value; }
        }
        
        private bool onlyUseItem = false;
        public bool OnlyUseItem
        {
            get { return onlyUseItem; }
            set { onlyUseItem = value; }
        }

        private bool useOverShifting = false;

        // Use this for initialization
        void Start()
        {
            GroundOne.InitializeGroundOne();

            this.txtGold.text = GroundOne.MC.Gold.ToString();

            this.CurrentStatusView = GroundOne.MC.PlayerStatusColor;
            this.cam.backgroundColor = GroundOne.MC.PlayerStatusColor;
            Debug.Log("cam backcolor: " + this.cam.backgroundColor.ToString());
            MainCharacter player = GetCurrentPlayer();
            SettingCharacterData(player);
            RefreshPartyMembersBattleStatus(player);

            RefreshPartyMembersLife();

            if (GroundOne.MC != null) { btnFirstChara.GetComponent<Image>().color = GroundOne.MC.PlayerColor; }
            if (GroundOne.SC != null) { btnSecondChara.GetComponent<Image>().color = GroundOne.SC.PlayerColor; }
            if (GroundOne.TC != null) { btnThirdChara.GetComponent<Image>().color = GroundOne.TC.PlayerColor; }

            backpackData = player.GetBackPackInfo();
            UpdateBackPackLabel(player);

            if (!GroundOne.WE.AvailableSecondCharacter && !GroundOne.WE.AvailableThirdCharacter)
            {
                btnFirstChara.gameObject.SetActive(false);
                labelFirstPlayerLife.gameObject.SetActive(false);
            }
            if (GroundOne.DuelMode == true)
            {
                btnFirstChara.gameObject.SetActive(false);
                labelFirstPlayerLife.gameObject.SetActive(false);
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
                GameObject emptyObj = new GameObject();
                emptyObj.AddComponent<RectTransform>();
                emptyObj.transform.SetParent(groupBtnChara.transform);
                GameObject emptyObj2 = new GameObject();
                emptyObj2.AddComponent<RectTransform>();
                emptyObj2.transform.SetParent(groupTxtChara.transform);
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
                GameObject emptyObj = new GameObject();
                emptyObj.AddComponent<RectTransform>();
                emptyObj.transform.SetParent(groupBtnChara.transform);
                GameObject emptyObj2 = new GameObject();
                emptyObj2.AddComponent<RectTransform>();
                emptyObj2.transform.SetParent(groupTxtChara.transform);
            }

            if (GroundOne.WE.AvailableSkill)
            {
                btnSkill.SetActive(true);
                skill.gameObject.SetActive(true);
            }
            else
            {
                btnSkill.SetActive(false);
                skill.gameObject.SetActive(false);
                GameObject emptyObj = new GameObject();
                emptyObj.AddComponent<RectTransform>();
                emptyObj.transform.SetParent(groupBtnLifeManaSkill.transform);
                GameObject emptyObj2 = new GameObject();
                emptyObj2.AddComponent<RectTransform>();
                emptyObj2.transform.SetParent(groupTxtLifeManaSkill.transform);
            }

            if (GroundOne.WE.AvailableMana)
            {
                btnMana.SetActive(true);
                mana.gameObject.SetActive(true);
            }
            else
            {
                btnMana.SetActive(false);
                mana.gameObject.SetActive(false);
                GameObject emptyObj = new GameObject();
                emptyObj.AddComponent<RectTransform>();
                emptyObj.transform.SetParent(groupBtnLifeManaSkill.transform);
                GameObject emptyObj2 = new GameObject();
                emptyObj2.AddComponent<RectTransform>();
                emptyObj2.transform.SetParent(groupTxtLifeManaSkill.transform);
            }

            if (!GroundOne.LevelUp)
            {
                mainMessage.text = "";
            }
            else
            {
                btnClose.gameObject.SetActive(false);
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
        void Update()
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    groupChoice.SetActive(false);
            //}

            if (this.usingToomiBlueSuisyou)
            {
                SceneDimension.Back();
                // todo
                //using (YesNoRequest yesno = new YesNoRequest())
                //{
                //    yesno.StartPosition = FormStartPosition.CenterParent;
                //    yesno.ShowDialog();
                //    if (yesno.DialogResult == DialogResult.Yes)
                //    {
                //        if (GroundOne.WE.SaveByDungeon)
                //        {
                //            this.DialogResult = DialogResult.Abort;
                //            return;
                //        }
                //        else
                //        {
                //            mainMessage.text = player.GetCharacterSentence(2012);
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        mainMessage.text = "";
                //    }
                //}

            }
            if (this.usingOvershifting)
            {
                MainCharacter player = GetCurrentPlayer();
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
        }

        public void tapClose()
        {
            if (GroundOne.OnlySelectTrash)
            {
                if (GroundOne.CannotSelectTrash != String.Empty)
                {
                    mainMessage.text = "アイン：いや【" + GroundOne.CannotSelectTrash + "】の入手を諦めるわけにはいかねえ。";
                    return;
                }
            }
            SceneDimension.Back();
        }

        private MainCharacter GetCurrentPlayer()
        {
            MainCharacter player = null;
            if (GroundOne.MC != null && GroundOne.MC.PlayerStatusColor == this.cam.backgroundColor)
            {
                Debug.Log("MC color");
                player = GroundOne.MC;
            }
            else if (GroundOne.SC != null && GroundOne.SC.PlayerStatusColor == this.cam.backgroundColor)
            {
                player = GroundOne.SC;
                Debug.Log("sc color");
            }
            else if (GroundOne.TC != null && GroundOne.TC.PlayerStatusColor == this.cam.backgroundColor)
            {
                player = GroundOne.TC;
                Debug.Log("tc color");
            }
            else
            {
                Debug.Log("unknown...");
                if (GroundOne.MC == null) { Debug.Log("fatal sequence..."); }
            }
            return player;
        }

        Text currentSelect = null;
        int currentNumber = 0;
        Vector3 currentPosition;

        public void Use_Click()
        {
            groupChoice.SetActive(false);
            backpackFilter.SetActive(false);

            MainCharacter player = GetCurrentPlayer();
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
                        effect = (int)((double)effect * 1.3f);
                    }
                    player.CurrentLife += effect;
                    player.DeleteBackPack(backpackData, 1, currentNumber);
                    this.life.text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();
                    mainMessage.text = String.Format(player.GetCharacterSentence(2001), effect);
                    UsingItemUpdateBackPackLabel(player, backpackData, currentSelect, currentNumber);
                    RefreshPartyMembersLife();
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
                    RefreshPartyMembersLife();
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
                    RefreshPartyMembersLife();
                    break;

                case Database.COMMON_REVIVE_POTION_MINI:
                    // todo
                    //MainCharacter target = null;
                    //using (SelectTarget st = new SelectTarget())
                    //{
                    //    st.StartPosition = FormStartPosition.Manual;
                    //    st.Location = new Point(this.mousePosX, this.mousePosY);
                    //    if (GroundOne.WE.AvailableThirdCharacter)
                    //    {
                    //        st.MaxSelectable = 3;
                    //        st.FirstName = GroundOne.MC.Name;
                    //        st.SecondName = GroundOne.SC.Name;
                    //        st.ThirdName = GroundOne.TC.Name;
                    //        st.ShowDialog();
                    //    }
                    //    else if (GroundOne.WE.AvailableSecondCharacter)
                    //    {
                    //        st.MaxSelectable = 2;
                    //        st.FirstName = GroundOne.MC.Name;
                    //        st.SecondName = GroundOne.SC.Name;
                    //        st.ShowDialog();
                    //    }
                    //    else
                    //    {
                    //        st.TargetNum = 1;
                    //    }

                    //    if (st.TargetNum == 1)
                    //    {
                    //        target = GroundOne.MC;
                    //    }
                    //    else if (st.TargetNum == 2)
                    //    {
                    //        target = GroundOne.SC;
                    //    }
                    //    else if (st.TargetNum == 3)
                    //    {
                    //        target = GroundOne.TC;
                    //    }
                    //}
                    //if (target.Dead)
                    //{
                    //    player.DeleteBackPack(backpackData, 1, currentNumber);
                    //    target.ResurrectPlayer(1);
                    //    this.life.text = target.CurrentLife.ToString() + " / " + target.MaxLife.ToString();
                    //    mainMessage.text = target.GetCharacterSentence(2016);
                    //}
                    //else if (target == player)
                    //{
                    //    mainMessage.text = player.GetCharacterSentence(2018);
                    //}
                    //else
                    //{
                    //    mainMessage.text = String.Format(player.GetCharacterSentence(2017), target.Name);
                    //}
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
                        player.CurrentSlow = 0;
                        player.DeBuff(player.pbSlow);
                        player.CurrentPoison = 0;
                        player.CurrentPoisonValue = 0;
                        player.DeBuff(player.pbPoison);
                        player.CurrentBlind = 0;
                        player.DeBuff(player.pbBlind);
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
                        // todo
                        //player.DeleteBackPack(backpackData, 1, currentNumber);
                        //player.CurrentStaminaUp = Database.INFINITY;
                        //player.CurrentStaminaUpValue = 100; // スタミナUPは内部処理で10倍されてるため、ここでは1000/10で100
                        //player.ActivateBuff(player.pbStaminaUp, Database.BaseResourceFolder + "BuffStaminaUp.bmp", Database.INFINITY);
                        //player.labelLife.text = player.CurrentLife.ToString();
                        //if (player.CurrentLife >= player.MaxLife)
                        //{
                        //    player.labelLife.ForeColor = Color.Green;
                        //}
                        //else
                        //{
                        //    player.labelLife.ForeColor = Color.Black;
                        //}
                        //player.labelLife.Update();
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
                        player.ActivateBuff(player.pbMagicAttackUp, Database.BaseResourceFolder + "BuffMagicAttackUp.bmp", Database.INFINITY);
                        player.ActivateBuff(player.pbPhysicalAttackUp, Database.BaseResourceFolder + "BuffPhysicalAttackUp.bmp", Database.INFINITY);
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

                case Database.RARE_COLORESS_ANTIDOTE:
                    if (this.onlyUseItem)
                    {
                        player.DeleteBackPack(backpackData, 1, currentNumber);
                        player.RemoveDebuffParam();
                        player.CurrentColoressAntidote = Database.INFINITY;
                        player.ActivateBuff(player.pbColoressAntidote, Database.BaseResourceFolder + Database.ITEMCOMMAND_COLORESS_ANTIDOTE, Database.INFINITY);
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
                        RefreshPartyMembersLife();
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
                        RefreshPartyMembersLife();
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
                        RefreshPartyMembersLife();
                    }
                    else
                    {
                        mainMessage.text = player.GetCharacterSentence(2028);
                    }
                    break;

                case "リヴァイヴポーション":
                    if (!GroundOne.WE.AlreadyUseRevivePotion)
                    {
                        // todo
                        //target = null;
                        //using (SelectTarget st = new SelectTarget())
                        //{
                        //    st.StartPosition = FormStartPosition.Manual;
                        //    st.Location = new Point(this.mousePosX, this.mousePosY);
                        //    if (we.AvailableThirdCharacter)
                        //    {
                        //        st.MaxSelectable = 3;
                        //        st.FirstName = mc.Name;
                        //        st.SecondName = sc.Name;
                        //        st.ThirdName = tc.Name;
                        //        st.ShowDialog();
                        //    }
                        //    else if (we.AvailableSecondCharacter)
                        //    {
                        //        st.MaxSelectable = 2;
                        //        st.FirstName = mc.Name;
                        //        st.SecondName = sc.Name;
                        //        st.ShowDialog();
                        //    }
                        //    else
                        //    {
                        //        st.TargetNum = 1;
                        //    }

                        //    if (st.TargetNum == 1)
                        //    {
                        //        target = mc;
                        //    }
                        //    else if (st.TargetNum == 2)
                        //    {
                        //        target = sc;
                        //    }
                        //    else if (st.TargetNum == 3)
                        //    {
                        //        target = tc;
                        //    }
                        //}
                        //if (target.Dead)
                        //{
                        //    we.AlreadyUseRevivePotion = true;
                        //    target.Dead = false;
                        //    target.CurrentLife = target.MaxLife / 2;
                        //    this.life.text = target.CurrentLife.ToString() + " / " + target.MaxLife.ToString();
                        //    mainMessage.text = target.GetCharacterSentence(2016);
                        //}
                        //else if (target == player)
                        //{
                        //    mainMessage.text = player.GetCharacterSentence(2018);
                        //}
                        //else
                        //{
                        //    mainMessage.text = String.Format(player.GetCharacterSentence(2017), target.Name);
                        //}
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
                        mainMessage.text = "アイン：ダメだ。ラナが囚われたままだ。助けるまではもう街へは帰らねえ。";
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

                    mainMessage.text = player.GetCharacterSentence(2006);
                    this.usingToomiBlueSuisyou = true;
                    break;
            }
        }
        
        public void Handover_Click()
        {
            groupChoice.SetActive(false);
            //backpackFilter.SetActive(false); // ExecHandOverの続きがある。

            if (!GroundOne.WE.AvailableSecondCharacter && !GroundOne.WE.AvailableThirdCharacter) // 1人しかいない場合、「わたす」コマンドではなく、「すてる」である。
            {
                Trash_Click();
            }
            else // ここからが「わたす」コマンドである
            {
                targetName1.text = GroundOne.MC.FirstName;
                targetName2.text = GroundOne.SC.FirstName;
                targetName3.text = GroundOne.TC.FirstName;

                if (GroundOne.WE.AvailableSecondCharacter)
                {
                    btnTargetName2.gameObject.SetActive(true);
                    labelSecondPlayerLife.gameObject.SetActive(true);
                }
                else
                {
                    btnTargetName2.gameObject.SetActive(false);
                    GameObject emptyObj = new GameObject();
                    emptyObj.AddComponent<RectTransform>();
                    emptyObj.transform.SetParent(groupTarget.transform);
                }

                if (GroundOne.WE.AvailableThirdCharacter)
                {
                    btnTargetName3.gameObject.SetActive(true);
                }
                else
                {
                    btnTargetName3.gameObject.SetActive(false);
                    GameObject emptyObj = new GameObject();
                    emptyObj.AddComponent<RectTransform>();
                    emptyObj.transform.SetParent(groupTarget.transform);
                }

                groupTarget.gameObject.transform.position = this.currentPosition;
                groupTarget.SetActive(true);
                return;
            }
        }

        public void Trash_Click()
        {
            groupChoice.SetActive(false);
            backpackFilter.SetActive(false);
            MainCharacter player = GetCurrentPlayer();
            ItemBackPack backpackData = new ItemBackPack(currentSelect.text);
            if (TruthItemAttribute.CheckImportantItem(backpackData.Name) == TruthItemAttribute.Transfer.Any)
            {
                int exchangeValue = CallBackPackExchangeValue(player, backpackData, this.currentNumber);
                if (exchangeValue <= -1) return;

                player.DeleteBackPack(backpackData, exchangeValue, this.currentNumber);
                UsingItemUpdateBackPackLabel(player, backpackData, currentSelect, this.currentNumber);
                currentSelect = null;
            }
            else
            {
                mainMessage.text = player.GetCharacterSentence(2013);
            }
        }


        public void ExecHandover_Click(Text sender)
        {
            groupTarget.SetActive(false);
            backpackFilter.SetActive(false);
            MainCharacter player = GetCurrentPlayer();
            ItemBackPack backpackData = new ItemBackPack(currentSelect.text);
            int exchangeValue = CallBackPackExchangeValue(player, backpackData, this.currentNumber);
            if (exchangeValue <= -1) return;

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
            if (player == target)
            {
                // todo (自分の荷物を自分に渡すメッセージ）
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
            MainCharacter player = GetCurrentPlayer();

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
            for (int currentNumber = 0; currentNumber < backpack.Length; currentNumber++ )
            {
                if (backpack[currentNumber].Equals(sender))
                {
                    this.currentNumber = currentNumber;
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
            // todo
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

        private void UsingItemUpdateBackPackLabel(MainCharacter player, ItemBackPack backpackData, Text sender, int currentNumber)
        {
            int stackValue = player.CheckBackPackExist(backpackData, currentNumber);
            if (stackValue <= 0)
            {
                backpack[currentNumber].text = "";
                backpackStack[currentNumber].text = "";
                backpackIcon[currentNumber].sprite = null;
                Method.UpdateRareColor(null, backpack[currentNumber], back_Backpack[currentNumber]);
                //back_Backpack[currentNumber].SetActive(false);
            }
            else
            {
                backpackStack[currentNumber].text = "x" + stackValue.ToString();
            }
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
            mainMessage.text = "";
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

        //private int mousePosX = 0;
        //private int mousePosY = 0;
        //void StatusPlayer_MouseDown(object sender, MouseEventArgs e)
        //{
        //    this.mousePosX = this.Location.X + ((Label)sender).Location.X + e.X;
        //    this.mousePosY = this.Location.Y + ((Label)sender).Location.Y + e.Y;
        //}

        public void HideAllChild()
        {
            groupChoice.SetActive(false);
            groupTarget.SetActive(false);
            backpackFilter.SetActive(false);
        }
        private void SettingCharacterData(MainCharacter chara)
        {
            this.txtName.text = chara.FullName;
            this.txtLevel.text = chara.Level.ToString();
            if (chara.Level < Database.CHARACTER_MAX_LEVEL5)
            {
                this.txtExperience.text = chara.Exp.ToString() + " / " + chara.NextLevelBorder.ToString();
            }
            else
            {
                this.txtExperience.text = "-----" + " / " + "-----";
            }

            this.strength.text = chara.Strength.ToString();
            // todo
            this.addStrength.text = " + " + chara.BuffStrength_Accessory.ToString();
            //if (chara.BuffStrength_Food == 0) this.addStrength2.text = "";
            //else this.addStrength2.text = " + " + chara.BuffStrength_Food.ToString();

            this.agility.text = chara.Agility.ToString();
            // todo
            this.addAgility.text = " + " + chara.BuffAgility_Accessory.ToString();
            //if (chara.BuffAgility_Food == 0) this.addAgility2.text = "";
            //else this.addAgility2.text = " + " + chara.BuffAgility_Food.ToString();

            this.intelligence.text = chara.Intelligence.ToString();
            // todo
            this.addIntelligence.text = " + " + chara.BuffIntelligence_Accessory.ToString();
            //if (chara.BuffIntelligence_Food == 0) this.addIntelligence2.text = "";
            //else this.addIntelligence2.text = " + " + chara.BuffIntelligence_Food.ToString();

            this.stamina.text = chara.Stamina.ToString();
            // todo
            this.addStamina.text = " + " + chara.BuffStamina_Accessory.ToString();
            //if (chara.BuffStamina_Food == 0) this.addStamina2.text = "";
            //else this.addStamina2.text = " + " + chara.BuffStamina_Food.ToString();

            this.mind.text = chara.Mind.ToString();
            // todo
            this.addMind.text = " + " + chara.BuffMind_Accessory.ToString();
            //if (chara.BuffMind_Food == 0) this.addMind2.text = "";
            //else this.addMind2.text = " + " + chara.BuffMind_Food.ToString();

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

            RefreshPartyMembersLife();
            this.life.text = chara.CurrentLife.ToString() + " / " + chara.MaxLife.ToString();

            if (chara.AvailableSkill)
            {
                if (chara.CurrentSkillPoint > chara.MaxSkillPoint)
                {
                    chara.CurrentSkillPoint = chara.MaxSkillPoint;
                }
                skill.text = chara.CurrentSkillPoint.ToString() + " / " + chara.MaxSkillPoint.ToString();
            }

            if (chara.AvailableMana)
            {
                mana.text = chara.CurrentMana.ToString() + " / " + chara.MaxMana.ToString();
            }

            this.weapon.text = "";
            this.subWeapon.text = "";
            this.armor.text = "";
            this.accessory.text = "";
            this.accessory2.text = "";
            if (chara.MainWeapon != null)
            {
                this.weapon.text = chara.MainWeapon.Name;
            }
            else
            {
                this.weapon.text = "";
            }
            Method.UpdateRareColor(chara.MainWeapon, weapon, back_weapon);

            if (chara.SubWeapon != null)
            {
                this.subWeapon.text = chara.SubWeapon.Name;
            }
            else
            {
                this.subWeapon.text = "";
            }
            Method.UpdateRareColor(chara.SubWeapon, subWeapon, back_subWeapon);

            if (chara.MainArmor != null)
            {
                this.armor.text = chara.MainArmor.Name;
            }
            else
            {
                this.armor.text = "";
            }
            Method.UpdateRareColor(chara.MainArmor, armor, back_armor);

            if (chara.Accessory != null)
            {
                this.accessory.text = chara.Accessory.Name;
            }
            else
            {
                this.accessory.text = "";
            }
            Method.UpdateRareColor(chara.Accessory, accessory, back_accessory);

            if (chara.Accessory2 != null)
            {
                this.accessory2.text = chara.Accessory2.Name;
            }
            else
            {
                this.accessory2.text = "";
            }
            Method.UpdateRareColor(chara.Accessory2, accessory2, back_accessory2);

            txtGold.text = GroundOne.MC.Gold.ToString() + "[G]";

            UpdateBackPackLabel(chara);
            UpdateSpellSkillLabel(chara);
            UpdateResistStatus(chara);
        }

        private void UpdateSpellSkillLabel(MainCharacter target)
        {
            if (target.FreshHeal == false)
            {
                SpellSkill[0].text = "";
            }
            else
            {
                SpellSkill[0].text = Database.FRESH_HEAL;
            }

            if (target.LifeTap == false)
            {
                SpellSkill[1].text = "";
            }
            else
            {
                SpellSkill[1].text = Database.LIFE_TAP;
            }

            if (target.Resurrection == false)
            {
                SpellSkill[2].text = "";
            }
            else
            {
                SpellSkill[2].text = Database.RESURRECTION;
            }

            if (target.CelestialNova == false)
            {
                SpellSkill[3].text = "";
            }
            else
            {
                SpellSkill[3].text = Database.CELESTIAL_NOVA;
            }

            if (target.SacredHeal == false)
            {
                SpellSkill[4].text = "";
            }
            else
            {
                SpellSkill[4].text = Database.SACRED_HEAL;
            }
        }

        private void UpdateBackPackLabel(MainCharacter target)
        {
            ItemBackPack[] backpackData = target.GetBackPackInfo();
            for (int currentNumber = 0; currentNumber < backpackData.Length; currentNumber++)
            {
                if (backpackData[currentNumber] == null)
                {
                    backpack[currentNumber].text = "";
                    backpackStack[currentNumber].text = "";
                    backpackIcon[currentNumber].sprite = null;
                    Method.UpdateRareColor(null, backpack[currentNumber], back_Backpack[currentNumber]);
                    //back_Backpack[currentNumber].SetActive(false);
                }
                else
                {
                    back_Backpack[currentNumber].SetActive(true);
                    backpack[currentNumber].text = backpackData[currentNumber].Name;
                    Method.UpdateRareColor(backpackData[currentNumber], backpack[currentNumber], back_Backpack[currentNumber]);
                    backpackStack[currentNumber].text = "x" + backpackData[currentNumber].StackValue.ToString();
                    if ((backpackData[currentNumber].Type == ItemBackPack.ItemType.Weapon_Heavy) ||
                        (backpackData[currentNumber].Type == ItemBackPack.ItemType.Weapon_Middle))
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("Weapon");
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Weapon_TwoHand)
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("TwoHand");
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Weapon_Light)
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("Knuckle");
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Weapon_Rod)
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("Rod");
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Shield)
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("Shield");
                    }
                    else if ((backpackData[currentNumber].Type == ItemBackPack.ItemType.Armor_Heavy) ||
                                (backpackData[currentNumber].Type == ItemBackPack.ItemType.Armor_Middle))
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("Armor");
                    }
                    else if ((backpackData[currentNumber].Type == ItemBackPack.ItemType.Armor_Light))
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("LightArmor");
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Accessory)
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("Accessory");
                    }
                    else if ((backpackData[currentNumber].Type == ItemBackPack.ItemType.Material_Equip) ||
                                (backpackData[currentNumber].Type == ItemBackPack.ItemType.Material_Food) ||
                                (backpackData[currentNumber].Type == ItemBackPack.ItemType.Material_Potion))
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("Material1");
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Use_Potion)
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("Potion");
                    }
                    else if (backpackData[currentNumber].Type == ItemBackPack.ItemType.Use_Any)
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("Useless");
                    }
                    else
                    {
                        backpackIcon[currentNumber].sprite = Resources.Load<Sprite>("Useless");
                    }
                }
            }
        }

        private void UpdateResistStatus(MainCharacter player)
        {
            // 【後編必須】％レジスト増強も実装してください。
            ResistLabel[0].text = Database.STRING_LIGHT;
            ResistLabelValue[0].text = "+" + player.TotalResistLight.ToString() + " (0%)";
            ResistLabel[1].text = Database.STRING_SHADOW;
            ResistLabelValue[1].text = "+" + player.TotalResistShadow.ToString() + " (0%)";
            ResistLabel[2].text = Database.STRING_FIRE;
            ResistLabelValue[2].text = "+" + player.TotalResistFire.ToString() + " (0%)";
            ResistLabel[3].text = Database.STRING_ICE;
            ResistLabelValue[3].text = "+" + player.TotalResistIce.ToString() + " (0%)";
            ResistLabel[4].text = Database.STRING_FORCE;
            ResistLabelValue[4].text = "+" + player.TotalResistForce.ToString() + " (0%)";
            ResistLabel[5].text = Database.STRING_WILL;
            ResistLabelValue[5].text = "+" + player.TotalResistWill.ToString() + " (0%)";

            ResistAbnormalStatus[0].text = Database.STRING_STUNNING;
            if (player.CheckResistStun) ResistAbnormalStatusValue[0].text = "○";
            else ResistAbnormalStatusValue[0].text = "--";
            ResistAbnormalStatus[1].text = Database.STRING_SILENCE;
            if (player.CheckResistSilence) ResistAbnormalStatusValue[1].text = "○";
            else ResistAbnormalStatusValue[1].text = "--";
            ResistAbnormalStatus[2].text = Database.STRING_POISON;
            if (player.CheckResistPoison) ResistAbnormalStatusValue[2].text = "○";
            else ResistAbnormalStatusValue[2].text = "--";
            ResistAbnormalStatus[3].text = Database.STRING_TEMPTATION;
            if (player.CheckResistTemptation) ResistAbnormalStatusValue[3].text = "○";
            else ResistAbnormalStatusValue[3].text = "--";
            ResistAbnormalStatus[4].text = Database.STRING_FROZEN;
            if (player.CheckResistFrozen) ResistAbnormalStatusValue[4].text = "○";
            else ResistAbnormalStatusValue[4].text = "--";
            ResistAbnormalStatus[5].text = Database.STRING_PARALYZE;
            if (player.CheckResistParalyze) ResistAbnormalStatusValue[5].text = "○";
            else ResistAbnormalStatusValue[5].text = "--";
            ResistAbnormalStatus[6].text = Database.STRING_SLOW;
            if (player.CheckResistSlow) ResistAbnormalStatusValue[6].text = "○";
            else ResistAbnormalStatusValue[6].text = "--";
            ResistAbnormalStatus[7].text = Database.STRING_BLIND;
            if (player.CheckResistBlind) ResistAbnormalStatusValue[7].text = "○";
            else ResistAbnormalStatusValue[7].text = "--";
            ResistAbnormalStatus[8].text = Database.STRING_SLIP;
            if (player.CheckResistSlip) ResistAbnormalStatusValue[8].text = "○";
            else ResistAbnormalStatusValue[8].text = "--";
            // [コメント] 復活不可は特殊なので、ステータスとして見せたくはない。
            //ResistAbnormalStatus[9].Text = Database.STRING_NORESURRECTION;
            //if (player.CheckResistNoResurrection) ResistAbnormalStatusValue[9].Text += "　○";
        }

        //Point basePhysicalLocation;
        private void RefreshPartyMembersBattleStatus(MainCharacter player)
        {
            double temp1 = 0;
            double temp2 = 0;
            temp1 = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false);
            temp2 = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Max, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false);
            txtPhysicalAttack.text = temp1.ToString("F2");
            txtPhysicalAttack.text += " - " + temp2.ToString("F2");

            temp1 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false);
            temp2 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, MainCharacter.PlayerStance.None, false);
            if (temp1 > 0)
            {
                //txtPhysicalAttack.Location = new Point(this.basePhysicalLocation.X, this.basePhysicalLocation.Y - 10); // todo
                txtPhysicalAttack.text += "\r\n" + temp1.ToString("F2");
                txtPhysicalAttack.text += " - " + temp2.ToString("F2");
            }
            else
            {
                //txtPhysicalAttack.Location = new Point(this.basePhysicalLocation.X, this.basePhysicalLocation.Y); // todo
            }

            temp1 = PrimaryLogic.PhysicalDefenseValue(player, PrimaryLogic.NeedType.Min, false);
            temp2 = PrimaryLogic.PhysicalDefenseValue(player, PrimaryLogic.NeedType.Max, false);
            txtPhysicalDefense.text = temp1.ToString("F2");
            txtPhysicalDefense.text += " - " + temp2.ToString("F2");

            temp1 = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Min, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false);
            temp2 = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Max, 1.0f, 0.0f, MainCharacter.PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false);
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

        private void RefreshPartyMembersLife()
        {
            if (GroundOne.WE.AvailableFirstCharacter)
            {
                labelFirstPlayerLife.text = GroundOne.MC.CurrentLife.ToString() + "/" + GroundOne.MC.MaxLife.ToString();
            }
            if (GroundOne.WE.AvailableSecondCharacter && GroundOne.DuelMode == false)
            {
                labelSecondPlayerLife.text = GroundOne.SC.CurrentLife.ToString() + "/" + GroundOne.SC.MaxLife.ToString();
            }
            if (GroundOne.WE.AvailableThirdCharacter && GroundOne.DuelMode == false)
            {
                labelThirdPlayerLife.text = GroundOne.TC.CurrentLife.ToString() + "/" + GroundOne.TC.MaxLife.ToString();
            }
        }

        public void btnSomeSpellSkill_Click(Text sender)
        {
            MainCharacter player = GetCurrentPlayer();

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

            if ((sender).text == Database.FRESH_HEAL_JP)
            {
                if (player.CurrentMana < Database.FRESH_HEAL_COST)
                {
                    mainMessage.text = player.GetCharacterSentence(2008);
                    return;
                }
            }
            else if ((sender).text == Database.LIFE_TAP_JP)
            {
                if (player.CurrentMana < Database.LIFE_TAP_COST)
                {
                    mainMessage.text = player.GetCharacterSentence(2008);
                    return;
                }
            }
            else if ((sender).text == Database.RESURRECTION_JP)
            {
                if (player.CurrentMana < Database.RESURRECTION_COST)
                {
                    mainMessage.text = player.GetCharacterSentence(2008);
                    return;
                }
            }
            else if ((sender).text == Database.SACRED_HEAL_JP)
            {
                if (player.CurrentMana < Database.SACRED_HEAL_COST)
                {
                    mainMessage.text = player.GetCharacterSentence(2008);
                    return;
                }
            }

            // 単体対象の場合
            if (((sender).text == Database.FRESH_HEAL_JP) ||
                ((sender).text == Database.LIFE_TAP_JP) ||
                ((sender).text == Database.RESURRECTION_JP))
            {
                MainCharacter target = null;
                if (!GroundOne.WE.AvailableSecondCharacter && !GroundOne.WE.AvailableThirdCharacter)
                {
                    target = GroundOne.MC;
                }
                else if (GroundOne.WE.AvailableSecondCharacter || GroundOne.WE.AvailableThirdCharacter)
                {
                    // todo
                    //using (SelectDungeon sa = new SelectDungeon())
                    //{
                    //    sa.StartPosition = FormStartPosition.Manual;
                    //    if ((this.Location.X + this.Size.Width - this.mousePosX) <= sa.Width) this.mousePosX = this.Location.X + this.Size.Width - sa.Width;
                    //    if ((this.Location.Y + this.Size.Height - this.mousePosY) <= sa.Height) this.mousePosY = this.Location.Y + this.Size.Height - sa.Height;
                    //    sa.Location = new Point(this.mousePosX, this.mousePosY + this.grpSpellSkill.Location.Y);
                    //    if (GroundOne.WE.AvailableSecondCharacter && GroundOne.WE.AvailableThirdCharacter)
                    //    {
                    //        sa.MaxSelectable = 3;
                    //        sa.FirstName = GroundOne.MC.Name;
                    //        sa.SecondName = GroundOne.SC.Name;
                    //        sa.ThirdName = GroundOne.TC.Name;
                    //    }
                    //    else if (GroundOne.WE.AvailableSecondCharacter && !GroundOne.WE.AvailableThirdCharacter)
                    //    {
                    //        sa.MaxSelectable = 2;
                    //        sa.FirstName = GroundOne.MC.Name;
                    //        sa.SecondName = GroundOne.SC.Name;
                    //    }
                    //    // after delete
                    //    //else if (!GroundOne.WE.AvailableSecondCharacter && GroundOne.WE.AvailableThirdCharacter)
                    //    //{
                    //    //    sa.MaxSelectable = 2;
                    //    //    sa.FirstName = mc.Name;
                    //    //    sa.SecondName = tc.Name;
                    //    //}
                    //    sa.EnablePopUpInfo = true;
                    //    sa.MC = GroundOne.MC;
                    //    sa.SC = GroundOne.SC;
                    //    sa.TC = GroundOne.TC;
                    //    sa.ShowDialog();
                    //    if (sa.TargetDungeon == 1)
                    //    {
                    //        target = GroundOne.MC;
                    //    }
                    //    else if (sa.TargetDungeon == 2)
                    //    {
                    //        target = GroundOne.SC;
                    //    }
                    //    else if (sa.TargetDungeon == 3)
                    //    {
                    //        target = GroundOne.TC;
                    //    }
                    //    else
                    //    {
                    //        // ESCキーキャンセルは何もしません。
                    //        return;
                    //    }
                    //}
                }

                if (((sender).text == Database.FRESH_HEAL_JP) ||
                    ((sender).text == Database.LIFE_TAP_JP))
                {
                    if (target.Dead)
                    {
                        mainMessage.text = "【" + target.FirstName + "は死んでしまっているため、効果がない。】";
                        return;
                    }

                    int lifeGain = 0;
                    if ((sender).text == Database.FRESH_HEAL_JP)
                    {
                        player.CurrentMana -= Database.FRESH_HEAL_COST;
                        lifeGain = (int)PrimaryLogic.FreshHealValue(player, false);
                    }
                    else if ((sender).text == Database.LIFE_TAP_JP)
                    {
                        player.CurrentMana -= Database.LIFE_TAP_COST;
                        lifeGain = (int)PrimaryLogic.LifeTapValue(player, false);
                    }

                    target.CurrentLife += lifeGain;
                    mainMessage.text = String.Format(player.GetCharacterSentence(2001), lifeGain.ToString());
                }
                else
                {
                    if (target.Dead)
                    {
                        player.CurrentMana -= Database.RESURRECTION_COST;

                        target.Dead = false;
                        target.CurrentLife = (int)PrimaryLogic.ResurrectionValue(target);
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
            }
            // 味方全体の場合
            else
            {
                int lifeGain = 0;
                if ((sender).text == Database.SACRED_HEAL_JP)
                {
                    player.CurrentMana -= Database.SACRED_HEAL_COST;
                    lifeGain = (int)PrimaryLogic.SacredHealValue(player, false);
                }

                List<MainCharacter> group = new List<MainCharacter>();
                if (GroundOne.MC != null && !GroundOne.MC.Dead) { group.Add(GroundOne.MC); }
                if (GroundOne.SC != null && !GroundOne.SC.Dead) { group.Add(GroundOne.SC); }
                if (GroundOne.TC != null && !GroundOne.TC.Dead) { group.Add(GroundOne.TC); }
                for (int currentNumber = 0; currentNumber < group.Count; currentNumber++)
                {
                    group[currentNumber].CurrentLife += lifeGain;
                    mainMessage.text = String.Format(player.GetCharacterSentence(2035), lifeGain.ToString());
                }
            }

            this.life.text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();
            this.mana.text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
            RefreshPartyMembersLife();
        }

        //private void btnResurrection_Click(object sender, EventArgs e)
        //{
        //    MainCharacter player = null;
        //    if (mc != null && mc.PlayerStatusColor == this.BackColor)
        //    {
        //        player = this.mc;
        //    }
        //    else if (sc != null && sc.PlayerStatusColor == this.BackColor)
        //    {
        //        player = this.sc;
        //    }
        //    else if (tc != null && tc.PlayerStatusColor == this.BackColor)
        //    {
        //        player = this.tc;
        //    }

        //    if (player.Dead)
        //    {
        //        mainMessage.Text = "【" + player.Name + "は死んでしまっているため、魔法詠唱ができない。】";
        //        return;
        //    }

        //    if (GroundOne.LevelUp)
        //    {
        //        mainMessage.Text = player.GetCharacterSentence(2002);
        //        return;
        //    }

        //    if (this.useOverShifting)
        //    {
        //        mainMessage.Text = player.GetCharacterSentence(2023);
        //        return;
        //    }

        //    if (GroundOne.OnlySelectTrash)
        //    {
        //        mainMessage.Text = player.GetCharacterSentence(2021);
        //        return;
        //    }

        //    if (player.CurrentMana < Database.RESURRECTION_COST)
        //    {
        //        mainMessage.Text = player.GetCharacterSentence(2008);
        //        return;
        //    }

        //    MainCharacter target = null;
        //    if (!we.AvailableSecondCharacter && !we.AvailableThirdCharacter)
        //    {
        //        target = this.mc;
        //    }
        //    else if (we.AvailableSecondCharacter || we.AvailableThirdCharacter)
        //    {
        //        using (SelectDungeon sa = new SelectDungeon())
        //        {
        //            sa.StartPosition = FormStartPosition.Manual;
        //            if ((this.Location.X + this.Size.Width - this.mousePosX) <= sa.Width) this.mousePosX = this.Location.X + this.Size.Width - sa.Width;
        //            if ((this.Location.Y + this.Size.Height - this.mousePosY) <= sa.Height) this.mousePosY = this.Location.Y + this.Size.Height - sa.Height;
        //            sa.Location = new Point(this.mousePosX, this.mousePosY);
        //            if (we.AvailableSecondCharacter && we.AvailableThirdCharacter)
        //            {
        //                sa.MaxSelectable = 3;
        //                sa.FirstName = mc.Name;
        //                sa.SecondName = sc.Name;
        //                sa.ThirdName = tc.Name;
        //            }
        //            else if (we.AvailableSecondCharacter && !we.AvailableThirdCharacter)
        //            {
        //                sa.MaxSelectable = 2;
        //                sa.FirstName = mc.Name;
        //                sa.SecondName = sc.Name;
        //            }
        //            //else if (!we.AvailableSecondCharacter && we.AvailableThirdCharacter)
        //            //{
        //            //    sa.MaxSelectable = 2;
        //            //    sa.FirstName = mc.Name;
        //            //    sa.SecondName = tc.Name;
        //            //}
        //            sa.ShowDialog();
        //            if (sa.TargetDungeon == 1)
        //            {
        //                target = this.mc;
        //            }
        //            else if (sa.TargetDungeon == 2)
        //            {
        //                target = this.sc;
        //            }
        //            else if (sa.TargetDungeon == 3)
        //            {
        //                target = this.tc;
        //            }
        //            else
        //            {
        //                // ESCキーキャンセルは何もしません。
        //                return;
        //            }
        //        }
        //    }

        //    if (target.Dead)
        //    {
        //        player.CurrentMana -= Database.RESURRECTION_COST;
        //        this.mana.Text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();

        //        target.Dead = false;
        //        target.CurrentLife = target.MaxLife / 2;
        //        mainMessage.Text = String.Format(target.GetCharacterSentence(2016));
        //    }
        //    else if (target == player)
        //    {
        //        mainMessage.Text = String.Format(player.GetCharacterSentence(2018));
        //    }
        //    else if (!target.Dead)
        //    {
        //        mainMessage.Text = String.Format(player.GetCharacterSentence(2017), target.Name);
        //    }
        //    RefreshPartyMembersLife();
        //}

        //private void btnCelestialNova_Click(object sender, EventArgs e)
        //{
        //    MainCharacter player = null;
        //    if (mc != null && mc.PlayerStatusColor == this.BackColor)
        //    {
        //        player = this.mc;
        //    }
        //    else if (sc != null && sc.PlayerStatusColor == this.BackColor)
        //    {
        //        player = this.sc;
        //    }
        //    else if (tc != null && tc.PlayerStatusColor == this.BackColor)
        //    {
        //        player = this.tc;
        //    }

        //    if (player.Dead)
        //    {
        //        mainMessage.Text = "【" + player.Name + "は死んでしまっているため、魔法詠唱ができない。】";
        //        return;
        //    }

        //    if (GroundOne.LevelUp)
        //    {
        //        mainMessage.Text = player.GetCharacterSentence(2002);
        //        return;
        //    }

        //    if (this.useOverShifting)
        //    {
        //        mainMessage.Text = player.GetCharacterSentence(2023);
        //        return;
        //    }

        //    if (GroundOne.OnlySelectTrash)
        //    {
        //        mainMessage.Text = player.GetCharacterSentence(2021);
        //        return;
        //    }

        //    if (player.CurrentMana < Database.CELESTIAL_NOVA_COST)
        //    {
        //        mainMessage.Text = player.GetCharacterSentence(2008);
        //        return;
        //    }

        //    MainCharacter target = null;
        //    if (!we.AvailableSecondCharacter && !we.AvailableThirdCharacter)
        //    {
        //        target = this.mc;
        //    }
        //    else if (we.AvailableSecondCharacter || we.AvailableThirdCharacter)
        //    {
        //        using (SelectDungeon sa = new SelectDungeon())
        //        {
        //            sa.StartPosition = FormStartPosition.Manual;
        //            if ((this.Location.X + this.Size.Width - this.mousePosX) <= sa.Width) this.mousePosX = this.Location.X + this.Size.Width - sa.Width;
        //            if ((this.Location.Y + this.Size.Height - this.mousePosY) <= sa.Height) this.mousePosY = this.Location.Y + this.Size.Height - sa.Height;
        //            sa.Location = new Point(this.mousePosX, this.mousePosY);

        //            if (we.AvailableSecondCharacter && we.AvailableThirdCharacter)
        //            {
        //                sa.MaxSelectable = 3;
        //                sa.FirstName = mc.Name;
        //                sa.SecondName = sc.Name;
        //                sa.ThirdName = tc.Name;
        //            }
        //            else if (we.AvailableSecondCharacter && !we.AvailableThirdCharacter)
        //            {
        //                sa.MaxSelectable = 2;
        //                sa.FirstName = mc.Name;
        //                sa.SecondName = sc.Name;
        //            }
        //            //else if (!we.AvailableSecondCharacter && we.AvailableThirdCharacter)
        //            //{
        //            //    sa.MaxSelectable = 2;
        //            //    sa.FirstName = mc.Name;
        //            //    sa.SecondName = tc.Name;
        //            //}
        //            sa.EnablePopUpInfo = true;
        //            sa.MC = this.mc;
        //            sa.SC = this.sc;
        //            sa.TC = this.tc;
        //            sa.ShowDialog();
        //            if (sa.TargetDungeon == 1)
        //            {
        //                target = this.mc;
        //            }
        //            else if (sa.TargetDungeon == 2)
        //            {
        //                target = this.sc;
        //            }
        //            else if (sa.TargetDungeon == 3)
        //            {
        //                target = this.tc;
        //            }
        //            else
        //            {
        //                // ESCキーキャンセルは何もしません。
        //                return;
        //            }
        //        }
        //    }

        //    if (target.Dead)
        //    {
        //        mainMessage.Text = "【" + target.Name + "は死んでしまっているため、効果がない。】";
        //        return;
        //    }

        //    player.CurrentMana -= Database.CELESTIAL_NOVA_COST;
        //    Random rd = new Random(DateTime.Now.Millisecond);
        //    int effectValue = 400 + player.Intelligence * 5 + rd.Next(player.Mind, player.Mind * 2);

        //    target.CurrentLife += effectValue;
        //    mainMessage.Text = String.Format(player.GetCharacterSentence(2001), effectValue.ToString());
        //    this.life.Text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();
        //    this.mana.Text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
        //    RefreshPartyMembersLife();
        //}

        public void weapon_Click(Text sender)
        {
            ChangeEquipment(0);
        }
        public void subWeapon_Click(Text sender)
        {
            MainCharacter targetPlayer = GetCurrentPlayer();
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
            ChangeEquipment(1);
        }
        public void armor_Click(Text sender)
        {
            ChangeEquipment(2);
        }
        public void accessory_Click(Text sender)
        {
            ChangeEquipment(3);
        }
        public void accessory2_Click(Text sender)
        {
            ChangeEquipment(4);
        }

        // equipType: 0:Weapon  1:SubWeapon  2:Armor  3:Accessory  4:Accessory2
        private void ChangeEquipment(int equipType)
        {
            MainCharacter targetPlayer = GetCurrentPlayer();
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

            // todo
            //TruthSelectEquipment tse = new TruthSelectEquipment();
            //ItemBackPack[] temp = targetPlayer.GetBackPackInfo();
            //int counter = 0;
            //for (int currentNumber = 0; currentNumber < temp.Length; currentNumber++)
            //{
            //    if (temp[currentNumber] == null)
            //        continue;

            //    if (CheckEquipmentType(targetPlayer, temp[currentNumber], equipType))
            //    {
            //        tse.btn[counter].Text = temp[currentNumber].Name;
            //        counter++;
            //    }
            //}
            GroundOne.EquipType = equipType;
            SceneDimension.Go(Database.TruthStatusPlayer, Database.TruthSelectEquipment);
            //tse.StartPosition = FormStartPosition.CenterParent;
            //tse.Player = targetPlayer;
            //tse.EquipType = equipType;
            //tse.ShowDialog();
            //if (tse.DialogResult == System.Windows.Forms.DialogResult.OK)
            //{
            //    ItemBackPack exchangeItem = new ItemBackPack(tse.SelectValue);
            //    ItemBackPack tempItem = null;
            //    if (equipType == 0)
            //    {
            //        tempItem = targetPlayer.MainWeapon;
            //        targetPlayer.MainWeapon = exchangeItem;
            //        if ((exchangeItem.Type == ItemBackPack.ItemType.Weapon_Rod) ||
            //            (exchangeItem.Type == ItemBackPack.ItemType.Weapon_TwoHand))
            //        {
            //            if (targetPlayer.SubWeapon != null)
            //            {
            //                if (targetPlayer.SubWeapon.Name != "")
            //                {
            //                    targetPlayer.AddBackPack(targetPlayer.SubWeapon);
            //                }
            //                targetPlayer.SubWeapon = null;
            //            }
            //        }
            //    }
            //    else if (equipType == 1)
            //    {
            //        tempItem = targetPlayer.SubWeapon;
            //        targetPlayer.SubWeapon = exchangeItem;
            //        if (targetPlayer.MainWeapon != null)
            //        {
            //            if (targetPlayer.MainWeapon.Name != "")
            //            {
            //                if ((targetPlayer.MainWeapon.Type == ItemBackPack.ItemType.Weapon_Rod) ||
            //                    (targetPlayer.MainWeapon.Type == ItemBackPack.ItemType.Weapon_TwoHand))
            //                {
            //                    targetPlayer.AddBackPack(targetPlayer.MainWeapon);
            //                    targetPlayer.MainWeapon = null;
            //                }
            //            }
            //        }
            //    }
            //    else if (equipType == 2)
            //    {
            //        tempItem = targetPlayer.MainArmor;
            //        targetPlayer.MainArmor = exchangeItem;
            //    }
            //    else if (equipType == 3)
            //    {
            //        tempItem = targetPlayer.Accessory;
            //        targetPlayer.Accessory = exchangeItem;
            //    }
            //    else if (equipType == 4)
            //    {
            //        tempItem = targetPlayer.Accessory2;
            //        targetPlayer.Accessory2 = exchangeItem;
            //    }
            //    if (exchangeItem != null)
            //    {
            //        if (exchangeItem.Name != "")
            //        {
            //            targetPlayer.DeleteBackPack(exchangeItem);
            //        }
            //    }
            //    if (tempItem != null)
            //    {
            //        if (tempItem.Name != "")
            //        {
            //            targetPlayer.AddBackPack(tempItem);
            //        }
            //    }
            //    SettingCharacterData(targetPlayer);
            //    RefreshPartyMembersBattleStatus(targetPlayer);
            //}
        }

        //// equipType: 0:Weapon  1:SubWeapon  2:Armor  3:Accessory  4:Accessory2
        //private bool CheckEquipmentType(MainCharacter player, ItemBackPack item, int equipType)
        //{
        //    if (equipType == 0)
        //    {
        //        if ((player == mc) && (item.Type == ItemBackPack.ItemType.Weapon_Heavy) ||
        //            (player == mc) && (item.Type == ItemBackPack.ItemType.Weapon_TwoHand))
        //        {
        //            return true;
        //        }
        //        else if ((player == sc) && (item.Type == ItemBackPack.ItemType.Weapon_Light) ||
        //                    (player == sc) && (item.Type == ItemBackPack.ItemType.Weapon_Rod))
        //        {
        //            return true;
        //        }
        //        else if ((player == tc) && (item.Type == ItemBackPack.ItemType.Weapon_TwoHand) ||
        //                    (player == tc) && (item.Type == ItemBackPack.ItemType.Weapon_Heavy) ||
        //                    (player == tc) && (item.Type == ItemBackPack.ItemType.Weapon_Middle) ||
        //                    (player == tc) && (item.Type == ItemBackPack.ItemType.Weapon_Light))
        //        {
        //            return true;
        //        }
        //    }
        //    else if (equipType == 1)
        //    {
        //        if ((player == mc) && (item.Type == ItemBackPack.ItemType.Weapon_Heavy) ||
        //            (player == mc) && (item.Type == ItemBackPack.ItemType.Shield))
        //        {
        //            return true;
        //        }
        //        else if ((player == sc) && (item.Type == ItemBackPack.ItemType.Weapon_Light))
        //        {
        //            return true;
        //        }
        //        else if ((player == tc) && (item.Type == ItemBackPack.ItemType.Weapon_Heavy) ||
        //                    (player == tc) && (item.Type == ItemBackPack.ItemType.Weapon_Middle) ||
        //                    (player == tc) && (item.Type == ItemBackPack.ItemType.Weapon_Light) ||
        //                    (player == tc) && (item.Type == ItemBackPack.ItemType.Shield))
        //        {
        //            return true;
        //        }
        //    }
        //    else if (equipType == 2)
        //    {
        //        if ((player == mc) && (item.Type == ItemBackPack.ItemType.Armor_Heavy) ||
        //            (player == mc) && (item.Type == ItemBackPack.ItemType.Armor_Middle))
        //        {
        //            return true;
        //        }
        //        else if ((player == sc) && (item.Type == ItemBackPack.ItemType.Armor_Light))
        //        {
        //            return true;
        //        }
        //        else if ((player == tc) && (item.Type == ItemBackPack.ItemType.Armor_Heavy) ||
        //                    (player == tc) && (item.Type == ItemBackPack.ItemType.Armor_Light) ||
        //                    (player == tc) && (item.Type == ItemBackPack.ItemType.Armor_Middle))
        //        {
        //            return true;
        //        }
        //    }
        //    else if (equipType == 3)
        //    {
        //        if (item.Type == ItemBackPack.ItemType.Accessory)
        //        {
        //            if ((player == mc) && (item.EquipablePerson == ItemBackPack.Equipable.All) ||
        //                (player == mc) && (item.EquipablePerson == ItemBackPack.Equipable.Ein))
        //            {
        //                return true;
        //            }
        //            if ((player == sc) && (item.EquipablePerson == ItemBackPack.Equipable.All) ||
        //                (player == sc) && (item.EquipablePerson == ItemBackPack.Equipable.Lana))
        //            {
        //                return true;
        //            }
        //            if ((player == tc) && (item.EquipablePerson == ItemBackPack.Equipable.All) ||
        //                (player == tc) && (item.EquipablePerson == ItemBackPack.Equipable.Verze) ||
        //                (player == tc) && (item.EquipablePerson == ItemBackPack.Equipable.Ol))
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    else if (equipType == 4)
        //    {
        //        if (item.Type == ItemBackPack.ItemType.Accessory)
        //        {
        //            if ((player == mc) && (item.EquipablePerson == ItemBackPack.Equipable.All) ||
        //                (player == mc) && (item.EquipablePerson == ItemBackPack.Equipable.Ein))
        //            {
        //                return true;
        //            }
        //            if ((player == sc) && (item.EquipablePerson == ItemBackPack.Equipable.All) ||
        //                (player == sc) && (item.EquipablePerson == ItemBackPack.Equipable.Lana))
        //            {
        //                return true;
        //            }
        //            if ((player == tc) && (item.EquipablePerson == ItemBackPack.Equipable.All) ||
        //                (player == tc) && (item.EquipablePerson == ItemBackPack.Equipable.Verze) ||
        //                (player == tc) && (item.EquipablePerson == ItemBackPack.Equipable.Ol))
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

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
                this.cam.backgroundColor = GroundOne.MC.PlayerStatusColor;
                this.currentStatusView = GroundOne.MC.PlayerStatusColor;
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
                this.cam.backgroundColor = GroundOne.SC.PlayerStatusColor;
                this.currentStatusView = GroundOne.SC.PlayerStatusColor;
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
                this.cam.backgroundColor = GroundOne.TC.PlayerStatusColor;
                this.currentStatusView = GroundOne.TC.PlayerStatusColor;
                SettingCharacterData(GroundOne.TC);
                RefreshPartyMembersBattleStatus(GroundOne.TC);
            }
        }

        public void ChangeViewButton_Click(int viewNumber)
        {
            MainCharacter targetPlayer = GetCurrentPlayer();
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

        // todo

        private void grpParameter_Paint()
        {
        }
        //private void grpParameter_Paint(object sender, PaintEventArgs e)
        //{
        //    if (this.useOverShifting)
        //    {
        //        Graphics g = e.Graphics;
        //        int BluePenWidth = 4;
        //        int SkyBluePenWidth = 2;
        //        Pen pen1 = new Pen(Brushes.DarkOrange, BluePenWidth);
        //        Pen pen2 = new Pen(Brushes.Orange, SkyBluePenWidth);
        //        int basePosX = 0; // 味方側のＸライン
        //        int basePosY = 0; // 味方：１人目のYライン(
        //        int len = 58; // 四角枠の横長さ
        //        int len2 = 54; // 四角枠の縦長さ

        //        if (this.number == upType.Strength) { basePosX = buttonStrength.Location.X; basePosY = buttonStrength.Location.Y; }
        //        else if (this.number == upType.Agility) { basePosX = buttonAgility.Location.X; basePosY = buttonAgility.Location.Y; }
        //        else if (this.number == upType.Intelligence) { basePosX = buttonIntelligence.Location.X; basePosY = buttonIntelligence.Location.Y; }
        //        else if (this.number == upType.Stamina) { basePosX = buttonStamina.Location.X; basePosY = buttonStamina.Location.Y; }
        //        else if (this.number == upType.Mind) { basePosX = buttonMind.Location.X; basePosY = buttonMind.Location.Y; }

        //        g.DrawRectangle(pen1, new Rectangle(basePosX - 4, basePosY - 4, len, len));
        //        g.DrawRectangle(pen2, new Rectangle(basePosX - 2, basePosY - 2, len2, len2));
        //    }
        //    else
        //    {
        //        base.OnPaint(e);
        //    }
        //}

        private void CheckUpPoint()
        {
            GroundOne.UpPoint--;
            if (GroundOne.UpPoint <= 0)
            {
                mainMessage.text = "ポイント割り振り完了！";
                txtClose.text = "完了";
                btnClose.gameObject.SetActive(true);
                // delete unity
                //buttonStrength.Enabled = false;
                //buttonAgility.Enabled = false;
                //buttonIntelligence.Enabled = false;
                //buttonStamina.Enabled = false;
                //buttonMind.Enabled = false;
                this.useOverShifting = false;
            }
            else
            {
                mainMessage.text = "あと" + GroundOne.UpPoint.ToString() + "ポイントを割り振ってください。";
            }
        }
        public void buttonStrength_Click()
        {
            if (GroundOne.UpPoint <= 0) { return; } // add unity

            // 通常レベルアップ＋１のロジック
            if (GroundOne.LevelUp)
            {
                if (GroundOne.MC != null && GroundOne.MC.PlayerStatusColor == this.cam.backgroundColor)
                {
                    GroundOne.MC.Strength++;
                    strength.text = GroundOne.MC.Strength.ToString();
                    RefreshPartyMembersBattleStatus(GroundOne.MC);
                }
                else if (GroundOne.SC != null && GroundOne.SC.PlayerStatusColor == this.cam.backgroundColor)
                {
                    GroundOne.SC.Strength++;
                    strength.text = GroundOne.SC.Strength.ToString();
                    RefreshPartyMembersBattleStatus(GroundOne.SC);
                }
                else if (GroundOne.TC != null && GroundOne.TC.PlayerStatusColor == this.cam.backgroundColor)
                {
                    GroundOne.TC.Strength++;
                    strength.text = GroundOne.TC.Strength.ToString();
                    RefreshPartyMembersBattleStatus(GroundOne.TC);
                }
                CheckUpPoint();
            }
            // オーバーシフティング
            else if (this.useOverShifting)
            {
                this.number = upType.Strength;
                grpParameter_Paint();
            }
        }

        public void buttonAgility_Click()
        {
            if (GroundOne.UpPoint <= 0) { return; } // add unity

            // 通常レベルアップ＋１のロジック
            if (GroundOne.LevelUp)
            {
                if (GroundOne.MC != null && GroundOne.MC.PlayerStatusColor == this.cam.backgroundColor)
                {
                    GroundOne.MC.Agility++;
                    agility.text = GroundOne.MC.Agility.ToString();
                    RefreshPartyMembersBattleStatus(GroundOne.MC);
                }
                else if (GroundOne.SC != null && GroundOne.SC.PlayerStatusColor == this.cam.backgroundColor)
                {
                    GroundOne.SC.Agility++;
                    agility.text = GroundOne.SC.Agility.ToString();
                    RefreshPartyMembersBattleStatus(GroundOne.SC);
                }
                else if (GroundOne.TC != null && GroundOne.TC.PlayerStatusColor == this.cam.backgroundColor)
                {
                    GroundOne.TC.Agility++;
                    agility.text = GroundOne.TC.Agility.ToString();
                    RefreshPartyMembersBattleStatus(GroundOne.TC);
                }
                CheckUpPoint();
            }
            // オーバーシフティング
            else if (this.useOverShifting)
            {
                this.number = upType.Agility;
                grpParameter_Paint();
            }
        }

        public void buttonIntelligence_Click()
        {
            if (GroundOne.UpPoint <= 0) { return; } // add unity

            // 通常レベルアップ＋１のロジック
            if (GroundOne.LevelUp)
            {
                if (GroundOne.MC != null && GroundOne.MC.PlayerStatusColor == this.cam.backgroundColor)
                {
                    GroundOne.MC.Intelligence++;
                    intelligence.text = GroundOne.MC.Intelligence.ToString();
                    if (GroundOne.MC.AvailableMana)
                    {
                        this.mana.text = GroundOne.MC.CurrentMana.ToString() + " / " + GroundOne.MC.MaxMana.ToString();
                    }
                    RefreshPartyMembersBattleStatus(GroundOne.MC);
                }
                else if (GroundOne.SC != null && GroundOne.SC.PlayerStatusColor == this.cam.backgroundColor)
                {
                    GroundOne.SC.Intelligence++;
                    intelligence.text = GroundOne.SC.Intelligence.ToString();
                    if (GroundOne.SC.AvailableMana)
                    {
                        this.mana.text = GroundOne.SC.CurrentMana.ToString() + " / " + GroundOne.SC.MaxMana.ToString();
                    }
                    RefreshPartyMembersBattleStatus(GroundOne.SC);
                }
                else if (GroundOne.TC != null && GroundOne.TC.PlayerStatusColor == this.cam.backgroundColor)
                {
                    GroundOne.TC.Intelligence++;
                    intelligence.text = GroundOne.TC.Intelligence.ToString();
                    if (GroundOne.TC.AvailableMana)
                    {
                        this.mana.text = GroundOne.TC.CurrentMana.ToString() + " / " + GroundOne.TC.MaxMana.ToString();
                    }
                    RefreshPartyMembersBattleStatus(GroundOne.TC);
                }
                CheckUpPoint();
            }
            // オーバーシフティング
            else if (this.useOverShifting)
            {
                this.number = upType.Intelligence;
                grpParameter_Paint();
            }
        }
        
        public void buttonStamina_Click()
        {
            if (GroundOne.UpPoint <= 0) { return; } // add unity

            // 通常レベルアップ＋１のロジック
            if (GroundOne.LevelUp)
            {
                if (GroundOne.MC != null && GroundOne.MC.PlayerStatusColor == this.cam.backgroundColor)
                {
                    GroundOne.MC.Stamina++;
                    stamina.text = GroundOne.MC.Stamina.ToString();
                    this.life.text = GroundOne.MC.CurrentLife.ToString() + " / " + GroundOne.MC.MaxLife.ToString();
                    RefreshPartyMembersBattleStatus(GroundOne.MC);
                }
                else if (GroundOne.SC != null && GroundOne.SC.PlayerStatusColor == this.cam.backgroundColor)
                {
                    GroundOne.SC.Stamina++;
                    stamina.text = GroundOne.SC.Stamina.ToString();
                    this.life.text = GroundOne.SC.CurrentLife.ToString() + " / " + GroundOne.SC.MaxLife.ToString();
                    RefreshPartyMembersBattleStatus(GroundOne.SC);
                }
                else if (GroundOne.TC != null && GroundOne.TC.PlayerStatusColor == this.cam.backgroundColor)
                {
                    GroundOne.TC.Stamina++;
                    stamina.text = GroundOne.TC.Stamina.ToString();
                    this.life.text = GroundOne.TC.CurrentLife.ToString() + " / " + GroundOne.TC.MaxLife.ToString();
                    RefreshPartyMembersBattleStatus(GroundOne.TC);
                }
                RefreshPartyMembersLife();
                CheckUpPoint();
            }
            // オーバーシフティング
            else if (this.useOverShifting)
            {
                this.number = upType.Stamina;
                grpParameter_Paint();
            }
        }

        public void buttonMind_Click()
        {
            if (GroundOne.UpPoint <= 0) { return; } // add unity

            // 通常レベルアップ＋１のロジック
            if (GroundOne.LevelUp)
            {
                if (GroundOne.MC != null && GroundOne.MC.PlayerStatusColor == this.cam.backgroundColor)
                {
                    GroundOne.MC.Mind++;
                    mind.text = GroundOne.MC.Mind.ToString();
                    RefreshPartyMembersBattleStatus(GroundOne.MC);
                }
                else if (GroundOne.SC != null && GroundOne.SC.PlayerStatusColor == this.cam.backgroundColor)
                {
                    GroundOne.SC.Mind++;
                    mind.text = GroundOne.SC.Mind.ToString();
                    RefreshPartyMembersBattleStatus(GroundOne.SC);
                }
                else if (GroundOne.TC != null && GroundOne.TC.PlayerStatusColor == this.cam.backgroundColor)
                {
                    GroundOne.TC.Mind++;
                    mind.text = GroundOne.TC.Mind.ToString();
                    RefreshPartyMembersBattleStatus(GroundOne.TC);
                }
                CheckUpPoint();
            }
            // オーバーシフティング
            else if (this.useOverShifting)
            {
                this.number = upType.Mind;
                grpParameter_Paint();
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
                RefreshPartyMembersLife();
                lblRemain.text = "残り " + remain.ToString();
            }
        }

        public void plus1_Click(Text sender)
        {
            MainCharacter player = GetCurrentPlayer();
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
                // todo (0point以下になった時、ボタン押下で継続してパラメタが割り振られないようにする事）
                //buttonStrength.Enabled = false;
                //buttonAgility.Enabled = false;
                //buttonIntelligence.Enabled = false;
                //buttonStamina.Enabled = false;
                //buttonMind.Enabled = false;
                grpParameter_Paint();
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
            MainCharacter player = GetCurrentPlayer();
            ResetParameter(ref player, ref GroundOne.UpPoint, ref this.addStrSC, ref this.addAglSC, ref this.addIntSC, ref this.addStmSC, ref this.addMndSC);
            SettingCharacterData(player);
            RefreshPartyMembersBattleStatus(player);
            RefreshPartyMembersLife();
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
