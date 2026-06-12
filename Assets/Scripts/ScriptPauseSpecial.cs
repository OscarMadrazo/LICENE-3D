using UnityEngine;

public class ScriptPauseSpecial : MonoBehaviour
{
    [Tooltip("Arrastra aquí el GameObject que actúa como 'mask' (UI panel, imagen, etc.)")]
    public GameObject gameMask;

    void Start()
    {
        if (gameMask == null)
            Debug.LogWarning("MaskToggle: gameMask no asignado en el Inspector.");
    }

    // Muestra la máscara
    public void ShowMask()
    {
        if (gameMask != null) gameMask.SetActive(true);
    }

    // Oculta la máscara
    public void HideMask()
    {
        if (gameMask != null) gameMask.SetActive(false);
    }

    // Alterna el estado
    public void ToggleMask()
    {
        if (gameMask != null) gameMask.SetActive(!gameMask.activeSelf);
    }

    // Ejemplo: presionar Escape alterna la máscara
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleMask();
    }
}
