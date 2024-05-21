// See https://aka.ms/new-console-template for more information

Console.WriteLine($"Введите цену:");
var price = Convert.ToDouble(Console.ReadLine());
Console.WriteLine($"Введите количество товара:");
var quantity = Convert.ToInt32(Console.ReadLine());
Console.WriteLine($"Введите штат:");
var state = Console.ReadLine();

var allStates = StatesRepository.GetAllStates();
var stateEntity = allStates.Single(x => x.Code == state);

var calculator = new Calculator();
var result = calculator.Calculate(new Product(price, quantity), stateEntity);

var saleCalculator = new SaleCalculator();
var allDiscounts = DiscountRepository.GetAllDiscounts();
var discountRange = allDiscounts.Single(r => r.LeftBound <= result && result <= r.RightBound);
var resultWithSale = saleCalculator.Calculate(sum: result, sale: discountRange.Value);
Console.WriteLine($"Итоговая стоимость: {resultWithSale}");

public record Product(double Price, int Quantity);
public record State(string Code, double Tax);
public record Discount(double LeftBound, double RightBound, double Value);

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

public static class DiscountRepository
{
    public static List<Discount> GetAllDiscounts()
    {
        return new List<Discount>()
        {
            new Discount(0, 999, 0.00),
            new Discount(1000, 4999, 0.03),
            new Discount(5000, 6999, 0.05),
            new Discount(7000, 9999, 0.07),
            new Discount(10000, 14999, 0.1),
            new Discount(15000, double.MaxValue, 0.15),
        };
    }
}
