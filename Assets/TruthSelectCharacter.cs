using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DungeonPlayer;
using System.Collections.Generic;

public class TruthSelectCharacter : MotherForm
{
    public Text mainMessage;
    public Button btnClose;
    public Text txtClose;
    public Text txtName;
    public Text txtLevel;
    public Text txtExperience;
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
    public Text subWeapon;
    public Text armor;
    public Text accessory;
    public Text accessory2;
    public GameObject back_weapon;
    public GameObject back_subWeapon;
    public GameObject back_armor;
    public GameObject back_accessory;
    public GameObject back_accessory2;

    public Button btnPlayer1;
    public Button btnPlayer2;
    public Button btnPlayer3;
    public Button btnPlayer4;
    public GameObject back_selected1;
    public Text selected1;
    public GameObject back_selected2;
    public Text selected2;
    public Button choice;
    public Button btnFix;

    bool usingOvershifting = false;
    bool usingOvershiftingFirstSleep = false;
    bool usingOvershiftingSecondSleep = false;
    bool usingToomiBlueSuisyou = false;

    public List<MainCharacter> playerList = new List<MainCharacter>();
    private MainCharacter currentPlayer = null;
    public const int MAX_ADD = 2;
    private int remainSC = 0;
    private int addStrSC = 0;
    private int addAglSC = 0;
    private int addIntSC = 0;
    private int addStmSC = 0;
    private int addMndSC = 0;
    private int remainTC = 0;
    private int addStrTC = 0;
    private int addAglTC = 0;
    private int addIntTC = 0;
    private int addStmTC = 0;
    private int addMndTC = 0;
    private bool choiceSC = false;
    private bool choiceTC = false;

    private enum CoreType
    {
        Strength,
        Agility,
        Intelligence,
        Stamina,
        Mind
    }

    public override void Start()
    {
        base.Start();

        this.Background.GetComponent<Image>().color = GroundOne.CurrentStatusView;
        MainCharacter player = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);
        SettingCharacterData(player);
        RefreshPartyMembersBattleStatus(player);

        btnMana.SetActive(GroundOne.MC.AvailableMana);
        mana.gameObject.SetActive(GroundOne.MC.AvailableMana);

        btnSkill.SetActive(GroundOne.MC.AvailableSkill);
        skill.gameObject.SetActive(GroundOne.MC.AvailableSkill);

        mainMessage.text = "";


    }

    public override void Update()
    {
        base.Update();
    }

    public override void SceneBack()
    {
        base.SceneBack();

        MainCharacter player = Method.GetCurrentPlayer(this.Background.GetComponent<Image>().color);
        SettingCharacterData(player);
        RefreshPartyMembersBattleStatus(player);
    }

    public void btnReset_Click()
    {
        this.playerList.Clear();
        selected1.text = "";
        selected2.text = "";
        back_selected1.GetComponent<Image>().color = UnityColor.Cornsilk;
        back_selected2.GetComponent<Image>().color = UnityColor.Cornsilk;
        btnPlayer1.enabled = true;
        btnPlayer2.enabled = true;
        btnPlayer3.enabled = true;
        btnPlayer4.enabled = true;
        choice.enabled = true;
        btnFix.enabled = false;
        this.choiceSC = false;
        this.choiceTC = false;
        UpdateBtnUpReset();
    }

    public void choice_Click()
    {
        SelectOrAdd(this.currentPlayer);
    }

    public void btnFix_Click()
    {
        SceneDimension.Back(this);
    }

    private void CheckMaxAdd()
    {
        if (this.playerList.Count >= MAX_ADD)
        {
            btnPlayer1.enabled = false;
            btnPlayer2.enabled = false;
            btnPlayer3.enabled = false;
            btnPlayer4.enabled = false;
            choice.enabled = false;
            btnFix.enabled = true;
        }
    }

    private void PlayerAdd(string name)
    {
        Text current = selected1;
        GameObject back_current = back_selected1;
        this.playerList.Add(this.currentPlayer);
        if (this.playerList.Count == 1)
        {
            current = selected1;
            back_current = back_selected1;
        }
        else if (this.playerList.Count == 2)
        {
            current = selected2;
            back_current = back_selected2;
        }
        current.text = this.currentPlayer.FullName;
        if (currentPlayer.FirstName == Database.RANA_AMILIA)
        {
            back_current.GetComponent<Image>().color = currentPlayer.PlayerStatusColor;
            this.btnUpReset.gameObject.SetActive(false);
        }
        else if (currentPlayer.FirstName == Database.OL_LANDIS)
        {
            back_current.GetComponent<Image>().color = currentPlayer.PlayerStatusColor;
            this.btnUpReset.gameObject.SetActive(false);
        }
        else if (currentPlayer.FirstName == Database.VERZE_ARTIE)
        {
            back_current.GetComponent<Image>().color = currentPlayer.PlayerStatusColor;
        }
        else if (currentPlayer.FirstName == Database.SINIKIA_KAHLHANZ)
        {
            back_current.GetComponent<Image>().color = currentPlayer.PlayerStatusColor;
        }
        CheckMaxAdd();
    }

    private void UpdateBtnUpReset()
    {
        if (currentPlayer.Equals(GroundOne.SC))
        {
            btnUpReset.gameObject.SetActive(!this.choiceSC);
        }
        else if (currentPlayer.Equals(GroundOne.TC))
        {
            btnUpReset.gameObject.SetActive(!this.choiceTC);
        }
        else
        {
            btnUpReset.gameObject.SetActive(false);
        }
    }

    private void SelectOrAdd(MainCharacter player)
    {
        if ((this.currentPlayer == null) ||
            (this.currentPlayer.FullName != player.FullName))
        {
            this.currentPlayer = player;
            this.Background.GetComponent<Image>().color = currentPlayer.PlayerStatusColor;
            SettingCharacterData(player);
            RefreshPartyMembersBattleStatus(player);
        }
        else
        {
            if (this.playerList.Contains(player))
            {
                // 何もしない
            }
            else
            {
                if (player.Equals(GroundOne.SC))
                {
                    if (this.remainSC > 0)
                    {
                        mainMessage.text = "アイン：パラメタを先に割り振ろう";// this.sc.FullName + "のパラメタ割り振りを完了してください。";
                    }
                    else
                    {
                        this.choiceSC = true;
                        PlayerAdd(player.FullName);
                    }
                }
                else if (player.Equals(GroundOne.TC))
                {
                    if (this.remainTC > 0)
                    {
                        mainMessage.text = "アイン：パラメタを先に割り振ろう"; // this.tc.FullName + "のパラメタ割り振りを完了してください。";
                    }
                    else
                    {
                        this.choiceTC = true;
                        PlayerAdd(player.FullName);
                    }
                }
                else
                {
                    PlayerAdd(player.FullName);
                }
            }
        }
        UpdateBtnUpReset();
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

        txtTotal.text = "= " + totalValue.ToString();
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


        SettingCoreParameter(CoreType.Strength, chara.Strength, chara.BuffStrength_Accessory, chara.BuffStrength_Food, this.strength, this.addStrength, this.addStrengthFood, this.totalStrength);
        SettingCoreParameter(CoreType.Intelligence, chara.Intelligence, chara.BuffIntelligence_Accessory, chara.BuffIntelligence_Food, this.intelligence, this.addIntelligence, this.addIntelligenceFood, this.totalIntelligence);
        SettingCoreParameter(CoreType.Agility, chara.Agility, chara.BuffAgility_Accessory, chara.BuffAgility_Food, this.agility, this.addAgility, this.addAgilityFood, this.totalAgility);
        SettingCoreParameter(CoreType.Stamina, chara.Stamina, chara.BuffStamina_Accessory, chara.BuffStamina_Food, this.stamina, this.addStamina, this.addStaminaFood, this.totalStamina);
        SettingCoreParameter(CoreType.Mind, chara.Mind, chara.BuffMind_Accessory, chara.BuffMind_Food, this.mind, this.addMind, this.addMindFood, this.totalMind);

        plus1.gameObject.SetActive(false);
        plus10.gameObject.SetActive(false);
        plus100.gameObject.SetActive(false);
        plus1000.gameObject.SetActive(false);
        btnUpReset.gameObject.SetActive(false);
        lblRemain.gameObject.SetActive(false);

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
    }

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
            txtPhysicalAttack.text += "\r\n" + temp1.ToString("F2");
            txtPhysicalAttack.text += " - " + temp2.ToString("F2");
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

    public void FirstChara_Click()
    {
        this.Background.GetComponent<Image>().color = GroundOne.MC.PlayerStatusColor;
        GroundOne.CurrentStatusView = GroundOne.MC.PlayerStatusColor;
        SettingCharacterData(GroundOne.MC);
        RefreshPartyMembersBattleStatus(GroundOne.MC);
    }


    public void SecondChara_Click()
    {
        this.Background.GetComponent<Image>().color = GroundOne.SC.PlayerStatusColor;
        GroundOne.CurrentStatusView = GroundOne.SC.PlayerStatusColor;
        SettingCharacterData(GroundOne.SC);
        RefreshPartyMembersBattleStatus(GroundOne.SC);
    }

    public void ThirdChara_Click()
    {
        this.Background.GetComponent<Image>().color = GroundOne.TC.PlayerStatusColor;
        GroundOne.CurrentStatusView = GroundOne.TC.PlayerStatusColor;
        SettingCharacterData(GroundOne.TC);
        RefreshPartyMembersBattleStatus(GroundOne.TC);
    }

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
            if (GroundOne.MC != null && GroundOne.MC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.MC.Strength++;
                strength.text = GroundOne.MC.Strength.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.MC);
            }
            else if (GroundOne.SC != null && GroundOne.SC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.SC.Strength++;
                strength.text = GroundOne.SC.Strength.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.SC);
            }
            else if (GroundOne.TC != null && GroundOne.TC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.TC.Strength++;
                strength.text = GroundOne.TC.Strength.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.TC);
            }
            CheckUpPoint();
        }
    }


    public void buttonAgility_Click()
    {
        if (GroundOne.UpPoint <= 0) { return; } // add unity

        // 通常レベルアップ＋１のロジック
        if (GroundOne.LevelUp)
        {
            if (GroundOne.MC != null && GroundOne.MC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.MC.Agility++;
                agility.text = GroundOne.MC.Agility.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.MC);
            }
            else if (GroundOne.SC != null && GroundOne.SC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.SC.Agility++;
                agility.text = GroundOne.SC.Agility.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.SC);
            }
            else if (GroundOne.TC != null && GroundOne.TC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.TC.Agility++;
                agility.text = GroundOne.TC.Agility.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.TC);
            }
            CheckUpPoint();
        }
    }

    public void buttonIntelligence_Click()
    {
        if (GroundOne.UpPoint <= 0) { return; } // add unity

        // 通常レベルアップ＋１のロジック
        if (GroundOne.LevelUp)
        {
            if (GroundOne.MC != null && GroundOne.MC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.MC.Intelligence++;
                intelligence.text = GroundOne.MC.Intelligence.ToString();
                if (GroundOne.MC.AvailableMana)
                {
                    this.mana.text = GroundOne.MC.CurrentMana.ToString() + " / " + GroundOne.MC.MaxMana.ToString();
                }
                RefreshPartyMembersBattleStatus(GroundOne.MC);
            }
            else if (GroundOne.SC != null && GroundOne.SC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.SC.Intelligence++;
                intelligence.text = GroundOne.SC.Intelligence.ToString();
                if (GroundOne.SC.AvailableMana)
                {
                    this.mana.text = GroundOne.SC.CurrentMana.ToString() + " / " + GroundOne.SC.MaxMana.ToString();
                }
                RefreshPartyMembersBattleStatus(GroundOne.SC);
            }
            else if (GroundOne.TC != null && GroundOne.TC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
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
    }

    public void buttonStamina_Click()
    {
        if (GroundOne.UpPoint <= 0) { return; } // add unity

        // 通常レベルアップ＋１のロジック
        if (GroundOne.LevelUp)
        {
            if (GroundOne.MC != null && GroundOne.MC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.MC.Stamina++;
                stamina.text = GroundOne.MC.Stamina.ToString();
                this.life.text = GroundOne.MC.CurrentLife.ToString() + " / " + GroundOne.MC.MaxLife.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.MC);
            }
            else if (GroundOne.SC != null && GroundOne.SC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.SC.Stamina++;
                stamina.text = GroundOne.SC.Stamina.ToString();
                this.life.text = GroundOne.SC.CurrentLife.ToString() + " / " + GroundOne.SC.MaxLife.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.SC);
            }
            else if (GroundOne.TC != null && GroundOne.TC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.TC.Stamina++;
                stamina.text = GroundOne.TC.Stamina.ToString();
                this.life.text = GroundOne.TC.CurrentLife.ToString() + " / " + GroundOne.TC.MaxLife.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.TC);
            }
            CheckUpPoint();
        }
    }

    public void buttonMind_Click()
    {
        if (GroundOne.UpPoint <= 0) { return; } // add unity

        // 通常レベルアップ＋１のロジック
        if (GroundOne.LevelUp)
        {
            if (GroundOne.MC != null && GroundOne.MC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.MC.Mind++;
                mind.text = GroundOne.MC.Mind.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.MC);
            }
            else if (GroundOne.SC != null && GroundOne.SC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.SC.Mind++;
                mind.text = GroundOne.SC.Mind.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.SC);
            }
            else if (GroundOne.TC != null && GroundOne.TC.PlayerStatusColor == this.Background.GetComponent<Image>().color)
            {
                GroundOne.TC.Mind++;
                mind.text = GroundOne.TC.Mind.ToString();
                RefreshPartyMembersBattleStatus(GroundOne.TC);
            }
            CheckUpPoint();
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
    }

}