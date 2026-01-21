using UnityEngine;

public class DaylightCycle : MonoBehaviour
{
    [SerializeField] GameObject globalLight;
    [SerializeField] bool AM;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        globalLight = this.gameObject;
    }

    private void Awake()
    {
        AM = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
