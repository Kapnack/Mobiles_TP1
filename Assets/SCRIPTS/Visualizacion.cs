using UnityEngine;

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

    //las distintas camaras
    public Camera CamCalibracion;
    public Camera CamConduccion;
    public Camera CamDescarga;

    int EnCurso = -1;

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
        GetComponent<ControlDireccion>();
        GetComponent<Player>();

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

        if (techoRenderer)
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