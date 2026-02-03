using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DaylightCycle : MonoBehaviour
{
    // At 8PM the light will start to lower and become dark at 10PM
    // At 4AM the light will start to rise and become bright at 6AM

    public Light2D globalLight;
    [SerializeField] PlayerGameInfo playerGameInfo;
    public GameInfoUI gameInfoUI;
    [SerializeField] bool startedDim;
    [SerializeField] bool startedBrighten;
    [SerializeField] float intensityChangePerSecond;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerGameInfo = GameObject.Find("PlayerGameInfo").GetComponent<PlayerGameInfo>();
        globalLight = this.GetComponent<Light2D>();
        // Calculate how much the light intensity should change within the update loop
        // The change occurs over 2 game hours, which is 15 real seconds
        float timeToChange = gameInfoUI.dayLengthInSeconds / 12;
        intensityChangePerSecond = 1f / timeToChange;
    }

    void Awake()
    {
        startedDim = false;
        startedBrighten = false;
    }

    // Update is called once per frame
    void Update()
    {
        int hour = CalculateHour();
        // Start dimming
        if (hour == 20 && !startedDim)
        {
            startedDim = true;
            InvokeRepeating("Dim", 0f, 0.1f);
        }
        // Stop dimming
        else if (hour == 22 && startedDim)
        {
            startedDim = false;
            CancelInvoke("Dim");
            if (globalLight.intensity < 0f)
            {
                globalLight.intensity = 0f;
            }
        }
        // Start brightening
        else if (hour == 4 && !startedBrighten)
        {
            startedBrighten = true;
            InvokeRepeating("Brighten", 0f, 0.1f);
        }
        // Stop brightening
        else if (hour == 6 && startedBrighten)
        {
            startedBrighten = false;
            CancelInvoke("Brighten");
            playerGameInfo.daysSurvived += 1;
            if (globalLight.intensity > 1f)
            {
                globalLight.intensity = 1f;
            }
        }
    }

    int CalculateHour()
    {
        // Every 180 seconds is a full day cycle
        // Ratio of real:game time is 180:86400 = 1:480
        // Game starts at 6:00 AM

        // Get game time in seconds
        float gameTime = Time.timeSinceLevelLoad;
        float dayTimeInSeconds = (gameTime % gameInfoUI.dayLengthInSeconds) + 45;

        // Convert to hours and minutes
        int totalGameSeconds = Mathf.FloorToInt(dayTimeInSeconds * 480);
        int hours = (totalGameSeconds / 3600) % 24;
        return hours;
    }

    void Dim()
    {
        globalLight.intensity -= intensityChangePerSecond * 0.1f;
    }

    void Brighten()
    {
        globalLight.intensity += intensityChangePerSecond * 0.1f;
    }
}
