using UnityEngine;

public class PyramidMission : MonoBehaviour
{
    [Header("ID de la misión")]
    public string missionID = "MISION_PIRAMIDE_01";

    [Header("Referencias")]
    public PyramidController pyramidController;
    public SymbolUI symbolUI;
    public GameObject symbolCanvas;
    public GameObject mapSymbolsParent;

    [Header("Configuración")]
    public int totalMapSymbols = 5;
    public int totalSlots = 5;

    private int collectedSymbols = 0;
    private int visitedSlots = 0;
    private bool missionActive = false;
    private bool phaseSlots = false;

    private void Start()
    {
        ResetMission();
    }

    // 🔹 Iniciar misión desde NPC, botón o trigger
    public void StartMission()
    {
        if (missionActive) return;

        missionActive = true;
        collectedSymbols = 0;
        visitedSlots = 0;
        phaseSlots = false;

        // Activar canvas
        if (symbolCanvas != null)
            symbolCanvas.SetActive(true);

        // Reset UI (todos los símbolos ocultos hasta recolectar)
        if (symbolUI != null)
            symbolUI.ResetUI();

        // Activar símbolos en mapa
        if (mapSymbolsParent != null)
        {
            mapSymbolsParent.SetActive(true);
            foreach (Transform child in mapSymbolsParent.transform)
                child.gameObject.SetActive(true);
        }

        // Reiniciar pirámide
        if (pyramidController != null)
            pyramidController.ResetPyramid();

        // Reiniciar SymbolManager
        if (SymbolManager.Instance != null)
            SymbolManager.Instance.ResetManager();

        Debug.Log("[PyramidMission] Misión iniciada");
    }

    // 🔹 Llamado al recolectar símbolo en mapa
    public void CollectMapSymbol(MathSymbolType symbol)
    {
        if (!missionActive || phaseSlots) return;

        collectedSymbols++;

        // Activar símbolo en UI
        if (symbolUI != null)
            symbolUI.ActivateSymbol(symbol);

        Debug.Log($"[PyramidMission] Símbolos recolectados: {collectedSymbols}/{totalMapSymbols}");

        if (collectedSymbols >= totalMapSymbols)
        {
            phaseSlots = true;
            ActivatePyramidSlots(); // Mostrar objetos de los slots
            Debug.Log("[PyramidMission] Fase de slots activada");
        }
    }

    // 🔹 Llamado al pasar por un slot
    public void VisitPyramidSlot(MathSymbolType symbol)
    {
        if (!missionActive || !phaseSlots) return;

        visitedSlots++;

        // Ocultar símbolo en UI
        if (symbolUI != null)
            symbolUI.DeactivateSymbol(symbol);

        // Mostrar símbolo en pirámide
        if (pyramidController != null)
            pyramidController.ShowSymbol(symbol);

        Debug.Log($"[PyramidMission] Slot visitado: {visitedSlots}/{totalSlots}");

        if (visitedSlots >= totalSlots)
            CompleteMission();
    }

    // 🔹 Activar todos los prefabs de los slots
    private void ActivatePyramidSlots()
    {
        if (pyramidController == null) return;

        foreach (var slot in pyramidController.symbolSlots)
        {
            // Instanciar prefab si no existe
            if (slot.currentSymbol == null && slot.symbolPrefab != null)
                slot.currentSymbol = Instantiate(slot.symbolPrefab, slot.slot.position, slot.slot.rotation, slot.slot);

            // Mostrar el objeto
            if (slot.currentSymbol != null)
                slot.currentSymbol.SetActive(true);

            // Permitir colocarlo de nuevo
            slot.occupied = false;
        }

        Debug.Log("[PyramidMission] Slots de la pirámide activados");
    }

    // 🔹 Completar misión
    private void CompleteMission()
    {
        missionActive = false;
        phaseSlots = false;

        if (symbolCanvas != null)
            symbolCanvas.SetActive(false);

        if (symbolUI != null)
            symbolUI.ResetUI();

        if (pyramidController != null)
            pyramidController.ResetPyramid();

        if (mapSymbolsParent != null)
            mapSymbolsParent.SetActive(false);

        if (SymbolManager.Instance != null)
            SymbolManager.Instance.ResetManager();

        Debug.Log("[PyramidMission] MISIÓN COMPLETADA");
    }

    // 🔹 Reinicio manual de la misión
    public void ResetMission()
    {
        missionActive = false;
        phaseSlots = false;
        collectedSymbols = 0;
        visitedSlots = 0;

        if (symbolCanvas != null)
            symbolCanvas.SetActive(false);

        if (symbolUI != null)
            symbolUI.ResetUI();

        if (mapSymbolsParent != null)
            mapSymbolsParent.SetActive(false);

        if (pyramidController != null)
        {
            foreach (var slot in pyramidController.symbolSlots)
            {
                if (slot.currentSymbol != null)
                    slot.currentSymbol.SetActive(false);
                slot.occupied = false;
            }
        }

        if (SymbolManager.Instance != null)
            SymbolManager.Instance.ResetManager();

        Debug.Log("[PyramidMission] Misión reiniciada");
    }

    // 🔹 Estado de la misión
    public bool IsMissionActive() => missionActive;
}
