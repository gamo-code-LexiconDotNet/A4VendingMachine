namespace VendingMachine
{
    public interface IVending
    {
        public int[] Denominations { get; }
        public int MoneyPool { get; }
        public string Purchase(int index);
        public string[] ShowAll();
        public void InsertMoney(int money);
        public int[] EndTransaction();
    }
}
