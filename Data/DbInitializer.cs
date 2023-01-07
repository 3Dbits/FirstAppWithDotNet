using FirstDemoCS.Context;
using FirstDemoCS.Models;

namespace FirstDemoCS.Data;

public static class DbInitializer 
{

    public static void Initialize(TestContext context) 
    {
        if (context.Tests.Any())
        {
            return;
        }

        var Tests = new Test[]
        {
            new Test{Date = DateTimeExtensions.SetKindUtc(DateTime.Now), Summary = "Test123", PinCode = 1111},
            new Test{Date = DateTimeExtensions.SetKindUtc(DateTime.Now), Summary = "Test124", PinCode = 2222},
            new Test{Date = DateTimeExtensions.SetKindUtc(DateTime.Now), Summary = "Test125", PinCode = 3333},
            new Test{Date = DateTimeExtensions.SetKindUtc(DateTime.Now), Summary = "Test126", PinCode = 4444}
        };

        context.Tests.AddRange(Tests);
        context.SaveChanges();
    }
}