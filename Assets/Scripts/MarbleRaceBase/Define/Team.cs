using System;
using System.Collections.Generic;
using UnityEngine;

namespace MarbleRaceBase.Define
{
    /// <summary>
    /// Team information in real time data
    /// </summary>
    [Serializable]
    public class Team
    {
        public Slot slot;
        public bool enable = true;
        public int tileCount = 0;
        public int ammo = 0;
        public List<Tile> cores = new List<Tile>();
        public List<Marble> marbles = new List<Marble>();

        public Team(Slot slot)
        {
            this.slot = slot;
        }

        public void AddMarble(Marble m)
        {
            marbles.Add(m);
        }

        public void RemoveMarble(Marble m)
        {
            marbles.Remove(m);
        }

        public void AddCore(Tile tile)
        {
            cores.Add(tile);
        }

        public void RemoveCore(Tile tile)
        {
            cores.Remove(tile);
            if (cores.Count == 0)
            {
                SelfDestruction();
            }
        }

        public void SelfDestruction()
        {
            enable = false;
            foreach (var m in marbles)
            {
                m.SelfDestruction();
            }
        }
    }
}