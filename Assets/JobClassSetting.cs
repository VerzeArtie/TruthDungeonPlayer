using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class JobClassSetting : MonoBehaviour {

	public Text jobClassName;
	public Text attribute1;
	public Text attribute2;
	public Text attribute3;
	// Use this for initialization
	void Start () {
		//Application.persistentDataPath;
		SaveData.DataLoad ();
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetMouseButtonDown (0)) {
        //			Application.LoadLevel("TruthHomeTown");
//		}
	}

	public void GotoBack() {
		SaveData.DataSave(jobClassName.text);
        Application.LoadLevel("TruthHomeTown");
	}
	public void tapFighter() {
		jobClassName.text = "Fighter";
		attribute1.text = "Increase Physical Defense 2%";
		attribute2.text = "Increase Max Life 5%";
		attribute3.text = "gain < Quick Parry >";
	}
	public void tapArcher() {
		jobClassName.text = "Archer";
		attribute1.text = "Increase Critical Rate 3%";
		attribute2.text = "Max Skill Point +1";
		attribute3.text = "gain < Shooting Star >";
	}
	public void tapLancer() {
		jobClassName.text = "Lancer";
		attribute1.text = "Increase Physical Damage 3%";
		attribute2.text = "Resist Blind Effect 25%";
		attribute3.text = "gain < Penetrating Smash >";
	}
	public void tapRouge() {
		jobClassName.text = "Rouge";
		attribute1.text = "Increase Battle Speed 2%";
		attribute2.text = "Resist Temptation Effect 25%";
		attribute3.text = "gain < Deceit Step >";
	}
	public void tapWarrior() {
		jobClassName.text = "Warrior";
		attribute1.text = "Increase Life Regeneration 5%";
		attribute2.text = "Resist Stun Effect 25%";
		attribute3.text = "gain < Self Restoration >";
	}
	public void tapBreaker() {
		jobClassName.text = "Breaker";
		attribute1.text = "Increase Potential 3%";
		attribute2.text = "Ignore Target Defense 5%";
		attribute3.text = "gain < Energy Destruction >";
	}
	public void tapConjurer() {
		jobClassName.text = "Conjurer";
		attribute1.text = "Increase Battle Response 3%";
		attribute2.text = "Resist Frozen Effect 25%";
		attribute3.text = "gain < Reverse Sphere >";
	}
	public void tapPriest() {
		jobClassName.text = "Priest";
		attribute1.text = "Increase Healing Effect 5%";
		attribute2.text = "Resist Poison Effect 25%";
		attribute3.text = "gain < Splash Heal >";
	}
	public void tapWarlock() {
		jobClassName.text = "Warlock";
		attribute1.text = "Increase Mana Regeneration 5%";
		attribute2.text = "Resist Slow Effect 25%";
		attribute3.text = "gain < Life Sacrifice >";
	}
	public void tapSorcerer() {
		jobClassName.text = "Sorcerer";
		attribute1.text = "Increase Magic Damage 3%";
		attribute2.text = "Resist Silence Effect 25%";
		attribute3.text = "gain < Silver Magic >";
	}
	public void tapDruid() {
		jobClassName.text = "Druid";
		attribute1.text = "Increase Magic Defense 2%";
		attribute2.text = "Increase Max Mana 5%";
		attribute3.text = "gain < Nature Call >";
	}
	public void tapEnchanter() {
		jobClassName.text = "Enchanter";
		attribute1.text = "Increase Self Buff Turn +1";
		attribute2.text = "Resist Paralyze Effect 25%";
		attribute3.text = "gain < Aura Explosion >";
	}
}
