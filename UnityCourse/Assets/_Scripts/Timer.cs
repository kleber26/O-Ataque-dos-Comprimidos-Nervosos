using System.Linq;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour {

    public static bool stopTimer;
    public TextMeshProUGUI timer, timerBlack;
    private float startTime = 0f;

    void Update() {
        if (stopTimer) return;
        
        startTime += Time.deltaTime;
        string minutes = ((int) startTime / 60).ToString();
        string seconds = (startTime % 60).ToString("f2");
        string decimals = seconds.Split('.').Last();
        seconds = seconds.Split('.').First();
        
        if (minutes.Length < 2) minutes = "0" + minutes;
        if (seconds.Length < 2) seconds = "0" + seconds;

        string time = minutes + ":" + seconds + "<size=20>" + decimals; 
     
        timer.text = time;
        timerBlack.text = "<color=black>" + time;
    }
}
