using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class BGMSettingsUI : MonoBehaviour
{
    public List<AudioClip> bgmClips;
    public TMP_Dropdown mainBGMDropdown;
    public TMP_Dropdown subBGMDropdown;
    public Slider mainBGMVolumeSlider;
    public Slider subBGMVolumeSlider;

    [HideInInspector] public AudioSource mainBGMSource;
    [HideInInspector] public AudioSource subBGMSource;

    void Awake()
    {
        // BGMPlayer ������Ʈ�� ã�Ƽ� AudioSource 2���� �Ҵ�
        GameObject bgmPlayer = GameObject.Find("BGMPlayer");
        if (bgmPlayer != null)
        {
            var sources = bgmPlayer.GetComponents<AudioSource>();
            if (sources.Length >= 2)
            {
                mainBGMSource = sources[0];
                subBGMSource = sources[1];
            }
            else
            {
                Debug.LogError("BGMPlayer�� AudioSource�� 2�� �ʿ��մϴ�!");
            }
        }
        else
        {
            Debug.LogError("BGMPlayer ������Ʈ�� ã�� �� �����ϴ�!");
        }
    }

    void Start()
    {
        SetSliderMin(mainBGMVolumeSlider);
        SetSliderMin(subBGMVolumeSlider);

        // ���� �����̴� �ʱⰪ �ҷ����� (�� ����)
        if (mainBGMVolumeSlider != null)
            mainBGMVolumeSlider.value = LoadVolume("MainBGMVolume");
        if (subBGMVolumeSlider != null)
            subBGMVolumeSlider.value = LoadVolume("SubBGMVolume");

        // ��Ӵٿ� �ɼ� ����
        mainBGMDropdown.ClearOptions();
        subBGMDropdown.ClearOptions();

        List<string> clipNames = new List<string>();
        foreach (var clip in bgmClips)
            clipNames.Add(clip.name);

        mainBGMDropdown.AddOptions(clipNames);

        List<string> subClipNames = new List<string> { "None" };
        subClipNames.AddRange(clipNames);
        subBGMDropdown.AddOptions(subClipNames);

        // �̺�Ʈ ���� (�ߺ� ���� ����)
        mainBGMDropdown.onValueChanged.RemoveAllListeners();
        subBGMDropdown.onValueChanged.RemoveAllListeners();
        mainBGMVolumeSlider.onValueChanged.RemoveAllListeners();
        subBGMVolumeSlider.onValueChanged.RemoveAllListeners();

        mainBGMDropdown.onValueChanged.AddListener(OnMainBGMChanged);
        subBGMDropdown.onValueChanged.AddListener(OnSubBGMChanged);
        mainBGMVolumeSlider.onValueChanged.AddListener(OnMainBGMVolumeChanged);
        subBGMVolumeSlider.onValueChanged.AddListener(OnSubBGMVolumeChanged);

        // ��Ӵٿ�/���� �ʱ�ȭ
        mainBGMDropdown.value = 0;
        subBGMDropdown.value = 0;
        OnMainBGMChanged(0);
        OnSubBGMChanged(0);
        OnMainBGMVolumeChanged(mainBGMVolumeSlider.value);
        OnSubBGMVolumeChanged(subBGMVolumeSlider.value);
    }

    void SetSliderMin(Slider slider)
    {
        if (slider != null)
            slider.minValue = 0.0001f;
    }

    float LoadVolume(string key)
    {
        return PlayerPrefs.GetFloat(key, 1f);
    }

    void SaveVolume(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    // Inspector���� ���� �����ϵ��� public���� ����!
    public void OnMainBGMChanged(int index)
    {
        if (mainBGMSource == null) return;
        if (index >= 0 && index < bgmClips.Count)
        {
            mainBGMSource.clip = bgmClips[index];
            mainBGMSource.loop = true;
            mainBGMSource.Play();
            // �����̴� ���� �ٽ� �Ҵ����� ����!
            ApplyVolume(mainBGMSource, mainBGMVolumeSlider.value);
        }
    }

    public void OnSubBGMChanged(int index)
    {
        if (subBGMSource == null) return;
        if (index == 0)
        {
            subBGMSource.Stop();
            subBGMSource.clip = null;
        }
        else
        {
            int clipIdx = index - 1;
            if (clipIdx >= 0 && clipIdx < bgmClips.Count)
            {
                subBGMSource.clip = bgmClips[clipIdx];
                subBGMSource.loop = true;
                subBGMSource.Play();
                ApplyVolume(subBGMSource, subBGMVolumeSlider.value);
            }
        }
    }

    public void OnMainBGMVolumeChanged(float value)
    {
        // �����̴� ���� �ٽ� �Ҵ����� ����!
        Debug.Log("MainBGM �����̴� ��: " + value);
        ApplyVolume(mainBGMSource, value);
        SaveVolume("MainBGMVolume", value);
    }

    public void OnSubBGMVolumeChanged(float value)
    {
        // �����̴� ���� �ٽ� �Ҵ����� ����!
        ApplyVolume(subBGMSource, value);
        SaveVolume("SubBGMVolume", value);
    }

    void ApplyVolume(AudioSource source, float value)
    {
        if (source != null)
            source.volume = Mathf.Max(value, 0.0001f);
    }
}
