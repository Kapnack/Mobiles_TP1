using UnityEngine;
using System.Collections;

public class Obstaculo : MonoBehaviour 
{
	public float TiempEmpDesapa = 1;
	float Tempo1 = 0;
	public float TiempDesapareciendo = 1;
	float Tempo2 = 0;
	public string PlayerTag = "Player";
	
	bool Chocado = false;
	bool Desapareciendo = false;

	private Rigidbody rb;
	private Collider collider;
	
	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		collider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Chocado)
		{
			Tempo1 += T.GetDT();
			if(Tempo1 > TiempEmpDesapa)
			{
				Chocado = false;
				Desapareciendo = true;
				rb.useGravity = false;
				collider.enabled = false;
			}
		}
		
		if(Desapareciendo)
		{
			//animacion de desaparecer
			
			Tempo2 += T.GetDT();
			if(Tempo2 > TiempDesapareciendo)
			{
				gameObject.SetActiveRecursively(false);
			}
		}
	}

	private void OnCollisionEnter(Collision coll)
	{
		if(coll.transform.CompareTag(PlayerTag))
		{
			Chocado = true;
		}
	}
	
	//------------------------------------------------//
}
