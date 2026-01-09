using UnityEngine;

public class BlastBehavior : MonoBehaviour
{
    private float BlastSpeed;
    public PlayerGameInfo playerGameInfo;
    private EnemySpawner enemySpawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerGameInfo = GameObject.Find("PlayerGameInfo").GetComponent<PlayerGameInfo>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    private void Awake()
    {
        BlastSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        // Move to the right at BlastSpeed
        transform.Translate(Vector2.right * BlastSpeed * Time.deltaTime);
        if (IsOutOfScreen(this.gameObject))
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);  // Destroy the blast

            // Get the enemies health and reduce it
            GhostBehavior ghostBehav = other.gameObject.GetComponent<GhostBehavior>();
            if (ghostBehav != null)
            {
                ghostBehav.health -= 20f;
                if (ghostBehav.health <= 0f)
                {
                    UpdateScore(1);
                    enemySpawner.enemiesYCoord.Remove(other.transform.position.y);
                    Destroy(other.gameObject); // Destroy the enemy if health is 0
                }
            }
        }
    }

    // Access the PlayerGameInfo script to update score
    void UpdateScore(int points)
    {
        Debug.Log("Updating score by: " + points);
        playerGameInfo.GetComponent<PlayerGameInfo>().score += points;
    }

    // Destruction offscreen function from https://stackoverflow.com/questions/23217840/unity-2d-destroy-instantiated-prefab-when-it-goes-off-screen
    public bool IsOutOfScreen(GameObject o, Camera cam = null)
    {
        bool result = false;
        Renderer ren = o.GetComponent<Renderer>();
        if (ren)
        {
            if (cam == null) cam = Camera.main;
            Vector2 sdim = SpriteScreenSize(o, cam);
            Vector2 pos = cam.WorldToScreenPoint(o.transform.position);
            Vector2 min = pos - sdim;
            Vector2 max = pos + sdim;
            if (min.x > Screen.width || max.x < 0f ||
                min.y > Screen.height || max.y < 0f)
            {
                result = true;
            }
        }
        else
        {
            //TODO: throw exception or something
        }
        return result;
    }

    public Vector2 SpriteScreenSize(GameObject o, Camera cam = null)
    {
        if (cam == null) cam = Camera.main;
        Vector2 sdim = new Vector2();
        Renderer ren = o.GetComponent<Renderer>() as Renderer;
        if (ren)
        {
            sdim = cam.WorldToScreenPoint(ren.bounds.max) -
                cam.WorldToScreenPoint(ren.bounds.min);
        }
        return sdim;
    }
}
