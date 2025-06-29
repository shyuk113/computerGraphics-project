using UnityEngine;
using System;

public class SunLightBySystemTime : MonoBehaviour
{
    public Light sunLight; // Directional Light ����
    public Gradient sunColorByTime; // Inspector���� ���� �׶��̼� ����
    public AnimationCurve intensityByTime; // Inspector���� ��� � ����

    void Update()
    {
        DateTime now = DateTime.Now;
        float hour = now.Hour + now.Minute / 60f; // 0~24 �ð�
        float t = Mathf.InverseLerp(6f, 18f, hour); // 6~18�ø� ��� (��ħ6��~����6��)
        t = Mathf.Clamp01(t);

        // ����� ��� ����
        sunLight.color = sunColorByTime.Evaluate(t);
        sunLight.intensity = intensityByTime.Evaluate(t);

        // �¾� ������ �ð��� ���� ��ȭ
        float sunAngle = Mathf.Lerp(0, 180, t); // 0~180�� (���漭)
        sunLight.transform.rotation = Quaternion.Euler(sunAngle, 30, 0);
    }
}
