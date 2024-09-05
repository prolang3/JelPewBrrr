using Discord;
using UnityEngine;

public class Discord_Controller : MonoBehaviour
{
    public long applicationID = 1279430738852188200;
    [Space]
    public string details = "Walking around the world";
    public string state = "Current Health: ";
    [Space]
    public string largeImage = "game_logo";
    public string largeText = "Jelly is Slayer (prototype)";

    private GameObject player;
    private Rigidbody2D rb;
    private HealthComponent health;
    private long time;

    private string currentString;

    private static bool instanceExists;
    public Discord.Discord discord;

    void Awake()
    {
        // Transition the GameObject between scenes, destroy any duplicates
        if (!instanceExists)
        {
            instanceExists = true;
            DontDestroyOnLoad(gameObject);
        }
        else if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Log in with the Application ID
        discord = new Discord.Discord(applicationID, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);

        player = GameObject.FindWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        health = player.GetComponent<HealthComponent>();
        time = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();

        UpdateStatus();
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        if (player != null && health == null)
        {
            health = player.gameObject.GetComponent<HealthComponent>();
        }

        // Destroy the GameObject if Discord isn't running
        try
        {
            discord.RunCallbacks();
        }
        catch
        {
            Destroy(gameObject);
        }
    }

    void LateUpdate()
    {
        UpdateStatus();
    }

    void UpdateStatus()
    {
        if (player == null || health == null)
        {
            currentString = ":JellyDeath:";
        }
        else
        {
            currentString = state + health.Health + " / " + health.MaxHealth;
        }

        // Update Status every frame
        try
        {
            var activityManager = discord.GetActivityManager();
            var activity = new Discord.Activity
            {
                Details = details,
                State = currentString,
                Assets =
                {
                    LargeImage = largeImage,
                    LargeText = largeText
                },
                Timestamps =
                {
                    Start = time
                }
            };

            activityManager.UpdateActivity(activity, (res) =>
            {
                if (res != Discord.Result.Ok) Debug.LogWarning("Failed connecting to Discord!");
            });
        }
        catch
        {
            // If updating the status fails, Destroy the GameObject
            Destroy(gameObject);
        }
    }
}