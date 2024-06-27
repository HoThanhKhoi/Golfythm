using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorTrigger : MonoBehaviour
{
    [SerializeField] GameObject bossDoor;
    [SerializeField] GameObject finalBossPhaseOne;
    [SerializeField] List<GameObject> fans;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bossDoor != null && finalBossPhaseOne != null)
        {
			bossDoor.SetActive(true);
			finalBossPhaseOne.SetActive(true);
			foreach (GameObject fan in fans)
            {
                if (fan != null)
                {
					fan.SetActive(true);
				}
            }
		}
    }
}
