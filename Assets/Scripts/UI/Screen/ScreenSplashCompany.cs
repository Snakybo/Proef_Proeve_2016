﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenSplashCompany : ScreenBase 
{
	public CanvasGroup overlay;
	private Vector2 goatPrintSize;
	public CanvasGroup[] groupList;
	public RectTransform[] rectList;
	public RectTransform logoBaseRect;
	public CanvasGroup logoBaseGroup;
	public RectTransform backgroundRect;
	public RectTransform background2Rect;

	[SerializeField] private AudioAsset naughtyGoatAudio;

	public override void OnScreenEnter()
	{
		overlay.alpha = 1f;
		goatPrintSize = rectList[2].sizeDelta;
	}

	public override IEnumerator OnScreenFadein()
	{
		backgroundRect.anchoredPosition3D = new Vector3 (0f, 0f, 0f);
		background2Rect.anchoredPosition3D = new Vector3 (0f, 200f, 0f);

		HOTweenHelper.Position (backgroundRect, new Vector3 (0f, -200f, 0f), 6.4f, 0f, Holoville.HOTween.EaseType.Linear);
		HOTweenHelper.Position (background2Rect, new Vector3 (0f, -100f, 0f), 6.4f, 0f, Holoville.HOTween.EaseType.Linear);

		yield return new WaitForSeconds(1f);

		logoBaseGroup.alpha = 0f;

		for(int i = 0; i < 5; i++)
		{
			groupList [i].alpha = 1f;
			rectList [i].sizeDelta = new Vector2 (0f, 0f);
		}

		HOTweenHelper.Fade (overlay, 1f, 0f, 0.5f, 0f);
		yield return new WaitForSeconds(0.4f);

		logoBaseGroup.alpha = 1f;
		logoBaseRect.sizeDelta = new Vector2 (0f, 0f);
		HOTweenHelper.Size (logoBaseRect, new Vector2 (1380f, 156f), 0.6f, 0f, Holoville.HOTween.EaseType.EaseOutElastic);

		yield return new WaitForSeconds(0.2f);

		StartCoroutine ("PlayAnimation");
	}

	public IEnumerator PlayAnimation()
	{
		yield return new WaitForSeconds(0.3f);

		groupList [0].alpha = 1f;
		HOTweenHelper.Size (rectList [0], goatPrintSize, 0.3f, 0f, Holoville.HOTween.EaseType.EaseOutBounce);

		yield return new WaitForSeconds(0.1f);

		groupList [1].alpha = 1f;
		HOTweenHelper.Size (rectList [1], goatPrintSize, 0.3f, 0f, Holoville.HOTween.EaseType.EaseOutBounce);

		yield return new WaitForSeconds(0.1f);

		groupList [2].alpha = 1f;
		HOTweenHelper.Size (rectList [2], goatPrintSize, 0.3f, 0f, Holoville.HOTween.EaseType.EaseOutBounce);

		yield return new WaitForSeconds(0.1f);

		groupList [3].alpha = 1f;
		HOTweenHelper.Size (rectList [3], goatPrintSize, 0.3f, 0f, Holoville.HOTween.EaseType.EaseOutBounce);

		yield return new WaitForSeconds(0.1f);

		groupList [4].alpha = 1f;
		HOTweenHelper.Size (rectList [4], goatPrintSize, 0.3f, 0f, Holoville.HOTween.EaseType.EaseOutBounce);

		yield return new WaitForSeconds(0.3f);

		StartCoroutine(PlayNaughtyGoatAudioDelayed());

		for(int i = 0; i < 5; i++)
		{
			if(i != 2)
			{
				HOTweenHelper.Fade (groupList [i], 1f, 0f, 0.3f, 0f);
			}
		}

		yield return new WaitForSeconds(2.5f);

		HOTweenHelper.Fade (overlay, 0f, 1f, 0.3f, 0f);

		yield return new WaitForSeconds(0.3f); 

		ScreenManager.Instance.setScreen ("ScreenSplashUnity");
	}

	private IEnumerator PlayNaughtyGoatAudioDelayed()
	{
		yield return new WaitForSeconds(0.15f);

		AudioManager.Play(naughtyGoatAudio);
	}

	void Update()
	{
		
	}

	public override IEnumerator OnScreenFadeout()
	{
		yield break;
	}

	public override void OnScreenExit()
	{
		
	}

	public override string getScreenName()
	{
		return "ScreenSplashCompany";
	}
}
