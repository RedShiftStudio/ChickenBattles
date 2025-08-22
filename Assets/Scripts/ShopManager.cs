using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    int pl1Times;

    int pl2Times;

    [SerializeField] List<WatchTimes> gameObjectsPlanes1;

    [SerializeField] List<WatchTimes> gameObjectsplanes2;

    [SerializeField] int[] costAdPl1;

    [SerializeField] int[] costAdPl2;

    string[] costStringAdpl1;

    string[] costStringAdpl2;

    [SerializeField] GameObject[] WatchAdButton;

    [SerializeField] Text curCostAd1;
    [SerializeField] Text curCostAd2;

    int currentIndex1;
    int currentIndex2;
 
    private void Awake()
    {
        //DontDestroyOnLoad(this);s

        instance = this;

        YandexGame.LoadLocal();

        pl1Times = YandexGame.savesData.pl1Times;

        pl2Times = YandexGame.savesData.pl2Times;

        costStringAdpl1 = costAdPl1.ToList().Select(x => x.ToString()).ToArray();

        costStringAdpl2 = costAdPl2.ToList().Select(x => x.ToString()).ToArray();

        costStringAdpl1[0] = "";

        costStringAdpl2[0] = "";

        CheckForLocalAfterRestart();
    }

    private void Update()
    {
        CheckWhoIsChosen();
    }

    public void CheckWhoIsChosen()
    {
        for (int i = 0; i < gameObjectsPlanes1.Count; i++)
        {
            if (gameObjectsPlanes1[i].gameObject.activeSelf)
            {
                if (!gameObjectsPlanes1[i].isBought)
                {
                    ShowWatchAdButton();
                }
                else
                {
                    HideAdWatchButton();
                }
            }
        }

        for (int i = 0; i < gameObjectsplanes2.Count; i++)
        {
            if (gameObjectsplanes2[i].gameObject.activeSelf)
            {
                if (!gameObjectsplanes2[i].isBought)
                {
                    ShowWatchAdButton2();
                }
                else
                {
                    HideAdWatchButton2();
                }
            }
        }
    }

    public void WatchRewVideo()
    {
        ShowAdvert();

        pl1Times++;

        YandexGame.savesData.pl1Times = pl1Times;

        YandexGame.SaveLocal();

        CheckForAvailiableSkin();

        ShowCur();
    }

    public void WatchRewVideo2()
    {
        ShowAdvert();

        pl2Times++;

        YandexGame.savesData.pl2Times = pl2Times;

        YandexGame.SaveLocal();

        CheckForAvailiableSkin();

        ShowCur2();
    }

    public void ShowWatchAdButton()
    {
        WatchAdButton[0].SetActive(true);
    }

    public void ShowWatchAdButton2()
    {
        WatchAdButton[1].SetActive(true);
    }

    public void HideAdWatchButton()
    {
        WatchAdButton[0].SetActive(false);
    }

    public void HideAdWatchButton2()
    {
        WatchAdButton[1].SetActive(false);
    }

    public void CheckForAvailiableSkin()
    {
        Debug.Log(pl1Times);
        Debug.Log(pl2Times);



        for (int i = 0; i < costAdPl1.Length; i++)
        {
            if (costAdPl1[i].Equals(pl1Times))
            {
                gameObjectsPlanes1[i].isBought = true;
            }
        }

        for (int i = 0; i < costAdPl2.Length; i++)
        {
            if (costAdPl2[i].Equals(pl2Times))
            {
                gameObjectsplanes2[i].isBought = true;
            }
        }
    }

    public void CheckForLocalAfterRestart()
    {
        for (int i = 0; i < costAdPl1.Length; i++)
        {
            if (costAdPl1[i] <= pl1Times)
            {
                gameObjectsPlanes1[i].isBought = true;
            }
        }

        for (int i = 0; i < costAdPl2.Length; i++)
        {
            if (costAdPl2[i] <= pl2Times)
            {
                gameObjectsplanes2[i].isBought = true;
            }
        }
    }

    public void ShowAdvert()
    {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();

        Disable(sources);

        YandexGame.RewVideoShow(0);

        Enable(sources);
    }

    void Enable(AudioSource[] sources)
    {
        sources.ToList().ForEach(source =>
        {
            source.volume = 1;
        });
    }

    void Disable(AudioSource[] sources)
    {
        sources.ToList().ForEach(source =>
        {
            source.volume = 0;
        });
    }

    public void ShowNext()
    {
        currentIndex1 = (currentIndex1 + 1) % costStringAdpl1.Length;
        curCostAd1.text = pl1Times + "/" + costStringAdpl1[currentIndex1].ToString();
    }

    public void ShowPrevious()
    {
        currentIndex1 = (currentIndex1 - 1 + costStringAdpl1.Length) % costStringAdpl1.Length;
        curCostAd1.text = pl1Times + "/" + costStringAdpl1[currentIndex1].ToString();
    }

    public void ShowCur()
    {
        curCostAd1.text = pl1Times + "/" + costStringAdpl1[currentIndex1].ToString();
    }

    public void ShowCur2()
    {
        curCostAd2.text = pl2Times + "/" + costStringAdpl2[currentIndex2].ToString();
    }

    public void ShowNext2()
    {
        currentIndex2 = (currentIndex2 + 1) % costStringAdpl2.Length;
        curCostAd2.text = pl2Times + "/" + costStringAdpl2[currentIndex2].ToString();
    }

    public void ShowPrevious2()
    {
        currentIndex2 = (currentIndex2 - 1 + costStringAdpl2.Length) % costStringAdpl2.Length;
        curCostAd2.text = pl2Times + "/" + costStringAdpl2[currentIndex2].ToString();
    }
}
