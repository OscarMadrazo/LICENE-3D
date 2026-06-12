using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    [Header("UI References")]
    public GameObject promptUI; // el panel del prompt
    public GameObject menuUI;   // el panel del menú de selección de nivel

    bool playerInRange = false;
    LevelSelectionMenu menuScript;
    UIAnimator promptAnimator;

    void Start()
    {
        if (promptUI != null)
        {
            promptAnimator = promptUI.GetComponent<UIAnimator>();
            if (promptAnimator == null)
                Debug.LogWarning("El PromptUI no tiene UIAnimator.");
        }

        if (menuUI != null)
        {
            menuUI.SetActive(false);
            menuScript = menuUI.GetComponent<LevelSelectionMenu>();
        }
    }

    void Update()
    {
        // Abrir con tecla E
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ToggleMenu(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (menuUI == null || !menuUI.activeSelf)
            {
                if (promptAnimator != null) promptAnimator.Show();
                else promptUI?.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (menuUI == null || !menuUI.activeSelf)
            {
                if (promptAnimator != null) promptAnimator.Hide();
                else promptUI?.SetActive(false);
            }
        }
    }

    // Método que llama el botón del prompt (OnClick)
    public void OnClickPrompt()
    {
        ToggleMenu(true);
    }

    // Abrir/Cerrar menú
    public void ToggleMenu(bool open)
    {
        if (menuUI == null) return;

        if (open)
        {
            menuUI.SetActive(true);
            if (promptAnimator != null) promptAnimator.Hide();
            else promptUI?.SetActive(false);

            Time.timeScale = 0f; // pausa juego (opcional)
            if (menuScript != null) menuScript.SetOwner(this);
        }
        else
        {
            menuUI.SetActive(false);
            Time.timeScale = 1f;
            if (playerInRange)
            {
                if (promptAnimator != null) promptAnimator.Show();
                else promptUI?.SetActive(true);
            }
            else
            {
                if (promptAnimator != null) promptAnimator.Hide();
                else promptUI?.SetActive(false);
            }
        }
    }
}
