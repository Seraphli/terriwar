using System.Collections.Generic;
using MarbleRaceBase.Define;
using UnityEngine;

namespace MarbleRaceBase.Predefine
{
    public class DefaultTeams : TeamsBase
    {
        public SlotsBase slots;

        public void Setup()
        {
            teams = new List<Team>();
            for (int i = 0; i < 9; i++)
            {
                var t = new Team(slots.slots[i]);
                teams.Add(t);
                if (i == 0)
                {
                    t.enable = false;
                }
            }
        }
    }
}