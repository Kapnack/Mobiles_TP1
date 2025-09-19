using UnityEngine;
using System.Collections;

public class ManejoPallets : MonoBehaviour 
{
	protected struct PalletsComponents
	{
		public Renderer renderer;
		public Pallet script;
	}	
	protected System.Collections.Generic.List<PalletsComponents> Pallets = new();
	public ControladorDeDescarga Controlador;

	public virtual bool Recibir(Pallet pallet)
	{
		var newPallet = new PalletsComponents
		{
			script = pallet,
			renderer = pallet.GetComponent<Renderer>()
		};

		Debug.Log(gameObject.name+" / Recibir()");
		Pallets.Add(newPallet);
		newPallet.script.Pasaje();
		
		return true;
	}
	
	public bool Tenencia()
	{
		if(Pallets.Count != 0)
			return true;
		
		return false;
	}
	
	public virtual void Dar(ManejoPallets receptor)
	{
		//es el encargado de decidir si le da o no la bolsa
	}
}
