using UnityEngine;

namespace Manager
{
	public class WayPointManager : MonoBehaviour
	{
		public static Transform[] positions;

		void Awake()
		{
			positions = new Transform[transform.childCount];
			for (int i = 0; i < positions.Length; i++)
			{
				positions[i] = transform.GetChild(i);
			}
		}

	}
}