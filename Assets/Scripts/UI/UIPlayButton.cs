// Suggestion: using UnityEngine no se usa en este archivo, borralo.
using UnityEngine;

public class UIPlayButton : UIMenuButton
{
    protected override void OnClick() => SceneLoader.LoadGameplay();
}
