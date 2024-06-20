using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class RhythmPattern : MonoBehaviour
{
    public Sprite noteSprite;
    public Sprite ghostSprite;
    public int gridColumns;
    public int gridRows;
    public float noteScaleFactor;
    public float noteSpawnInterval; // Time interval between note spawns
    public AudioClip noteSound;
    public float minDistanceFromPreviousNote;
    public float maxDistanceFromPreviousNote;
    public float ghostCircleDuration;
    public float destroyDuration;

    private float spriteWidth, spriteHeight;
    private float verticalSize, horizontalSize;
    private Dictionary<Vector2, GameObject> activeNotes = new Dictionary<Vector2, GameObject>();
    private AudioSource audioSource;

    private Vector2 previousNotePosition = Vector2.zero;

    private void Start()
    {
        UpdateGridParameters();
        audioSource = gameObject.GetComponent<AudioSource>();
        StartCoroutine(SpawnNoteCo(noteSpawnInterval));
    }

    private void UpdateGridParameters()
    {
        verticalSize = Camera.main.orthographicSize;
        horizontalSize = verticalSize * Camera.main.aspect;

        spriteWidth = (horizontalSize * 2) / gridColumns;
        spriteHeight = (verticalSize * 2) / gridRows;
    }

    private IEnumerator SpawnNoteCo(float initialDelay)
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            float ghostTime = 0.5f;
            Vector2 newNotePosition = GetRandomGridPosition();

            while (Vector2.Distance(newNotePosition, previousNotePosition) < minDistanceFromPreviousNote ||
                  Vector2.Distance(newNotePosition, previousNotePosition) > maxDistanceFromPreviousNote)
            {
                newNotePosition = GetRandomGridPosition();
            }

            SpawnGhost(newNotePosition);
            SpawnGhost(GetMirrorPosition(newNotePosition));

            yield return new WaitForSeconds(ghostTime);

            SpawnNoteAtPosition(newNotePosition);
            SpawnNoteAtPosition(GetMirrorPosition(newNotePosition));

            previousNotePosition = newNotePosition;

            yield return new WaitForSeconds(noteSpawnInterval - ghostTime);
        }
    }

    private Vector2 GetRandomGridPosition()
    {
        int x = Random.Range(0, gridColumns);
        int y = Random.Range(0, gridRows);
        return new Vector2(x, y);
    }

    private Vector2 GetMirrorPosition(Vector2 originalPosition)
    {
        int mirroredX = gridColumns - 1 - (int)originalPosition.x; // Mirror position across the middle line
        return new Vector2(mirroredX, originalPosition.y);
    }

    private void SpawnGhost(Vector2 gridPosition)
    {
        float posX = -horizontalSize + gridPosition.x * spriteWidth + spriteWidth / 2;
        float posY = -verticalSize + gridPosition.y * spriteHeight + spriteHeight / 2;

        GameObject ghostObject = new GameObject("Ghost");
        ghostObject.transform.position = new Vector3(posX, posY, 0);
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
        noteObject.transform.localScale = new Vector3(noteScaleFactor, noteScaleFactor, 1); // Set to desired note scale

        var spriteRenderer = noteObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = noteSprite;

        // Schedule destruction of note object after it appears
        StartCoroutine(DestroyNoteAfterBeat(noteObject));
    }

    private IEnumerator DestroyNoteAfterBeat(GameObject noteObject)
    {
        yield return new WaitForSeconds(destroyDuration);

        // Play note sound and destroy the note object
        //audioSource.PlayOneShot(noteSound);
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

    public Vector2 GetGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition.x + horizontalSize) / spriteWidth);
        int y = Mathf.FloorToInt((worldPosition.y + verticalSize) / spriteHeight);
        return new Vector2(x, y);
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

        Gizmos.color = Color.red;
        foreach (var note in activeNotes)
        {
            float posX = -horizontalSize + note.Key.x * spriteWidth + spriteWidth / 2;
            float posY = -verticalSize + note.Key.y * spriteHeight + spriteHeight / 2;
            Gizmos.DrawCube(new Vector3(posX, posY, 0), new Vector3(spriteWidth, spriteHeight, 0));
        }
    }
}
