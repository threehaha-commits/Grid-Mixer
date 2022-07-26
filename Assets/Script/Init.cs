using TMPro;
using UnityEngine;

public class Init : MonoBehaviour
{
    [SerializeField] private TMP_Text ErrorText;
    [SerializeField] private CreateGrid CreateGrid;
    [SerializeField] private ParamInputController InputFieldRow;
    [SerializeField] private ParamInputController InputFieldColumn;

    private void Awake()
    {
        new Error(ErrorText);
        InputFieldRow.Construct(CreateGrid);
        InputFieldColumn.Construct(CreateGrid);
    }
}
