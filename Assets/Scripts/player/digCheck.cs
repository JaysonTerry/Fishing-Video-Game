using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class digCheck : MonoBehaviour
{
    public bool isDiggable = false;
    private playerMovement moveScript;
    GameObject player;
    [SerializeField] playerTraveling travelScript;


    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.Find("Player");
       moveScript = player.GetComponent<playerMovement>();
        travelScript = player.GetComponent<playerTraveling>();
    }

    // Update is called once per frame
    void Update()
    {
        
        isDiggable = moveScript.isDigging;
        int LayerGroundPass = LayerMask.NameToLayer("GroundPass");
      if(isDiggable)
        {
            
            player.layer = LayerGroundPass;
        }
      
      else
        {
            if (travelScript.rising == false)
            {
                player.layer = 0;
            }
        }
    }
}
