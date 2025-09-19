using UnityEngine;
using System.Collections;

public class Cinta : ManejoPallets
{
    public bool Encendida; //lo que hace la animacion
    bool ConPallet = false;
    public float Velocidad = 1;
    public GameObject Mano;
    public float Tiempo = 0.5f;
    float Tempo = 0;
    Transform ObjAct = null;

    //animacion de parpadeo
    public float Intervalo = 0.7f;
    public float Permanencia = 0.2f;
    float AnimTempo = 0;
    public GameObject ModelCinta;
    public Color32 ColorParpadeo;
    Color32 ColorOrigModel;


    private Renderer ModelCintaRenderer;
    //------------------------------------------------------------//

    private void Awake()
    {
        ModelCintaRenderer = ModelCinta.GetComponent<Renderer>();
    }

    private void Start()
    {
        ColorOrigModel = ModelCinta.GetComponent<Renderer>().material.color;
    }

    private void Update()
    {
        //animacion de parpadeo
        if (Encendida)
        {
            AnimTempo += T.GetDT();
            if (AnimTempo > Permanencia)
            {
                if (ModelCintaRenderer.material.color == ColorParpadeo)
                {
                    AnimTempo = 0;
                    ModelCintaRenderer.material.color = ColorOrigModel;
                }
            }

            if (AnimTempo > Intervalo)
            {
                if (ModelCintaRenderer.material.color == ColorOrigModel)
                {
                    AnimTempo = 0;
                    ModelCintaRenderer.material.color = ColorParpadeo;
                }
            }
        }

        //movimiento del pallet
        for (int i = 0; i < Pallets.Count; i++)
        {
            if (Pallets[i].renderer.enabled)
            {
                if (!Pallets[i].script.EnSmoot)
                {
                    Pallets[i].script.enabled = false;
                    Pallets[i].script.TempoEnCinta += T.GetDT();

                    Pallets[i].script.transform.position += transform.right * (Velocidad * T.GetDT());
                    Vector3 vAux = Pallets[i].script.transform.localPosition;
                    vAux.y = 3.61f; //altura especifica
                    Pallets[i].script.transform.localPosition = vAux;

                    if (Pallets[i].script.TempoEnCinta >= Pallets[i].script.TiempEnCinta)
                    {
                        Pallets[i].script.TempoEnCinta = 0;
                        ObjAct.gameObject.SetActiveRecursively(false);
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        ManejoPallets recept = other.GetComponent<ManejoPallets>();
        if (recept != null)
        {
            Dar(recept);
        }
    }


    //------------------------------------------------------------//

    public override bool Recibir(Pallet p)
    {
        Tempo = 0;
        Controlador.LlegadaPallet(p);
        p.Portador = this.gameObject;
        ConPallet = true;
        ObjAct = p.transform;
        base.Recibir(p);
        //p.GetComponent<Pallet>().enabled = false;
        Apagar();

        return true;
    }

    public void Encender()
    {
        Encendida = true;
         ModelCintaRenderer.material.color = ColorOrigModel;
    }

    private void Apagar()
    {
        Encendida = false;
        ConPallet = false;
        ModelCintaRenderer.material.color = ColorOrigModel;
    }
}