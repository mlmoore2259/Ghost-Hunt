using UnityEngine;

public class Cleanup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Iterate through all objects and destroy those that are offscreen
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Blast"))
        {
            if (IsOutOfScreen(obj))
            {
                Destroy(obj);
            }
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            GhostBehavior ghostBehavior = obj.GetComponent<GhostBehavior>();
            if (IsOutOfScreen(obj) && !ghostBehavior.cleanupFlag)
            {
                Destroy(obj);
            }
        }
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
