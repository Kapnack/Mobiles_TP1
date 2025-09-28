using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// clase encargada de TODA la visualizacion
/// de cada player, todo aquello que corresconda a 
/// cada seccion de la pantalla independientemente
/// </summary>
public class Visualizacion : MonoBehaviour
{
    public enum Lado
    {
        Izq,
        Der
    }

    public Lado LadoAct;

    ControlDireccion Direccion;
    Player Pj;

    //las distintas camaras
    public Camera CamCalibracion;
    public Camera CamConduccion;
    public Camera CamDescarga;


    //EL DINERO QUE SE TIENE
    public Vector2[] DinPos;
    public Vector2 DinEsc = Vector2.zero;

    //EL VOLANTE
    public Vector2[] VolantePos;
    public float VolanteEsc = 0;


    //PARA EL INVENTARIO
    public Vector2[] FondoPos;
    public Vector2 FondoEsc = Vector2.zero;

    //public Vector2 SlotsEsc = Vector2.zero;
    //public Vector2 SlotPrimPos = Vector2.zero;
    //public Vector2 Separacion = Vector2.zero;

    //public int Fil = 0;
    //public int Col = 0;

    public Texture2D TexturaVacia; //lo que aparece si no hay ninguna bolsa
    public Texture2D TextFondo;

    public float Parpadeo = 0.8f;
    public float TempParp = 0;
    public bool PrimIma = true;

    public Texture2D[] TextInvIzq;
    public Texture2D[] TextInvDer;

    //BONO DE DESCARGA
    public Vector2 BonusPos = Vector2.zero;
    public Vector2 BonusEsc = Vector2.zero;

    public Color32 ColorFondoBolsa;
    public Vector2 ColorFondoPos = Vector2.zero;
    public Vector2 ColorFondoEsc = Vector2.zero;

    public Vector2 ColorFondoFondoPos = Vector2.zero;
    public Vector2 ColorFondoFondoEsc = Vector2.zero;


    //CALIBRACION MAS TUTO BASICO
    public Vector2 ReadyPos = Vector2.zero;
    public Vector2 ReadyEsc = Vector2.zero;
    public Texture2D[] ImagenesDelTuto;
    public float Intervalo = 0.8f; //tiempo de cada cuanto cambia de imagen
    float TempoIntTuto = 0;
    int EnCurso = -1;
    public Texture2D ImaEnPosicion;
    public Texture2D ImaReady;

    //NUMERO DEL JUGADOR
    public Texture2D TextNum1;
    public Texture2D TextNum2;

    public GameObject Techo;
    public Renderer techoRenderer;

    Rect R;

    //------------------------------------------------------------------//

    // Use this for initialization
    private void Start()
    {
        TempoIntTuto = Intervalo;
        Direccion = GetComponent<ControlDireccion>();
        Pj = GetComponent<Player>();

        techoRenderer = Techo.GetComponent<Renderer>();
    }

    //--------------------------------------------------------//

    public void CambiarACalibracion()
    {
        CamCalibracion.enabled = true;
        CamConduccion.enabled = false;
        CamDescarga.enabled = false;
    }

    public void CambiarATutorial()
    {
        CamCalibracion.enabled = false;
        CamConduccion.enabled = true;
        CamDescarga.enabled = false;
    }

    public void CambiarAConduccion()
    {
        CamCalibracion.enabled = false;
        CamConduccion.enabled = true;
        CamDescarga.enabled = false;
    }

    public void CambiarADescarga()
    {
        CamCalibracion.enabled = false;
        CamConduccion.enabled = false;
        CamDescarga.enabled = true;
    }

    //---------//

    public void SetLado(Lado lado)
    {
        LadoAct = lado;

        Rect r = new Rect
        {
            width = CamConduccion.rect.width,
            height = CamConduccion.rect.height,
            y = CamConduccion.rect.y
        };

        r.x = lado switch
        {
            Lado.Der => 0.5f,
            Lado.Izq => 0,
            _ => r.x
        };

        CamCalibracion.rect = r;
        CamConduccion.rect = r;
        CamDescarga.rect = r;

        techoRenderer.material.mainTexture = LadoAct == Lado.Izq ? TextNum1 : TextNum2;
    }

    public string PrepararNumeros(int dinero)
    {
        string strDinero = dinero.ToString();
        string res = "";

        if (dinero < 1) //sin ditero
        {
            res = "";
        }
        else if (strDinero.Length == 6) //cientos de miles
        {
            for (int i = 0; i < strDinero.Length; i++)
            {
                res += strDinero[i];

                if (i == 2)
                {
                    res += ".";
                }
            }
        }
        else if (strDinero.Length == 7) //millones
        {
            for (int i = 0; i < strDinero.Length; i++)
            {
                res += strDinero[i];

                if (i == 0 || i == 3)
                {
                    res += ".";
                }
            }
        }

        return res;
    }
}