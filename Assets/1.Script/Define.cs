using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define : MonoBehaviour
{
    public enum CameraMode
    {
        QuarterView,
    }
    public enum WorldObject
    {
        Unknown,
        Player,
        Monster
    }

    public enum State
    {
        Die,
        Moving,
        Attack,
        Idle,
        Skill,
        Defense
    }
    public enum Quest
    {
        Talk,
        Pick,
        Reach,
    }
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount
    }
    public enum Scene
    {
        Unknown,
        GameScene,
        LoginScene


    }
    public enum UIEvent
    {
        Click,
        Drag,
    }
}
