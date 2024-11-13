using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class NpcDialogInteract : MonoBehaviour
{
    public Flowchart flowchart;
    [SerializeField] private string act;
    private bool playerinRange = false;
    private bool dialogFinised = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerinRange && Input.GetKeyDown(KeyCode.F))
        {
            flowchart.ExecuteBlock(act);
            StartCoroutine(WaitForDialogtoEnd());
            playerinRange = false;
        }
    }

    IEnumerator WaitForDialogtoEnd()
    {
        while (flowchart.GetExecutingBlocks().Count > 0)
        {
            yield return null;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerinRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerinRange = false;
        }
    }
}
