namespace Catalog.Services.Util;
public class SumCounter
{
    public decimal Sum {get; private set;} = 0;
    public decimal EachSecondProductSum {get; private set;} = 0;
    private bool isSecondProduct  = false; 

    public void Increase(decimal amount)
     {
         Sum += amount;
        if(isSecondProduct)
            EachSecondProductSum += amount;
        isSecondProduct = !isSecondProduct;
    }
}
