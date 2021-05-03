using System;
using UnityEngine;

namespace AUI.Internal
{
	public class AUIPath : Attribute
	{
		public string path;
		public AUIPath(string resourcePath) => path = resourcePath;
	}

	public class AUIBase : MonoBehaviour 
	{ 
		RectTransform			_rectTransform;
		public RectTransform	rectTransform	=> _rectTransform ?? ( _rectTransform = GetComponent<RectTransform>() );
	}

	public abstract class AUIContainer<T> : AUIBase where T : AUIContainer<T>
	{
		public abstract T OnOpen();
		public virtual void OnClose(){ }
	}
}