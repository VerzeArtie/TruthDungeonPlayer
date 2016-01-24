using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace DungeonPlayer
{
    public class TruthBattleSetting : MotherForm
    {
        public GameObject back_commandName;
        public Text commandName;
        public GameObject back_description;
        public Text description;
        public Image[] pbAction;
        public Image[] pbSorcery;
        public Image[] pbCurrentAction;
        public Image[] pbCurrentActionSorcery;
        public Image moveActionBox;
        public Image moveActionBoxSorcery;
        MainCharacter currentPlayer;


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
        public TruthImage dragObj;

        private Vector3 screenPoint;
        private Vector3 offset;

        const int CURRENT_ACTION_NUM = 9;
        const int BASIC_ACTION_NUM = 8; // 基本行動
        const int MIX_ACTION_NUM = 45; // [警告] 暫定、本来Databaseに記載するべき
        const int MIX_ACTION_NUM_2 = 30; // [警告]暫定、本来Databaseに記載するべき
        const int ARCHETYPE_NUM = 1; // アーキタイプ

        public void TruthBattleSetting_MouseMove(Button sender)
        {
            moveActionBox.gameObject.SetActive(true);
            moveActionBox.gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            // todo
//            moveActionBox.Location = new Point(((PictureBox)sender).Location.X + e.X - adjustX, ((PictureBox)sender).Location.Y + e.Y - adjustY);

            if (moveActionBox.gameObject.activeInHierarchy == false)
            {
                // todo
                //if (popupInfo == null)
                //{
                //    popupInfo = new PopUpMini();
                //}

                //popupInfo.StartPosition = FormStartPosition.Manual;
                //popupInfo.Location = new Point(this.Location.X + ((PictureBox)sender).Location.X + e.X + 5, this.Location.Y + ((PictureBox)sender).Location.Y + e.Y - 18);
                //popupInfo.PopupColor = Color.Black;
                //// s 後編編集
                //System.OperatingSystem os = System.Environment.OSVersion;
                //int osNumber = os.Version.Major;
                //if (osNumber != 5)
                //{
                //    popupInfo.Opacity = 0.7f;
                //}
                ////popupInfo.Opacity = 0.7f; // 後編削除
                ////popupInfo.PopupTextColor = Brushes.White; // 後編削除
                //// e 後編編集

                //// temp del//popupInfo.FontFamilyName = new Font("ＭＳ ゴシック", 14.0F, FontStyle.Regular, GraphicsUnit.Pixel, 128, true);

                //// [警告] for文がグルグルともったいない。ロースペックが来たら遅いかもしれない。
                //for (int ii = 0; ii < CURRENT_ACTION_NUM; ii++)
                //{
                //    if (((PictureBox)sender).Equals(pbCurrentAction[ii]))
                //    {
                //        if (((PictureBox)sender).Image != null)
                //        {
                //            popupInfo.CurrentInfo = this.currentCommand[this.currentPlayerNumber][ii] + "\r\n";
                //            for (int jj = 0; jj < BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM + MIX_ACTION_NUM_2 + ARCHETYPE_NUM; jj++)
                //            {
                //                if (this.currentCommand[this.currentPlayerNumber][ii] == battleCommandList[jj])
                //                {
                //                    popupInfo.CurrentInfo += battleDescriptionList[jj];
                //                }
                //            }
                //            popupInfo.Show();
                //            return;
                //        }
                //    }
                //}

                //for (int ii = 0; ii < BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM + MIX_ACTION_NUM_2 + ARCHETYPE_NUM; ii++)
                //{
                //    if (((PictureBox)sender).Equals(pbAction[ii]))
                //    {
                //        if (((PictureBox)sender).Image != null)
                //        {
                //            popupInfo.CurrentInfo = battleCommandList[ii] + "\r\n" + battleDescriptionList[ii];
                //            popupInfo.Show();
                //            return;
                //        }
                //    }
                //}
            }

        }

        public void TruthBattleSetting_MouseUp(Button sender)
        {
            moveActionBox.gameObject.SetActive(false);
            Debug.Log("TruthBattleSetting_MouseUp... " + sender.gameObject.transform.position.ToString());

            // todo
            float positionX = Input.mousePosition.x; //+ sender.gameObject.transform.position.x;
            float positionY = Input.mousePosition.y; // +sender.gameObject.transform.position.y;
            //int positionX = e.X + ((PictureBox)sender).Location.X;
            //int positionY = e.Y + ((PictureBox)sender).Location.Y;
            for (int ii = 0; ii < CURRENT_ACTION_NUM; ii++)
            {
                if (pbCurrentAction[ii].gameObject.transform.position.x <= positionX && positionX <= pbCurrentAction[ii].gameObject.transform.position.x + (pbCurrentAction[ii].GetComponent<RectTransform>()).rect.width &&
                    pbCurrentAction[ii].gameObject.transform.position.y <= positionY && positionY <= pbCurrentAction[ii].gameObject.transform.position.y + (pbCurrentAction[ii].GetComponent<RectTransform>()).rect.height)
                {
                    pbCurrentAction[ii].sprite = sender.image.sprite;
                    pbCurrentAction[ii].name = sender.name;
                    if (TruthActionCommand.GetTimingType(sender.name) == TruthActionCommand.TimingType.Sorcery)
                    {
                        pbCurrentActionSorcery[ii].sprite = Resources.Load<Sprite>("sorcery_mark");
                    }
                    else
                    {
                        pbCurrentActionSorcery[ii].sprite = Resources.Load<Sprite>("instant_mark");
                    }

                    GroundOne.MC.BattleActionCommandList[ii] = sender.name;
                    Debug.Log("MC.battlecommand : " + sender.name);
                    //this.currentCommand[this.currentPlayerNumber][ii] = Database.STAY_EN;
                    //for (int jj = 0; jj < BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM + MIX_ACTION_NUM_2 + ARCHETYPE_NUM; jj++)
                    //{
                    //    if (pbCurrentAction[ii].Image.Equals(pbAction[jj].Image))
                    //    {
                    //        this.currentCommand[this.currentPlayerNumber][ii] = battleCommandList[jj];
                    //        break;
                    //    }
                    //}
                    break;
                }
            }
        }

        int adjustX = 0;
        int adjustY = 0;
        public void TruthBattleSetting_MouseDown(Button sender)
        {
            Debug.Log("TruthBattleSetting_MouseDown: " + sender.name);
            moveActionBox.gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            moveActionBox.sprite = sender.image.sprite;
            moveActionBox.name = sender.name;

            //moveActionBox.sprite = Resources.Load<Sprite>(ssName[ii]);
            if (TruthActionCommand.GetTimingType(sender.name) == TruthActionCommand.TimingType.Sorcery)
            {
                moveActionBoxSorcery.sprite = Resources.Load<Sprite>("sorcery_mark");
            }
            else
            {
                moveActionBoxSorcery.sprite = Resources.Load<Sprite>("instant_mark");
            }


            moveActionBox.gameObject.SetActive(true);
            // todo
            //moveActionBox.Image = ((PictureBox)sender).Image;
            //moveActionBox.Visible = true;
            //this.adjustX = e.X;
            //this.adjustY = e.Y;
            //moveActionBox.Location = new Point(((PictureBox)sender).Location.X + e.X, ((PictureBox)sender).Location.Y + e.Y);
        }

        // Use this for initialization
        public override void Start()
        {
            base.Start();

            currentPlayer = GroundOne.MC;

            // todo まだ持ってくるモノがある。

            SetupAllIcon();
        }

        // Update is called once per frame
        void Update()
        {
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
            SceneDimension.Back();
        }

        public void ViewCommandContent(Button sender)
        {
            string command = sender.name;
            this.commandName.text = @"<color=black>" + TruthActionCommand.ConvertToJapanese(command) + @"</color>";
            this.description.text = TruthActionCommand.GetDescription(command);
        }

        private void SetupAllIcon()
        {
            string fileExt = ".bmp";
            string[] ssName = TruthActionCommand.GetActionList(currentPlayer);
            bool[] ssAvailable = TruthActionCommand.GetAvailableActionList(currentPlayer);
            for (int ii = 0; ii < Database.TOTAL_COMMAND_NUM; ii++)
            {
                try { 
                    if (ssAvailable[ii]) {
                        pbAction[ii].sprite = Resources.Load<Sprite>(ssName[ii]);
                        if (TruthActionCommand.GetTimingType(ssName[ii]) == TruthActionCommand.TimingType.Sorcery) {
                            pbSorcery[ii].sprite = Resources.Load<Sprite>("sorcery_mark");
                        } else {
                            pbSorcery[ii].sprite = Resources.Load<Sprite>("instant_mark");
                        }
                    }
                }
                catch { }
            }

            for (int ii = 0; ii < currentPlayer.BattleActionCommandList.Length; ii++)
            {
                pbCurrentAction[ii].sprite = Resources.Load<Sprite>(currentPlayer.BattleActionCommandList[ii]);
                pbCurrentAction[ii].name = currentPlayer.BattleActionCommandList[ii];
                if (TruthActionCommand.GetTimingType(currentPlayer.BattleActionCommandList[ii]) == TruthActionCommand.TimingType.Sorcery)
                {
                    pbCurrentActionSorcery[ii].sprite = Resources.Load<Sprite>("sorcery_mark");
                }
                else
                {
                    pbCurrentActionSorcery[ii].sprite = Resources.Load<Sprite>("instant_mark");
                }
            }
            // todo
            // プレイヤーのバトルコマンドを反映する。
            //if (this.currentCommand[this.currentPlayerNumber][0] == null) this.currentCommand[this.currentPlayerNumber][0] = currentPlayer.BattleActionCommand1;
            //if (this.currentCommand[this.currentPlayerNumber][1] == null) this.currentCommand[this.currentPlayerNumber][1] = currentPlayer.BattleActionCommand2;
            //if (this.currentCommand[this.currentPlayerNumber][2] == null) this.currentCommand[this.currentPlayerNumber][2] = currentPlayer.BattleActionCommand3;
            //if (this.currentCommand[this.currentPlayerNumber][3] == null) this.currentCommand[this.currentPlayerNumber][3] = currentPlayer.BattleActionCommand4;
            //if (this.currentCommand[this.currentPlayerNumber][4] == null) this.currentCommand[this.currentPlayerNumber][4] = currentPlayer.BattleActionCommand5;
            //if (this.currentCommand[this.currentPlayerNumber][5] == null) this.currentCommand[this.currentPlayerNumber][5] = currentPlayer.BattleActionCommand6;
            //if (this.currentCommand[this.currentPlayerNumber][6] == null) this.currentCommand[this.currentPlayerNumber][6] = currentPlayer.BattleActionCommand7;
            //if (this.currentCommand[this.currentPlayerNumber][7] == null) this.currentCommand[this.currentPlayerNumber][7] = currentPlayer.BattleActionCommand8;
            //if (this.currentCommand[this.currentPlayerNumber][8] == null) this.currentCommand[this.currentPlayerNumber][8] = currentPlayer.BattleActionCommand9;

            //for (int ii = 0; ii < Database.BATTLE_COMMAND_MAX; ii++)
            //{
            //    pbCurrentAction[ii].Image = pbAction[2].Image;
            //    for (int jj = 0; jj < BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM + MIX_ACTION_NUM_2 + ARCHETYPE_NUM; jj++)
            //    {
            //        if (currentCommand[this.currentPlayerNumber][ii] == battleCommandList[jj])
            //        {
            //            pbCurrentAction[ii].Image = pbAction[jj].Image;
            //            break;
            //        }
            //    }
            //}
        }
    }
}
