using apps;
using apps.exception;
using NUnit.Framework;
using System;

public class TestEvents
{
    [Test]
    public void TestEventsLoogerAddEvent()
    {
        ArrayHasObjectNullException result = null;
        try
        {
            EventsLogger.AddEvents(new IEvent[] { null });
        }
        catch (Exception e)
        {
            result = e as ArrayHasObjectNullException;
        }
        finally
        {
            Assert.IsTrue(result != null);
        }
    }

    [Test]
    public void TestEventsLoogerAddEvents()
    {
        ArgumentNullException result = null;
        try
        {
            EventsLogger.AddEvent(null);
        }
        catch (Exception e)
        {
            result = e as ArgumentNullException;
        }
        finally
        {
            Assert.IsTrue(result != null);
        }
    }

    [Test]
    public void TestEventsLoggerDebugger()
    {
        EventsLogger.AddEvent(new EventsDebug());
    }

    [Test]
    public void SendSomeEventsWithoutIEvents()
    {
        EventsLogger.ClearEvents();
        EventsLogger.AddEvent(new EventsDebug());
        EventsLogger.CustomEvent("BoughtProduct:RemoveADS:Product_1");
        EventsLogger.CustomEvent("EntryGallery:Menu", 5);
        EventsLogger.CustomEvent("ExitStore:Main:Choice colors", -50);
        EventsLogger.CustomEvent("EntryStore", -0);
    }
}
