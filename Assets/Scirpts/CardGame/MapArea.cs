using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MapArea : MonoBehaviour
{
    /// <summary>
    /// 6,7,8
    /// 3,4,5
    /// 0,1,2
    /// </summary>
    public MapSolt[] MapAreas =new MapSolt[9];
    MapSolt[,] Position_MapAreas = new MapSolt[3,3];
    int Row = 3;//x
    int Column = 3;//y
    public byte PlayerOwnerID { get; private set; } = new();

    private void Awake()
    {
        int x,y ;
        for (int i = 0; i < MapAreas.Length; i++)
        {
            MapAreas[i].SetMyMapArea(this,i);
            x = i % Row;
            y = i / Column;

            //Position_MapAreas_Dictionary.Add(Position_MapAreas[x,y],MapAreas[i]);
            Position_MapAreas[x, y]=MapAreas[i];
        }

    }

    public void SetPlayerOwnerID(byte b)
    {
        PlayerOwnerID = b;
        foreach (var a in MapAreas)
        {
            a.SetPlayerOwnerNumberID(PlayerOwnerID);
        }
       
    }
    public PositionData? FindToPosition(MapSolt mapSolt)
    {
        int x, y;
        for (int i = 0; i < MapAreas.Length; i++)
        {
            y = i % Row;
            x = i / Column;

            if (Position_MapAreas[x, y] == mapSolt)
            {
                return new PositionData(x, y);
            }
              
        }
        return null;
    }
    public List<MapSolt> GetExpansion_3(MapSolt mapSolt)
    {
        int T = 3;
        var List = new List<MapSolt>();
        var data = (PositionData)FindToPosition(mapSolt);
        
        int Tag = -1;
        for(int i = 0; i < 9; i++)
        {
            var a =SwtichMapExpansion(data,i);
            if(a != null)
            {
                if (Tag == -1) Tag = i+1;
                List.Add(a);
                if (List.Count >= T) break;

            }
            if (i== Tag && List.Count < 2)
            {
                List.Clear();
            }
        }
        List.Add(mapSolt);
        return List;
    }

    MapSolt SwtichMapExpansion(PositionData data, int i)
    {
        ///5 4 3
        ///6 X 2
        ///7 0 1
        int x = data.X, y= data.Y;
        switch (i)
        {
            case 0:
            case 8:
                y = data.Y - 1;
                break;
            case 1:
                x = data.X + 1; 
                y = data.Y - 1;
                break;
            case 2:
                x = data.X + 1;
                break;
            case 3:
                x = data.X + 1;
                y = data.Y + 1;
                break;
            case 4:
                y = data.Y + 1;
                break;
            case 5:
                x = data.X - 1;
                y = data.Y + 1;
                break;
            case 6:
                x = data.X - 1;
                break;
            case 7:
                x = data.X - 1;
                y = data.Y - 1;
                break;
            default:
                return null;

        }

        if (x < Row && x > -1 && y < Column && y > -1)
            return Position_MapAreas[x, y];
        return null;
    }


    public List<MapSolt> GetRangeCrossDirections(MapSolt mapSolt)
    {
        var List = new List<MapSolt>();
        var data = (PositionData)FindToPosition(mapSolt);
        for (int i = 0; i < 7; i+=2)
        {
            var a = SwtichMapExpansion(data, i);
            if(a!=null)
                List.Add(a);
        }
          
        List.Add(mapSolt);
        return List;
    }
    public List<MapSolt> GetRange_X_Directions(MapSolt mapSolt)
    {
        ///     y,x
        /// 1,-1    1,1
        ///     0,0
        ///-1,-1    -1,1
        var List = new List<MapSolt>();
        var data = (PositionData)FindToPosition(mapSolt);
        Debug.Log("dataX" + data.X + "Y" + data.Y);
        List.Add(mapSolt);
        if (data.X - 1 > -1     && data.Y + 1 < Column)   List.Add(Position_MapAreas[data.X-1, data.Y + 1]);
        if (data.X + 1 < Row && data.Y - 1 > -1)     List.Add(Position_MapAreas[data.X+1, data.Y - 1]);

        if (data.X + 1 < Row && data.Y + 1 < Column)    List.Add(Position_MapAreas[data.X + 1, data.Y+1]);
        if (data.X - 1 > -1     && data.Y - 1 > -1)    List.Add(Position_MapAreas[data.X - 1, data.Y-1]);
        Debug.Log("ADDEND"+List.Count);
        foreach (var a in List)
        {
            var d =FindToPosition(a);
            Debug.Log("X" + d.Value.X + "Y" + d.Value.Y);
        }
        return List;
    }
    public struct PositionData
    {
        public int X, Y;
        public PositionData(int x,int y)
        {
            X = x;
            Y = y;
        }
    }
    public List<MapSolt> Get_SwitchMaps(MapSolt mapSolt, MapShowTpye ShowTpye)
    {
        List<MapSolt> CurrentMaps = new();
        switch (ShowTpye)
        {
            case MapShowTpye.X_Tpye:
                CurrentMaps = GetRange_X_Directions(mapSolt);
                break;
            case MapShowTpye.Cross_Tpye:
                CurrentMaps = GetRangeCrossDirections(mapSolt);
                break;
            case MapShowTpye.Row_Tpye:
                CurrentMaps = GetRange_Row(mapSolt);
                break;
            case MapShowTpye.Column_Tpye:
                CurrentMaps = GetRange_Column(mapSolt);
                break;
            case MapShowTpye.All:
                CurrentMaps = GetAllMapSolt();
                break;
            case MapShowTpye.Null:
                CurrentMaps.Add(mapSolt);
                break;
            case MapShowTpye.GetExpansion_3:
                CurrentMaps = GetExpansion_3(mapSolt);
                break;
            default:
                break;
        }

        return CurrentMaps;

    }
    
    public List<Unit> GetAllUnit()
    {
        var List = new List<Unit>();
        foreach (var a in MapAreas)
        {
            var u = a.GetUnit();
            if (u!=null)
            List.Add(u);
        }
        return List;
    }
    public List<MapSolt> GetAllMapSolt()
    {
        var List = new List<MapSolt>(MapAreas);
        return List;
    }


    public List<MapSolt> GetRange_Row(MapSolt map )
    {
        List<MapSolt> solts = new();
        
        int mapIndex = Array.IndexOf(MapAreas, map);
        int Run = Row;

        if (mapIndex >= 0)
        {
            int range = mapIndex / Run;
            for (int i = 0; i < Run; i++)
            {
                solts.Add(MapAreas[i + range * Run]);
            }
        }
        return solts;
    }
    public List<MapSolt> GetRange_Column(MapSolt map)
    {
        List<MapSolt> solts = new();

        int mapIndex = Array.IndexOf(MapAreas, map);
        int Run = Row;

        if (mapIndex >= 0)
        {
            int range = mapIndex % Run;
            for (int i = 0; i < MapAreas.Length / Run; i++)
            {
                solts.Add(MapAreas[ range + Run * i ]);
            }
        }
        return solts;
    }


}
