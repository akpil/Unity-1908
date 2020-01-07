using System;
using System.Collections;
using System.Collections.Generic;

public enum eEffectType
{
    Touch,
    PhaseShift
}
public enum eTextEffectType
{
    ColleagueIncome
}
public enum eValueType
{
    Expo,
    Numeric,
    Percent
}

public enum eSkillID
{
    Chain,
    Overwork
}

[Serializable]
public class PlayerSaveData
{
    public int Stage;
    public int GemID;
    public double Gold;
    public double Soul;
    public double GemHP;
    public int[] PlayerLevels;
    public float[] Cooltimes;
    public int[] ColleagueLevels;
}