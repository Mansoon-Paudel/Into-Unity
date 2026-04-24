using UnityEngine;

public class BirdScript : MonoBehaviour{
   private Rigidbody2D myrigidbody;
   void Start() {
      myrigidbody = GetComponent<Rigidbody2D>();
   }
   void Update() {
       

      if(Input.GetKeyDown(KeyCode.Space)){
         myrigidbody.linearVelocityY = 10;

      }
   }
};