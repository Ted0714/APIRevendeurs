using APIRevendeurs;
using APIRevendeurs.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace TestsAPIRevendeurs;

[TestClass]
public class TestProduitsController
{
    [TestMethod]
    public void GetAllProducts_ShouldReturnAllProducts()
    {
        var testProducts = GetTestProducts();
        var controller = new ProduitsController();
        var result = controller.GetAsync() as Task<List<APIRevendeurs.Produits>>;
        Assert.AreEqual(testProducts, result);
    }

    [TestMethod]
    public void GetProduct_ShouldReturnCorrectProduct()
    {
        var testProducts = GetTestProduct(4);
        var controller = new ProduitsController();
        var result = controller.Get(4) as Task<APIRevendeurs.Produits>;
        Assert.IsNotNull(result);
        Assert.AreEqual(testProducts, result);
    }

    [TestMethod]
    public void GetProduct_ShouldNotFindProduct()
    {
        var controller = new ProduitsController();

        var result = controller.Get(999);
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    private Task<List<APIRevendeurs.Produits>> GetTestProducts()
    {
        var controller = new ProduitsController();
        Task<List<Produits>> testProducts = controller.GetAsync();

        return testProducts;
    }

    private Task<APIRevendeurs.Produits> GetTestProduct(int id)
    {
        var controller = new ProduitsController();
        Task<Produits> testProducts = controller.Get(id);

        return testProducts;
    }
}
