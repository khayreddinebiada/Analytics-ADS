using System;
using System.Collections;
using apps;
using apps.exception;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ADSSimulatorTester
{
    [Test]
    public void ADSSimulatorTesterCreateEmpty()
    {
        ArgumentEmptryOrNullException result = null;
        try
        {
            new ADSSimulator("", true, true, false);
        }
        catch (Exception e)
        {
            result = e as ArgumentEmptryOrNullException;
        }
        finally
        {
            Assert.IsTrue(result != null);
        }
    }

    [Test]
    public void ADSSimulatorTesterCreateNull()
    {
        ArgumentEmptryOrNullException result = null;
        try
        {
            new ADSSimulator(null, true, true, false);
        }
        catch (Exception e)
        {
            result = e as ArgumentEmptryOrNullException;
        }
        finally
        {
            Assert.IsTrue(result != null);
        }
    }
    
    [Test]
    public void ADSSimulatorTesterCreationRight()
    {
        new ADSSimulator("kssss", true, false, false);
    }

    [Test]
    public void ADSSimulatorTesterCreationCheckUses()
    {
        ADSSimulator simulator = new ADSSimulator("kssss", true, true, true);
        Assert.IsTrue(simulator.useBanner);
        Assert.IsTrue(simulator.useInterstitial);
        Assert.IsTrue(simulator.useRewardedVideo);
    }

    [Test]
    public void ADSSimulatorTesterCreationCheckUses2()
    {
        ADSSimulator simulator = new ADSSimulator("kssss", false, false, false);
        Assert.IsTrue(!simulator.useBanner);
        Assert.IsTrue(!simulator.useInterstitial);
        Assert.IsTrue(!simulator.useRewardedVideo);
    }

    [Test]
    public void ADSSimulatorTesterCreationCheckUses3()
    {
        ADSSimulator simulator = new ADSSimulator("kssss", false, true, false);
        Assert.IsTrue(!simulator.useBanner);
        Assert.IsTrue(simulator.useInterstitial);
        Assert.IsTrue(!simulator.useRewardedVideo);
    }

    [Test]
    public void ADSSimulatorTesterCreationCheckUses4()
    {
        ADSSimulator simulator = new ADSSimulator("kssss", true, false, false);
        Assert.IsTrue(simulator.useBanner);
        Assert.IsTrue(!simulator.useInterstitial);
        Assert.IsTrue(!simulator.useRewardedVideo);
    }

    [Test]
    public void ADSSimulatorTesterCreationNonLoadBanner()
    {
        ADSSimulator simulator = new ADSSimulator("kssss", false, true, true);
        simulator.LoadBanner();
        Assert.IsTrue(!simulator.bannerIsLoaded);
    }

    [Test]
    public void ADSSimulatorTesterCreationLoadDisplayBanner()
    {
        ADSSimulator simulator = new ADSSimulator("kssss", true, false, false);

        Assert.IsTrue(!simulator.bannerIsDisplaying);
        simulator.DisplayBanner();
        Assert.IsTrue(simulator.bannerIsDisplaying);
    }

    [Test]
    public void ADSSimulatorTesterWithShowRewardsVideoOption1()
    {
        new GameObject("Camre View").AddComponent(typeof(Camera));
        ADSSimulator simulator = new ADSSimulator("kssss", false, false, true);

        simulator.LoadRewardedVideo();
        simulator.ShowRewardedVideo(null);
    }

    [UnityTest]
    public IEnumerator ADSSimulatorTesterWithShowInterstitial()
    {
        bool isPlaying = true;
        new GameObject("Camre View").AddComponent(typeof(Camera));

        ADSSimulator simulator = new ADSSimulator("kssss", true, true, true);

        Assert.IsTrue(!simulator.interstitialIsDisplaying);
        simulator.ShowInterstitial("Interstitial View", () => isPlaying = false);
        Assert.IsTrue(!simulator.interstitialIsLoaded);
        Assert.IsTrue(simulator.interstitialIsDisplaying);

        while (isPlaying)
        {
            Assert.IsTrue(simulator.interstitialIsDisplaying);
            yield return null;
        }
    }

    [UnityTest]
    public IEnumerator ADSSimulatorTesterWithShowRewardsVideo()
    {
        bool isPlaying = true;
        new GameObject("Camre View").AddComponent(typeof(Camera));

        ADSSimulator simulator = new ADSSimulator("kssss", false, false, true);

        Assert.IsTrue(!simulator.rewardsVideoIsDisplaying);
        Assert.IsTrue(simulator.ShowRewardedVideo("Rewards Video x5 Coins", () => isPlaying = false, () => isPlaying = false));
        Assert.IsTrue(!simulator.rewardsVideoIsLoaded);
        Assert.IsTrue(simulator.rewardsVideoIsDisplaying);

        while (isPlaying)
        {
            Assert.IsTrue(simulator.rewardsVideoIsDisplaying);
            yield return null;
        }
    }
}
