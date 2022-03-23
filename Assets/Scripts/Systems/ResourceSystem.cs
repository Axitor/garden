public static class ResourceSystem
{
    /// <summary>
    /// Public properties
    /// </summary>
    public static int StarsAmount { get { return _starsAmount; } set { _starsAmount = value; } }
    public static int StarsAmountAddEvent { get { return _starsAmountAddEvent; } set { _starsAmountAddEvent = value; } }
    public static string StarsWarningMessage { get { return _starsWarningMessage; } set { _starsWarningMessage = value; } }

    /// <summary>
    /// Private fields
    /// </summary>
    private static int _starsAmount = 100;
    private static int _starsAmountAddEvent = 1;
    private static string _starsWarningMessage = "Not enough resources";

    /// <summary>
    /// Returns remaining stars amount
    /// </summary>
    public static int GetAmount()
    {
        return _starsAmount;
    }

    /// <summary>
    /// Check is enough stars
    /// </summary>
    public static bool CheckAmount(int starsCost)
    {
        if(starsCost <= _starsAmount)
        {
            return true;
        }
        else
        {
            TextSystem.UpdateWarningTextField(_starsWarningMessage);
            return false;
        }
    }

    /// <summary>
    ///Add stars on event and return new value
    /// </summary>
    public static int AddAmount()
    {
        return SetAmount(0, _starsAmountAddEvent);
    }

    /// <summary>
    /// Sets stars amount, updates the text field and returns new value
    /// </summary>
    public static int SetAmount(int spend = 0, int add = 0)
    {
        _starsAmount += add;
        _starsAmount -= spend;

        if(_starsAmount < 0)
        {
            _starsAmount = 0;
        }

        TextSystem.UpdateStarsTextField(_starsAmount.ToString());

        return _starsAmount;
    }
}
