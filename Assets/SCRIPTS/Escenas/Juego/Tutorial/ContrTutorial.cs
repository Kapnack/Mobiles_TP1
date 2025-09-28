using UnityEngine;
using System.Collections;

public class ContrTutorial : MonoBehaviour 
{
	public Player Pj;

	public bool Finalizado = false;
	bool Iniciado = false;
	
	GameManager GM;
	public void Start () 
	{
		GM = GameObject.Find("GameMgr").GetComponent<GameManager>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<Player>() == Pj)
			Finalizar();
	}
	
	//------------------------------------------------------------------//
	
	public void Iniciar()
	{
		Pj.GetComponent<Frenado>().RestaurarVel();
		Iniciado = true;
	}

	private void Finalizar()
	{
		Finalizado = true;
		GM.FinTutorial(Pj.IdPlayer);
		Pj.GetComponent<Frenado>().Frenar();
		Pj.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
		Pj.VaciarInv();
	}
}
