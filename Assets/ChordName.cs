using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChordName : MonoBehaviour
{
    public TextMeshProUGUI Text;

    private Vector3 goalPosition;
    private Color goalColor;

    public void Init(string name, Vector3 position, Transform parent) {
        this.transform.position = position;
        this.goalPosition = position;
        this.transform.SetParent(parent);
        this.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        this.Text.color = new Color(1, 1, 1, 0.0f);
        this.goalColor = new Color(1, 1, 1, 0.2f) ;
        this.SetChordName(name);
    }

    public void SetChordName(string name) {
        this.Text.text = name;
    }

    public void GoToPos(Vector3 position) {
        this.goalPosition = position;
        this.goalColor = Color.white;
    }

    private void Update() {
        this.transform.position = Vector3.Lerp(this.transform.position, this.goalPosition, Time.deltaTime * 10f);
        this.Text.color = Color.Lerp(this.Text.color, goalColor, Time.deltaTime * 10f);
    }
}
