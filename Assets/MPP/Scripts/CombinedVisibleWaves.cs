using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedVisibleWaves : VisibleWave
{
    public VisibleWave[] FoundationWaves;

    protected override Vector3[] BuildWave() {
        List<Vector3> pts = new List<Vector3>();

        float cX = 0.0f;

        while (cX < this.LengthInUnityUnits) {
            float combinedAmplitude = 0f;
            float combinedTotalAmplitude = 0f;

            foreach (VisibleWave wave in this.FoundationWaves) {
                combinedAmplitude += wave.Amplitude * Mathf.Sin((cX + wave.Offset) * wave.Frequency / wave.UnityUnitsPerSecond);
                combinedTotalAmplitude += wave.Amplitude;
            }

            pts.Add(new Vector3(cX, combinedAmplitude, 0.0f));
            cX += Mathf.Max(this.ResolutionInUnityUnits, 0.001f);

            this.RepMarker.Start = new Vector3(0f, -Mathf.Abs(combinedTotalAmplitude) - 0.5f, 0f);
            this.RepMarker.End = new Vector3(LengthInUnityUnits, -Mathf.Abs(combinedTotalAmplitude) - 0.5f, 0f);
        }

        return pts.ToArray();
    }

    protected override void Update() {
        base.Update();
    }
}
