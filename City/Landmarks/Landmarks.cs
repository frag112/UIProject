using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Landmarks", menuName = "ScriptableObjects/CreateNewLandmark", order = 1)]
public class Landmarks : ScriptableObject
{
    public string title;

    public Sprite[] photos;
    public string description;
    
}