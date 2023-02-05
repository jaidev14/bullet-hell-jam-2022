using UnityEngine;

[CreateAssetMenu(fileName = "DialogueObject", menuName = "Dialogue/DialogueObject", order = 0)]
public class DialogueObject : ScriptableObject {
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;

    public string[] Dialogue => dialogue;

    public Response[] Responses => responses;

    public bool HasResponses => Responses != null && Responses.Length > 0;    
}
