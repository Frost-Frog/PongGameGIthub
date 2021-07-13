using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VolumeText : MonoBehaviour
{
    public TextMeshProUGUI VolumePercent;
    public void SetVolumePercent(float volume)
    {
        VolumePercent.text = "Volume " + (volume+80) + "%";
    }
}
