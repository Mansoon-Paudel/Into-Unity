using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    public float speed = 5;
      void Start()
    {
        
    }

    
    void Update()
    {
        transform.position = transform.position + (Vector3.left * speed * Time.deltaTime);
    }
}
