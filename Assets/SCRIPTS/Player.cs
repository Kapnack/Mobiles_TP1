using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public int Dinero = 0;
	public int IdPlayer = 0;
	
	public Bolsa[] Bolasas;
	int CantBolsAct = 0;

	public enum Estados{EnDescarga, EnConduccion, EnCalibracion, EnTutorial}
	public Estados EstAct = Estados.EnConduccion;

	public ControladorDeDescarga ContrDesc;
	public ContrCalibracion ContrCalib;
	public ContrTutorial ContrTuto;
	
	Visualizacion MiVisualizacion;
	
	//------------------------------------------------------------------//

	// Use this for initialization
	private void Start () 
	{
		for(int i = 0; i< Bolasas.Length;i++)
			Bolasas[i] = null;
		
		MiVisualizacion = GetComponent<Visualizacion>();
	}
	
	//------------------------------------------------------------------//
	
	public bool AgregarBolsa(Bolsa b)
	{
		if(CantBolsAct + 1 <= Bolasas.Length)
		{
			Bolasas[CantBolsAct] = b;
			CantBolsAct++;
			Dinero += (int)b.Monto;
			b.Desaparecer();
			return true;
		}

		return false;
	}
	
	public void VaciarInv()
	{
		for(int i = 0; i< Bolasas.Length;i++)
			Bolasas[i] = null;
		
		CantBolsAct = 0;
	}
	
	public bool ConBolasas()
	{
		for(int i = 0; i< Bolasas.Length;i++)
		{
			if(Bolasas[i] != null)
			{
				return true;
			}
		}
		
		return false;
	}
	
	public void SetContrDesc(ControladorDeDescarga contr)
	{
		ContrDesc = contr;
	}

	public void CambiarACalibracion()
	{
		MiVisualizacion.CambiarACalibracion();
		EstAct = Player.Estados.EnCalibracion;
	}
	
	public void CambiarATutorial()
	{
		MiVisualizacion.CambiarATutorial();
		EstAct = Player.Estados.EnTutorial;
		ContrTuto.Iniciar();
	}
	
	public void CambiarAConduccion()
	{
		MiVisualizacion.CambiarAConduccion();
		EstAct = Player.Estados.EnConduccion;
	}
	
	public void CambiarADescarga()
	{
		MiVisualizacion.CambiarADescarga();
		EstAct = Estados.EnDescarga;
	}
	
	public void SacarBolasa()
	{
		for(int i = 0; i < Bolasas.Length; i++)
		{
			if(Bolasas[i])
			{
				Bolasas[i] = null;
				return;
			}				
		}
	}
	
	
}
