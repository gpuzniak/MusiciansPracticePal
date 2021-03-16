using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;

[ExecuteInEditMode]
public class VisibleWave : MonoBehaviour
{
    public Polyline Line;
    public float Frequency = 440.0f;
    public float Offset = 0.0f;
    public float Amplitude = 1.0f;
    public float UnityUnitsPerSecond = 100.0f;
    public float LengthInUnityUnits = 10.0f;
    [Range(0.001f, 1.0f)]
    public float ResolutionInUnityUnits = 0.1f;
    public Line RepMarker;

    private float repititionPeriod;

    protected virtual Vector3[] BuildWave() {
        List<Vector3> pts = new List<Vector3>();

        float cX = 0.0f;

        while(cX < this.LengthInUnityUnits) {
            pts.Add(new Vector3(cX, this.Amplitude * Mathf.Sin((cX + this.Offset) * (this.Frequency)), 0.0f));
            cX += Mathf.Max(this.ResolutionInUnityUnits, 0.001f);
        }

        this.repititionPeriod = this.UnityUnitsPerSecond / this.Frequency; 

        this.RepMarker.DashSize = this.repititionPeriod;
        this.RepMarker.Start = new Vector3(0f, -Mathf.Abs(this.Amplitude) - 0.5f, 0f);
        this.RepMarker.End = new Vector3(LengthInUnityUnits, -Mathf.Abs(this.Amplitude) - 0.5f, 0f);

        return pts.ToArray();
    }

    protected virtual void Update() {
        Vector3[] pts = this.BuildWave();
        this.Line.SetPoints(pts);
    }
}