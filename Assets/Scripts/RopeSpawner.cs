using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawner : MonoBehaviour
{
    
    [Header("Tweakables")]
    [SerializeField]
    GameObject ropeObject;
    [SerializeField] [Range(1, 30)]
    int ropeLength = 1;

    int orig_length;
    Rigidbody2D prevRB;
    Rigidbody2D myBody;

    void Start()
    {
        generateRope();
    }

    void Update()
    {
        if(ropeLength != orig_length)
        {
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            generateRope();
        }
    }

    void generateRope()
    {
        orig_length = ropeLength;
        float rootx = gameObject.transform.position.x;
        float rooty = gameObject.transform.position.y;
        myBody = gameObject.GetComponent<Rigidbody2D>();
        prevRB = myBody;
        for(int ia = 1; ia <= ropeLength; ia++)
        {
            GameObject newLink = Instantiate(ropeObject, transform);
            newLink.transform.position = new Vector2(newLink.transform.position.x, newLink.transform.position.y - (0.25f + (ropeObject.transform.localScale.y*(ia-1))));
            newLink.GetComponent<HingeJoint2D>().connectedBody = prevRB;
            prevRB = newLink.GetComponent<Rigidbody2D>();
        }
    }
}
