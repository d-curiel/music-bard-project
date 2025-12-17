
[System.AttributeUsage(System.AttributeTargets.Class)]
public class AutoRegisterInputAttribute : System.Attribute
{
    public InputMode Mode { get; }
    
    public AutoRegisterInputAttribute(InputMode mode)
    {
        Mode = mode;
    }
}

public enum InputMode
{
    Note,
}