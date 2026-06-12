using UnityEngine;

public class LoadingScreenManager : MonoBehaviour
{
    private Animator _animatorComponent;

    private void Start()
    {
        // Obtiene el Animator del mismo objeto
        _animatorComponent = GetComponent<Animator>();

        if (_animatorComponent == null)
        {
            Debug.LogError("❌ No se encontró Animator en el objeto " + gameObject.name);
            return;
        }

        // Oculta la pantalla de carga al iniciar
        HideLoadingScreen();
    }

    public void RevealLoadingScreen()
    {
        if (_animatorComponent != null)
            _animatorComponent.SetTrigger("Reveal");
    }

    public void HideLoadingScreen()
    {
        if (_animatorComponent != null)
            _animatorComponent.SetTrigger("Hide");
    }

    // Eliminamos referencias a DemoSceneManager para evitar NullReference
    public void OnFinishedReveal()
    {
        // Puedes añadir aquí tus propias funciones si lo deseas
    }

    public void OnFinishedHide()
    {
        // Puedes añadir aquí tus propias funciones si lo deseas
    }
}
