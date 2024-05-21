// See https://aka.ms/new-console-template for more information

Console.WriteLine($"Введите цену:");
var price = Convert.ToDouble(Console.ReadLine());
Console.WriteLine($"Введите количество товара:");
var quantity = Convert.ToInt32(Console.ReadLine());
Console.WriteLine($"Введите штат:");
var state = Console.ReadLine();

var saleTrashhold = 1000;
var salePersentage = 0.03;

var allStates = StatesRepository.GetAllStates();
var stateEntity = allStates.Single(x => x.Code == state);

var calculator = new Calculator();
var result = calculator.Calculate(new Product(price, quantity), stateEntity);

var saleCalculator = new SaleCalculator();
if (result >= saleTrashhold)
{
    var resultWithSale = saleCalculator.Calculate(sum: result, sale: salePersentage);
    Console.WriteLine($"Итоговая стоимость: {resultWithSale}");
}
else
{
    Console.WriteLine($"Итоговая стоимость: {result}");
}

public record Product(double Price, int Quantity);
public record State(string Code, double Tax);

public class SaleCalculator
{
    public double Calculate(double sum, double sale)
    {
        return sum * (1 - sale);
    }
}

public class Calculator
{
    public double Calculate(Product product, State state)
    {
        var sum = product.Quantity * product.Price;
        return sum * (1 + state.Tax / 100);
    }
}

public static class StatesRepository
{
    public static List<State> GetAllStates()
    {
        return new List<State>()
        {
            new State("UT", 6.85),
            new State("NV", 8.00),
            new State("TX", 6.25),
            new State("AL", 4.00),
            new State("CA", 8.25)
        };
    }
}
