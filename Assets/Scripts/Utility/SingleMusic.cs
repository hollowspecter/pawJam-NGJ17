using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleMusic : MonoBehaviour {

    public static SingleMusic Instance;

    [SerializeField]
    private AudioClip sfx_emptyPaw;
    [SerializeField]
    private Vector2 pitch_emptyPaw;
    [SerializeField]
    private Vector2 vol_emptyPaw;

    private AudioSource m_audiosrcSFX;

	// Use this for initialization
	void Awake () {
        if (Instance == null) Instance = this;
        else
            Destroy(this.gameObject);

        m_audiosrcSFX = transform.GetChild(0).GetComponent<AudioSource>();
    }

    public void PlayEmptyPaw() { PlaySFXatRandomPitchVolume(sfx_emptyPaw, pitch_emptyPaw, vol_emptyPaw); }

    void PlaySFXatRandomPitchVolume(AudioClip clip, Vector2 minMaxPitch, Vector2 minMaxVolume)
    {
        m_audiosrcSFX.volume = Random.Range(minMaxVolume.x, minMaxVolume.y);
        m_audiosrcSFX.pitch = Random.Range(minMaxPitch.x, minMaxPitch.y);

        m_audiosrcSFX.PlayOneShot(clip);
    }
}
