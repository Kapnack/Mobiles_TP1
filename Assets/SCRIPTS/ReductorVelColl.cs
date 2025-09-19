using UnityEngine;
using System.Collections;

public class ReductorVelColl : MonoBehaviour 
{
	bool Usado = false;
	public string PlayerTag = "Player";

	private void OnCollisionEnter(Collision other)
	{
		if (!other.transform.CompareTag(PlayerTag)) 
			return;
		
		if(!Usado)
		{
			Chocado();
		}
	}

	protected virtual void Chocado()
	{
		Usado = true;
	}
}
