using UnityEngine;

public class DialoguePyramidMissionEvents : MonoBehaviour
{
    [Header("Controlador de misión")]
    public PyramidMission missionController;

    private void Awake()
    {
        // Buscar automáticamente el PyramidMission si no está asignado
        if (missionController == null)
            missionController = GameObject.FindFirstObjectByType<PyramidMission>();

        if (missionController == null)
            Debug.LogWarning("[DialoguePyramidMissionEvents] No se encontró PyramidMission en la escena.");
    }

    // Este método se llama desde el diálogo del NPC
    public void StartPyramidMission()
    {
        if (missionController == null)
        {
            Debug.LogWarning("[DialoguePyramidMissionEvents] No se puede iniciar la misión: PyramidMission es null.");
            return;
        }

        if (missionController.IsMissionActive())
        {
            Debug.Log("[DialoguePyramidMissionEvents] La misión ya está activa.");
            return;
        }

        // Iniciar misión directamente usando el método del controlador
        missionController.StartMission();

        Debug.Log("[DialoguePyramidMissionEvents] Misión de pirámide iniciada correctamente.");
    }
}
