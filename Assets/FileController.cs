using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class FileController : MonoBehaviour {

//	[System.Serializable()]
//	public class SaveData{
//		[System.Serializable()]
//		public class InnerClass{
//			public string ppppppp;
//			public int ggggggg;
//		}
//		
//		public int aaaaaaa;
//		public float bbbbbb;
//		public string cccccccc;
//		public int[] ddddddd;
//		public InnerClass eeeeeeee;
//		
//	}
//
//	[System.Serializable()]
//	public static bool Save( string prefKey, T serializableObject ){
//		MemoryStream memoryStream = new MemoryStream();
//		#if UNITY_IPHONE || UNITY_IOS
//		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
//		#endif
//		BinaryFormatter bf = new BinaryFormatter();
//		bf.Serialize( memoryStream, serializableObject );
//		
//		string tmp = System.Convert.ToBase64String( memoryStream.ToArray() );
//		try {
//			PlayerPrefs.SetString ( prefKey, tmp );
//		} catch( PlayerPrefsException ) {
//			return false;
//		}
//		return true;
//	}
//
//	public static T Load( string prefKey ){
//		if (!PlayerPrefs.HasKey(prefKey)) return default(T);
//		#if UNITY_IPHONE || UNITY_IOS
//		System.Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
//		#endif
//		BinaryFormatter bf = new BinaryFormatter();
//		string serializedData = PlayerPrefs.GetString( prefKey );
//		
//		MemoryStream dataStream = new MemoryStream( System.Convert.FromBase64String(serializedData) );
//		T deserializedObject = (T)bf.Deserialize( dataStream );
//		
//		return deserializedObject;
//	}
//	
//	public void save(SaveData data) {
//		// 保存用クラスにデータを格納.
//		FileController.Save("{PlayerPrefsに保存するキー}", data);
//		PlayerPrefs.Save();
//	}
//
//	public SaveData load() {
//		SaveData data_tmp = FileController.Load("{PlayerPrefsに保存するキー}");
//		if( data_tmp != null){
//			return data_tmp;
//		}else{
//			return null;
//		}
//	}
}