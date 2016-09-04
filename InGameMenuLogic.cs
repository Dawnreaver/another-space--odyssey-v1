using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGameMenuLogic : MonoBehaviour
{
    public CoreGame m_coreGameObject;
    // menus
    public GameObject m_buildMenu;
    public GameObject m_buildPrompt;
    public GameObject m_buttonCancel;
    public GameObject m_buttonBuildMenu;

    // ingame UI
    public Text m_energyText;

    void Start()
    {
        InitializeGameUI();
    }

    void FixedUpdate()
    {
        m_energyText.text = "" + m_coreGameObject.m_storedEnergy;
    }


    public void InitializeGameUI()
    {
        m_buttonCancel.SetActive(false);
        m_buildMenu.SetActive(false);
        m_buttonBuildMenu.SetActive(true);
        m_buildPrompt.SetActive(false);
    }

    public void BuildBuilding()
    {
        m_buildPrompt.SetActive(false);
        m_coreGameObject.CheckPoolStartBuilding(m_coreGameObject.m_buildingType, m_coreGameObject.m_previewObject.transform.position);
        m_buttonBuildMenu.SetActive(true);
    }

    public void CancelBuildMode()
    {
        m_buttonBuildMenu.SetActive(true);
        m_buildMenu.SetActive(false);

        m_coreGameObject.EnableAllCells();
        m_coreGameObject.DisableBuildMode();
    }

    public void CancelBuildPrompt()
    {
        m_buildMenu.SetActive(true);
        m_buildPrompt.SetActive(false);
        m_coreGameObject.ResetBuildingPreview();
    }

    public void BuildPrompt()
    {
        m_buildMenu.SetActive(false);
        m_buildPrompt.SetActive(true);
        m_coreGameObject.SetBuildingPreview(m_coreGameObject.m_buildingType, m_coreGameObject.m_previewObject);
    }
    public void EnableBuildMode()
    {
        m_buttonBuildMenu.SetActive(false);
        m_coreGameObject.EnableBuildeMode();
    }

    public void OpenBuildMenu(Transform targetObject)
    {
        m_buildMenu.SetActive(true);
        SetBuildMenuPosition(targetObject);
    }

    public void SetBuildMenuPosition(Transform targetObject)
    {
        RectTransform CanvasRect = GetComponent<RectTransform>();
        Vector2 ViewportPosition = (Camera.main.WorldToViewportPoint(targetObject.position));
        Vector2 WorldObject_ScreenPosition = new Vector2(((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));
        m_buildMenu.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
        m_buildPrompt.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition;
    }
}