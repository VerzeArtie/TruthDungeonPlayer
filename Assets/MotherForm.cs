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

        public virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                BookManual_Click();
            }
        }
        
        public Text yesnoSystemMessage;
        public GameObject groupYesnoSystemMessage;
        public GameObject Filter;
        public ItemBackPack GettingNewItem;

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
                SceneDimension.JumpToTruthDungeon(this.GetType().ToString(), false);
            }
            else
            {
                SceneDimension.JumpToTruthHomeTown(this.GetType().ToString());
            }
        }

        public virtual void ExitYes()
        {
            if (yesnoSystemMessage.text == Database.exitMessage1)
            {
                GroundOne.TruthHomeTown_NowExit = true;
                SceneDimension.CallSaveLoad(this.GetType().ToString(), true, false, this);
            }
            else if (yesnoSystemMessage.text == Database.exitMessage2)
            {
                GroundOne.TruthHomeTown_NowExit = false;
                SceneDimension.JumpToTitle();
            }
            else if (yesnoSystemMessage.text == Database.exitMessage3)
            {
                GroundOne.TruthHomeTown_NowExit = false;
                SceneDimension.JumpToTruthHomeTown(this.GetType().ToString());
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
            else if (this.yesnoSystemMessage.text == Database.exitMessage3)
            {
                this.groupYesnoSystemMessage.SetActive(false);
                this.Filter.SetActive(false);
                GroundOne.TruthHomeTown_NowExit = false;
            }
        }
        
        public virtual void BookManual_Click()
        {
            Application.LoadLevelAdditive(Database.TruthInformation);
        }

        public void GetNewItemAndBack()
        {
            GetNewItem(this.GettingNewItem);
            SceneDimension.Back();
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
    }
}
