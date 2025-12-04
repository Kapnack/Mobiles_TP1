using UnityEngine;
using System.Collections;
using Particle;
using Systems;
using Systems.Pool;

public class Bolsa : MonoBehaviour
{
    public Pallet.Valores Monto;

    public string TagPlayer = "";
    Player Pj = null;

    bool Desapareciendo;
    public GameObject Particulas;
    public float TiempParts = 2.5f;

    private Renderer _renderer;
    private Collider _collider;

    private IObjectPool<ParticleController> _pool;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();
    }

    private void Start()
    {
        Monto = Pallet.Valores.Valor2;

        _pool = ServiceProvider.GetService<IObjectPool<ParticleController>>();

        if (Particulas != null)
            Particulas.SetActive(false);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (!coll.CompareTag(TagPlayer))
            return;

        Pj = coll.GetComponent<Player>();

        if (Pj.AgregarBolsa(this))
            Desaparecer();
    }

    public async void Desaparecer()
    {
        PoolData<ParticleController> particleGo = await _pool.Get(Particulas);

        particleGo.Obj.transform.position = transform.position;
        particleGo.Obj.transform.rotation = transform.rotation;

        particleGo.Component.SetUp(() => _pool.Return(particleGo));

        _renderer.enabled = false;
        _collider.enabled = false;

        if (Particulas != null)
        {
            Particulas.GetComponent<ParticleSystem>().Play();
        }
    }
}