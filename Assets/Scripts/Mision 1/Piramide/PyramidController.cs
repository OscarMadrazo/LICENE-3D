using UnityEngine;
using System.Collections.Generic;

public class PyramidController : MonoBehaviour
{
    [System.Serializable]
    public class SymbolSlot
    {
        public MathSymbolType symbolType;
        public Transform slot;
        public GameObject symbolPrefab;

        [HideInInspector] public bool occupied;
        [HideInInspector] public GameObject currentSymbol;
    }

    public List<SymbolSlot> symbolSlots;

    public void ShowSymbol(MathSymbolType symbol)
    {
        foreach (var s in symbolSlots)
        {
            if (s.symbolType == symbol)
            {
                if (s.occupied && s.currentSymbol != null)
                {
                    s.currentSymbol.SetActive(true);
                    return;
                }

                if (s.symbolPrefab != null)
                {
                    if (s.currentSymbol == null)
                        s.currentSymbol = Instantiate(s.symbolPrefab, s.slot.position, s.slot.rotation, s.slot);
                    else
                        s.currentSymbol.SetActive(true);

                    s.occupied = true;
                }
                return;
            }
        }
    }

    public void ResetPyramid()
    {
        foreach (var s in symbolSlots)
        {
            if (s.currentSymbol != null)
                s.currentSymbol.SetActive(false);
            s.occupied = false;
        }
    }

    private void Start()
    {
        ResetPyramid();
    }
}
