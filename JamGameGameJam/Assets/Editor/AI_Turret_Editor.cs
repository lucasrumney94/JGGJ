using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(AI_Turret))]
public class AI_Turret_Editor : Editor {

    private void OnSceneGUI()
    {
        AI_Turret turret = target as AI_Turret;
        //Vector3 muzzlePos = turret.transform.InverseTransformPoint(turret.transform.localPosition + turret.transform.TransformPoint(turret.muzzlePosition)) + turret.transform.position;
        Vector3 muzzlePos = turret.transform.TransformPoint(turret.muzzlePosition);

        Handles.color = Color.red;
        Handles.CubeCap(0, muzzlePos, turret.transform.rotation, HandleUtility.GetHandleSize(muzzlePos) * 0.5f);
    }
}
