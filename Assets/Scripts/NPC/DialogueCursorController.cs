using UnityEngine;
using DialogueEditor;

public class DialogueCursorController : MonoBehaviour
{
    [Header("Opcional")]
    [SerializeField] private GameObject playerController; // tu script de movimiento

    private void OnEnable()
    {
        ConversationManager.OnConversationStarted += OnConversationStarted;
        ConversationManager.OnConversationEnded += OnConversationEnded;
    }

    private void OnDisable()
    {
        ConversationManager.OnConversationStarted -= OnConversationStarted;
        ConversationManager.OnConversationEnded -= OnConversationEnded;
    }

    private void OnConversationStarted()
    {
        // Mostrar cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Desactivar movimiento del jugador (opcional)
        if (playerController != null)
            playerController.SetActive(false);
    }

    private void OnConversationEnded()
    {
        // Ocultar cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Reactivar movimiento
        if (playerController != null)
            playerController.SetActive(true);
    }
}
