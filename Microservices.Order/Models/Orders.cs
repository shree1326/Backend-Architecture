namespace Microservices.Order.Models
{
    public class Orders
    {
        //write properties of Order class like id, name, price, quantity, etc.
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
    }
}
