/// <summary>
/// State of the letter being typed.
/// </summary>
public enum LetterState
{
    Correct,        //if a letter is typed right
    MissTyped,      //if a letter is missTyped
    Default         //if a letter is backspaced
}
