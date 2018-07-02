using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class battle_master : MonoBehaviour {

	public AudioMixer battle_mixer;

	private Object[] AudioArray_loops;

	public float fadeout_speed = 15.0f;
	public float fadein_speed = 25.0f;

	private AudioSource audio_loop1;
	private AudioSource audio_loop2;
	private AudioSource audio_loop3;

	private float audio_soft_vol;
	private float audio_med_vol;
	private float audio_forte_vol;

	public bool soft;
	public bool med;
	public bool forte;

	private bool first_run;

	// Use this for initialization
	void Start () {
		first_run = false;


		audio_loop1 = (AudioSource)gameObject.AddComponent <AudioSource>();
		audio_loop2 = (AudioSource)gameObject.AddComponent <AudioSource>();
		audio_loop3 = (AudioSource)gameObject.AddComponent <AudioSource>();
	
		audio_loop1.outputAudioMixerGroup = battle_mixer.FindMatchingGroups("soft")[0];
		audio_loop2.outputAudioMixerGroup = battle_mixer.FindMatchingGroups("med")[0];
		audio_loop3.outputAudioMixerGroup = battle_mixer.FindMatchingGroups("forte")[0];
		AudioArray_loops = Resources.LoadAll("loops",typeof(AudioClip));

		audio_loop1.clip = AudioArray_loops [0] as AudioClip;
		audio_loop2.clip = AudioArray_loops [1] as AudioClip;
		audio_loop3.clip = AudioArray_loops [2] as AudioClip;
		audio_loop1.loop = true;
		audio_loop2.loop = true;
		audio_loop3.loop = true;

	}
	
	// Update is called once per frame
	void Update () {

		battle_mixer.SetFloat ("soft", audio_soft_vol);
		battle_mixer.SetFloat ("med", audio_med_vol);
		battle_mixer.SetFloat ("forte", audio_forte_vol);

		if (!audio_loop1.isPlaying & !audio_loop2.isPlaying & !audio_loop3.isPlaying) {
			if (soft | med | forte){
					if (soft){
						audio_soft_vol = 0.0f;
						audio_med_vol = -80.0f;
						audio_forte_vol = -80.0f;
					}
					if (med){
						audio_soft_vol = -80.0f;
						audio_med_vol = 0.0f;
						audio_forte_vol = -80.0f;
					}
					if (forte){
						audio_forte_vol = 0.0f;
						audio_med_vol = -80.0f;
						audio_soft_vol = -80.0f;
					}
					audio_loop1.Play ();
					audio_loop2.Play ();
					audio_loop3.Play ();
			}
		}


		if (soft) {
			if (audio_soft_vol < 0.0f) {
				audio_soft_vol += fadein_speed * Time.deltaTime;	
			}
			if (audio_med_vol > -80.0f & audio_soft_vol > -20.0f) {
				audio_med_vol -= fadeout_speed * Time.deltaTime;	
			}
			if (audio_forte_vol > -80.0f & audio_soft_vol > -20.0f) {
				audio_forte_vol -= fadeout_speed * Time.deltaTime;	
			}
		}

		if (med) {
			if (audio_med_vol < 0.0f) {
				audio_med_vol += fadein_speed * Time.deltaTime;	
			}
			if (audio_soft_vol > -80.0f & audio_med_vol > -20.0f) {
				audio_soft_vol -= fadeout_speed * Time.deltaTime;	
			}
			if (audio_forte_vol > -80.0f & audio_med_vol > -20.0f) {
				audio_forte_vol -= fadeout_speed * Time.deltaTime;	
			}
		}

		if (forte) {
			if (audio_forte_vol < 0.0f) {
				audio_forte_vol += fadein_speed * Time.deltaTime;	
			}
			if (audio_med_vol > -80.0f & audio_forte_vol > -20.0f) {
				audio_med_vol -= fadeout_speed * Time.deltaTime;	
			}
			if (audio_soft_vol > -80.0f & audio_forte_vol > -20.0f) {
				audio_soft_vol -= fadeout_speed * Time.deltaTime;	
			}
		}

		if (!soft & !med & !forte) {
			if (audio_forte_vol > -80.0f) {
				audio_forte_vol -= fadeout_speed * Time.deltaTime;	
			}
			if (audio_forte_vol < -70.0f) {
				audio_loop3.Stop();
				first_run = true;
			}
			if (audio_med_vol > -80.0f) {
				audio_med_vol -= fadeout_speed * Time.deltaTime;	
			}
			if (audio_med_vol < -70.0f) {
				audio_loop2.Stop();
				first_run = true;
			}
			if (audio_soft_vol > -80.0f) {
				audio_soft_vol -= fadeout_speed * Time.deltaTime;	
			}
			if (audio_soft_vol < -70.0f) {
				audio_loop1.Stop();
				first_run = true;
			}

		}
	
	}

	public void Soft_OnClick(){
		soft = true;
		med = false;
		forte = false;
		if (first_run) {
			if (soft){
				audio_soft_vol = 0.0f;
				audio_med_vol = -80.0f;
				audio_forte_vol = -80.0f;
			}
			if (med){
				audio_soft_vol = -80.0f;
				audio_med_vol = 0.0f;
				audio_forte_vol = -80.0f;
			}
			if (forte){
				audio_forte_vol = 0.0f;
				audio_med_vol = -80.0f;
				audio_soft_vol = -80.0f;
			}
			audio_loop1.Play ();
			audio_loop2.Play ();
			audio_loop3.Play ();
			first_run = false;
		}
	
	}

	public void Med_OnClick(){
		soft = false;
		med = true;
		forte = false;
		if (first_run) {

			if (soft){
				audio_soft_vol = 0.0f;
				audio_med_vol = -80.0f;
				audio_forte_vol = -80.0f;
			}
			if (med){
				audio_soft_vol = -80.0f;
				audio_med_vol = 0.0f;
				audio_forte_vol = -80.0f;
			}
			if (forte){
				audio_forte_vol = 0.0f;
				audio_med_vol = -80.0f;
				audio_soft_vol = -80.0f;
			}
			audio_loop1.Play ();
			audio_loop2.Play ();
			audio_loop3.Play ();
			first_run = false;
		}

	}

	public void Forte_OnClick(){
		soft = false;
		med = false;
		forte = true;
		if (first_run) {
			if (soft){
				audio_soft_vol = 0.0f;
				audio_med_vol = -80.0f;
				audio_forte_vol = -80.0f;
			}
			if (med){
				audio_soft_vol = -80.0f;
				audio_med_vol = 0.0f;
				audio_forte_vol = -80.0f;
			}
			if (forte){
				audio_forte_vol = 0.0f;
				audio_med_vol = -80.0f;
				audio_soft_vol = -80.0f;
			}
			audio_loop1.Play ();
			audio_loop2.Play ();
			audio_loop3.Play ();
			first_run = false;
		}

	}

	public void Stop_OnClick(){
		soft = false;
		med = false;
		forte = false;
	}
}
