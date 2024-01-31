using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;

public class Calculator : MonoBehaviour
{
    //
    //Properties
    //

    //This TextMeshProUGUI variable is how we interact with the OutputLabel in the interface
    public TextMeshProUGUI label;

    //Stores the previous input for calculations
    private float prevInput;

    //Tells us when to start clearing the numbers in the label
    private bool clearPrevInput = false;

    //Variable for later when we find out what type of equation the person wants
    private EquationType equationType;

    //
    //Methods
    //

    private void Start()
    {
        //Starts up by clearing
        Clear();
    }

    public void AddInput(string input)
    {
        //Clears the label when clearPrevInput is true
        if (clearPrevInput == true) 
        {
            label.text = string.Empty;
            clearPrevInput = false;
        }

        label.text += input;
    }

    public void SetEquationAsAdd()
    {
        //Sets the EquationType as Add and stores the prevInput for later
        prevInput = float.Parse(label.text);
        clearPrevInput = true;
        equationType = EquationType.ADD;
    }

    public void SetEquationAsSubtract()
    {
        //Sets the EquationType as Subtract and stores the prevInput for later
        prevInput = float.Parse(label.text);
        clearPrevInput = true;
        equationType = EquationType.SUBTRACT;
    }

    public void SetEquationAsMultiply() 
    {
        //Sets the EquationType as Multiply and stores the prevInput for later
        prevInput = float.Parse(label.text);
        clearPrevInput = true;
        equationType = EquationType.MULTIPLY;
    }

    public void SetEquationAsDivide() 
    {
        //Sets the EquationType as Divide and stores the prevInput for later
        prevInput = float.Parse(label.text);
        clearPrevInput = true;
        equationType = EquationType.DIVIDE;
    }

    public void Add()
    {
        //Just adds the numbers together and puts in label
        float currentInput;
        float output;
        currentInput = float.Parse(label.text);
        output = prevInput + currentInput;
        label.text = $"{output}";

        clearPrevInput = true;
    }

    public void Subtract() 
    {
        //Just subtracts the numbers together and puts in label
        float currentInput;
        float output;
        currentInput = float.Parse(label.text);
        output = prevInput - currentInput;
        label.text = $"{output}";

        clearPrevInput = true;
    }

    public void Multiply()
    {
        //Just multiplies the numbers together and puts in label
        float currentInput;
        float output;
        currentInput = float.Parse(label.text);
        output = prevInput * currentInput;
        label.text = $"{output}";

        clearPrevInput = true;
    }

    public void Divide()
    {
        //Just divides the numbers together and puts in label
        float currentInput;
        float output;
        currentInput = float.Parse(label.text);
        output = prevInput / currentInput;
        label.text = $"{output}";

        clearPrevInput = true;
    }

    public void Clear()
    {
        //Resets the calculator when C Button is pressed
        label.text = "0";
        prevInput = 0;
        clearPrevInput = true;
        equationType = EquationType.None;        
    }

    public void Calculate()
    {
        //Calls the method that does the math depending on which equation the person chose
        switch (equationType) 
        {
            case EquationType.ADD:
                Add();
                break;
            case EquationType.SUBTRACT:
                Subtract();
                break;
            case EquationType.DIVIDE:
                Divide();
                break;
            case EquationType.MULTIPLY:
                Multiply();
                break;

        }
    }

    public enum EquationType
    {
        None = 0,
        ADD = 1,
        SUBTRACT = 2,
        MULTIPLY = 3,
        DIVIDE = 4
    }
}
