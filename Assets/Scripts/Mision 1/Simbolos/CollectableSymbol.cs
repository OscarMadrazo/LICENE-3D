using UnityEngine;

public class CollectableSymbol : MonoBehaviour
{
    public MathSymbolType symbolType;
    public PyramidMission mission;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (mission == null || !mission.IsMissionActive()) return;

        // Evitar errores si SymbolManager no existe
        if (SymbolManager.Instance != null)
            SymbolManager.Instance.CollectSymbol(symbolType);

        mission.CollectMapSymbol(symbolType);

        // Desactivar temporalmente
        gameObject.SetActive(false);
    }
}
