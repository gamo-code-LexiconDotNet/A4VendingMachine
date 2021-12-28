namespace VendingMachine
{
    public interface IVendingHandler
    {
        public void InsertMoney();
        public void Purchase(int index);
        public void ShowAll();
        public string[] GetInfos();
        public void MoneyPool();
        public void EndTransaction();
    }
}