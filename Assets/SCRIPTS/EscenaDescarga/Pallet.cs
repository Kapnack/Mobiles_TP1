using UnityEngine;
using System.Collections;

public class Pallet : MonoBehaviour 
{
	public Valores Valor;
	public float Tiempo;

	private GameObject portador;
	public GameObject Portador
	{
		get => portador;
		set
		{
			portador = value;
			manoReceptora = value.GetComponent<ManoRecept>();
		}
	}
	public float TiempEnCinta = 1.5f;
	public float TempoEnCinta = 0;

	public enum Valores {Valor1 = 100000, 
						 Valor2 = 250000, 
						 Valor3 = 500000}
	
	
	public float TiempSmoot = 0.3f;
	float TempoSmoot = 0;
	public bool EnSmoot = false;
	
	ManoRecept manoReceptora;
	
	//----------------------------------------------//

	private void Start()
	{
		Pasaje();
	}

	private void LateUpdate ()
	{
		if (!Portador) 
			return;
		
		if(EnSmoot)
		{
			TempoSmoot += Time.deltaTime;
			if(TempoSmoot >= TiempSmoot)
			{
				EnSmoot = false;
				TempoSmoot = 0;
			}
			else
			{
				if(manoReceptora)
					transform.position = Portador.transform.position - Vector3.up * 1.2f;
				else
					transform.position = Vector3.Lerp(transform.position, Portador.transform.position, Time.deltaTime * 10);
			}
				
		}
		else
		{
			if(manoReceptora)
				transform.position = Portador.transform.position - Vector3.up * 1.2f;
			else
				transform.position = Portador.transform.position;
					
		}

	}
	
	//----------------------------------------------//
	
	public float GetBonus()
	{
		if(Tiempo > 0)
		{
			//calculo del bonus
		}
		return -1;
	}
	
	public void Pasaje()
	{
		EnSmoot = true;
		TempoSmoot = 0;
	}
}
