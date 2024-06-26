using UnityEngine;
using System.Collections.Generic;

public class RhythmScore : MonoBehaviour
{
	public float perfectHitThreshold = 0.1f; // Time window for a perfect hit
	public float goodHitThreshold = 0.2f;    // Time window for a good hit
	public float missThreshold = 0.3f;       // Time window for a miss

	private NoteSpawner noteSpawner;
	private Dictionary<GameObject, float> noteSpawnTimes = new Dictionary<GameObject, float>();
	private int score = 0;
	private int combo = 0;

	private void Start()
	{
		noteSpawner = FindObjectOfType<NoteSpawner>();
		noteSpawner.OnNoteSpawned += HandleNoteSpawned;
		noteSpawner.OnNoteDestroyed += HandleNoteDestroyed;
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
			score -= 10; // Subtract points for missed notes
			Debug.Log("Miss! Score: " + score + " Combo: " + combo);
		}
		noteSpawnTimes.Remove(noteObject); // Ensure note is removed from the dictionary
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			CheckForHit();
		}
	}

	private void CheckForHit()
	{
		List<GameObject> notesToRemove = new List<GameObject>();
		foreach (var kvp in noteSpawnTimes)
		{
			GameObject note = kvp.Key;
			if (note == null) continue;

			float spawnTime = kvp.Value;
			float elapsedTime = Time.time - spawnTime;

			if (elapsedTime <= perfectHitThreshold)
			{
				HandleHit(note, "Perfect");
				notesToRemove.Add(note);
			}
			else if (elapsedTime <= goodHitThreshold)
			{
				HandleHit(note, "Good");
				notesToRemove.Add(note);
			}
			else if (elapsedTime <= missThreshold)
			{
				notesToRemove.Add(note);
			}
		}

		foreach (var note in notesToRemove)
		{
			noteSpawnTimes.Remove(note);
			if (note != null) Destroy(note);
		}
	}

	private void HandleHit(GameObject note, string hitType)
	{
		Vector2 gridPosition = noteSpawner.GetGridPositionFromWorld(note.transform.position);
		noteSpawner.MarkNoteAsHit(note, gridPosition);
		switch (hitType)
		{
			case "Perfect":
				score += 100;
				combo++;
				Debug.Log("Perfect Hit! Score: " + score + " Combo: " + combo);
				break;
			case "Good":
				score += 50;
				combo++;
				Debug.Log("Good Hit! Score: " + score + " Combo: " + combo);
				break;
		}
	}
}
