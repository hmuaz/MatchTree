using UnityEngine;
using System.Collections;

public class Collectible : GamePiece 
{
	public bool clearedByBomb = false;
	public bool clearedAtBottom = true;

	void Start () 
	{
		matchValue = MatchValue.None;
	}

}
