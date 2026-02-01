using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    public GameObject interactionWindow;
    public Image holdingBar;
    public TextMeshProUGUI interactionText;
    public Image crosshairDefault;
    public Image crosshairHover;


    private void Start()
    {
        ResetInteractionWindow();
    }

    public void UpdateInteractionText(string newText)
    {
        interactionText.SetText(newText);
    }

    public void UpdateHoldingBar(float percent)
    {
        holdingBar.fillAmount = percent;
    }


    public void ShorHoverCrosshair()
    {
        crosshairDefault.enabled = false;
        crosshairHover.enabled = true;
    }


    public void ResetInteractionWindow()
    {
        holdingBar.fillAmount = 0;
        interactionText.SetText("");
        crosshairDefault.enabled = true;
        crosshairHover.enabled = false;
    }
}
