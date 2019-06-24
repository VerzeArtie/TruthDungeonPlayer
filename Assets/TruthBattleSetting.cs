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
        public GameObject nodeActionCommand;
        public GameObject group_MC;
        public GameObject group_SC;
        public GameObject group_TC;
        public GameObject group_MC_Basic;
        public GameObject group_SC_Basic;
        public GameObject group_TC_Basic;
        public GameObject contentActionCommand_MC;
        public GameObject contentBasicCommand_MC;
        public GameObject contentActionCommand_SC;
        public GameObject contentBasicCommand_SC;
        public GameObject contentActionCommand_TC;
        public GameObject contentBasicCommand_TC;
        public GameObject btnCharacterGroup;
        public Button btnFirstChara;
        public Text txtFirstChara;
        public Button btnSecondChara;
        public Text txtSecondChara;
        public Button btnThirdChara;
        public Text txtThirdChara;
        public Text commandName;
        public Text commandNameEn;
        public Text commandAttributeType;
        public Text commandCost;
        public Text commandTarget;
        public Text commandTiming;
        public Text description;
        public Image[] pbCurrentAction;
        public Image[] pbCurrentActionSorcery;
        public Image moveActionBox;
        public Image moveActionBoxSorcery;
        public Text lblBasicCommand;
        public Text lblAdvancedCommand;
        public Text lblComplete;

        MainCharacter currentPlayer;

        private Vector3 screenPoint;
        private Vector3 offset;

        const int CURRENT_ACTION_NUM = 9;

        private float moveAnimation = 0.0f;
        private float moveAnimationValue = 0.0f;

        // Use this for initialization
        public override void Start()
        {
            base.Start();

            if (GroundOne.Language == GroundOne.GameLanguage.Japanese)
            {
                lblBasicCommand.text = Database.GUI_B_SETTING_BASIC_COMMAND;
                lblAdvancedCommand.text = Database.GUI_B_SETTING_ADVANCED_COMMAND;
                lblComplete.text = Database.GUI_B_SETTING_COMPLETE;
            }

            this.currentPlayer = GroundOne.MC;
            this.Background.GetComponent<Image>().color = GroundOne.MC.PlayerStatusColor;

            if (GroundOne.MC != null)
            {
                btnFirstChara.GetComponent<Image>().color = GroundOne.MC.PlayerColor;
                txtFirstChara.text = GroundOne.MC.FirstName;
                List<string> commandList = TruthActionCommand.GetActionCommandList(GroundOne.MC);
                ConstructActionCommandView(GroundOne.MC, contentActionCommand_MC, commandList);
                List<string> basicList = TruthActionCommand.GetBasicCommandList(GroundOne.MC);
                ConstructActionCommandView(GroundOne.MC, contentBasicCommand_MC, basicList);
            }
            if (GroundOne.SC != null)
            {
                btnSecondChara.GetComponent<Image>().color = GroundOne.SC.PlayerColor;
                txtSecondChara.text = GroundOne.SC.FirstName;
                List<string> commandList = TruthActionCommand.GetActionCommandList(GroundOne.SC);
                ConstructActionCommandView(GroundOne.SC, contentActionCommand_SC, commandList);
                List<string> basicList = TruthActionCommand.GetBasicCommandList(GroundOne.SC);
                ConstructActionCommandView(GroundOne.SC, contentBasicCommand_SC, basicList);
            }
            if (GroundOne.TC != null)
            {
                btnThirdChara.GetComponent<Image>().color = GroundOne.TC.PlayerColor;
                txtThirdChara.text = GroundOne.TC.FirstName;
                List<string> commandList = TruthActionCommand.GetActionCommandList(GroundOne.TC);
                ConstructActionCommandView(GroundOne.TC, contentActionCommand_TC, commandList);
                List<string> basicList = TruthActionCommand.GetBasicCommandList(GroundOne.TC);
                ConstructActionCommandView(GroundOne.TC, contentBasicCommand_TC, basicList);
            }

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

            FirstChara_Click();
        }

        public override void Update()
        {
            base.Update();

            if (this.moveAnimation > 0.0f)
            {
                moveAnimation -= 1.0f;
                moveAnimationValue *= 0.5f;
                RectTransform rect = GetCurrentContent().GetComponent<RectTransform>();
                rect.position = new Vector3(rect.position.x + moveAnimationValue, rect.position.y);
            }
        }

        private void ConstructActionCommandView(MainCharacter player, GameObject contentAC, List<string> commandList)
        {
            int offset = 50;
            int width = 320;
            for (int ii = 0; ii < commandList.Count; ii++)
            {
                GameObject obj = Instantiate(this.nodeActionCommand, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                obj.name = commandList[ii];
                obj.transform.SetParent(contentAC.transform);
                obj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                RectTransform rect = obj.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector3(offset + width * ii, 0);
                obj.SetActive(true);

                RectTransform contentRectAC = contentAC.GetComponent<RectTransform>();

                // 個数に応じて、コンテンツ長さを延長する。
                contentRectAC.sizeDelta = new Vector2(contentRectAC.sizeDelta.x + width, contentRectAC.sizeDelta.y);

                Image[] imgList = obj.GetComponentsInChildren<Image>();
                Image imgIsSorcery = null;
                for (int jj = 0; jj < imgList.Length; jj++)
                {
                    if (imgList[jj].name == "IsSorcery")
                    {
                        imgIsSorcery = imgList[jj];
                        Image img = obj.GetComponent<Image>();
                        Method.SetupActionButton(img.gameObject, imgIsSorcery, commandList[ii]);
                        break;
                    }
                }

                Text[] txtList = obj.GetComponentsInChildren<Text>();
                for (int jj = 0; jj < txtList.Length; jj++)
                {
                    if (txtList[jj].name == "txtName")
                    {
                        txtList[jj].text = commandList[ii];
                        break;
                    }
                }
            }

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

            Debug.Log("pbCurrentAction " + pbCurrentAction.Length.ToString());
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

        private void RefleshCurrentActionCommand(MainCharacter player)
        {
            for (int ii = 0; ii < player.BattleActionCommandList.Length; ii++)
            {
                Method.SetupActionButton(pbCurrentAction[ii].gameObject, pbCurrentActionSorcery[ii], currentPlayer.BattleActionCommandList[ii]);
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

        private GameObject GetCurrentContent()
        {
            GameObject content = null;
            if (currentPlayer.Equals(GroundOne.MC))
            {
                if (group_MC.activeInHierarchy)
                {
                    content = contentActionCommand_MC;
                }
                else
                {
                    content = contentBasicCommand_MC;
                }
            }
            else if (currentPlayer.Equals(GroundOne.SC))
            {
                if (group_SC.activeInHierarchy)
                {
                    content = contentActionCommand_SC;
                }
                else
                {
                    content = contentBasicCommand_SC;
                }
            }
            else if (currentPlayer.Equals(GroundOne.TC))
            {
                if (group_TC.activeInHierarchy)
                {
                    content = contentActionCommand_TC;
                }
                else
                {
                    content = contentBasicCommand_TC;
                }
            }
            else
            {
                if (group_MC.activeInHierarchy)
                {
                    content = contentActionCommand_MC;
                }
                else
                {
                    content = contentBasicCommand_MC;
                }
            }
            return content;
        }

        public void tapArrowLeft()
        {
            if (this.moveAnimation <= 0.0f)
            {
                this.moveAnimation = 20.0f;
                RectTransform rect = GetCurrentContent().GetComponent<RectTransform>();
                if (rect.position.x >= -1.0f)
                {
                    Debug.Log("left tap, but greater than -1.0f: " + rect.position.x.ToString());
                    this.moveAnimationValue = +10;
                }
                else
                {
                    this.moveAnimationValue = +Screen.width;
                }
            }
        }
        public void tapArrowRight()
        {
            if (this.moveAnimation <= 0.0f)
            {
                this.moveAnimation = 20.0f;
                List<string> list = TruthActionCommand.GetActionCommandList(currentPlayer);
                RectTransform rect = GetCurrentContent().GetComponent<RectTransform>();
                CanvasScaler scaler = GameObject.Find("Canvas").gameObject.GetComponent<CanvasScaler>();
                float diff = -rect.sizeDelta.x - rect.localPosition.x + (scaler.referenceResolution.x);
                if (diff > -12.0f) // 現実的な調整
                {
                    this.moveAnimationValue = -10;
                }
                else
                {
                    this.moveAnimationValue = -Screen.width;
                }
            }
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

            switch (TruthActionCommand.GetAttribute(command))
            {
                case TruthActionCommand.Attribute.Spell:
                    this.commandAttributeType.text = "魔法";
                    break;
                case TruthActionCommand.Attribute.Skill:
                    this.commandAttributeType.text = "スキル";
                    break;
                case TruthActionCommand.Attribute.Archetype:
                    this.commandAttributeType.text = "元核";
                    break;
                case TruthActionCommand.Attribute.None:
                case TruthActionCommand.Attribute.NormalAttack:
                default:
                    this.commandAttributeType.text = "通常コマンド";
                    break;
            }

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
        
        public void FirstChara_Click()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_BATTLESET_PLAYERFIRST, String.Empty, String.Empty);
            this.currentPlayer = GroundOne.MC;
            this.Background.GetComponent<Image>().color = GroundOne.MC.PlayerStatusColor;
            RefleshCurrentActionCommand(this.currentPlayer);
            group_MC.SetActive(true);
            group_SC.SetActive(false);
            group_TC.SetActive(false);
            group_MC_Basic.SetActive(false);
            group_SC_Basic.SetActive(false);
            group_TC_Basic.SetActive(false);
        }

        public void SecondChara_Click()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_BATTLESET_PLAYERSECOND, String.Empty, String.Empty);
            this.currentPlayer = GroundOne.SC;
            this.Background.GetComponent<Image>().color = GroundOne.SC.PlayerStatusColor;
            RefleshCurrentActionCommand(this.currentPlayer);
            group_MC.SetActive(false);
            group_SC.SetActive(true);
            group_TC.SetActive(false);
            group_MC_Basic.SetActive(false);
            group_SC_Basic.SetActive(false);
            group_TC_Basic.SetActive(false);
        }

        public void ThirdChara_Click()
        {
            //GroundOne.SQL.UpdateOwner(Database.LOG_BATTLESET_PLAYERTHIRD, String.Empty, String.Empty);
            this.currentPlayer = GroundOne.TC;
            this.Background.GetComponent<Image>().color = GroundOne.TC.PlayerStatusColor;
            RefleshCurrentActionCommand(this.currentPlayer);
            group_MC.SetActive(false);
            group_SC.SetActive(false);
            group_TC.SetActive(true);
            group_MC_Basic.SetActive(false);
            group_SC_Basic.SetActive(false);
            group_TC_Basic.SetActive(false);
        }

        public void TapBasicType()
        {
            if (group_MC.activeInHierarchy)
            {
                group_MC_Basic.SetActive(true);
                group_MC.SetActive(false);
                return;
            }
            if (group_SC.activeInHierarchy)
            {
                group_SC_Basic.SetActive(true);
                group_SC.SetActive(false);
                return;
            }
            if (group_TC.activeInHierarchy)
            {
                group_TC_Basic.SetActive(true);
                group_TC.SetActive(false);
                return;
            }
        }
        public void TapAdvancedType()
        {
            if (group_MC_Basic.activeInHierarchy)
            {
                group_MC.SetActive(true);
                group_MC_Basic.SetActive(false);
                return;
            }
            if (group_SC_Basic.activeInHierarchy)
            {
                group_SC.SetActive(true);
                group_SC_Basic.SetActive(false);
                return;
            }
            if (group_TC_Basic.activeInHierarchy)
            {
                group_TC.SetActive(true);
                group_TC_Basic.SetActive(false);
                return;
            }
        }
    }
}
