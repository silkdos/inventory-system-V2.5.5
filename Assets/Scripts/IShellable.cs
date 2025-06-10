namespace Inventory
{


    public interface IShellable
    {

        #region Propperties
        public float Price { get; set; }
        #endregion

        #region Public Methods
        public float Sell();
        #endregion

    }
}