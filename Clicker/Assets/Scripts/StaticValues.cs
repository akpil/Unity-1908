using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticValues
{
    public static readonly int Move = Animator.StringToHash("IsMove");
    public static readonly int UIMove = Animator.StringToHash("Move");
    public delegate void TwoIntPramCallback(int a, int b);
    public delegate void VoidCallback();
    private const string JSON_PATH = "JsonFiles/";
    public const string COLLEAGUE_DATA_PATH = JSON_PATH +"Colleague";
    public const string PLAYER_DATA_PATH = JSON_PATH + "PlayerInfo";

    public const int PLAYER_INFOS_LEGNTH = 7;
    public const int COOLTIME_LENGTH = 2;
    public const int COLLEAGUE_INFOS_LENGTH = 3;
}
