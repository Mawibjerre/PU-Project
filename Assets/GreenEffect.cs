using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ShinyGreenButton : MonoBehaviour
{
    [Header("Gradient Colors")]
    public Color colorA = new Color(0.1f, 0.8f, 0.1f); // bright green
    public Color colorB = new Color(0.0f, 0.5f, 0.0f); // darker green

    [Header("Animation Settings")]
    public float speed = 2f; // how fast the gradient shifts
    public float shineAmplitude = 0.2f; // brightness pulse
    public float shineSpeed = 4f;       // speed of shine pulse

    private Image img;

    void Awake()
    {
        img = GetComponent<Image>();
    }

    void Update()
    {
        // Smoothly blend between two greens
        float t = (Mathf.Sin(Time.time * speed) * 0.5f) + 0.5f;
        Color target = Color.Lerp(colorA, colorB, t);

        // Add a subtle brightness pulse
        float shine = 1f + Mathf.Sin(Time.time * shineSpeed) * shineAmplitude;
        target.r *= shine;
        target.g *= shine;
        target.b *= shine;

        img.color = target;
    }
}