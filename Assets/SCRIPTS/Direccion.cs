using UnityEngine;
using System.Collections;

public class Direccion : MonoBehaviour
{
    public Transform ManoDer;
    public Transform ManoIzq;

    public float DifMin = 0;
    public float DifMax = 0;
    public float Sensibilidad = 1;

    public Transform Camion; //lo que va a conducir
    public Transform Volante;


    enum Sentido
    {
        Der,
        Izq
    }

    Sentido DirAct;
    float Diferencia;
    Vector3 Aux;

    //---------------------------------------------------------//

    private void Update()
    {
        if (ManoIzq.position.y > ManoDer.position.y)
        {
            DirAct = Sentido.Der;
            Diferencia = ManoIzq.position.y - ManoDer.position.y;
        }
        else
        {
            DirAct = Sentido.Izq;
            Diferencia = ManoDer.position.y - ManoIzq.position.y;
        }

        if (Diferencia > DifMin && Diferencia < DifMax)
        {
            if (DirAct == Sentido.Der)
            {
                Camion.rotation *= Quaternion.AngleAxis(Diferencia * Sensibilidad * Time.deltaTime, Camion.up);
                Volante.localRotation *= Quaternion.AngleAxis(Diferencia * Sensibilidad * Time.deltaTime, Vector3.up);
            }
            else
            {
                Camion.rotation *= Quaternion.AngleAxis((-1) * Diferencia * Sensibilidad * Time.deltaTime, Camion.up);
                Volante.localRotation *=
                    Quaternion.AngleAxis((-1) * Diferencia * Sensibilidad * Time.deltaTime, Vector3.up);
            }
        }
    }
}