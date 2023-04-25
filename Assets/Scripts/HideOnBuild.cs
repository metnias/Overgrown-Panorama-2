using UnityEngine;

public class HideOnBuild : MonoBehaviour
{
    private void Start()
    {
        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
            renderer.enabled = false;
    }
}
