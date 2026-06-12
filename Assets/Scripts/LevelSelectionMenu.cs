using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionMenu : MonoBehaviour
{
    Interactable owner;

    // El Interactable que abrió este menú se lo pasamos así:
    public void SetOwner(Interactable interactable)
    {
        owner = interactable;
    }

    // Llamar desde un botón UI: Start -> pasar nombre de escena en el inspector
    public void StartLevel(string sceneName)
    {
        // Restaurar tiempo
        Time.timeScale = 1f;

        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("StartLevel: sceneName vacío.");
        }
    }

    // Botón Salir / Cerrar menú
    public void CloseMenu()
    {
        if (owner != null)
            owner.ToggleMenu(false);
        else
        {
            // fallback
            gameObject.SetActive(false);
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
