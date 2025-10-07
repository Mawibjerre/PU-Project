using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class KillBlockColorPulse : MonoBehaviour
{
    [Header("Color Pulse")]
    public Color colorA = new Color(1f, 0.1f, 0.1f);  // Red-ish
    public Color colorB = new Color(1f, 0.6f, 0f);    // Orange
    public float pulseSpeed = 1.5f;                   // How fast to swap colors

    [Header("Brightness Pulse (optional)")]
    public bool enableShine = true;
    public float shineAmplitude = 0.15f;              // 0 = no shine, 0.15 = subtle
    public float shineSpeed = 3f;                     // Speed of brightness pulse

    private SpriteRenderer sr;
    private Color baseColor;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        baseColor = sr.color; // in case you had a tint set
    }

    void Update()
    {
        // Smooth color blend between A and B
        float t = (Mathf.Sin(Time.time * pulseSpeed) * 0.5f) + 0.5f; // 0..1
        Color target = Color.Lerp(colorA, colorB, t);

        // Optional brightness pulse for "shiny" feel
        if (enableShine)
        {
            float shine = 1f + Mathf.Sin(Time.time * shineSpeed) * shineAmplitude; // ~0.85..1.15
            target.r *= shine;
            target.g *= shine;
            target.b *= shine;
        }

        sr.color = target;
    }
}