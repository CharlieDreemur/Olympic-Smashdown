using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private SpriteRenderer[] spriteRenderers;
    IEnumerator ShaderChange(float seconds)
    {

        for (float timer = seconds; timer >= 0; timer -= Time.deltaTime)
        {
            SetShader("_FlashAmount", timer / seconds);
            yield return 0;
        }
    }
    public void SetShader(string parameter, float amount)
    {
        if (spriteRenderers == null || spriteRenderers.Length == 0)
        {
            return;
        }
        foreach (SpriteRenderer item in spriteRenderers)
        {
            item.material.SetFloat(parameter, amount);
        }

    }
}

public static class VectorHelperExtensions
{
    public static Vector3 Flatten(this Vector3 vector)
    {
        vector.y = 0.0f;
        return vector;
    }

    public static float Distance2D(this Vector3 from, in Vector3 to)
    {
        return Vector3.Distance(Flatten(from), Flatten(to));
    }

    public static Vector3 Direction2D(this Vector3 from, in Vector3 to)
    {
        return (Flatten(to) - Flatten(from)).normalized;
    }

    public static Vector3 RotateAboutUp(this Vector3 currentRot, in float angle)
    {
        return Quaternion.AngleAxis(angle, Vector3.up) * currentRot;
    }
}
