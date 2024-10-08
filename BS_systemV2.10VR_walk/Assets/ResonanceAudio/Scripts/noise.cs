using UnityEngine;

public class ContinuousAudio : MonoBehaviour
{
    public AudioClip blindNoiseClip; // 鳴らしたい音源
    private AudioSource audioSource; // 音源を再生するためのAudioSource

    // Start is called before the first frame update
    void Start()
    {
        // AudioSourceコンポーネントを取得
        audioSource = GetComponent<AudioSource>();

        // AudioSourceに音源を設定
        audioSource.clip = blindNoiseClip;

        // ループ再生しない
        audioSource.loop = false;

        // 音源再生開始
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // 音源が再生中でない場合、再度再生を開始
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
