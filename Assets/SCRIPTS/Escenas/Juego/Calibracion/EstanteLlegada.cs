public class EstanteLlegada : ManejoPallets
{
	public ContrCalibracion ContrCalib;
	
	public override bool Recibir(Pallet p)
	{
        p.Portador = gameObject;
        base.Recibir(p);
        ContrCalib.FinTutorial();

        return true;
    }
}
