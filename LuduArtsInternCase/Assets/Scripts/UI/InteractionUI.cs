using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionUI : MonoBehaviour
{
    public GameObject interactionWindow;
    public Image holdingBar;
    public TextMeshProUGUI interactionText;

    public void UpdateInteractionText(string newText)
    {
        interactionText.SetText(newText);
    }

    public void UpdateHoldingBar(float percent)
    {
        holdingBar.fillAmount = percent;
    }

    public void HideInteractionWindow()
    {
        holdingBar.fillAmount = 0;
        interactionWindow.SetActive(false);
    }
}
