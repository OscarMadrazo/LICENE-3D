using UnityEngine;

public class StartMissionFromNPC : MonoBehaviour
{
    [Header("Referencia a la misión de la pirámide")]
    public PyramidMission pyramidMission;

    private void Awake()
    {
        // Si no se asignó la misión en el inspector, buscar automáticamente
        if (pyramidMission == null)
        {
            pyramidMission = GameObject.FindObjectOfType<PyramidMission>();
            if (pyramidMission == null)
            {
                Debug.LogWarning("No se encontró PyramidMission en la escena.");
            }
        }
    }

    // Este método se llama desde un diálogo, botón o trigger del NPC
    public void TriggerMission()
    {
        if (pyramidMission == null)
        {
            Debug.LogWarning("No hay PyramidMission asignada para iniciar.");
            return;
        }

        if (pyramidMission.IsMissionActive())
        {
            Debug.Log("[StartMissionForNPC] La misión ya está activa.");
            return;
        }

        pyramidMission.StartMission();
        Debug.Log("[StartMissionForNPC] Misión de pirámide iniciada desde NPC.");
    }
}
