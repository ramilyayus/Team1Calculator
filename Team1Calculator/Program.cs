// See https://aka.ms/new-console-template for more information

Console.WriteLine($"Введите цену:");
var price = Convert.ToDouble(Console.ReadLine());
var quantity = 1;

var calculator = new Calculator();
var result = calculator.Calculate(new Product(price, quantity));
Console.WriteLine($"Итоговая стоимость: {result}");

public record Product(double Price, int Quantity);
public class Calculator
{
    public double Calculate(Product product)
    {
        return product.Quantity * product.Price;
    }
}
