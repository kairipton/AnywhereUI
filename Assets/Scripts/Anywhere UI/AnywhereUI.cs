using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using AUI.Internal;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class AnywhereUI : MonoBehaviour
{
	static AnywhereUI				_instance		= null;
	static AnywhereUI				instance
	{
		get
		{
			if( _instance != null ) return _instance;
			GameObject prefab = Resources.Load( rscPath ) as GameObject;
			var go = Instantiate( prefab );
			_instance = go.GetComponent<AnywhereUI>();
			_instance.transform.localPosition = Vector3.zero;

			return _instance;
		}
	}

	const string					rscPath			= "Anywhere UI";
	static List<AUIBase>			current			= new List<AUIBase>();

	[SerializeField] RectTransform	attachRoot;
	public RectTransform			AttachRoot		=> attachRoot;

#if UNITY_EDITOR
	internal static void OpenPrefab(string path, Type uiType)
	{
		GameObject LoadPrefab(string path, Type uiType)
		{
			var go = FindObjectOfType( uiType );
			if( go != null )
			{
				return ((MonoBehaviour)go).gameObject;
			}

			var o = AssetDatabase.LoadAssetAtPath( $"Assets/Resources/{path}.prefab", typeof(GameObject) );
			if( o == null )
			{
				Debug.Log( $"UI를 불러올 수 없음: Assets/Resources/{path}.prefab" );
				return null;
			}

			go = o as GameObject;

			return PrefabUtility.InstantiatePrefab( go ) as GameObject;
		}

		GameObject _manager = LoadPrefab( rscPath, typeof(AnywhereUI) );
		var ui = LoadPrefab( path, uiType );
		
		AnywhereUI manager = _manager.GetComponent<AnywhereUI>();

		manager.transform.localPosition = Vector3.zero;
		manager.transform.localScale = Vector3.one;
		ui.transform.SetParent( manager.transform );
		ui.transform.localPosition = Vector3.zero;

		EditorGUIUtility.PingObject( manager );
		EditorGUIUtility.PingObject( ui );
	}
#endif

	public static T Open<T>() where T : AUIContainer<T>
	{
		Type t = typeof(T);

		AUIBase existUI = current.Find( curr=> Equals( t, curr.GetType() ) );
		if( existUI != null )
		{
			Debug.Log( string.Format( $"{typeof(T)}는 이미 열려있음" ) );
			return existUI as T;
		}
		
		AUIPath pathInfo = t.GetCustomAttribute<AUIPath>();

		var prefab = Resources.Load( pathInfo.path ) as GameObject;
		var go = Instantiate( prefab );
		go.transform.SetParent( instance.attachRoot );
		go.transform.localPosition = Vector3.zero;

		var ui = go.GetComponent<T>();
		current.Add( ui );

		return ui.OnOpen();
	}

	public static void Close<T>() where T : AUIContainer<T>
	{
		Type t = typeof(T);
		var ui = current.Find( curr=> Equals( t, curr.GetType() ) );
		if( ui != null )
		{
			current.Remove( ui );
			var uiInstance = ui.GetComponent<T>();
			uiInstance.OnClose();
			Destroy( uiInstance.gameObject );
		}
	}
}