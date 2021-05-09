using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private Renderer rend;
    [SerializeField]
    private Color colorToTurnTo = Color.white;
    // Start is called before the first frame update
    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = colorToTurnTo;
    }


}
