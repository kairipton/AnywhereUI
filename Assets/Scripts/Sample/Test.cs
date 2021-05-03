using UnityEngine;

public class Test : MonoBehaviour
{
	void Start()
	{
		AnywhereUI.Open<AUI.SampleUI>().SetText( "This is Sample" );
	}

	void Update()
	{
		if( Input.GetKeyDown( KeyCode.Escape ) )
		{
			AnywhereUI.Close<AUI.SampleUI>();
		}
	}
}