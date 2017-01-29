using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Xml;
using UnityEngine.Purchasing;

namespace DungeonPlayer
{
	public class DungeonTicket : MotherForm
	{
		public override void Start()
		{
			base.Start();
		}

		public override void Update()
		{
			base.Update();
		}

		public void tapClose()
		{
			SceneDimension.Back(this);
		}
	}
}
