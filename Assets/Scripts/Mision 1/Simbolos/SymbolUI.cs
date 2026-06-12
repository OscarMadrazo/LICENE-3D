using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class SymbolImagePair
{
    public MathSymbolType symbol;
    public Image image;
}

public class SymbolUI : MonoBehaviour
{
    public List<SymbolImagePair> symbolPairs;
    private Dictionary<MathSymbolType, Image> symbolImages = new Dictionary<MathSymbolType, Image>();

    private void Awake()
    {
        foreach (var pair in symbolPairs)
        {
            if (pair != null && pair.image != null)
            {
                symbolImages[pair.symbol] = pair.image;
                pair.image.gameObject.SetActive(false);
            }
        }
    }

    public void ActivateSymbol(MathSymbolType symbol)
    {
        if (symbolImages.ContainsKey(symbol))
            symbolImages[symbol].gameObject.SetActive(true);
    }

    public void DeactivateSymbol(MathSymbolType symbol)
    {
        if (symbolImages.ContainsKey(symbol))
            symbolImages[symbol].gameObject.SetActive(false);
    }

    public void ResetUI()
    {
        foreach (var img in symbolImages.Values)
            img.gameObject.SetActive(false);
    }
}
