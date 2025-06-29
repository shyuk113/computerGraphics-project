using UnityEngine;
using System;

public class SunLightBySystemTime : MonoBehaviour
{
    public Light sunLight; // Directional Light 연결
    public Gradient sunColorByTime; // Inspector에서 색상 그라데이션 지정
    public AnimationCurve intensityByTime; // Inspector에서 밝기 곡선 지정

    void Update()
    {
        DateTime now = DateTime.Now;
        float hour = now.Hour + now.Minute / 60f; // 0~24 시간
        float t = Mathf.InverseLerp(6f, 18f, hour); // 6~18시만 밝게 (아침6시~저녁6시)
        t = Mathf.Clamp01(t);

        // 색상과 밝기 적용
        sunLight.color = sunColorByTime.Evaluate(t);
        sunLight.intensity = intensityByTime.Evaluate(t);

        // 태양 각도도 시간에 따라 변화
        float sunAngle = Mathf.Lerp(0, 180, t); // 0~180도 (동→서)
        sunLight.transform.rotation = Quaternion.Euler(sunAngle, 30, 0);
    }
}
