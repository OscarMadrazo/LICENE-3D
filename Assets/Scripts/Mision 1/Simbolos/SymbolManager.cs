using System.Collections.Generic;
using UnityEngine;

public class SymbolManager : MonoBehaviour
{
    // Singleton para acceder desde cualquier parte
    public static SymbolManager Instance;

    // Guarda los símbolos recolectados
    private HashSet<MathSymbolType> collectedSymbols = new HashSet<MathSymbolType>();

    [Header("Referencias")]
    public SymbolUI symbolUI;           // Tu UI de símbolos
    public PyramidController pyramid;   // La pirámide donde se muestran los símbolos

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // 🔹 Llamar cada vez que el jugador recoge un símbolo
    public void CollectSymbol(MathSymbolType symbol)
    {
        // Evita duplicados
        if (collectedSymbols.Contains(symbol))
            return;

        collectedSymbols.Add(symbol);

        // Actualiza la UI del símbolo
        if (symbolUI != null)
            symbolUI.ActivateSymbol(symbol);

        // Muestra el símbolo en la pirámide
        if (pyramid != null)
            pyramid.ShowSymbol(symbol);
    }

    // 🔹 Saber si el jugador ya tiene un símbolo
    public bool HasSymbol(MathSymbolType symbol)
    {
        return collectedSymbols.Contains(symbol);
    }

    // 🔹 Reinicia los símbolos (para reiniciar misión o pruebas)
    public void ResetSymbols()
    {
        collectedSymbols.Clear();

        if (symbolUI != null)
            symbolUI.ResetUI();

        if (pyramid != null)
            pyramid.ResetPyramid();

        Debug.Log("[SymbolManager] Símbolos reiniciados");
    }

    // 🔹 Alias para compatibilidad con PyramidMission
    public void ResetManager()
    {
        ResetSymbols();
        Debug.Log("[SymbolManager] ResetManager llamado (alias de ResetSymbols)");
    }
}
