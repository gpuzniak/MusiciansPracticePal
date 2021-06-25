using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChordSpawner : MonoBehaviour
{
    public string[] NoteNames;
    public string[] ChordVariants;

    public Chord ChordTemplate;
    public float BeatsPerMinute = 80.0f;
    public float BeatsPerBar = 4.0f;
    public float ChordsPerBar = 1.0f;
    public RectTransform ChordSpawnPoint;
    public RectTransform ChordGoalPoint;

    public Blip BeatBlip;

    private float timeSinceLastBeat = 0.0f;
    private float timeSinceLastBar = 0.0f;
    private float timeSinceLastChord = 0.0f;

    private void Update()
    {
        this.timeSinceLastBeat += Time.deltaTime;
        this.timeSinceLastBar += Time.deltaTime;
        this.timeSinceLastChord += Time.deltaTime;

        float beatIntervalInSeconds = 60.0f / this.BeatsPerMinute;

        if (this.timeSinceLastBeat >= beatIntervalInSeconds) {
            this.BeatBlip.DoBlip();
            this.timeSinceLastBeat = 0.0f;
        }

        if (this.timeSinceLastBar >= beatIntervalInSeconds * this.BeatsPerBar) {
            this.BeatBlip.DoBlip(true);
            this.timeSinceLastBar = 0.0f;
        }

        if (this.timeSinceLastChord >= (beatIntervalInSeconds * this.BeatsPerBar) / this.ChordsPerBar) {
            this.SpawnChord();
            this.timeSinceLastChord = 0.0f;
        }
    }

    private void SpawnChord() {
        Chord spawnedChord = Instantiate(this.ChordTemplate);
        float beatIntervalInSeconds = 60.0f / this.BeatsPerMinute;
        spawnedChord.transform.SetParent(this.transform);
        spawnedChord.GetComponent<RectTransform>().anchoredPosition = this.ChordSpawnPoint.anchoredPosition;
        string note = this.NoteNames[Random.Range(0, this.NoteNames.Length)];
        string variant = this.ChordVariants[Random.Range(0, this.ChordVariants.Length)];
        spawnedChord.Init(note + " " + variant, this.ChordGoalPoint.anchoredPosition, (beatIntervalInSeconds * this.BeatsPerBar) / this.ChordsPerBar);
    }
}