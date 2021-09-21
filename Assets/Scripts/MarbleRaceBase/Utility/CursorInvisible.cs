using UnityEngine;

namespace MarbleRaceBase.Utility
{
    public class CursorInvisible : MonoBehaviour
    {
        public void Setup()
        {
            if (!Application.isEditor)
            {
                Cursor.visible = false;
            }
        }
    }
}