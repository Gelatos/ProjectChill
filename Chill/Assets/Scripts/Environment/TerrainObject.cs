using UnityEngine;
using System.Collections;

public class TerrainObject : MonoBehaviour {

	[SerializeField]
	protected Vector2[] terrainUVs = new Vector2[4];

	public Vector2[] TerrainUVs {
		get {
			return terrainUVs;
		} set {
			terrainUVs = value;
//			GetComponent<SpriteRenderer>();
		}
	}
}
