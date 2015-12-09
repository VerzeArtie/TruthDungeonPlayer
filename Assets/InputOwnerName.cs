using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace DungeonPlayer
{
    public class InputOwnerName : MonoBehaviour
    {
        public Text reply1 = null;
        public InputField ownerName = null;
        public Text description = null;

        private string workReply;

        bool firstAction = false;

        // Use this for initialization
        void Start() {
            GroundOne.InitializeNetworkConnection();
            if (GroundOne.IsConnect)
            {
                GroundOne.CS.rcm += new ClientSocket.ReceiveClientMessage (ReceiveFromClientSocket);
            }
    	}

        string GetString(string msg, string protocolStr)
        {
            return msg.Substring(protocolStr.Length, msg.Length - protocolStr.Length);
        }
    	private void ReceiveFromClientSocket(string msg)
        {
            if (msg.Contains(Protocol.ExistOwner))
            {
                if (GetString(msg, Protocol.ExistOwner) == "false")
                {
                    workReply = "そのオーナー名は利用可能です。【登録】ボタンを押してください。";
                    byte[] bb = System.Text.Encoding.UTF8.GetBytes(Protocol.CreateOwner + this.ownerName.text);
                    GroundOne.CS.SendMessage(bb);
                }
                else
                {
                    workReply = "そのオーナー名は既に使われています。別のオーナー名を入力してください。";
                }
            }
            else if (msg.Contains(Protocol.CreateOwner))
            {
                workReply = "オーナー情報が登録されました。guidは"+GetString(msg, Protocol.CreateOwner)+"です。";
                workReply += "\r\n 「Game Start」を押してください。";
                PlayerPrefs.SetString("OwnerName", ownerName.text);
                PlayerPrefs.SetString("OwnerGuid", GetString(msg, Protocol.CreateOwner));
                PlayerPrefs.Save();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (this.firstAction == false)
            {
                this.firstAction = true;
                if (GroundOne.OwnerName != "")//PlayerPrefs.GetString("OwnerName", "") != "")
                {
                    this.ownerName.text = GroundOne.OwnerName;
                    workReply = this.ownerName.text + "さん、それではゲームを始めましょう。";
                }
            }

            reply1.text = workReply;
        }

        public void tapOK()
        {
            byte[] bb = System.Text.Encoding.UTF8.GetBytes(Protocol.ExistOwner + this.ownerName.text);
            GroundOne.CS.SendMessage(bb);
        }

        public void tapStart()
        {
            Application.LoadLevel("TruthDungeon");
        }
    }
}
