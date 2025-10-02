using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Estadisticas : MonoBehaviour
{
    [SerializeField] private bool UnSoloJugador;
    private int CantBolsasMax = 3;

    private float CooldownFlash = 0.2f;

    [SerializeField] private Player Camion;

    [SerializeField] private List<GameObject> ProgresoBolsas;
    [SerializeField] private GameObject FinalBolsas;
    [SerializeField] private TMP_Text TextoDinero;
    private string FormatoTexto;

    private float TimerFlash = 0f;
    private Coroutine flashCorrutina;

    [SerializeField] private GameObject[] Botones;

    private void Awake()
    {
#if UNITY_ANDROID || UNITY_IOS
        foreach (var obj in Botones)
            obj.SetActive(true);
#else
        foreach (var obj in Botones)
            obj.SetActive(false);
#endif

        if (UnSoloJugador && GameplaySettingsManager.Instance.IsMultiplayer ||
            !UnSoloJugador && !GameplaySettingsManager.Instance.IsMultiplayer)
            Destroy(gameObject);


        if (Camion == null)
        {
            Debug.LogWarning("Falta el Player en " + gameObject.name);
            return;
        }

        CantBolsasMax = Camion.Bolasas.Length;
        if (TextoDinero == null)
            Debug.LogError("Falta el TextoDinero en " + gameObject.name);

        FormatoTexto = TextoDinero.text;
        TextoDinero.text = string.Format(FormatoTexto, 0);
    }

    private void Update()
    {
        if (!Camion)
            return;

        TextoDinero.text = string.Format(FormatoTexto, Camion.Dinero);

        if (Camion.CantBolsAct == CantBolsasMax)
            flashCorrutina ??= StartCoroutine(Flash());

        if (Camion.CantBolsAct > 0)
        {
            ProgresoBolsas[Camion.CantBolsAct - 1].SetActive(false);
            ProgresoBolsas[Camion.CantBolsAct].SetActive(true);
        }
        else
        {
            if (!ProgresoBolsas[0].activeSelf)
            {
                foreach (var item in ProgresoBolsas)
                {
                    item.SetActive(false);
                }

                ProgresoBolsas[0].SetActive(true);
            }
        }
    }

    private IEnumerator Flash()
    {
        while (Camion.CantBolsAct == CantBolsasMax)
        {
            FinalBolsas.SetActive(!FinalBolsas.activeSelf);
            yield return new WaitForSeconds(CooldownFlash);
        }

        FinalBolsas.SetActive(false);

        flashCorrutina = null;
    }
}