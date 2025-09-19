using UnityEngine;
using System.Collections;

public class AcelerAuto : MonoBehaviour
{
    public float AcelPorSeg = 0;
    float Velocidad = 0;
    public float VelMax = 0;
    ReductorVelColl Obstaculo = null;


    bool Avil = true;
    public float TiempRecColl = 0;
    float Tempo = 0;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Avil)
        {
            Tempo += Time.deltaTime;
            if (Tempo > TiempRecColl)
            {
                Tempo = 0;
                Avil = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Velocidad < VelMax)
        {
            Velocidad += AcelPorSeg * Time.fixedDeltaTime;
        }

        rb.AddForce(this.transform.forward * Velocidad);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!Avil)
        {
            Obstaculo = collision.transform.GetComponent<ReductorVelColl>();
            if (Obstaculo != null)
            {
                rb.linearVelocity /= 2;
            }

            Obstaculo = null;
        }
    }
}