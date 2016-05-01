using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class DockUtilizationCounterTest {

    [Test]
    public void CountAverageDockUtilizationTest()
    {
		Map map = new Map ();

		Dock dock1 = new Dock ();
		Dock dock2 = new Dock ();
		map.AddDock (dock1);
		map.AddDock (dock2);

		MapController mapController = new MapController ();



    }
}
