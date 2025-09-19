using UnityEngine;

public class EstantePartida : ManejoPallets
{
	public void OnTriggerEnter(Collider other)
	{
		ManejoPallets recept = other.GetComponent<ManejoPallets>();
		if(recept != null)
		{
			Dar(recept);
		}
	}
	
	//------------------------------------------------------------//
	
	public override void Dar(ManejoPallets receptor)
	{
        if (receptor.Recibir(Pallets[0].script)) {
            Pallets.RemoveAt(0);
        }
    }
	
	public override bool Recibir (Pallet pallet)
	{
		//pallet.CintaReceptora = CintaReceptora.gameObject;
		pallet.Portador = gameObject;
		return base.Recibir (pallet);
	}
}
