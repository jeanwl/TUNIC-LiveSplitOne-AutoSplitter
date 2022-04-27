class Split
{
    public readonly Func<bool> Passed;
    public readonly bool IsLast;
    public Split Next;
    public Split(Func<bool> passed, Split next = null, bool isLast = false)
    {
        Passed = passed;
        Next = next;
        IsLast = isLast;
    }
}