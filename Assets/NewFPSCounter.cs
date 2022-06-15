
using UnityEngine;
using UnityEngine.UI;

public class NewFPSCounter : MonoBehaviour
{
    [SerializeField] private Text _fpsText;
    [SerializeField] private float _hudRefreshRate = 0.5f;

    private float _timer;

    private void Update()
    {
        if (Time.unscaledTime > _timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            _fpsText.text = "" + fps;
            _timer = Time.unscaledTime + _hudRefreshRate;
        }
    }
}