using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MarbleRaceBase.Utility;

public class TestPos
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestPosSize1()
    {
        // Use the Assert class to test conditions
        int count = 0;
        var p = new SquarePos(1);
        foreach (var VARIABLE in p)
        {
            Debug.Log(VARIABLE);
            count++;
        }

        Assert.AreEqual(count, 4);
    }

    [Test]
    public void TestPosSize2()
    {
        // Use the Assert class to test conditions
        int count = 0;
        var p = new SquarePos(2);
        foreach (var VARIABLE in p)
        {
            Debug.Log(VARIABLE);
            count++;
        }

        Assert.AreEqual(count, 16);
    }

    [Test]
    public void TestPosFloatCheck()
    {
        var a = 0.5f;
        a += 1;
        Assert.IsTrue(1.5f == a);
    }
}