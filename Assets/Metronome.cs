using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Metronome : MonoBehaviour
{
    public string[] Chords;
    public int BPM = 80;
    
    public int ChordInterval = 4;
    public int RepeatCount = 2;
    public AudioSource AudioSourceA;
    public AudioSource AudioSourceB;
    public Image TickImage;
    public RectTransform CurrentMarker;
    public RectTransform NextMarker;
    public ChordName ChordNameBase;
    public RectTransform Parent;

    private ChordName CurrentChordName;
    private ChordName NextChordName;

    private float timeOfLastBeat = float.MinValue;
    private int currentBeat = 0;
    private int currentRep = 0;
    private int currentChordIndex = 0;

    private void Update()
    {
        float beatInterval = 60.0f / this.BPM;
        if(Time.time >= this.timeOfLastBeat + beatInterval) {
            this.timeOfLastBeat = Time.time;
            this.PlayTick(this.currentBeat == 0);
        }

        Color c = this.TickImage.color;
        this.TickImage.color = new Color(c.r, c.g, c.b, c.a * 0.9f);
        
    }

    private void PlayTick(bool emphasize = false) {
        this.TickImage.color = Color.white;

        this.TickImage.transform.localScale = emphasize ?
            Vector3.one * 2f :
            Vector3.one;

        if (emphasize) {

            if (this.currentRep >= this.RepeatCount - 1) {
                this.currentChordIndex = Random.Range(0, this.Chords.Length);
                this.currentRep = 0;
            } else {
                this.currentRep++;
            }

            if (this.CurrentChordName != null) {
                Destroy(this.CurrentChordName.gameObject);
            }

            if (this.NextChordName != null) {
                this.CurrentChordName = this.NextChordName;
                this.CurrentChordName.GoToPos(this.CurrentMarker.transform.position);
            }

            this.NextChordName = Instantiate(ChordNameBase);
            this.NextChordName.Init(this.Chords[this.currentChordIndex], this.NextMarker.transform.position, this.Parent);
            this.AudioSourceA.Play();

        }

        this.AudioSourceB.Play();

        this.currentBeat++;
        if(this.currentBeat >= 4) {
            this.currentBeat = 0;
        }
    }
}
