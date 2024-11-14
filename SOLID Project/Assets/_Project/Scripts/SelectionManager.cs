﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";

    private HighlightSelectionRespones _selectionRespones;

    private Transform _selection;



    private void Awake()
    {
        SceneManager.LoadScene("Environment", LoadSceneMode.Additive);
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
    }

    private void Update()
    {
        if (_selection != null)
        {
            _selectionRespones.OnDeselect(_selection);
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        _selection = null;
        if (Physics.Raycast(ray, out var hit))
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                _selection = selection;
            }
        }

        if (_selection != null)
        {
            _selectionRespones.OnSelect(_selection);
        }
    }
}

internal class HighlightSelectionRespones : MonoBehaviour
{
    [SerializeField] public Material highlightMaterial;
    [SerializeField] public Material defaultMaterial;

    public void OnSelect(Transform selection)
    {
        var selectionRenderer = selection.GetComponent<Renderer>();
        if (selectionRenderer != null)
        {
            selectionRenderer.material = this.highlightMaterial;
        }
    }

    public void OnDeselect(Transform selection)
    {
        var selectionRenderer = selection.GetComponent<Renderer>();
        if (selectionRenderer != null)
        {
            selectionRenderer.material = this.defaultMaterial;
        }
    }
}