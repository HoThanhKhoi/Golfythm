using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class NoteSpawner : MonoBehaviour
{
	public Sprite noteSprite;
	public Sprite ghostSprite;
	public int gridColumns;
	public int gridRows;
	public float noteScaleFactor;
	//public AudioClip noteSound;
	public int minDistanceFromPreviousNote;
	public int maxDistanceFromPreviousNote;
	public float ghostCircleDuration;
	public float destroyDuration;
	public float simultaneousSpawnProbability;

	private float spriteWidth, spriteHeight;
	private float verticalSize, horizontalSize;
	private Dictionary<Vector2, GameObject> activeNotes = new Dictionary<Vector2, GameObject>();
	//private AudioSource audioSource;

	private Vector2 previousNotePosition = Vector2.zero;
	private List<float> noteTimes;

	private void Start()
	{
		UpdateGridParameters();
		//audioSource = gameObject.GetComponent<AudioSource>();
		LoadNoteChart();
	}

	private void UpdateGridParameters()
	{
		verticalSize = Camera.main.orthographicSize;
		horizontalSize = verticalSize * Camera.main.aspect;

		spriteWidth = (horizontalSize * 2) / gridColumns;
		spriteHeight = (verticalSize * 2) / gridRows;
	}

	private void LoadNoteChart()
	{
		string path = Application.dataPath + "/beatTimes.json";
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			NoteChart noteChart = JsonUtility.FromJson<NoteChart>(json);
			noteTimes = noteChart.beatTimes;

			if (noteTimes.Count > 0)
			{
				StartCoroutine(PlayNotes());
			}
		}
		else
		{
			Debug.LogError("Note chart file not found at " + path);
		}
	}

	private IEnumerator PlayNotes()
	{
		float startTime = Time.time;
		for (int i = 0; i < noteTimes.Count; i++)
		{
			float waitTime = noteTimes[i] - (Time.time - startTime);
			yield return new WaitForSeconds(waitTime - ghostCircleDuration);

			Vector2 newNotePosition = GetNextRandomGridPosition(previousNotePosition);

			if (Random.value < simultaneousSpawnProbability)
			{
				// Spawn on both sides
				Vector2 mirrorPosition = GetMirrorPosition(newNotePosition);
				SpawnGhost(newNotePosition);
				SpawnGhost(mirrorPosition);
				//audioSource.PlayOneShot(noteSound);
				yield return new WaitForSeconds(ghostCircleDuration);
				SpawnNoteAtPosition(newNotePosition);
				SpawnNoteAtPosition(mirrorPosition);
			}
			else
			{
				// Spawn on one side
				bool spawnOnLeft = Random.value < 0.5f;
				if (spawnOnLeft)
				{
					SpawnGhost(newNotePosition);
					//audioSource.PlayOneShot(noteSound);
					yield return new WaitForSeconds(ghostCircleDuration);
					SpawnNoteAtPosition(newNotePosition);
				}
				else
				{
					Vector2 mirrorPosition = GetMirrorPosition(newNotePosition);
					SpawnGhost(mirrorPosition);
					//audioSource.PlayOneShot(noteSound);
					yield return new WaitForSeconds(ghostCircleDuration);
					SpawnNoteAtPosition(mirrorPosition);
				}
			}

			previousNotePosition = newNotePosition;
		}
	}

	private Vector2 GetNextRandomGridPosition(Vector2 currentGridPos)
	{
		Vector2 newGridPosition;
		int attempts = 0;

		do
		{
			int tempRandomX = Random.Range(minDistanceFromPreviousNote, maxDistanceFromPreviousNote + 1);
			int tempRandomY = Random.Range(minDistanceFromPreviousNote, maxDistanceFromPreviousNote + 1);
			int randomDirectionX = Random.Range(0, 2) == 0 ? 1 : -1;
			int randomDirectionY = Random.Range(0, 2) == 0 ? 1 : -1;

			newGridPosition = new Vector2(currentGridPos.x + tempRandomX * randomDirectionX, currentGridPos.y + tempRandomY * randomDirectionY);

			// Clamp to ensure the new position is within the grid bounds
			newGridPosition.x = Mathf.Clamp(newGridPosition.x, 0, gridColumns - 1);
			newGridPosition.y = Mathf.Clamp(newGridPosition.y, 0, gridRows - 1);

			attempts++;
		}
		while ((newGridPosition == currentGridPos ||
				activeNotes.ContainsKey(newGridPosition) ||
				activeNotes.ContainsKey(GetMirrorPosition(newGridPosition)) ||
				!IsValidDistance(newGridPosition, currentGridPos)) && attempts < 100);

		return newGridPosition;
	}

	private bool IsValidDistance(Vector2 newGridPosition, Vector2 currentGridPos)
	{
		Vector2 mirrorPosition = GetMirrorPosition(newGridPosition);

		float distanceToCurrent = Vector2.Distance(newGridPosition, currentGridPos);
		float distanceToMirror = Vector2.Distance(newGridPosition, mirrorPosition);

		bool isValidDistance = distanceToCurrent >= minDistanceFromPreviousNote &&
							   distanceToCurrent <= maxDistanceFromPreviousNote &&
							   distanceToMirror >= minDistanceFromPreviousNote &&
							   distanceToMirror <= maxDistanceFromPreviousNote;

		return isValidDistance;
	}

	private Vector2 GetMirrorPosition(Vector2 originalPosition)
	{
		int mirroredX = gridColumns - 1 - (int)originalPosition.x;
		return new Vector2(mirroredX, originalPosition.y);
	}

	private void SpawnGhost(Vector2 gridPosition)
	{
		Vector3 worldPosition = GetWorldPositionFromGrid(gridPosition);

		GameObject ghostObject = new GameObject("Ghost");
		ghostObject.transform.position = worldPosition;
		ghostObject.transform.localScale = Vector3.zero; // Start scale
		var ghostRenderer = ghostObject.AddComponent<SpriteRenderer>();
		ghostRenderer.sprite = ghostSprite;
		ghostRenderer.color = Color.red;

		StartCoroutine(AnimateGhostCircle(ghostObject));
	}

	private Vector3 GetWorldPositionFromGrid(Vector2 gridPosition)
	{
		float posX = -horizontalSize + gridPosition.x * spriteWidth + spriteWidth / 2;
		float posY = -verticalSize + gridPosition.y * spriteHeight + spriteHeight / 2;
		return new Vector3(posX, posY, 0);
	}

	private void SpawnNoteAtPosition(Vector2 gridPosition)
	{
		Vector3 worldPosition = GetWorldPositionFromGrid(gridPosition);

		GameObject noteObject = new GameObject("Note");
		noteObject.transform.position = worldPosition;
		noteObject.transform.localScale = new Vector3(noteScaleFactor, noteScaleFactor, 1);

		var spriteRenderer = noteObject.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = noteSprite;

		activeNotes[gridPosition] = noteObject;
		StartCoroutine(DestroyNoteAfterBeat(noteObject));
	}

	private IEnumerator DestroyNoteAfterBeat(GameObject noteObject)
	{
		yield return new WaitForSeconds(destroyDuration);

		activeNotes.Remove(GetGridPositionFromWorld(noteObject.transform.position));
		Destroy(noteObject);
	}

	private IEnumerator AnimateGhostCircle(GameObject ghostObject)
	{
		float elapsedTime = 0f;
		Vector3 targetScale = new Vector3(1, 1, 1); // Fixed size for ghost circle

		while (elapsedTime < ghostCircleDuration)
		{
			ghostObject.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, elapsedTime / ghostCircleDuration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		ghostObject.transform.localScale = targetScale;
		Destroy(ghostObject);
	}

	private Vector2 GetGridPositionFromWorld(Vector3 worldPosition)
	{
		float posX = (worldPosition.x + horizontalSize) / spriteWidth;
		float posY = (worldPosition.y + verticalSize) / spriteHeight;
		return new Vector2(Mathf.FloorToInt(posX), Mathf.FloorToInt(posY));
	}

	private void OnDrawGizmos()
	{
		if (Camera.main == null)
			return;

		UpdateGridParameters();

		Gizmos.color = Color.gray;
		for (int x = 0; x < gridColumns; x++)
		{
			for (int y = 0; y < gridRows; y++)
			{
				float posX = -horizontalSize + x * spriteWidth + spriteWidth / 2;
				float posY = -verticalSize + y * spriteHeight + spriteHeight / 2;
				Gizmos.DrawWireCube(new Vector3(posX, posY, 0), new Vector3(spriteWidth, spriteHeight, 0));
			}
		}

		foreach (var note in activeNotes)
		{
			float posX = -horizontalSize + note.Key.x * spriteWidth + spriteWidth / 2;
			float posY = -verticalSize + note.Key.y * spriteHeight + spriteHeight / 2;
		}
	}

	[System.Serializable]
	public class NoteChart
	{
		public List<float> beatTimes;
	}
}

