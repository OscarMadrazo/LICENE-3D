using UnityEngine;

public class PyramidSlotTrigger : MonoBehaviour
{
    public MathSymbolType slotSymbol;
    public PyramidMission mission;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (mission == null || !mission.IsMissionActive()) return;

        mission.VisitPyramidSlot(slotSymbol);
    }
}
