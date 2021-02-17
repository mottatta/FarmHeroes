using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using LoLSDK;

using System;

public static class SharedState
{

	// private static int score = 0;
	private static JSONNode startGameData;
	private static JSONNode languageDefs;
	private static MultipleChoiceQuestionList questionList;
	private static int currentQuestionIndex = 0;

    private static int currentProgress = 0;
    private static int totalProgress = 18;
    private static int money;

    // public static int Score
    // {
    // 	get
    // 	{
    // 		return score;
    // 	}
    // 	set
    // 	{
    // 		score = value;
    // 	}
    // }
    public static void GameComplete()
    {
        if(LOLSDK.Instance != null) LOLSDK.Instance.CompleteGame();
    }

    public static void SubmitProgress()
    {

        #if UNITY_EDITOR
        #elif UNITY_WEBGL
        currentProgress++;
        if(currentProgress > totalProgress) return;
            if (currentProgress >= totalProgress)
            {
                Debug.LogError("Current progress is greater than total progress " + currentProgress.ToString());
                currentProgress = totalProgress;
            }
            LOLSDK.Instance.SubmitProgress(0, currentProgress, totalProgress);
            Debug.Log("Submit progress " + currentProgress + "/" + totalProgress);
        #endif
    }

    public static string GetJsonText(string key, bool speakText = false)
    {
        if (LanguageDefs != null)
        {
            if (LanguageDefs[key] != null)
            {
                if(speakText) LOLSDK.Instance.SpeakText(key);
                return LanguageDefs[key];
            }
            Debug.LogError("No such jason text " + key);
            return "Lorem epsum";
        }
        else return "Lorem epsum";
    }

    public static int Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
        }
    }

    public static JSONNode StartGameData
	{
		get
		{
			return startGameData;
		}
		set
		{
			startGameData = value;
		}
	}

	public static JSONNode LanguageDefs
	{
		get
		{
			return languageDefs;
		}
		set
		{
			languageDefs = value;
		}
	}

	public static MultipleChoiceQuestionList QuestionList
	{
		get
		{
			return questionList;
		}
		set
		{
			questionList = value;
		}
	}

	public static MultipleChoiceQuestion GetQuestion() {
		if (questionList != null && questionList.questions != null && currentQuestionIndex < questionList.questions.Length) {
			MultipleChoiceQuestion question = questionList.questions [currentQuestionIndex];
			currentQuestionIndex++;
			return question;
		}

		return null;
	}
}

