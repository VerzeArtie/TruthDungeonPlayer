using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DungeonPlayer
{
    public class TruthItemDesc : MotherForm
    {
        public Text ItemTitleText;
        public Text ItemDescriptionText;
        public Text ItemNameButtonSentence;

        public override void Start()
        {
            base.Start();
            ItemBackPack item = new ItemBackPack(GroundOne.ItemNameTitle);

            this.ItemTitleText.text = GroundOne.ItemNameTitle;
            this.ItemDescriptionText.text = item.Description;
            this.ItemNameButtonSentence.text = GroundOne.ItemNameTitle + "が新しく入荷しました！";
        }

        public override void Update()
        {
            base.Update();
        }

        public void tapClose()
        {
            GroundOne.PlaySoundEffect(Database.SOUND_SELECT_TAP);
            SceneDimension.Back(this);
        }
    }
}