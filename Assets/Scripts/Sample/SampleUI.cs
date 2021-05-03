using UnityEngine;
using UnityEngine.UI;

namespace AUI
{	
	[Internal.AUIPath( "Sample UI" )]
	public class SampleUI : Internal.AUIContainer<SampleUI>
	{
		[SerializeField] Text	text;

		public override SampleUI OnOpen()
		{
			return this;
		}

		public SampleUI SetText(string txt)
		{
			text.text = txt;
			return this;
		}
	}
}