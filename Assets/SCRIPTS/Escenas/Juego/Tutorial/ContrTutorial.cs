using UnityEngine;
using System.Collections;

public class ContrTutorial : MonoBehaviour 
{
	public Player Pj;
	public float TiempTuto = 15;
	public float Tempo = 0;
	
	public bool Finalizado = false;
	bool Iniciado = false;
	
	GameManager GM;
	public void Start () 
	{
		GM = GameObject.Find("GameMgr").GetComponent<GameManager>();
		
		Pj.ContrTuto = this;
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
