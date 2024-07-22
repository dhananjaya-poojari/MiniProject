namespace Kafka.Shared
{
    public class Order
    {
        public string customerName;
        public string product;
        public int quantity;

        public Order(string custName, string pr, int q)
        {
            customerName = custName;
            product = pr;
            quantity = q;
        }
    }
}
