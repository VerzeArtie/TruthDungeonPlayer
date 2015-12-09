using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DungeonPlayer
{
    public partial class DungeonField : MonoBehaviour
    {

        private void UpdatePlayerLocation(MainCharacter player, Vector3 src, Vector3 dst)
        {
            if (player != null)
            {
                var velo = new Vector3(dst.x - src.x, dst.y - src.y, dst.z - src.z);
                player.GetComponent<Rigidbody>().velocity = (velo).normalized * 5.0f;
            }
        }

        private void InstantiateCharacter(List<MainCharacter> list, MainCharacter player, Vector3 position, Color color)
        {
            MainCharacter obj2 = Instantiate(player, position, Quaternion.identity) as MainCharacter;
            obj2.GetComponent<Renderer>().material.color = color;
            list.Add(obj2);
        }
    }
}