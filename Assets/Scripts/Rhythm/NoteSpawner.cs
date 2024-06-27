using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.IO;

public class NoteSpawner : MonoBehaviour
{
	[Header("Sprites and Grid Settings")]
	public Sprite noteSprite;
	public Sprite ghostSprite;
	public int gridColumns;
	public int gridRows;
	public float noteScaleFactor;

	[Header("Note Timing Settings")]
	public int minDistanceFromPreviousNote;
	public int maxDistanceFromPreviousNote;
	public float ghostCircleDuration;
	public float destroyDuration;
	public float simultaneousSpawnProbability;

	private float spriteWidth, spriteHeight;
	private float verticalSize, horizontalSize;
	private Dictionary<Vector2, GameObject> activeNotes = new Dictionary<Vector2, GameObject>();
	private Vector2 previousNotePosition = Vector2.zero;
	private List<float> noteTimes;

	public delegate void NoteSpawnedHandler(GameObject noteObject, Vector2 gridPosition);
	public event NoteSpawnedHandler OnNoteSpawned;

	public delegate void NoteDestroyedHandler(GameObject noteObject, bool isMissed);
	public event NoteDestroyedHandler OnNoteDestroyed;

	[System.Serializable]
	public class NoteChart
	{
		public List<float> beatTimes;
	}

	private void Start()
	{
		UpdateGridParameters();
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
		string path = Path.Combine(Application.dataPath, "beatTimes.json");
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
			Debug.LogError($"Note chart file not found at {path}");
		}
	}

	private IEnumerator PlayNotes()
	{
		float startTime = Time.time;

		foreach (float noteTime in noteTimes)
		{
			float waitTime = noteTime - (Time.time - startTime);
			yield return new WaitForSeconds(waitTime - ghostCircleDuration - 0.3f);

			Vector2 newNotePosition = GetNextRandomGridPosition(previousNotePosition);

			if (Random.value < simultaneousSpawnProbability)
			{
				HandleSimultaneousSpawn(newNotePosition);
			}
			else
			{
				HandleSingleSpawn(newNotePosition);
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
			newGridPosition = GenerateRandomPosition(currentGridPos);
			attempts++;
		}
		while ((newGridPosition == currentGridPos || IsPositionOccupied(newGridPosition) || !IsValidDistance(newGridPosition, currentGridPos)) && attempts < 100);

		return newGridPosition;
	}

	private Vector2 GenerateRandomPosition(Vector2 currentGridPos)
	{
		int tempRandomX = Random.Range(minDistanceFromPreviousNote, maxDistanceFromPreviousNote + 1);
		int tempRandomY = Random.Range(minDistanceFromPreviousNote, maxDistanceFromPreviousNote + 1);
		int randomDirectionX = Random.Range(0, 2) == 0 ? 1 : -1;
		int randomDirectionY = Random.Range(0, 2) == 0 ? 1 : -1;

		Vector2 newGridPosition = new Vector2(currentGridPos.x + tempRandomX * randomDirectionX, currentGridPos.y + tempRandomY * randomDirectionY);

		newGridPosition.x = Mathf.Clamp(newGridPosition.x, 0, gridColumns - 1);
		newGridPosition.y = Mathf.Clamp(newGridPosition.y, 0, gridRows - 1);

		return newGridPosition;
	}

	private bool IsPositionOccupied(Vector2 newGridPosition)
	{
		return activeNotes.ContainsKey(newGridPosition) || activeNotes.ContainsKey(GetMirrorPosition(newGridPosition));
	}

	private bool IsValidDistance(Vector2 newGridPosition, Vector2 currentGridPos)
	{
		float distanceToCurrent = Vector2.Distance(newGridPosition, currentGridPos);
		float distanceToMirror = Vector2.Distance(newGridPosition, GetMirrorPosition(newGridPosition));

		return distanceToCurrent >= minDistanceFromPreviousNote && distanceToCurrent <= maxDistanceFromPreviousNote &&
			   distanceToMirror >= minDistanceFromPreviousNote && distanceToMirror <= maxDistanceFromPreviousNote;
	}

	private Vector2 GetMirrorPosition(Vector2 originalPosition)
	{
		int mirroredX = gridColumns - 1 - (int)originalPosition.x;
		return new Vector2(mirroredX, originalPosition.y);
	}

	private void HandleSimultaneousSpawn(Vector2 newNotePosition)
	{
		Vector2 mirrorPosition = GetMirrorPosition(newNotePosition);
		SpawnGhost(newNotePosition);
		SpawnGhost(mirrorPosition);

		StartCoroutine(WaitAndSpawnNotes(newNotePosition, mirrorPosition));
	}

	private void HandleSingleSpawn(Vector2 newNotePosition)
	{
		bool spawnOnLeft = Random.value < 0.5f;

		if (spawnOnLeft)
		{
			SpawnGhost(newNotePosition);
			StartCoroutine(WaitAndSpawnSingleNoteAtPosition(newNotePosition));
		}
		else
		{
			Vector2 mirrorPosition = GetMirrorPosition(newNotePosition);
			SpawnGhost(mirrorPosition);
			StartCoroutine(WaitAndSpawnSingleNoteAtPosition(mirrorPosition));
		}
	}

	private IEnumerator WaitAndSpawnNotes(Vector2 firstPosition, Vector2 secondPosition)
	{
		yield return new WaitForSeconds(ghostCircleDuration);
		SpawnNoteAtPosition(firstPosition);
		SpawnNoteAtPosition(secondPosition);
	}

	private IEnumerator WaitAndSpawnSingleNoteAtPosition(Vector2 position)
	{
		yield return new WaitForSeconds(ghostCircleDuration);
		SpawnNoteAtPosition(position);
	}

	private void SpawnGhost(Vector2 gridPosition)
	{
		Vector3 worldPosition = GetWorldPositionFromGrid(gridPosition);

		GameObject ghostObject = CreateGameObject("Ghost", worldPosition, ghostSprite, Color.red);
		StartCoroutine(AnimateGhostCircle(ghostObject));
	}

	private void SpawnNoteAtPosition(Vector2 gridPosition)
	{
		Vector3 worldPosition = GetWorldPositionFromGrid(gridPosition);

		GameObject noteObject = CreateGameObject("Note", worldPosition, noteSprite, Color.white);
		noteObject.transform.localScale = new Vector3(noteScaleFactor, noteScaleFactor, 1);

		activeNotes[gridPosition] = noteObject;
		OnNoteSpawned?.Invoke(noteObject, gridPosition);
		StartCoroutine(DestroyNoteAfterBeat(noteObject, gridPosition));
	}

	private GameObject CreateGameObject(string name, Vector3 position, Sprite sprite, Color color)
	{
		GameObject gameObject = new GameObject(name);
		gameObject.transform.position = position;
		gameObject.transform.localScale = Vector3.zero;

		SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = sprite;
		spriteRenderer.color = color;

		return gameObject;
	}

	public void MarkNoteAsHit(GameObject noteObject, Vector2 gridPosition)
	{
		if (activeNotes.ContainsKey(gridPosition))
		{
			activeNotes.Remove(gridPosition);
			OnNoteDestroyed?.Invoke(noteObject, false);
			Destroy(noteObject);
		}
	}

	private IEnumerator DestroyNoteAfterBeat(GameObject noteObject, Vector2 gridPosition)
	{
		yield return new WaitForSeconds(destroyDuration);

		if (activeNotes.ContainsKey(gridPosition))
		{
			activeNotes.Remove(gridPosition);
			OnNoteDestroyed?.Invoke(noteObject, true);
			Destroy(noteObject);
		}
	}

	private Vector3 GetWorldPositionFromGrid(Vector2 gridPosition)
	{
		float posX = -horizontalSize + gridPosition.x * spriteWidth + spriteWidth / 2;
		float posY = -verticalSize + gridPosition.y * spriteHeight + spriteHeight / 2;
		return new Vector3(posX, posY, 0);
	}

	private IEnumerator AnimateGhostCircle(GameObject ghostObject)
	{
		float elapsedTime = 0f;
		Vector3 targetScale = new Vector3(noteScaleFactor, noteScaleFactor, 1) * 0.8f;

		while (elapsedTime < ghostCircleDuration)
		{
			ghostObject.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, elapsedTime / ghostCircleDuration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		ghostObject.transform.localScale = targetScale;
		Destroy(ghostObject);
	}

	public Vector2 GetGridPositionFromWorld(Vector3 worldPosition)
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
		DrawGridGizmos();
	}

	private void DrawGridGizmos()
	{
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
			Gizmos.DrawWireSphere(new Vector3(posX, posY, 0), 0.2f);
		}
	}
}
