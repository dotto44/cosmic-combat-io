using System.Collections;
using System.Collections.Generic;
using UnityEngine;

	public class Done_BGScroller : MonoBehaviour
	{
		public float scrollSpeed;
		public float tileSizeZ;
     	public float offsetSpeed;

		private Vector3 startPosition;

		void Start ()
		{
			startPosition = transform.position;
		}

		void Update ()
		{
			float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
			transform.position = startPosition + Vector3.left * newPosition;
		}
	}
