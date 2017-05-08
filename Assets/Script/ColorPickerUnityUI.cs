using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickerUnityUI : MonoBehaviour {
    public bool circular;
    public Color value = Color.white;
    public Image thumb;
    public Image colorPalette;
    public float test;


    public void OnPress()
    {
        UpdateThumbPosition();
    }

    public void OnDrag()
    {
        UpdateThumbPosition();
    }

    // Get color of mouse point
    private Color GetColor()
    {
        Vector2 spectrumScreenPosition = colorPalette.transform.position;
        Vector2 ThumbScreenPosition = thumb.transform.position;
        //float ThumbWidth = thumb.GetComponent<RectTransform>().rect.width;
        //Vector2 SpectrumXY = new Vector2(0f, colorPalette.GetComponent<RectTransform>().rect.width);
        Vector2 SpectrumXY = new Vector2(colorPalette.GetComponent<RectTransform>().rect.width, colorPalette.GetComponent<RectTransform>().rect.height);
        Vector2 position = ThumbScreenPosition - spectrumScreenPosition + SpectrumXY * 0.5f; //--> SpectrumXY doit sûrement être le diamiètre du thumb
        //Vector2 position = ThumbScreenPosition - spectrumScreenPosition;
        Texture2D texture = colorPalette.mainTexture as Texture2D;

        if (circular)
        {
            position = new Vector2((position.x / (colorPalette.GetComponent<RectTransform>().rect.width * colorPalette.transform.localScale.y)), 
                                    position.y / (colorPalette.GetComponent<RectTransform>().rect.height * colorPalette.transform.localScale.y));

        }else
        {
            position = new Vector2((position.x / colorPalette.GetComponent<RectTransform>().rect.width), (position.y / colorPalette.GetComponent<RectTransform>().rect.height));
        }

        Color SelectedColor = texture.GetPixelBilinear(position.x, position.y);
        SelectedColor.a = 1;
        return SelectedColor;
    }

    // Move the object only where the picture is
    private void UpdateThumbPosition()
    {
        if (circular && colorPalette.GetComponent<CircleCollider2D>())
        {
            Vector3 center = transform.position;
            Vector3 position = Input.mousePosition;
            Vector3 offset = position - center;
            Vector3 Set = Vector3.ClampMagnitude(offset, (colorPalette.GetComponent<CircleCollider2D>().radius + test));
            Vector3 newPos = center + Set;

            if (thumb.transform.position != newPos)
            {
                // Move the thumb to the new position
                thumb.transform.position = newPos;
                value = GetColor();
            }
        }
    }
}
