// See https://aka.ms/new-console-template for more information

Console.WriteLine($"Введите цену:");
var price = Convert.ToDouble(Console.ReadLine());
Console.WriteLine($"Введите количество товара:");
var quantity = Convert.ToInt32(Console.ReadLine());
Console.WriteLine($"Введите штат:");
var state = Console.ReadLine();

var calculator = new Calculator();
var result = calculator.Calculate(new Product(price, quantity), state);
Console.WriteLine($"Итоговая стоимость: {result}");

public record Product(double Price, int Quantity);
public class Calculator
{
    public double Calculate(Product product, string state)
    {
        var sum = product.Quantity * product.Price;
        return state == "UT" ? (sum * 1.0685) : sum;
    }
}
