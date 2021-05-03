#if UNITY_EDITOR
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using AUI.Internal;

public class AnywhereUIFinder : EditorWindow
{
	Vector2			scrollPos	= Vector3.zero;
	Type			baseType;
	Type[]			drivedTypes;
	
	[MenuItem( "Tools/AnywhereUI/Open Finder") ]
	public static void Open()
	{
		var instance = GetWindow<AnywhereUIFinder>();
		instance.baseType = typeof(AUIBase);
		instance.drivedTypes = instance.baseType.Assembly.GetTypes()
			.Where( t=> t.IsSubclassOf( instance.baseType ) && t.IsGenericType == false )
			.OrderBy( x=> x.Name )
			.ToArray();
	}

	private void OnGUI()
	{
		scrollPos = EditorGUILayout.BeginScrollView( scrollPos );
		for(int i=0; i<drivedTypes.Length; i++)
		{
			GUILayout.BeginHorizontal();
			Type t = drivedTypes[i];
			var att = (AUIPath)t.GetCustomAttribute( typeof(AUIPath) );
			if( GUILayout.Button( t.Name ) == true )
			{
				AnywhereUI.OpenPrefab( att.path,t  );
			}

			if( GUILayout.Button( "→", GUILayout.Width( 35 ) ) == true )
			{
				var go = AssetDatabase.LoadAssetAtPath( $"Assets/Resources/{att.path}.prefab", typeof(GameObject) );
				EditorGUIUtility.PingObject( go );
			}
			GUILayout.EndHorizontal();
		}
		EditorGUILayout.EndScrollView();
	}
}
#endif
