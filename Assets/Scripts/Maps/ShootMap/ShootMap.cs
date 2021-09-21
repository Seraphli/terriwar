using System;
using System.Collections.Generic;
using MarbleRaceBase.Define;
using MarbleRaceBase.Utility;
using UnityEditor;
using UnityEngine;

namespace Maps.ShootMap
{
    public class ShootMap : Map
    {
        private GM _gm;
        private RealTimeData _realTimeData;
        private ShootMapData _mapData;

        void PlaceColor(Tile tile, float i, float j, int size)
        {
            var _case =
                Utils.SquareCase(i, j, size + _mapData.baseShift, _mapData.baseSize);
            if (Array.IndexOf(_mapData.teams, _case) > -1)
            {
                var oldTeam = tile.slot.index;
                tile.SetSlot(_mapData.slotsBase.slots[_case]);
                _realTimeData.UpdateCount(oldTeam, tile.slot.index);
            }

            _case = Utils.SquareCase(i, j, size + _mapData.coreShift, _mapData.coreSize);
            if (Array.IndexOf(_mapData.teams, _case) > -1)
            {
                tile.SetCore(true);
                _realTimeData.teams[_case].AddCore(tile);
            }
        }

        void PlaceTiles()
        {
            var tileZ = _mapData.tile.transform.position.z;
            _mapData.tile.transform.localScale =
                new Vector3(_mapData.tileSize, _mapData.tileSize, _mapData.tileSize);
            var tiles = new GameObject("Tiles");
            var size =
                (int) Math.Ceiling(_mapData.backgroundSize / 2f / _mapData.tileSize);
            GameObject go;
            var posIter = new SquarePos(size);
            foreach (ValueTuple<float, float> pos in posIter)
            {
                var i = pos.Item1;
                var j = pos.Item2;
                var realPos = new Vector3(i * _mapData.tileSize,
                    j * _mapData.tileSize, tileZ);
                go = Instantiate(_mapData.tile, realPos, Quaternion.identity,
                    tiles.transform);
                var t = go.GetComponent<Tile>();
                t.SetSlot(_mapData.slotsBase.defaultSlot);
                t.SetPos(i, j);
                _realTimeData.UpdateCount(t.slot.index);
                PlaceColor(t, i, j, size);
            }
        }

        // TODO: 放置cannon的逻辑
        // void PlaceCannon()
        // {
        //     var cannonZ = cannon.transform.position.z;
        //     var cannons = new GameObject("Cannons");
        //     GameObject go;
        //     int[] place = {0};
        //     foreach (var entry in gm.cores)
        //     {
        //         foreach (var i in place)
        //         {
        //             int team = entry.Key;
        //             var tilePos = entry.Value[i].transform.position;
        //             var pos = new Vector3(tilePos.x, tilePos.y, cannonZ);
        //             go = Instantiate(cannon, pos, Quaternion.identity, cannons.transform);
        //             go.layer = team;
        //             var b = go.GetComponent<Cannon>();
        //             b.gm = gm;
        //             b.team = team;
        //             var c = gm.data.teamMap[team].ballColor;
        //             b.ChangeColor(c);
        //             if (!gm.cannons.ContainsKey(team))
        //             {
        //                 gm.cannons.Add(team, go);
        //             }
        //         }
        //     }
        // }
        //
        public override void Setup()
        {
            _gm = MonoUtils.GetGM();
            _mapData = _gm.mapData as ShootMapData;
            _realTimeData = _gm.realTimeData;
            PlaceTiles();
        }
    }
}