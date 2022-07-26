using UnityEngine;
using TMPro;

public enum InputFields { Column, Raw };

public class ParamInputController : MonoBehaviour
{
    [SerializeField] private InputFields IField;
    private TMP_InputField Field;
    private IGridGridGenerator GridGenerator;

    public void Construct(IGridGridGenerator GridGenerator)
    {
        this.GridGenerator = GridGenerator;
        DoStart();
    }

    private void DoStart()
    {
        Field = GetComponent<TMP_InputField>();
    }
    
    public void EndInputValue() 
    {
        int value = System.Convert.ToInt32(Field.text); 
        switch (IField)
        {
            case InputFields.Column:
                GridGenerator.Column = value;
                break;

            case InputFields.Raw:
                GridGenerator.Row = value;
                break;
        }
    }
}
