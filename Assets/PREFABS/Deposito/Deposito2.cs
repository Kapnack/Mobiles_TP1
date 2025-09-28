using UnityEngine;
using System.Collections;

public class Deposito2 : MonoBehaviour 
{
	Player PjActual;
	public bool Vacio = true;
	public ControladorDeDescarga Contr1;
	public ControladorDeDescarga Contr2;
	
	Collider[] PjColl;
	
	//----------------------------------------------//

	void Start () 
	{
		Contr1 = GameObject.Find("ContrDesc1").GetComponent<ControladorDeDescarga>();
		Contr2 = GameObject.Find("ContrDesc2").GetComponent<ControladorDeDescarga>();
		
		Physics.IgnoreLayerCollision(8,9,false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!Vacio)
		{
			PjActual.transform.position = transform.position;
			PjActual.transform.forward = transform.forward;
		}
	}
	
	//----------------------------------------------//
	
	public void Soltar()
	{
		PjActual.SalirDelDeposito(transform.position + Vector3.up, transform.forward);
		
		Physics.IgnoreLayerCollision(8,9,false);
		
		PjActual = null;
		Vacio = true;
	}
	
	public void Entrar(Player pj)
	{
		if(pj.ConBolasas())
		{
			PjActual = pj;

			pj.EntrarAlDeposito(transform.position + Vector3.up, transform.forward);
			
			Vacio = false;
			
			Physics.IgnoreLayerCollision(8,9,true);
			
			Entro();
		}
	}

	private void Entro()
	{		
		if(PjActual.IdPlayer == 0)
			Contr1.Activar(this);
		else
			Contr2.Activar(this);
	}
}
