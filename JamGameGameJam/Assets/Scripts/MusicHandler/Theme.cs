﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

[System.Serializable]
public class Theme
{
    public string name;
    public List<AudioClip> tracks = new List<AudioClip>();

    public Theme(string Name)
    {
        name = Name;
    }




}
