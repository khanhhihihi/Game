using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;
public class allDetail 
{
    public List<Map> mapdt { get; set; }

    public allDetail()
    {
    }

    public allDetail(List<Map> mapdt)
    {
        this.mapdt = mapdt;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
    public int GetLength()
    {
        return mapdt.Count;
    }
}
