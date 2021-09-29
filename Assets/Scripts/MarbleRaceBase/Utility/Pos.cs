using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarbleRaceBase.Utility
{
    public class SquarePos : IEnumerable
    {
        /* 顺时针螺旋
        14 15  4  5
        13  3  0  6
        12  2  1  7
        11 10  9  8
        */
        public int Size { get; set; }

        private float _i, _j;

        public SquarePos(int size)
        {
            Debug.Assert(size > 0);
            Size = size;
        }

        public IEnumerator GetEnumerator()
        {
            int index = 0;
            int range = 4 * Size * Size;
            int radius = 1;
            int[][] dirs = {new[] {1, 0}, new[] {0, -1}, new[] {-1, 0}, new[] {0, 1}};
            while (index < range)
            {
                // checkpoint
                float ckpt = radius - 0.5f;
                _i = 0.5f;
                _j = ckpt;
                int dirCount = 0;
                int[] curDir = dirs[dirCount];
                while (index < 4 * radius * radius)
                {
                    yield return (_i, _j);
                    index++;
                    if (Mathf.Abs(_i) == ckpt && Mathf.Abs(_j) == ckpt)
                    {
                        dirCount++;
                        if (dirCount == 4)
                        {
                            dirCount = 0;
                        }

                        curDir = dirs[dirCount];
                    }

                    _i += curDir[0];
                    _j += curDir[1];
                }

                radius++;
            }
        }
    }

    public class LoopPos : IEnumerable
    {
        public readonly int width;
        public readonly int height;

        public LoopPos(int size)
        {
            Debug.Assert(size > 0);
            width = size;
            height = size;
        }
        
        public LoopPos(int width, int height)
        {
            Debug.Assert(width > 0);
            Debug.Assert(height > 0);
            this.width = width;
            this.height = height;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = -width; i < width; i++)
            {
                for (int j = -height; j < height; j++)
                {
                    yield return (i + 0.5f, j + 0.5f);
                }
            }
        }
    }
}