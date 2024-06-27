using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class RhythmScore : MonoBehaviour
{
	public float perfectHit;
	public float goodHit;
	public float miss;

	public int perfectScore;
	public int goodScore;
	public int missScore;

	public TMP_Text scoreText;
	public TMP_Text comboText;

	public InputReader inputReader;

	private NoteSpawner noteSpawner;
	private Dictionary<GameObject, float> noteSpawnTimes = new Dictionary<GameObject, float>();
	private int score = 0;
	private int combo = 0;

	private void Start()
	{
		noteSpawner = FindObjectOfType<NoteSpawner>();
		noteSpawner.OnNoteSpawned += HandleNoteSpawned;
		noteSpawner.OnNoteDestroyed += HandleNoteDestroyed;

		UpdateScoreText();
	}

	private void Update()
	{
		CheckForTouchHit();
	}

	private void HandleNoteSpawned(GameObject noteObject, Vector2 gridPosition)
	{
		noteSpawnTimes[noteObject] = Time.time;
	}

	private void HandleNoteDestroyed(GameObject noteObject, bool isMissed)
	{
		if (isMissed)
		{
			combo = 0;
			score -= missScore;
			Debug.Log("Miss! Score: " + score + " Combo: " + combo);

			UpdateScoreText();
			UpdateComboText();
		}
		noteSpawnTimes.Remove(noteObject);
	}

	private void CheckForTouchHit()
	{
		List<GameObject> notesToRemove = new List<GameObject>();

		foreach (var kvp in new Dictionary<GameObject, float>(noteSpawnTimes))
		{
			GameObject note = kvp.Key;
			if (note == null) continue;

			float spawnTime = kvp.Value;
			float elapsedTime = Time.time - spawnTime;

			foreach (Touch touch in Input.touches)
			{
				if (touch.phase == TouchPhase.Began)
				{
					Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
					if (Vector2.Distance(touchPosition, note.transform.position) <= perfectHit)
					{
						HandleHit(note, "Perfect");
						notesToRemove.Add(note);
						break;
					}
					else if (Vector2.Distance(touchPosition, note.transform.position) <= goodHit)
					{
						HandleHit(note, "Good");
						notesToRemove.Add(note);
						break;
					}
				}
			}

			if (elapsedTime > miss)
			{
				notesToRemove.Add(note);
			}
		}

		foreach (var note in notesToRemove)
		{
			if (noteSpawnTimes.ContainsKey(note))
			{
				noteSpawnTimes.Remove(note);
				Destroy(note);
			}
		}
	}

	private void HandleHit(GameObject note, string hitType)
	{
		Vector2 gridPosition = noteSpawner.GetGridPositionFromWorld(note.transform.position);
		noteSpawner.MarkNoteAsHit(note, gridPosition);

		switch (hitType)
		{
			case "Perfect":
				score += perfectScore;
				combo++;
				Debug.Log("Perfect Hit! Score: " + score + " Combo: " + combo);
				break;
			case "Good":
				score += goodScore;
				combo++;
				Debug.Log("Good Hit! Score: " + score + " Combo: " + combo);
				break;
		}
		UpdateScoreText();
		UpdateComboText();
	}

	private void UpdateScoreText()
	{
		scoreText.text = "" + score;
	}

	private void UpdateComboText()
	{
		comboText.text = combo + "\n Combo";
		comboText.fontSize = 16;
	}
}
