// Suggestion: using UnityEngine no se usa en este archivo, borralo.
using UnityEngine;

public class UIMainMenuButton : UIMenuButton
{
    protected override void OnClick() => SceneLoader.LoadMainMenu();
}
