using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chord : MonoBehaviour
{
    public TextMeshProUGUI ChordNameText;

    private float timeSinceSpawn = 0.0f;
    private Vector3 startPosition;
    private Vector3 goalPosition;
    private float goalTime;

    public void Init(string name, Vector3 goalPosition, float goalTime)
    {
        this.ChordNameText.text = name;
        this.startPosition = this.GetComponent<RectTransform>().anchoredPosition;
        this.goalPosition = goalPosition;
        this.goalTime = goalTime;
    }

    private void Update() {
        this.timeSinceSpawn += Time.deltaTime;
        this.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().localToWorldMatrix * Vector3.Lerp(this.startPosition, this.goalPosition, this.timeSinceSpawn / this.goalTime);

        if(this.timeSinceSpawn > this.goalTime * 1.5f) {
            Destroy(this.gameObject);
        }
    }
}
