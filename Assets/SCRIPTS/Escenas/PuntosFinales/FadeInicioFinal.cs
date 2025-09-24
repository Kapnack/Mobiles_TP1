using System;
using UnityEngine;

public class FadeInicioFinal : MonoBehaviour 
{
    public float Duracion = 2;
    float TiempInicial;
	
    private MngPts Mng;
	
    private Color materialColor;
    private Renderer renderer;
	
    private bool MngAvisado;
	
    [Obsolete("Obsolete")]
    private void Start ()
    {
        Mng = (MngPts)FindObjectOfType(typeof (MngPts));
        TiempInicial = Mng.TiempEspReiniciar;
		
        renderer = GetComponent<Renderer>();
        
        materialColor = GetComponent<Renderer>().material.color;
        materialColor.a = 0;
        
        renderer.material.color = materialColor;
    }
	
    private void Update () 
    {
        if(Mng.TiempEspReiniciar > TiempInicial - Duracion)//aparicion
        {
            materialColor = renderer.material.color;
            materialColor.a += Time.deltaTime / Duracion;
            renderer.material.color = materialColor;
        }
        else if(Mng.TiempEspReiniciar < Duracion)//desaparicion
        {
            materialColor = renderer.material.color;
            materialColor.a -= Time.deltaTime / Duracion;
            renderer.material.color = materialColor;
			
            if(!MngAvisado)
            {
                MngAvisado = true;
                Mng.DesaparecerGUI();
            }
        }
				
    }
}