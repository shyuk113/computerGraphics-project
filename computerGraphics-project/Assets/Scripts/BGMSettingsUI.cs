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
        // BGMPlayer 오브젝트를 찾아서 AudioSource 2개를 할당
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
                Debug.LogError("BGMPlayer에 AudioSource가 2개 필요합니다!");
            }
        }
        else
        {
            Debug.LogError("BGMPlayer 오브젝트를 찾을 수 없습니다!");
        }
    }

    void Start()
    {
        SetSliderMin(mainBGMVolumeSlider);
        SetSliderMin(subBGMVolumeSlider);

        // 볼륨 슬라이더 초기값 불러오기 (한 번만)
        if (mainBGMVolumeSlider != null)
            mainBGMVolumeSlider.value = LoadVolume("MainBGMVolume");
        if (subBGMVolumeSlider != null)
            subBGMVolumeSlider.value = LoadVolume("SubBGMVolume");

        // 드롭다운 옵션 세팅
        mainBGMDropdown.ClearOptions();
        subBGMDropdown.ClearOptions();

        List<string> clipNames = new List<string>();
        foreach (var clip in bgmClips)
            clipNames.Add(clip.name);

        mainBGMDropdown.AddOptions(clipNames);

        List<string> subClipNames = new List<string> { "None" };
        subClipNames.AddRange(clipNames);
        subBGMDropdown.AddOptions(subClipNames);

        // 이벤트 연결 (중복 연결 방지)
        mainBGMDropdown.onValueChanged.RemoveAllListeners();
        subBGMDropdown.onValueChanged.RemoveAllListeners();
        mainBGMVolumeSlider.onValueChanged.RemoveAllListeners();
        subBGMVolumeSlider.onValueChanged.RemoveAllListeners();

        mainBGMDropdown.onValueChanged.AddListener(OnMainBGMChanged);
        subBGMDropdown.onValueChanged.AddListener(OnSubBGMChanged);
        mainBGMVolumeSlider.onValueChanged.AddListener(OnMainBGMVolumeChanged);
        subBGMVolumeSlider.onValueChanged.AddListener(OnSubBGMVolumeChanged);

        // 드롭다운/볼륨 초기화
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

    // Inspector에서 연결 가능하도록 public으로 선언!
    public void OnMainBGMChanged(int index)
    {
        if (mainBGMSource == null) return;
        if (index >= 0 && index < bgmClips.Count)
        {
            mainBGMSource.clip = bgmClips[index];
            mainBGMSource.loop = true;
            mainBGMSource.Play();
            // 슬라이더 값을 다시 할당하지 않음!
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
        // 슬라이더 값을 다시 할당하지 않음!
        Debug.Log("MainBGM 슬라이더 값: " + value);
        ApplyVolume(mainBGMSource, value);
        SaveVolume("MainBGMVolume", value);
    }

    public void OnSubBGMVolumeChanged(float value)
    {
        // 슬라이더 값을 다시 할당하지 않음!
        ApplyVolume(subBGMSource, value);
        SaveVolume("SubBGMVolume", value);
    }

    void ApplyVolume(AudioSource source, float value)
    {
        if (source != null)
            source.volume = Mathf.Max(value, 0.0001f);
    }
}
