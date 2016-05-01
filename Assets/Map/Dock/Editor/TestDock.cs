using System;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class TestDock {

    [Test]
    public void AddUtilizedTimeTest()
    {
		Dock dock = new Dock ();
		dock.utilizedTime = new TimeSpan (1, 2, 3);

		dock.AddUtilizeTime (new TimeSpan (0, 20, 13));

		Assert.AreEqual (new TimeSpan (1, 22, 16), dock.utilizedTime);
    }
}
