using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
    [SerializeField] Text coordinatesText;
    private void OnMouseDrag()
    {
        float yPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(yPos, 0.0f, 7.0f));
    }

    // Start is called before the first frame update
    void Start()
    {
       Vector2 textPosition = Camera.main.WorldToViewportPoint(transform.position);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
