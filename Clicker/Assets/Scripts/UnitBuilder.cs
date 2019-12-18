using System.Collections;
using System.Collections.Generic;

public static class UnitBuilder
{
    private static readonly string[] UNIT_ARR = { "", "K", "M", "B", "T", "aa", "ab", "ac" };
    public static string GetUnitStr(double value)
    {
        string valStr = value.ToString("N0");
        string[] splited = valStr.Split(',');

        string result = "";

        if(splited.Length > 1)
        {
            char[] underPoint = splited[1].ToCharArray();
            result = string.Format("{0}.{1}{2} {3}", splited[0], underPoint[0], underPoint[1], UNIT_ARR[splited.Length - 1]);
        }
        else
        {
            result = splited[0];
        }
        return result;
    }
}
