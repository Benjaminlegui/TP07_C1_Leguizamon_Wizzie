using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class UIMenuButton : MonoBehaviour
{
    private Button button;

    private void OnEnable()
    {
        // Suggestion: GetComponent se llama cada vez que el botón se habilita. Cachealo en Awake y en OnEnable solo agregá el listener.
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnDisable() => button.onClick.RemoveListener(OnClick);

    protected abstract void OnClick();
}
