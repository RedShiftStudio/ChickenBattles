using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using YG;
using System.Linq;

public class ShowAdvertisment : MonoBehaviour
{
    public void ShowAdvert()
    {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();

        Disable(sources);

        YandexGame.FullscreenShow();

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
}
