using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;
public enum MapState
{
    Ground,
    Jump,
    Forest,
}
public class Map 
{
    public int x {get; set; }
    public int y {get; set;}
    public MapState Mapstate {get; set; }

    public Map ()
    {

    }

    public Map (int x, int y, MapState mapState)
    {
        this.x = x;
        this.y = y;
        this.Mapstate = mapState;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
