﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonPlayer
{
    public class MotherForm : MonoBehaviour
    {
        public GameObject Background;
        public Text yesnoSystemMessage;
        public GameObject groupYesnoSystemMessage;
        public GameObject Filter;
        public ItemBackPack GettingNewItem;
        protected DateTime pushTime = DateTime.Now; // ボタン連打防止

        public virtual void Start()
        {
            GroundOne.InitializeGroundOne(false);

            //if (GroundOne.WE2 != null && GroundOne.SQL != null && GroundOne.WE2.Account != String.Empty)
            //{
            //    GroundOne.SQL.UpdateOwner(string.Empty, string.Empty, this.GetType().ToString());
            //}
        }

        public virtual void Update()
        {
            if (GroundOne.bgmSource[GroundOne.BgmNumber] != null && GroundOne.bgmSource[GroundOne.BgmNumber].isPlaying)
            {
                if (GroundOne.bgmSource[GroundOne.BgmNumber].time >= GroundOne.bgmSource[GroundOne.BgmNumber].clip.length - 1)
                {
                    GroundOne.bgmSource[GroundOne.BgmNumber].time = GroundOne.BgmLoopPoint[GroundOne.BgmNumber];
                }
            }

            if (Input.GetKeyDown(KeyCode.F1))
            {
                BookManual_Click();
            }
        }
        
        public virtual void SceneBack()
        {
            if (this.Filter != null)
            {
                this.Filter.SetActive(false);
            }
        }

        public void NextScene()
        {
            //this.Filter.SetActive(false);
            this.Filter.GetComponent<Image>().color = Color.black;

            Application.UnloadLevel(Database.SaveLoad);
            if (GroundOne.WE.SaveByDungeon)
            {
                SceneDimension.JumpToTruthDungeon(false);
            }
            else
            {
                SceneDimension.JumpToTruthHomeTown();
            }
        }

        public virtual void ExitYes()
        {
            if (yesnoSystemMessage.text == Database.exitMessage1)
            {
                GroundOne.TruthHomeTown_NowExit = true;
                SceneDimension.CallSaveLoad(this, true, false);
            }
            else if (yesnoSystemMessage.text == Database.exitMessage2)
            {
                GroundOne.TruthHomeTown_NowExit = false;
                SceneDimension.JumpToTitle();
            }
            else if (yesnoSystemMessage.text == Database.exitMessage3)
            {
                GroundOne.TruthHomeTown_NowExit = false;
                Method.ExecSave(null, Database.AutoSaveNum, true);
                SceneDimension.JumpToTruthHomeTown();
            }
            else if (yesnoSystemMessage.text == Database.exitMessage4)
            {
                GroundOne.TruthHomeTown_NowExit = false;
                SceneDimension.JumpToTitle();
            }
        }

        public virtual void ExitNo()
        {
            if (this.yesnoSystemMessage.text == Database.exitMessage1)
            {
                this.yesnoSystemMessage.text = Database.exitMessage2;
                GroundOne.TruthHomeTown_NowExit = false;
            }
            else if (this.yesnoSystemMessage.text == Database.exitMessage2)
            {
                this.yesnoSystemMessage.text = Database.exitMessage1;
                this.groupYesnoSystemMessage.SetActive(false);
                this.Filter.SetActive(false);
                GroundOne.TruthHomeTown_NowExit = false;
            }
            else if (this.yesnoSystemMessage.text == Database.exitMessage3 ||
                     this.yesnoSystemMessage.text == Database.exitMessage4)
            {
                this.groupYesnoSystemMessage.SetActive(false);
                this.Filter.SetActive(false);
                GroundOne.TruthHomeTown_NowExit = false;
            }
        }
        
        public virtual void BookManual_Click()
        {
            SceneDimension.CallTruthBookManual(this);
        }

        public void GetNewItemAndBack()
        {
            GetNewItem(this.GettingNewItem);
            SceneDimension.Back(this);
        }

        public bool GetNewItem(ItemBackPack newItem)
        {
            bool getOK = false;
            if (GroundOne.MC.AddBackPack(newItem) && getOK == false)
            {
                getOK = true;
            }
            if (GroundOne.WE.AvailableSecondCharacter && getOK == false)
            {
                if (GroundOne.SC.AddBackPack(newItem))
                {
                    getOK = true;
                }
            }
            if (GroundOne.WE.AvailableThirdCharacter && getOK == false)
            {
                if (GroundOne.TC.AddBackPack(newItem))
                {
                    getOK = true;
                }
            }

            return getOK;
        }

        protected bool CheckPushButtonShortly
        {
            get {
                if (DateTime.Now - pushTime < new TimeSpan(0, 0, 2))
                {
                    Debug.Log("push timing is very short, then no action. " + DateTime.Now);
                    return true;
                }

                this.pushTime = DateTime.Now;
                return false;

            }
        }
    }
}
