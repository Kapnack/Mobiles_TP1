using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescargaHUD : MonoBehaviour
{
    [SerializeField] private bool UnSoloJugador;
    [SerializeField] private Player Pj;
    [SerializeField] private Image Relleno;
    [SerializeField] private int MaxBonus = 0;

    [SerializeField] private GameObject Boton;

    [SerializeField] private TMP_Text TextoBonus;
    private string formatoTexto;

    private void Awake()
    {
#if UNITY_ANDROID || UNITY_IOS
        Boton.SetActive(true);
#else
         Boton.SetActive(false);
#endif

        if (UnSoloJugador && GameplaySettingsManager.Instance.IsMultiplayer ||
            !UnSoloJugador && !GameplaySettingsManager.Instance.IsMultiplayer)
            Destroy(gameObject);

        formatoTexto = TextoBonus.text;
        TextoBonus.text = String.Format(formatoTexto, 0);
    }

    private void Update()
    {
        if (Pj.ContrDesc?.PEnMov)
        {
            MaxBonus = (int)Pj.ContrDesc.PEnMov.Valor;

            var fill = Mathf.Clamp01(Pj.ContrDesc.Bonus / MaxBonus);

            Relleno.fillAmount = Mathf.Lerp(Relleno.fillAmount, fill, Time.deltaTime * 5f);

            TextoBonus.text = String.Format(formatoTexto, Pj.ContrDesc.Bonus);
        }
        else
        {
            TextoBonus.text = String.Format(formatoTexto, 0);
        }
    }
}