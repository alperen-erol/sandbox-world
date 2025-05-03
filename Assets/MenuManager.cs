using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    private VisualElement ui;
    private VisualElement MainMenu;
    private VisualElement CharacterMenu;

    private Button PlayButton;
    private Button CharacterButton;
    private Button ExitButton;

    private Button WarriorClass;
    private Button RangeClass;
    private Button MageClass;

    private void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        MainMenu = ui.Q<VisualElement>("MainMenu");

        CharacterMenu = ui.Q<VisualElement>("CharacterMenu");

        PlayButton = ui.Q<Button>("PlayButton");

        CharacterButton = ui.Q<Button>("CharacterButton");
        CharacterButton.clicked += OnCharacterButtonClicked;

        ExitButton = ui.Q<Button>("ExitButton");

        WarriorClass = ui.Q<Button>("WarriorClass");

        RangeClass = ui.Q<Button>("RangeClass");

        MageClass = ui.Q<Button>("MageClass");

    }

    private void OnCharacterButtonClicked()
    {
        MainMenu.AddToClassList("hide");
        CharacterMenu.RemoveFromClassList("hide");
        Debug.Log("Character Menu Open");
    }





}
