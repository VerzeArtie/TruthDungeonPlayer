using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonPlayer
{
    public class MotherForm : MonoBehaviour
    {
        public virtual void Start()
        {
            if (GroundOne.InitializeGroundOne())
            {
                DontDestroyOnLoad(GroundOne.MC);
                DontDestroyOnLoad(GroundOne.SC);
                DontDestroyOnLoad(GroundOne.TC);
                DontDestroyOnLoad(GroundOne.WE);
                DontDestroyOnLoad(GroundOne.WE2);
            }
        }

        protected string exitMessage1 = "セーブしていない場合、現在データは破棄されます。セーブしますか？";
        protected string exitMessage2 = "タイトルへ戻りますか？";
        protected string exitMessage3 = "ユングの町に戻りますか？";

        public Text yesnoSystemMessage;
        public GameObject groupYesnoSystemMessage;
        public GameObject Filter;

        public void SceneBack()
        {
            this.Filter.SetActive(false);
        }

        public void NextScene()
        {
            //this.Filter.SetActive(false);
            this.Filter.GetComponent<Image>().color = Color.black;

            Application.UnloadLevel(Database.SaveLoad);
            if (GroundOne.WE.SaveByDungeon)
            {
                SceneDimension.CallTruthDungeon(this.GetType().ToString());
            }
            else
            {
                SceneDimension.CallTruthHomeTown(this.GetType().ToString());
            }
        }

        public void ExitYes()
        {
            if (yesnoSystemMessage.text == exitMessage1)
            {
                GroundOne.TruthHomeTown_NowExit = true;
                SceneDimension.CallSaveLoad(this.GetType().ToString(), true, false, this);
            }
            else if (yesnoSystemMessage.text == exitMessage2)
            {
                GroundOne.TruthHomeTown_NowExit = false;
                SceneDimension.JumpToTitle();
            }
            else if (yesnoSystemMessage.text == exitMessage3)
            {
                GroundOne.TruthHomeTown_NowExit = false;
                SceneDimension.CallTruthHomeTown(this.GetType().ToString());
            }
        }

        public void ExitNo()
        {
            if (this.yesnoSystemMessage.text == exitMessage1)
            {
                this.yesnoSystemMessage.text = exitMessage2;
            }
            else if (this.yesnoSystemMessage.text == exitMessage2)
            {
                this.yesnoSystemMessage.text = exitMessage1;
                this.groupYesnoSystemMessage.SetActive(false);
            }
            else if (this.yesnoSystemMessage.text == exitMessage3)
            {
                this.groupYesnoSystemMessage.SetActive(false);
            }
            GroundOne.TruthHomeTown_NowExit = false;
        }
    }
}
