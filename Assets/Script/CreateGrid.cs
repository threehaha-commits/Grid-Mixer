using UnityEngine;
using TMPro;
using System.Collections.Generic;

public interface IGridGridGenerator
{
    int Column { get; set; }
    int Row { get; set; }
    void Generate();
}

public class CreateGrid : MonoBehaviour, IGridGridGenerator
{
    private Stack<IMover> Cells;
    private IGridGridGenerator GridGenerator;
    private int column;
    private int row;
    int IGridGridGenerator.Column { get => column;
        set 
        {
            if(value <= 0)
            {
                column = 1;
                Error.Put("Значение не должно быть меньше или ровно 0!");
                return;
            }
            column = value;
        } 
    }
    int IGridGridGenerator.Row { get => row;
        set
        {
            if (value <= 0)
            {
                row  = 1;
                Error.Put("Значение не должно быть меньше или ровно 0!");
                return;
            }
            row = value;
        } 
    }
    
    [SerializeField] private RectTransform parent;
    [SerializeField] private TMP_Text Txt;
    private UIMixGrid UIMix;
    private UIGenerateGrid UIGenerate;
    private ClearGrid GridClear;
    private Vector2 ParentSize
    {
        get
        {
            return new Vector2(parent.sizeDelta.x / GridGenerator.Column, parent.sizeDelta.y / GridGenerator.Row);
        }
    }
    private float factorX;
    private float factorY;
    private float FactorX
    {
        get
        {
            float size = ParentSize.x / 2;
            float x = factorX % 2 == 0 ? factorX * size : (factorX - 1) * (-size);
            return x;
        }
        set 
        {
            factorX = value;
        }
    }
    private float FactorY
    {
        get
        {
            float size = ParentSize.y / 2;
            float y = factorY % 2 == 0 ? factorY * size : (factorY - 1) * (-size);
            return y;
        }
        set
        {
            factorY = value;
        }
    }

    private void Start()
    {
        GridGenerator = this;
        GridClear = new ClearGrid();
        UIMix = FindObjectOfType<UIMixGrid>();
        UIGenerate = FindObjectOfType<UIGenerateGrid>();
    }

    public void Generate()
    {
        if (parent.childCount > 0)
            GridClear.Clear();

        Cells = new Stack<IMover>();

        Vector2 size = ParentSize;
        Txt.rectTransform.sizeDelta = size;

        float offsetX = GridGenerator.Column % 2 == 0 ? size.x / 2 : 0;
        float offsetY = GridGenerator.Row % 2 == 0 ? size.y / 2 : 0;

        for (int i = 1; i < GridGenerator.Column + 1; i++)
        {
            FactorX = i;
            for (int j = 1; j < GridGenerator.Row + 1; j++)
            {
                TMP_Text text = Instantiate(Txt, parent);
                text.name = i + "_" + j;
                text.text = text.name;
                FactorY = j;
                text.rectTransform.anchoredPosition = new Vector2(FactorX - offsetX, FactorY - offsetY);
                Cells.Push(text.GetComponent<CellMove>());
                text.color = Random.ColorHSV();
                UIGenerate.AddText(text);
                GridClear.AddObject(text.gameObject);
            }
        }
        UIMix.SetCells(Cells);
        UIGenerate.Generate();
    }
}
