using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip blindNoiseClip; // blind_noiseの音源ファイルをアタッチするための変数

    private AudioSource audioSource;

    void Start()
    {
        // AudioSourceコンポーネントを取得
        audioSource = GetComponent<AudioSource>();

        // blind_noiseの音源ファイルを設定
        audioSource.clip = blindNoiseClip;

        // 音源を繰り返し再生する
        audioSource.loop = true;

        // 音源の再生を開始する
        audioSource.Play();
    }
}
