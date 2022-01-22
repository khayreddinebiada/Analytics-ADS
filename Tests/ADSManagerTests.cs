using System;
using System.Collections;
using apps;
using apps.exception;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class ADSManagerTests
{
    ADSSimulator simulator;

    [UnityTest]
    public IEnumerator ADSManagerTesterAutoInterstitial()
    {
        EventsLogger.AddEvent(new EventsDebug());
        new GameObject("Camre View").AddComponent(typeof(Camera));
        simulator = new ADSSimulator("YesWorking", true, false, false);
        ADSManager.Initialize(simulator, true, 5);

        Assert.IsTrue(ADSManager.isInited);
        Assert.IsTrue(ADSManager.enableBanner);
        Assert.IsFalse(ADSManager.enableInterstitial);
        Assert.IsFalse(ADSManager.enableRewardedVideo);

        ADSManager.DisplayBanner();
        Assert.IsTrue(simulator.bannerIsDisplaying);
        ADSManager.HideBanner();
        Assert.IsFalse(simulator.bannerIsDisplaying);

        ADSManager.enableBanner = false;

        ADSManager.DisplayBanner();
        Assert.IsFalse(simulator.bannerIsDisplaying);
        ADSManager.HideBanner();
        Assert.IsFalse(simulator.bannerIsDisplaying);


        Assert.IsFalse(simulator.interstitialIsDisplaying);
        ADSManager.enableInterstitial = true;
        Assert.IsFalse(ADSManager.AutoShowInterstitial("AutoShowInterstitial"));
        Assert.IsFalse(simulator.interstitialIsDisplaying);
        
        yield return new WaitForSeconds(5f);
        Assert.IsTrue(ADSManager.AutoShowInterstitial("AutoShowInterstitial"));

        while (simulator.interstitialIsDisplaying)
        {
            Assert.IsTrue(simulator.interstitialIsDisplaying);
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        Assert.IsFalse(ADSManager.AutoShowInterstitial("AutoShowInterstitial"));

        yield return new WaitForSeconds(3f);
        Assert.IsTrue(ADSManager.AutoShowInterstitial("AutoShowInterstitial"));

        while (simulator.interstitialIsDisplaying)
        {
            Assert.IsTrue(simulator.interstitialIsDisplaying);
            yield return null;
        }

        ADSManager.enableInterstitial = false;
        Assert.IsFalse(simulator.interstitialIsDisplaying);
        ADSManager.ShowInterstitial("Yes");
        Assert.IsFalse(simulator.interstitialIsDisplaying);
    }

    [UnityTest]
    public IEnumerator ADSManagerTesterShowInterstitial()
    {
        Assert.IsFalse(simulator.interstitialIsDisplaying);
        ADSManager.enableInterstitial = true;
        ADSManager.ShowInterstitial("Yes");
        Assert.IsTrue(simulator.interstitialIsDisplaying);
        while (simulator.interstitialIsDisplaying)
        {
            Assert.IsTrue(simulator.interstitialIsDisplaying);
            yield return null;
        }


        ADSManager.enableInterstitial = false;
        Assert.IsFalse(simulator.interstitialIsDisplaying);
        ADSManager.ShowInterstitial("Yes");
        Assert.IsFalse(simulator.interstitialIsDisplaying);
    }

    [UnityTest]
    public IEnumerator ADSManagerTesterShowRewardedVideo()
    {
        Assert.IsFalse(simulator.rewardsVideoIsDisplaying);
        ADSManager.ShowRewardedVideo("Yes", null);
        Assert.IsFalse(simulator.rewardsVideoIsDisplaying);

        ADSManager.enableRewardedVideo = false;
        Assert.IsFalse(simulator.rewardsVideoIsDisplaying);
        ADSManager.ShowRewardedVideo("Yes", null);
        Assert.IsFalse(simulator.rewardsVideoIsDisplaying);

        ADSManager.enableRewardedVideo = true;
        ADSManager.ShowRewardedVideo("Yes", null);
        Assert.IsTrue(simulator.rewardsVideoIsDisplaying);
        while (simulator.rewardsVideoIsDisplaying)
        {
            Assert.IsTrue(simulator.rewardsVideoIsDisplaying);
            yield return null;
        }
    }
}