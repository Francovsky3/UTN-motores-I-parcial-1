using TMPro;
using UnityEngine;

public class UICounter : MonoBehaviour
{
    int totalPacks;
    int deliveredPacks;
    [SerializeField] TextMeshProUGUI UiCounter;

    void OnEnable()
    {
        Mailbox.Delivered += PackDelivered;
    }

    void Update()
    {
        UiCounter.SetText(deliveredPacks + "/" + totalPacks + " entregados");
    }

    public void SetTotalPacks(int value)
    {
        totalPacks = value;
    }

    void PackDelivered()
    {
        deliveredPacks++;
    }
}
