using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class RhythmPatternEditor : EditorWindow
{
	public AudioSource audioSource;
	private AudioClip audioClip;
	private bool isPlaying = false;
	private bool isPaused = false;
	private float songPosition = 0f;
	private float songDuration = 0f;

	private List<float> beatTimes = new List<float>();

	[MenuItem("Window/Rhythm Pattern Editor")]
	public static RhythmPatternEditor ShowWindow()
	{
		RhythmPatternEditor editor = GetWindow<RhythmPatternEditor>("Rhythm Pattern Editor");
		return editor;
	}

	private void OnGUI()
	{
		GUILayout.Label("Rhythm Pattern Editor", EditorStyles.boldLabel);

		audioClip = (AudioClip)EditorGUILayout.ObjectField("Audio Clip", audioClip, typeof(AudioClip), false);

		if (audioClip != null)
		{
			GUILayout.BeginHorizontal();
			if (isPlaying)
			{
				if (isPaused)
				{
					if (GUILayout.Button("Play"))
					{
						TogglePause();
					}
				}
				else
				{
					if (GUILayout.Button("Pause"))
					{
						TogglePause();
					}
				}

				if (GUILayout.Button("Restart"))
				{
					RestartSong();
				}
			}
			else
			{
				if (GUILayout.Button("Play"))
				{
					PlayAudioClip();
				}
			}
			GUILayout.EndHorizontal();

			GUILayout.Label("Song Position: " + FormatTime(songPosition) + " / " + FormatTime(songDuration));
		}

		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Add Beat Time"))
		{
			AddBeatTime();
		}

		if (GUILayout.Button("Delete All Beat Times"))
		{
			DeleteAllBeatTimes();
		}
		GUILayout.EndHorizontal();

		GUILayout.Space(20);

		if (GUILayout.Button("Save Note Chart"))
		{
			SaveNoteChart();
		}

		GUILayout.Space(20);

		GUILayout.Label("Beat Times:", EditorStyles.boldLabel);
		for (int i = 0; i < beatTimes.Count; i++)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label(FormatTime(beatTimes[i]));
			if (GUILayout.Button("Delete"))
			{
				DeleteBeatTime(i);
			}
			GUILayout.EndHorizontal();
		}
	}

	private void PlayAudioClip()
	{
		if (audioSource == null)
		{
			GameObject audioSourceObject = new GameObject("AudioSourceObject");
			audioSource = audioSourceObject.AddComponent<AudioSource>();
			audioSource.clip = audioClip;
			songDuration = audioClip.length;
		}

		if (isPaused)
		{
			audioSource.UnPause();
		}
		else
		{
			audioSource.Play();
		}

		isPlaying = true;
		isPaused = false;

		EditorApplication.update += UpdateSongPosition;
	}

	private void TogglePause()
	{
		if (audioSource != null)
		{
			if (isPaused)
			{
				audioSource.UnPause();
				isPaused = false;
				EditorApplication.update += UpdateSongPosition;
			}
			else
			{
				audioSource.Pause();
				isPaused = true;
				EditorApplication.update -= UpdateSongPosition;
			}
		}
	}

	private void RestartSong()
	{
		if (audioSource != null)
		{
			audioSource.Stop();
			audioSource.time = 0f;
			songPosition = 0f;
			isPaused = false;
			PlayAudioClip();
		}
	}

	private void UpdateSongPosition()
	{
		if (audioSource != null && audioSource.isPlaying)
		{
			songPosition = audioSource.time;
			Repaint();
		}
	}

	private string FormatTime(float time)
	{
		int minutes = Mathf.FloorToInt(time / 60f);
		int seconds = Mathf.FloorToInt(time % 60f);
		return string.Format("{0:00}:{1:00}", minutes, seconds);
	}

	private void AddBeatTime()
	{
		beatTimes.Add(songPosition);
		Debug.Log("Added Beat Time at: " + FormatTime(songPosition));
	}

	private void DeleteAllBeatTimes()
	{
		beatTimes.Clear();
		Debug.Log("All beat times deleted.");
	}

	private void DeleteBeatTime(int index)
	{
		if (index >= 0 && index < beatTimes.Count)
		{
			beatTimes.RemoveAt(index);
			Debug.Log("Deleted Beat Time at index: " + index);
		}
	}

	private void SaveNoteChart()
	{
		string path = Application.dataPath + "/beatTimes.json";
		NoteChart noteChart = new NoteChart { beatTimes = beatTimes };

		string json = JsonUtility.ToJson(noteChart, true);
		File.WriteAllText(path, json);
		Debug.Log("Note chart saved to " + path);
	}

	[System.Serializable]
	public class NoteChart
	{
		public List<float> beatTimes;
	}
}
