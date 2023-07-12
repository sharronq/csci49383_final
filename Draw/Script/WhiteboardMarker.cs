using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
    Class placed on whiteboard marker game object to enable drawing on the whiteboard.
*/
public class WhiteboardMarker : MonoBehaviour
{
    [SerializeField] private Transform tip;
    [SerializeField] private int penSize = 5; 

    private Renderer _renderer; 
    private Color[] colors;
    private float tipHeight;

    private RaycastHit touch;
    private Whiteboard whiteboard;
    private Vector2 touchPos, lastTouchPos;
    private bool touchedLastFrame;
    private Quaternion lastTouchRotation;

    void Start()
    {
        _renderer = tip.GetComponent<Renderer>(); // gets color of tip component
        colors = Enumerable.Repeat(_renderer.material.color, penSize * penSize).ToArray();
        tipHeight = tip.localScale.y;
    }

    void Update()
    {
        Draw();
    }

    private void Draw() {
        if (Physics.Raycast(tip.position, transform.up, out touch, tipHeight))  
        {
            if (touch.transform.CompareTag("Whiteboard")) // Checks if the item the pen is touching is a whiteboard
            {
                if (whiteboard == null) 
                {
                    // Only get whiteboard if not already touching it
                    whiteboard = touch.transform.GetComponent<Whiteboard>();
                }

                // Grab position that pen touches whiteboard
                touchPos = new Vector2(touch.textureCoord.x, touch.textureCoord.y);

                // Translating position of whiteboard to resolution value
                var x = (int)(touchPos.x * whiteboard.textureSize.x - (penSize/2)); // Accounts for size of pen 
                var y = (int)(touchPos.y * whiteboard.textureSize.y - (penSize/2));

                // Out of bounds check that accounts for edges of whiteboard plane
                if (y < 0 || y > whiteboard.textureSize.y || x < 0 || x > whiteboard.textureSize.x) 
                {
                    return; // if out of bounds, stop drawing
                }

                if (touchedLastFrame) 
                { // checks if the last frame touched was on the whiteboard
                    whiteboard.texture.SetPixels(x, y, penSize, penSize, colors); // Sets the point that pen touches with relation to pen size

                    // Loops 100 times to fill in space between last point touched and current point touched
                    for (float f = 0.01f; f < 1.00f; f += 0.01f)  
                    {
                        var lerpX = (int)Mathf.Lerp(lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(lastTouchPos.y, y, f);
                        whiteboard.texture.SetPixels(lerpX, lerpY, penSize, penSize, colors);
                    }

                    // Locks rotation of pen when touching whiteboard
                    transform.rotation = lastTouchRotation;
                    whiteboard.texture.Apply(); // Applies colored pixels to the whiteboard texture

                }
                // Updates values each frame if not touching whiteboard anymore
                lastTouchPos = new Vector2(x, y);
                lastTouchRotation = transform.rotation;
                touchedLastFrame = true;
                return;
            }
        }
        // if all if statements fail (not touching whiteboard), unsets whiteboard
        whiteboard = null;
        touchedLastFrame = false;
    }
}
