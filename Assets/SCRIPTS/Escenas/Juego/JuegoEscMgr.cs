using UnityEngine;
using System.Collections;

public class JuegoEscMgr : MonoBehaviour 
{
	bool JuegoFinalizado = false;
	public float TiempoEsperaFin = 25;//tiempo que espera la aplicacion para volver al video introductorio desp de terminada la partida
	float Tempo;
	
	bool JuegoIniciado = false;
	public float TiempoEsperaInicio = 120; //tiempo que espera la aplicacion para volver al video introductorio desp de terminada la partida

	private void Update () 
	{
		if(JuegoFinalizado)
		{
			Tempo += Time.deltaTime;
			if(Tempo > TiempoEsperaFin)
			{
				Tempo = 0;
				Application.LoadLevel(0);
			}
		}
		
		if(!JuegoIniciado)
		{
			if(Tempo > TiempoEsperaInicio)
			{
				Application.LoadLevel(0);
			}
		}		
		
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		
		//reinicia
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
