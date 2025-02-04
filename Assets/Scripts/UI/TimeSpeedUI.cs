using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class TimeSpeedUI : MonoBehaviour
{
    [SerializeField] Text _text;
    CancellationTokenSource _updateCancelToken;

    void OnEnable()
    {
        _updateCancelToken = new CancellationTokenSource();
        _ = TimeScaleLoop(_updateCancelToken.Token);
    }

    void OnDisable()
    {
        _updateCancelToken.Cancel();
    }

    //it's async so it doesn't stop on timescale = 0
    async Task TimeScaleLoop(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            _text.text = $"Current time speed: {Time.timeScale.ToString("0.00")}\nPress Up/Down arrow keys to change";
            await Task.Delay(50);
        }
    }
}
