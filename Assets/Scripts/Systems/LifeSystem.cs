public static class LifeSystem
{
    /// <summary>
    /// Public properties
    /// </summary>
    public static int LivesAmount { get { return _lifeAmount; } }
    public static int LivesBuyAmount { get { return _lifeBuy; } }

    /// <summary>
    /// Private fields
    /// </summary>
    private static int _lifeAmount = -1;
    private static int _lifeBuy = -1;

    /// <summary>
    /// Initialize lives
    /// </summary>
    public static void InitLives(int lifeAmount, int lifeBuy)
    {
        _lifeAmount = lifeAmount;
        _lifeBuy = lifeBuy;
    }

    /// <summary>
    /// Add lives buy amount
    /// </summary>
    public static void AddLivesBuyAmount()
    {
        _lifeAmount += _lifeBuy;
    }

    /// <summary>
    /// Delete life
    /// </summary>
    public static void DeleteLife()
    {
        _lifeAmount = (0 < _lifeAmount) ? _lifeAmount - 1 : 0;
    }
}