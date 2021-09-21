using System;
using System.Collections.Generic;
using MarbleRaceBase.Define;
using MarbleRaceBase.Utility;
using UnityEngine;

namespace Maps.CircleMap
{
    public class CircleMap : Map
    {
        private GM _gm;
        private RealTimeData _realTimeData;
        private CircleMapData _mapData;

        void PlaceColor(Tile tile, float i, float j, int size)
        {
            var _case =
                Utils.CircleCase(i, j, size + _mapData.baseShift, _mapData.baseSize);
            int team = _case + 5;
            if (Array.IndexOf(_mapData.teams, _case) > -1)
            {
                var oldTeam = tile.slot.index;
                tile.SetSlot(_mapData.slotsBase.slots[_case]);
                _realTimeData.UpdateCount(oldTeam, tile.slot.index);
            }

            _case = Utils.CircleCase(i, j, size + _mapData.coreShift, _mapData.coreSize);
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
            var posIter = new LoopPos(size);
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

        void PlaceMarble()
        {
            var marbleZ = _mapData.marble.transform.position.z;
            var marbles = new GameObject("Marbles");
            GameObject go;
            foreach (var entry in _gm.realTimeData.teams)
            {
                if (!entry.Value.enable)
                {
                    continue;
                }
                foreach (var i in _mapData.marblePlaces)
                {
                    var tilePos = entry.Value.cores[i].transform.position;
                    var pos = new Vector3(tilePos.x, tilePos.y, marbleZ);
                    go = Instantiate(_mapData.marble, pos, Quaternion.identity,
                        marbles.transform);
                    var m = go.GetComponent<Marble>();
                    m.SetSlot(entry.Value.slot);
                    _gm.realTimeData.teams[entry.Key].AddMarble(m);
                }
            }
        }


        public override void Setup()
        {
            _gm = MonoUtils.GetGM();
            _mapData = _gm.mapData as CircleMapData;
            _realTimeData = _gm.realTimeData;
            PlaceTiles();
            PlaceMarble();
        }
    }
}