using UnityEngine;
using System.Collections;

namespace DungeonPlayer
{
    public class MainCamera : MonoBehaviour
    {

        public MainCharacter target;
        public bool BattleStart;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (target != null && BattleStart == false)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.target.transform.position.y + 3.0f, this.target.transform.position.z + 2.0f);
            }
        }

        void tapButton()
        {
        }
    }
}