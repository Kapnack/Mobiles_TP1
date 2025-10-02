using UnityEngine;
using System.Collections;

public class ContrTutorial : MonoBehaviour 
{
	public Player Pj;

	bool Iniciado = false;

	public void Start () 
	{
		GameObject.Find("GameMgr").GetComponent<GameManager>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Player>() == Pj)
			Finalizar();
	}
	
	//------------------------------------------------------------------//

	private void Finalizar()
	{
		Pj.GetComponent<Frenado>().Frenar();
		Pj.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
		Pj.VaciarInv();
	}
}
