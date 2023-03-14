using UnityEngine;

using Newtonsoft.Json.Linq;

public static class JArrayExtended
{
    public static JArray setJArrayResult(this JArray data, int index)
    {
        if (data[index].ToString().ToLower() != "false")
        {
            return JArray.Parse(data[index].ToString());
        }
        else
        {
            return null;
        }
    }

    public static bool isJArrayNull(this JArray data)
    {
        if (data == null)
        {
            return true;
        }
        else
        {
            if (data.Count == 0) return true;
        }

        return false;
    }

    public static bool checkingJArrayData(this JArray data)
    {
        if (data == null) return false;

        return (data.Count == 0) ? false : true;
    }
}
