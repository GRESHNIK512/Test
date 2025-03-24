using TMPro;
using UnityEngine;
using Zenject;

public class WindowDescription : Window
{ 
    private Screen _screen; 

    [SerializeField] private OkButton _OkButton;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;  

    [Inject]
    public void Construct(Screen screen, WindowGame windowGame)
    { 
        _screen = screen; 
        _OkButton.OnClickButton += () =>
        {
            windowGame.StopAllAnimationLoad();
            _screen.ShowOnlyMe(windowGame);
        };
    }  

    public void RefreshInfo(BreedDescriptionMessage msg)
    { 
        _title.text = msg.BreedData.Attributes.Name;
        _description.text = msg.BreedData.Attributes.Description;
        _screen.ShowOnlyMe(this);
    } 
}
