using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace DungeonPlayer
{
    public class TruthBattleSetting : MonoBehaviour
    {
        public Image[] pbAction;
        public Image[] pbCurrentAction;
        public Image moveActionBox;

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
        public Button btnSkill;
        public Button btnClass;
        public Button command1;
        public Button command2;
        public Button command3;
        public Button command4;
        public Button command5;
        //public Panel commandList;
        public Text commandName;
        public Text commandDescription;
        public Button back;
        public TruthImage dragObj;

        public TruthImage imgBasic1;
        public TruthImage imgBasic2;
        public TruthImage imgBasic3;

        public TruthImage imgLightDamage1;
        public TruthImage imgLightDamage2;
        public TruthImage imgLightDamage3;
        public TruthImage imgLightBuff1;
        public TruthImage imgLightBuff2;
        public TruthImage imgLightBuff3;
        public TruthImage imgLightEffect1;
        public TruthImage imgLightEffect2;
        public TruthImage imgLightEffect3;

        public TruthImage imgShadowDamage1;
        public TruthImage imgShadowDamage2;
        public TruthImage imgShadowDamage3;
        public TruthImage imgShadowBuff1;
        public TruthImage imgShadowBuff2;
        public TruthImage imgShadowBuff3;
        public TruthImage imgShadowEffect1;
        public TruthImage imgShadowEffect2;
        public TruthImage imgShadowEffect3;

        public TruthImage imgFireDamage1;
        public TruthImage imgFireDamage2;
        public TruthImage imgFireDamage3;
        public TruthImage imgFireDamage4;
        public TruthImage imgFireDamage5;
        public TruthImage imgFireBuff1;
        public TruthImage imgFireBuff2;
        public TruthImage imgFireEffect1;
        public TruthImage imgFireEffect2;

        public TruthImage imgIceDamage1;
        public TruthImage imgIceDamage2;
        public TruthImage imgIceDamage3;
        public TruthImage imgIceDamage4;
        public TruthImage imgIceBuff1;
        public TruthImage imgIceBuff2;
        public TruthImage imgIceBuff3;
        public TruthImage imgIceEffect1;
        public TruthImage imgIceEffect2;

        public TruthImage imgForceDamage1;
        public TruthImage imgForceDamage2;
        public TruthImage imgForceBuff1;
        public TruthImage imgForceBuff2;
        public TruthImage imgForceBuff3;
        public TruthImage imgForceBuff4;
        public TruthImage imgForceEffect1;
        public TruthImage imgForceEffect2;
        public TruthImage imgForceEffect3;

        public TruthImage imgWillDamage1;
        public TruthImage imgWillDamage2;
        public TruthImage imgWillBuff1;
        public TruthImage imgWillBuff2;
        public TruthImage imgWillEffect1;
        public TruthImage imgWillEffect2;
        public TruthImage imgWillEffect3;
        public TruthImage imgWillEffect4;
        public TruthImage imgWillEffect5;

        public TruthImage imgActiveDamage1;
        public TruthImage imgActiveDamage2;
        public TruthImage imgActiveDamage3;
        public TruthImage imgActiveDamage4;
        public TruthImage imgActiveBuff1;
        public TruthImage imgActiveBuff2;
        public TruthImage imgActiveBuff3;
        public TruthImage imgActiveEffect1;
        public TruthImage imgActiveEffect2;

        public TruthImage imgPassiveDamage1;
        public TruthImage imgPassiveDamage2;
        public TruthImage imgPassiveBuff1;
        public TruthImage imgPassiveBuff2;
        public TruthImage imgPassiveBuff3;
        public TruthImage imgPassiveEffect1;
        public TruthImage imgPassiveEffect2;
        public TruthImage imgPassiveEffect3;
        public TruthImage imgPassiveEffect4;

        public TruthImage imgSoftDamage1;
        public TruthImage imgSoftDamage2;
        public TruthImage imgSoftDamage3;
        public TruthImage imgSoftBuff1;
        public TruthImage imgSoftBuff2;
        public TruthImage imgSoftBuff3;
        public TruthImage imgSoftEffect1;
        public TruthImage imgSoftEffect2;
        public TruthImage imgSoftEffect3;

        public TruthImage imgHardDamage1;
        public TruthImage imgHardDamage2;
        public TruthImage imgHardDamage3;
        public TruthImage imgHardDamage4;
        public TruthImage imgHardDamage5;
        public TruthImage imgHardBuff1;
        public TruthImage imgHardBuff2;
        public TruthImage imgHardEffect1;
        public TruthImage imgHardEffect2;

        public TruthImage imgTruthDamage1;
        public TruthImage imgTruthBuff1;
        public TruthImage imgTruthBuff2;
        public TruthImage imgTruthBuff3;
        public TruthImage imgTruthBuff4;
        public TruthImage imgTruthBuff5;
        public TruthImage imgTruthEffect1;
        public TruthImage imgTruthEffect2;
        public TruthImage imgTruthEffect3;

        public TruthImage imgVoidDamage1;
        public TruthImage imgVoidBuff1;
        public TruthImage imgVoidBuff2;
        public TruthImage imgVoidBuff3;
        public TruthImage imgVoidEffect1;
        public TruthImage imgVoidEffect2;
        public TruthImage imgVoidEffect3;
        public TruthImage imgVoidEffect4;
        public TruthImage imgVoidEffect5;

        private List<TruthImage> basicList = null;
        private List<TruthImage> spellList = null;
        private List<TruthImage> spellLightList = null;
        private List<TruthImage> spellShadowList = null;
        private List<TruthImage> spellFireList = null;
        private List<TruthImage> spellIceList = null;
        private List<TruthImage> spellForceList = null;
        private List<TruthImage> spellWillList = null;
        private List<TruthImage> skillList = null;
        private List<TruthImage> skillActiveList = null;
        private List<TruthImage> skillPassiveList = null;
        private List<TruthImage> skillSoftList = null;
        private List<TruthImage> skillHardList = null;
        private List<TruthImage> skillTruthList = null;
        private List<TruthImage> skillVoidList = null;

        private Vector3 screenPoint;
        private Vector3 offset;

        byte[] ReadPngFile(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader bin = new BinaryReader(fileStream);
            byte[] values = bin.ReadBytes((int)bin.BaseStream.Length);

            bin.Close();

            return values;
        }

        Texture ReadTexture(string path, int width, int height)
        {
            byte[] readBinary = ReadPngFile(path);

            Texture2D texture = new Texture2D(width, height);
            texture.LoadImage(readBinary);

            return texture;
        }
        // Use this for initialization
        void Start()
        {
            GroundOne.InitializeGroundOne();
            currentPlayer = GroundOne.MC;
            // initialize
            this.basicList = new List<TruthImage>();
            this.spellList = new List<TruthImage>();
            this.spellLightList = new List<TruthImage>();
            this.spellShadowList = new List<TruthImage>();
            this.spellFireList = new List<TruthImage>();
            this.spellIceList = new List<TruthImage>();
            this.spellForceList = new List<TruthImage>();
            this.spellWillList = new List<TruthImage>();
            this.skillList = new List<TruthImage>();
            this.skillActiveList = new List<TruthImage>();
            this.skillPassiveList = new List<TruthImage>();
            this.skillSoftList = new List<TruthImage>();
            this.skillHardList = new List<TruthImage>();
            this.skillTruthList = new List<TruthImage>();
            this.skillVoidList = new List<TruthImage>();

            //Texture2D byteTexture=(Texture2D) ReadTexture("resource/FreshHeal.bmp", 320, 240);
            //this.imgBasic1.GetComponent<Image> ().sprite = Sprite.Create(byteTexture, new Rect(0,0,320,240), Vector2.zero);

            // setup command-name
            //this.imgBasic1.ImageName = Database.ATTACK_EN;
            //this.imgBasic2.ImageName = Database.DEFENSE_EN;
            //this.imgBasic3.ImageName = Database.TAMERU_EN;
            //this.imgLightDamage1.ImageName = Database.FRESH_HEAL;
            //this.imgLightDamage2.ImageName = Database.HOLY_SHOCK;
            //this.imgLightDamage3.ImageName = Database.CELESTIAL_NOVA;
            //this.imgLightBuff1.ImageName = Database.PROTECTION;
            //this.imgLightBuff2.ImageName = Database.SAINT_POWER;
            //this.imgLightBuff3.ImageName = "";
            //this.imgLightEffect1.ImageName = Database.GLORY;
            //this.imgLightEffect2.ImageName = Database.RESURRECTION;
            //this.imgLightEffect3.ImageName = "";

            //this.imgShadowDamage1.ImageName = Database.DARK_BLAST;
            //this.imgShadowDamage2.ImageName = Database.LIFE_TAP;
            //this.imgShadowDamage3.ImageName = Database.DEVOURING_PLAGUE;
            //this.imgShadowBuff1.ImageName = Database.SHADOW_PACT;
            //this.imgShadowBuff2.ImageName = Database.BLACK_CONTRACT;
            //this.imgShadowBuff3.ImageName = Database.BLOODY_VENGEANCE;
            //this.imgShadowEffect1.ImageName = Database.DAMNATION;
            //this.imgShadowEffect2.ImageName = "";
            //this.imgShadowEffect3.ImageName = "";

            //this.imgFireDamage1.ImageName = Database.FIRE_BALL;
            //this.imgFireDamage2.ImageName = Database.FLAME_STRIKE;
            //this.imgFireDamage3.ImageName = Database.VOLCANIC_WAVE;
            //this.imgFireDamage4.ImageName = "";
            //this.imgFireDamage5.ImageName = Database.LAVA_ANNIHILATION;
            //this.imgFireBuff1.ImageName = Database.FLAME_AURA;
            //this.imgFireBuff1.ImageName = Database.HEAT_BOOST;
            //this.imgFireEffect1.ImageName = Database.IMMORTAL_RAVE;
            //this.imgFireEffect2.ImageName = "";


            //// setup image list
            //this.basicList.Add(this.imgBasic1);
            //this.basicList.Add(this.imgBasic2);
            //this.basicList.Add(this.imgBasic3);

            //this.spellLightList.Add(this.imgLightDamage1);
            //this.spellLightList.Add(this.imgLightDamage2);
            //this.spellLightList.Add(this.imgLightDamage3);
            //this.spellLightList.Add(this.imgLightBuff1);
            //this.spellLightList.Add(this.imgLightBuff2);
            //this.spellLightList.Add(this.imgLightBuff3);
            //this.spellLightList.Add(this.imgLightEffect1);
            //this.spellLightList.Add(this.imgLightEffect2);
            //this.spellLightList.Add(this.imgLightEffect3);

            //this.spellShadowList.Add(this.imgShadowDamage1);
            //this.spellShadowList.Add(this.imgShadowDamage2);
            //this.spellShadowList.Add(this.imgShadowDamage3);
            //this.spellShadowList.Add(this.imgShadowBuff1);
            //this.spellShadowList.Add(this.imgShadowBuff2);
            //this.spellShadowList.Add(this.imgShadowBuff3);
            //this.spellShadowList.Add(this.imgShadowEffect1);
            //this.spellShadowList.Add(this.imgShadowEffect2);
            //this.spellShadowList.Add(this.imgShadowEffect3);

            //this.spellFireList.Add(this.imgFireDamage1);
            //this.spellFireList.Add(this.imgFireDamage2);
            //this.spellFireList.Add(this.imgFireDamage3);
            //this.spellFireList.Add(this.imgFireDamage4);
            //this.spellFireList.Add(this.imgFireDamage5);
            //this.spellFireList.Add(this.imgFireBuff1);
            //this.spellFireList.Add(this.imgFireBuff2);
            //this.spellFireList.Add(this.imgFireEffect1);
            //this.spellFireList.Add(this.imgFireEffect2);

            //this.spellIceList.Add(this.imgIceDamage1);
            //this.spellIceList.Add(this.imgIceDamage2);
            //this.spellIceList.Add(this.imgIceDamage3);
            //this.spellIceList.Add(this.imgIceDamage4);
            //this.spellIceList.Add(this.imgIceBuff1);
            //this.spellIceList.Add(this.imgIceBuff2);
            //this.spellIceList.Add(this.imgIceBuff3);
            //this.spellIceList.Add(this.imgIceEffect1);
            //this.spellIceList.Add(this.imgIceEffect2);

            //this.spellForceList.Add(this.imgForceDamage1);
            //this.spellForceList.Add(this.imgForceDamage2);
            //this.spellForceList.Add(this.imgForceBuff1);
            //this.spellForceList.Add(this.imgForceBuff2);
            //this.spellForceList.Add(this.imgForceBuff3);
            //this.spellForceList.Add(this.imgForceBuff4);
            //this.spellForceList.Add(this.imgForceEffect1);
            //this.spellForceList.Add(this.imgForceEffect2);
            //this.spellForceList.Add(this.imgForceEffect3);

            //this.spellWillList.Add(this.imgWillDamage1);
            //this.spellWillList.Add(this.imgWillDamage2);
            //this.spellWillList.Add(this.imgWillBuff1);
            //this.spellWillList.Add(this.imgWillBuff2);
            //this.spellWillList.Add(this.imgWillEffect1);
            //this.spellWillList.Add(this.imgWillEffect2);
            //this.spellWillList.Add(this.imgWillEffect3);
            //this.spellWillList.Add(this.imgWillEffect4);
            //this.spellWillList.Add(this.imgWillEffect5);

            //this.skillActiveList.Add(this.imgActiveDamage1);
            //this.skillActiveList.Add(this.imgActiveDamage2);
            //this.skillActiveList.Add(this.imgActiveDamage3);
            //this.skillActiveList.Add(this.imgActiveDamage4);
            //this.skillActiveList.Add(this.imgActiveBuff1);
            //this.skillActiveList.Add(this.imgActiveBuff2);
            //this.skillActiveList.Add(this.imgActiveBuff3);
            //this.skillActiveList.Add(this.imgActiveEffect1);
            //this.skillActiveList.Add(this.imgActiveEffect2);

            //this.skillPassiveList.Add(this.imgPassiveDamage1);
            //this.skillPassiveList.Add(this.imgPassiveDamage2);
            //this.skillPassiveList.Add(this.imgPassiveBuff1);
            //this.skillPassiveList.Add(this.imgPassiveBuff2);
            //this.skillPassiveList.Add(this.imgPassiveBuff3);
            //this.skillPassiveList.Add(this.imgPassiveEffect1);
            //this.skillPassiveList.Add(this.imgPassiveEffect2);
            //this.skillPassiveList.Add(this.imgPassiveEffect3);
            //this.skillPassiveList.Add(this.imgPassiveEffect4);

            //this.skillSoftList.Add(this.imgSoftDamage1);
            //this.skillSoftList.Add(this.imgSoftDamage2);
            //this.skillSoftList.Add(this.imgSoftDamage3);
            //this.skillSoftList.Add(this.imgSoftBuff1);
            //this.skillSoftList.Add(this.imgSoftBuff2);
            //this.skillSoftList.Add(this.imgSoftBuff3);
            //this.skillSoftList.Add(this.imgSoftEffect1);
            //this.skillSoftList.Add(this.imgSoftEffect2);
            //this.skillSoftList.Add(this.imgSoftEffect3);

            //this.skillHardList.Add(this.imgHardDamage1);
            //this.skillHardList.Add(this.imgHardDamage2);
            //this.skillHardList.Add(this.imgHardDamage3);
            //this.skillHardList.Add(this.imgHardDamage4);
            //this.skillHardList.Add(this.imgHardDamage5);
            //this.skillHardList.Add(this.imgHardBuff1);
            //this.skillHardList.Add(this.imgHardBuff2);
            //this.skillHardList.Add(this.imgHardEffect1);
            //this.skillHardList.Add(this.imgHardEffect2);

            //this.skillTruthList.Add(this.imgTruthDamage1);
            //this.skillTruthList.Add(this.imgTruthBuff1);
            //this.skillTruthList.Add(this.imgTruthBuff2);
            //this.skillTruthList.Add(this.imgTruthBuff3);
            //this.skillTruthList.Add(this.imgTruthBuff4);
            //this.skillTruthList.Add(this.imgTruthBuff5);
            //this.skillTruthList.Add(this.imgTruthEffect1);
            //this.skillTruthList.Add(this.imgTruthEffect2);
            //this.skillTruthList.Add(this.imgTruthEffect3);

            //this.skillVoidList.Add(this.imgVoidDamage1);
            //this.skillVoidList.Add(this.imgVoidBuff1);
            //this.skillVoidList.Add(this.imgVoidBuff2);
            //this.skillVoidList.Add(this.imgVoidBuff3);
            //this.skillVoidList.Add(this.imgVoidEffect1);
            //this.skillVoidList.Add(this.imgVoidEffect2);
            //this.skillVoidList.Add(this.imgVoidEffect3);
            //this.skillVoidList.Add(this.imgVoidEffect4);
            //this.skillVoidList.Add(this.imgVoidEffect5);

            //this.spellList.AddRange(this.spellLightList);
            //this.spellList.AddRange(this.spellShadowList);
            //this.spellList.AddRange(this.spellFireList);
            //this.spellList.AddRange(this.spellIceList);
            //this.spellList.AddRange(this.spellForceList);
            //this.spellList.AddRange(this.spellWillList);
            //this.skillList.AddRange(this.skillActiveList);
            //this.skillList.AddRange(this.skillPassiveList);
            //this.skillList.AddRange(this.skillSoftList);
            //this.skillList.AddRange(this.skillHardList);
            //this.skillList.AddRange(this.skillTruthList);
            //this.skillList.AddRange(this.skillVoidList);

            // todo まだ持ってくるモノがある。

            SetupAllIcon();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Application.LoadLevel("TruthHomeTown");
            }
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

        public void tapBasic()
        {
            for (int ii = 0; ii < this.basicList.Count; ii++) { this.basicList[ii].enabled = true; }
            for (int ii = 0; ii < this.spellList.Count; ii++) { this.spellList[ii].enabled = false; }
            for (int ii = 0; ii < this.skillList.Count; ii++) { this.skillList[ii].enabled = false; }
        }
        public void tapSpell()
        {
            for (int ii = 0; ii < this.basicList.Count; ii++) { this.basicList[ii].enabled = false; }
            for (int ii = 0; ii < this.spellList.Count; ii++) { this.spellList[ii].enabled = true; }
            for (int ii = 0; ii < this.skillList.Count; ii++) { this.skillList[ii].enabled = false; }
        }
        public void tapSkill()
        {
            for (int ii = 0; ii < this.basicList.Count; ii++) { this.basicList[ii].enabled = false; }
            //for (int ii = 0; ii < this.spellList.Count; ii++) { this.spellList[ii].enabled = false; }
            //for (int ii = 0; ii < this.skillList.Count; ii++) { this.skillList[ii].enabled = true; }
        }
        public void tapClass()
        {
        }

        public void ViewCommandContent()
        {
            string command = "";
            this.commandName.text = TruthActionCommand.ConvertToJapanese(command);
            this.commandDescription.text = TruthActionCommand.GetDescription(command);
        }

        private void SetupAllIcon()
        {
            string fileExt = ".bmp";
            string[] ssName = TruthActionCommand.GetActionList(currentPlayer);
            bool[] ssAvailable = TruthActionCommand.GetAvailableActionList(currentPlayer);
            for (int ii = 0; ii < Database.TOTAL_COMMAND_NUM; ii++)
            {
                try { if (ssAvailable[ii]) { pbAction[ii].sprite = Resources.Load<Sprite>(ssName[ii]); } } // Image.FromFile(Database.BaseResourceFolder + ssName[ii] + fileExt); } else { pbAction[ii].Image = null; } }
                catch { }
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