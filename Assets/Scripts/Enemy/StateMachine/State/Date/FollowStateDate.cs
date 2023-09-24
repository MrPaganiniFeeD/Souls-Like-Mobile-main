using System;
using UnityEngine;

[Serializable]
public class FollowStateDate : StateDate
{
    public float MinimalDistance;
    public float Speed;
    public FollowTransitionDate FollowTransitionDate;
    public AudioClip StepAudioClip;
}