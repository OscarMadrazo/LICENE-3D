using UnityEngine;

public class DialogueMissionEvent : MonoBehaviour
{
    [Header("Referencia a la misión de la pirámide")]
    public PyramidMission pyramidMission;

    private void Awake()
    {
        // Buscar automáticamente la misión en la escena si no se asignó
        if (pyramidMission == null)
        {
            pyramidMission = GameObject.FindObjectOfType<PyramidMission>();
            if (pyramidMission == null)
            {
                Debug.LogWarning("No se encontró PyramidMission en la escena.");
            }
        }
    }

    // Método que se llama desde un diálogo, botón o trigger
    public void StartMission()
    {
        if (pyramidMission == null)
        {
            Debug.LogWarning("No hay PyramidMission asignada para iniciar.");
            return;
        }

        if (pyramidMission.IsMissionActive())
        {
            Debug.Log("[DialogueMissionEvent] La misión ya está activa.");
            return;
        }

        pyramidMission.StartMission();
        Debug.Log("[DialogueMissionEvent] Misión de pirámide iniciada desde diálogo.");
    }
}
