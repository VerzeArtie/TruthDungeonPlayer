using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

namespace DungeonPlayer
{
    public class TruthBattleSetting : MotherForm
    {
        public GameObject btnMixSpell;
        public GameObject btnMixSkill;
        public GameObject groupTypeBasic;
        public GameObject groupTypeSpell;
        public GameObject groupTypeSkill;
        public GameObject groupTypeMixSpell;
        public GameObject groupTypeMixSkill;
        public GameObject groupCommandBasic;
        public GameObject groupCommandSpell;
        public GameObject groupCommandSkill;
        public GameObject groupCommandMixSpell;
        public GameObject groupCommandMixSkill;
        public GameObject groupListBasic;
        public GameObject groupListSpell;
        public GameObject groupListSkill;
        public GameObject groupListMixSpell;
        public GameObject groupListMixSkill;
        public GameObject groupStandardCommand;
        public GameObject groupItemCommand;
        public GameObject groupArchitypeCommand;
        public GameObject groupLightSpell;
        public GameObject groupShadowSpell;
        public GameObject groupFireSpell;
        public GameObject groupIceSpell;
        public GameObject groupForceSpell;
        public GameObject groupWillSpell;
        public GameObject groupActiveSkill;
        public GameObject groupPassiveSkill;
        public GameObject groupSoftSkill;
        public GameObject groupHardSkill;
        public GameObject groupTruthSkill;
        public GameObject groupVoidSkill;
        public GameObject groupLightShadow;
        public GameObject groupLightFire;
        public GameObject groupLightIce;
        public GameObject groupLightForce;
        public GameObject groupLightWill;
        public GameObject groupShadowFire;
        public GameObject groupShadowIce;
        public GameObject groupShadowForce;
        public GameObject groupShadowWill;
        public GameObject groupFireIce;
        public GameObject groupFireForce;
        public GameObject groupFireWill;
        public GameObject groupIceForce;
        public GameObject groupIceWill;
        public GameObject groupForceWill;
        public GameObject groupActivePassive;
        public GameObject groupActiveSoft;
        public GameObject groupActiveHard;
        public GameObject groupActiveTruth;
        public GameObject groupActiveVoid;
        public GameObject groupPassiveSoft;
        public GameObject groupPassiveHard;
        public GameObject groupPassiveTruth;
        public GameObject groupPassiveVoid;
        public GameObject groupSoftHard;
        public GameObject groupSoftTruth;
        public GameObject groupSoftVoid;
        public GameObject groupHardTruth;
        public GameObject groupHardVoid;
        public GameObject groupTruthVoid;
        public GameObject groupArcheType;
        public GameObject btnCharacterGroup;
        public Button btnFirstChara;
        public Button btnSecondChara;
        public Button btnThirdChara;
        public Text commandName;
        public Text commandNameEn;
        public Text commandCost;
        public Text commandTarget;
        public Text commandTiming;
        public Text description;
        public Image[] pbAction;
        public Image[] pbSorcery;
        public Image[] pbCurrentAction;
        public Image[] pbCurrentActionSorcery;
        public Image moveActionBox;
        public Image moveActionBoxSorcery;
        public GameObject panelBasic;
        public Text txtBasic;
        public GameObject panelSpell;
        public Text txtSpell;
        public GameObject panelSkill;
        public Text txtSkill;
        public GameObject panelClass;
        public Text txtClass;
        public Button btnBasic;
        public Button btnSpell;
        public Button btnExit;
        public Button btnClass;
        public Button command1;
        public Button command2;
        public Button command3;
        public Button command4;
        public Button command5;
        //public Panel commandList;
        public Button back;
        public Text lblBasic;
        public Text lblLight;
        public Text lblShadow;
        public Text lblFire;
        public Text lblIce;
        public Text lblForce;
        public Text lblWill;
        public Text lblActive;
        public Text lblPassive;
        public Text lblSoft;
        public Text lblHard;
        public Text lblTruth;
        public Text lblVoid;
        public Text lblMixSpell;
        public Text lblMixSkill;            
        public Text lblComplete;

        MainCharacter currentPlayer;

        private Vector3 screenPoint;
        private Vector3 offset;

        const int CURRENT_ACTION_NUM = 9;
        const int BASIC_ACTION_NUM = 8; // 基本行動
        const int MIX_ACTION_NUM = 45; // [警告] 暫定、本来Databaseに記載するべき
        const int MIX_ACTION_NUM_2 = 30; // [警告]暫定、本来Databaseに記載するべき
        const int ARCHETYPE_NUM = 1; // アーキタイプ

        // Use this for initialization
        public override void Start()
        {
            base.Start();

            if (GroundOne.Language == GroundOne.GameLanguage.Japanese)
            {
                lblBasic.text = Database.GUI_B_SETTING_BASIC;
                lblLight.text = Database.GUI_B_SETTING_LIGHT;
                lblShadow.text = Database.GUI_B_SETTING_SHADOW;
                lblFire.text = Database.GUI_B_SETTING_FIRE;
                lblIce.text = Database.GUI_B_SETTING_ICE;
                lblForce.text = Database.GUI_B_SETTING_FORCE;
                lblWill.text = Database.GUI_B_SETTING_WILL;
                lblActive.text = Database.GUI_B_SETTING_ACTIVE;
                lblPassive.text = Database.GUI_B_SETTING_PASSIVE;
                lblSoft.text = Database.GUI_B_SETTING_SOFT;
                lblHard.text = Database.GUI_B_SETTING_HARD;
                lblTruth.text = Database.GUI_B_SETTING_TRUTH;
                lblVoid.text = Database.GUI_B_SETTING_VOID;
                lblMixSpell.text = Database.GUI_B_SETTING_MIXSPELL;
                lblMixSkill.text = Database.GUI_B_SETTING_MIXSKILL;
                lblComplete.text = Database.GUI_B_SETTING_COMPLETE;
            }

            this.currentPlayer = GroundOne.MC;
            this.Background.GetComponent<Image>().color = GroundOne.MC.PlayerStatusColor;

            if (GroundOne.MC != null) { btnFirstChara.GetComponent<Image>().color = GroundOne.MC.PlayerColor; }
            if (GroundOne.SC != null) { btnSecondChara.GetComponent<Image>().color = GroundOne.SC.PlayerColor; }
            if (GroundOne.TC != null) { btnThirdChara.GetComponent<Image>().color = GroundOne.TC.PlayerColor; }

            this.btnMixSpell.SetActive(GroundOne.WE.AvailableMixSpellSkill);
            this.btnMixSkill.SetActive(GroundOne.WE.AvailableMixSpellSkill);

            this.groupArcheType.SetActive(GroundOne.WE.AvailableArchetypeCommand);

            if (GroundOne.DuelMode)
            {
                btnCharacterGroup.SetActive(false);
            }
            else
            {
                if (GroundOne.WE.AvailableSecondCharacter)
                {
                    btnCharacterGroup.SetActive(true);
                    btnFirstChara.gameObject.SetActive(true);
                    btnSecondChara.gameObject.SetActive(true);
                    if (GroundOne.WE.AvailableThirdCharacter)
                    {
                        btnThirdChara.gameObject.SetActive(true);
                    }
                    else
                    {
                        Method.AddEmptyObj(ref btnCharacterGroup, 1);
                    }
                }
                else
                {
                    btnCharacterGroup.SetActive(false);
                }
            }

            SetupAllIcon();

            TapBasicList(0);
        }
        
        public void TruthBattleSetting_MouseMove(Button sender)
        {
            moveActionBox.gameObject.SetActive(true);
            moveActionBox.gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        }

        public void TruthBattleSetting_MouseUp(Button sender)
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_BATTLESET_CHOICEEND, sender.name, String.Empty);
            moveActionBox.gameObject.SetActive(false);

            float positionX = Input.mousePosition.x;
            float positionY = Input.mousePosition.y;

            for (int ii = 0; ii < CURRENT_ACTION_NUM; ii++)
            {
                float width = (pbCurrentAction[ii].GetComponent<RectTransform>()).rect.width;
                float height = (pbCurrentAction[ii].GetComponent<RectTransform>()).rect.height;
                if (pbCurrentAction[ii].gameObject.transform.position.x - width/2.0f <= positionX && positionX <= pbCurrentAction[ii].gameObject.transform.position.x + (pbCurrentAction[ii].GetComponent<RectTransform>()).rect.width/2.0f &&
                    pbCurrentAction[ii].gameObject.transform.position.y - height/2.0f <= positionY && positionY <= pbCurrentAction[ii].gameObject.transform.position.y + (pbCurrentAction[ii].GetComponent<RectTransform>()).rect.height/2.0f)
                {
                    Method.SetupActionButton(pbCurrentAction[ii].gameObject, pbCurrentActionSorcery[ii], sender.name);
                    this.currentPlayer.BattleActionCommandList[ii] = sender.name;
                    break;
                }
            }
        }

        public void TruthBattleSetting_MouseDown(Button sender)
        {
            ViewCommandContent(sender);
            //GroundOne.SQL.UpdateOwner(Database.LOG_BATTLESET_CHOICESTART, sender.name, String.Empty);
            moveActionBox.gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            Method.SetupActionButton(moveActionBox.gameObject, moveActionBoxSorcery, sender.name);
            moveActionBox.gameObject.SetActive(true);
            moveActionBox.gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        }

        void OnMouseDown()
        {
            this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }

        void OnMouseDrag()
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
            transform.position = currentPosition;
        }

        void OnMouseUp()
        {
        }

        public void tapExit()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_BATTLESET_CLOSE, String.Empty, String.Empty);
            SceneDimension.Back(this);
        }

        public void ViewCommandContent(Button sender)
        {
            string command = sender.name;
            this.commandName.text = TruthActionCommand.ConvertToJapanese(command);
            this.commandNameEn.text = sender.name;
            this.commandCost.text = "消費コスト : " + TruthActionCommand.GetCost(sender.name).ToString();

            switch (TruthActionCommand.GetTargetType(command))
            {
                case TruthActionCommand.TargetType.AllMember:
                    commandTarget.text = "対象：場全体";
                    break;
                case TruthActionCommand.TargetType.Ally:
                    commandTarget.text = "対象：味方単体";
                    break;
                case TruthActionCommand.TargetType.AllyGroup:
                    commandTarget.text = "対象：味方全体";
                    break;
                case TruthActionCommand.TargetType.AllyOrEnemy:
                    commandTarget.text = "対象：敵単体 / 味方単体";
                    break;
                case TruthActionCommand.TargetType.Enemy:
                    commandTarget.text = "対象：敵単体";
                    break;
                case TruthActionCommand.TargetType.EnemyGroup:
                    commandTarget.text = "対象：敵全体";
                    break;
                case TruthActionCommand.TargetType.InstantTarget:
                    commandTarget.text = "対象：インスタント対象";
                    break;
                case TruthActionCommand.TargetType.NoTarget:
                    commandTarget.text = "対象：なし";
                    break;
                case TruthActionCommand.TargetType.Own:
                    commandTarget.text = "対象：自分";
                    break;
            }

            switch (TruthActionCommand.GetTimingType(sender.name))
            {
                case TruthActionCommand.TimingType.Sorcery:
                    commandTiming.text = "ソーサリー";
                    break;
                case TruthActionCommand.TimingType.Normal:
                    commandTiming.text = "ノーマル";
                    break;
                case TruthActionCommand.TimingType.Instant:
                    commandTiming.text = "インスタント";
                    break;
            }
            this.description.text = TruthActionCommand.GetDescription(command);
        }

        private void SetupAllIcon()
        {
            string[] ssName = TruthActionCommand.GetActionList(currentPlayer);
            bool[] ssAvailable = TruthActionCommand.GetAvailableActionList(currentPlayer);
            for (int ii = 0; ii < Database.TOTAL_COMMAND_NUM; ii++)
            {
                try { 
                    if (ssAvailable[ii]) {
                        pbAction[ii].gameObject.SetActive(true);
                        Method.SetupActionButton(pbAction[ii].gameObject, pbSorcery[ii], ssName[ii]);
                    }
                    else
                    {
                        pbAction[ii].gameObject.SetActive(false);
                    }
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.ToString());
                }
            }

            for (int ii = 0; ii < currentPlayer.BattleActionCommandList.Length; ii++)
            {
                Method.SetupActionButton(pbCurrentAction[ii].gameObject, pbCurrentActionSorcery[ii], currentPlayer.BattleActionCommandList[ii]);
            }
        }
        
        public void FirstChara_Click()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_BATTLESET_PLAYERFIRST, String.Empty, String.Empty);
            this.currentPlayer = GroundOne.MC;
            this.Background.GetComponent<Image>().color = GroundOne.MC.PlayerStatusColor;
            SetupAllIcon();
        }

        public void SecondChara_Click()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_BATTLESET_PLAYERSECOND, String.Empty, String.Empty);
            this.currentPlayer = GroundOne.SC;
            this.Background.GetComponent<Image>().color = GroundOne.SC.PlayerStatusColor;
            SetupAllIcon();
        }

        public void ThirdChara_Click()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_BATTLESET_PLAYERTHIRD, String.Empty, String.Empty);
            this.currentPlayer = GroundOne.TC;
            this.Background.GetComponent<Image>().color = GroundOne.TC.PlayerStatusColor;
            SetupAllIcon();
        }

        public void TapCommandType(int num)
        {
            int COMMAND_TYPE = 5;
            List<bool> list = new List<bool>();
            for (int ii = 0; ii < COMMAND_TYPE; ii++)
            {
                if (ii == num) { list.Add(true); }
                else { list.Add(false); }
            }
            UpdateGroupType(list[0], list[1], list[2], list[3], list[4]);

            if (num == 0) { TapBasicList(0); }
            else if (num == 1) { TapSpellList(0); }
            else if (num == 2) { TapSkillList(0); }
            else if (num == 3) { TapMixSpellList(0); }
            else if (num == 4) { TapMixSkillList(0); }
        }
        private void UpdateGroupType(bool g1, bool g2, bool g3, bool g4, bool g5)
        {
            groupTypeBasic.SetActive(g1);
            groupTypeSpell.SetActive(g2);
            groupTypeSkill.SetActive(g3);
            groupTypeMixSpell.SetActive(g4);
            groupTypeMixSkill.SetActive(g5);

            groupListBasic.SetActive(g1);
            groupListSpell.SetActive(g2);
            groupListSkill.SetActive(g3);
            groupListMixSpell.SetActive(g4);
            groupListMixSkill.SetActive(g5);
        }

        public void TapBasicList(int num)
        {
            int BASIC_COMMAND_NUM = 3;
            List<bool> list = new List<bool>();
            for (int ii = 0; ii < BASIC_COMMAND_NUM; ii++)
            {
                if (ii == num) { list.Add(true); }
                else { list.Add(false); }
            }
            UpdateGroupBasic(list[0], list[1], list[2]);
        }

        private void UpdateGroupBasic(bool g1, bool g2, bool g3)
        {
            groupStandardCommand.SetActive(g1);
            groupItemCommand.SetActive(g2);
            groupArchitypeCommand.SetActive(g3);
        }

        public void TapSpellList(int num)
        {
            List<bool> list = new List<bool>();
            for (int ii = 0; ii < Database.SPELL_MAX_NUM; ii++)
            {
                if (ii == num) { list.Add(true); }
                else { list.Add(false); }
            }
            UpdateGroupSpell(list[0], list[1], list[2], list[3], list[4], list[5]);
        }

        private void UpdateGroupSpell(bool g1, bool g2, bool g3, bool g4, bool g5, bool g6)
        {
            groupLightSpell.SetActive(g1);
            groupShadowSpell.SetActive(g2);
            groupFireSpell.SetActive(g3);
            groupIceSpell.SetActive(g4);
            groupForceSpell.SetActive(g5);
            groupWillSpell.SetActive(g6);
        }

        public void TapSkillList(int num)
        {
            List<bool> list = new List<bool>();
            for (int ii = 0; ii < Database.SKILL_TYPE_NUM; ii++)
            {
                if (ii == num) { list.Add(true); }
                else { list.Add(false); }
            }
            UpdateGroupSkill(list[0], list[1], list[2], list[3], list[4], list[5]);

        }
        private void UpdateGroupSkill(bool g1, bool g2, bool g3, bool g4, bool g5, bool g6)
        {
            groupActiveSkill.SetActive(g1);
            groupPassiveSkill.SetActive(g2);
            groupSoftSkill.SetActive(g3);
            groupHardSkill.SetActive(g4);
            groupTruthSkill.SetActive(g5);
            groupVoidSkill.SetActive(g6);
        }

        public void TapMixSpellList(int num)
        {
            int MIX_SPELL_NUM = 15;
            List<bool> list = new List<bool>();
            for (int ii = 0; ii < MIX_SPELL_NUM; ii++)
            {
                if (ii == num) { list.Add(true); }
                else { list.Add(false); }
            }
            UpdateGroupMixSpell(list[0], list[1], list[2], list[3], list[4], list[5], list[6], list[7], list[8], list[9], list[10], list[11], list[12], list[13], list[14]);
        }
        private void UpdateGroupMixSpell(bool g1, bool g2, bool g3, bool g4, bool g5, bool g6, bool g7, bool g8, bool g9, bool g10, bool g11, bool g12, bool g13, bool g14, bool g15)
        {
            groupLightShadow.SetActive(g1);
            groupLightFire.SetActive(g2);
            groupLightIce.SetActive(g3);
            groupLightForce.SetActive(g4);
            groupLightWill.SetActive(g5);
            groupShadowFire.SetActive(g6);
            groupShadowIce.SetActive(g7);
            groupShadowForce.SetActive(g8);
            groupShadowWill.SetActive(g9);
            groupFireIce.SetActive(g10);
            groupFireForce.SetActive(g11);
            groupFireWill.SetActive(g12);
            groupIceForce.SetActive(g13);
            groupIceWill.SetActive(g14);
            groupForceWill.SetActive(g15);
        }

        public void TapMixSkillList(int num)
        {
            int MIX_SKILL_NUM = 15;
            List<bool> list = new List<bool>();
            for (int ii = 0; ii < MIX_SKILL_NUM; ii++)
            {
                if (ii == num) { list.Add(true); }
                else { list.Add(false); }
            }
            UpdateGroupMixSkill(list[0], list[1], list[2], list[3], list[4], list[5], list[6], list[7], list[8], list[9], list[10], list[11], list[12], list[13], list[14]);
        }

        private void UpdateGroupMixSkill(bool g1, bool g2, bool g3, bool g4, bool g5, bool g6, bool g7, bool g8, bool g9, bool g10, bool g11, bool g12, bool g13, bool g14, bool g15)
        {
            groupActivePassive.SetActive(g1);
            groupActiveSoft.SetActive(g2);
            groupActiveHard.SetActive(g3);
            groupActiveTruth.SetActive(g4);
            groupActiveVoid.SetActive(g5);
            groupPassiveSoft.SetActive(g6);
            groupPassiveHard.SetActive(g7);
            groupPassiveTruth.SetActive(g8);
            groupPassiveVoid.SetActive(g9);
            groupSoftHard.SetActive(g10);
            groupSoftTruth.SetActive(g11);
            groupSoftVoid.SetActive(g12);
            groupHardTruth.SetActive(g13);
            groupHardVoid.SetActive(g14);
            groupTruthVoid.SetActive(g15);
        }
    }
}
