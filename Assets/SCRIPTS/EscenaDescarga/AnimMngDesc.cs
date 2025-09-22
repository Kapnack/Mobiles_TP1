using UnityEngine;
using System.Collections;

public class AnimMngDesc : MonoBehaviour 
{
	public string AnimEntrada = "Entrada";
	public string AnimSalida = "Salida";
	public ControladorDeDescarga ContrDesc;
	
	enum AnimEnCurso{Salida,Entrada,Nada}
	AnimEnCurso AnimAct = AnimEnCurso.Nada;
	
	public GameObject PuertaAnimada;

	private Animation animation;
	private Animation puertaAnimadaAnimation;
	
	private void Awake ()
	{
		animation = GetComponent<Animation>();
		puertaAnimadaAnimation = PuertaAnimada.GetComponent<Animation>();
	}
	
	private void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Z))
			Entrar();
		if(Input.GetKeyDown(KeyCode.X))
			Salir();
		
		switch(AnimAct)
		{
		case AnimEnCurso.Entrada:
			
			if(!animation.IsPlaying(AnimEntrada))
			{
				AnimAct = AnimMngDesc.AnimEnCurso.Nada;
				ContrDesc.FinAnimEntrada();
				print("fin Anim Entrada");
			}
			
			break;
			
		case AnimEnCurso.Salida:
			
			if(!animation.IsPlaying(AnimSalida))
			{
				AnimAct = AnimEnCurso.Nada;
				ContrDesc.FinAnimSalida();
				print("fin Anim Salida");
			}
			
			break;
			
		case AnimEnCurso.Nada:
			break;
		}
	}
	
	public void Entrar()
	{
		AnimAct = AnimMngDesc.AnimEnCurso.Entrada;
		animation.Play(AnimEntrada);
		
		if(PuertaAnimada)
		{
			puertaAnimadaAnimation["AnimPuerta"].time = 0;
			puertaAnimadaAnimation["AnimPuerta"].speed = 1;
			puertaAnimadaAnimation.Play("AnimPuerta");
		}
	}
	
	public void Salir()
	{
		AnimAct = AnimEnCurso.Salida;	
		animation.Play(AnimSalida);
		
		if(PuertaAnimada)
		{
			puertaAnimadaAnimation["AnimPuerta"].time = puertaAnimadaAnimation["AnimPuerta"].length;
			puertaAnimadaAnimation["AnimPuerta"].speed = -1;
			puertaAnimadaAnimation.Play("AnimPuerta");
		}
	}
}
