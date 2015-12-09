using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public static class Protocol {

	private static string FIXED = "/*!#$%&'()=~|";

    public static string FailSafe = FIXED + "FailSafe:";
    public static string CreateOwner = FIXED + "CreateOwner:";
    public static string ExistOwner = FIXED + "ExistOwner:";

	// basic
	public static string AttachUserName = FIXED + "AttachUserName:";
	public static string BeforeAddingUserName = FIXED + "BeforeAddingUserName:";
	public static string SendCommonMessage = FIXED + "SendCommonMessage:";
	public static string DetachUserName = FIXED + "DetachUserName:";
	// data save-load
	public static string LoadCharacter = FIXED + "LoadCharacter:";
	public static string SaveCharacter = FIXED + "SaveCharacter:";
	public static string BattleStart = FIXED + "BattleStart:";

	public static string CreateCharacter = FIXED + "CreateCharacter:";
	public static string ExistCharacter = FIXED + "ExistCharacter:";

	public static string UpdateCharacter = FIXED + "UpdateCharacter:";
    public static string LoadBackpackList = FIXED + "LoadBackpackList:";
	public static string GetItemData = FIXED + "GetItemData:";

	public static string GetMonsterData = FIXED + "GetMonsterData:";
}
