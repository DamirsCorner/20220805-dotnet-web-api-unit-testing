using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using WebApiUnitTesting.Controllers;
using WebApiUnitTesting.Models;

namespace WebApiUnitTesting.Tests;

public class DataControllerTests
{
    [Test]
    public void ActionResultValueIsNull()
    {
        var controller = new DataController();

        var result = controller.Get(1);

        result.Value.Should().BeNull();
    }

    [Test]
    public void GetShouldReturn200ForValidIds()
    {
        var controller = new DataController();

        var result = controller.Get(1);
        var okResult = result.Result as OkObjectResult;

        var expected = new Data(1, "one");

        okResult.Should().NotBeNull();
        okResult?.StatusCode.Should().Be(200);
        okResult?.Value.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void GetShouldReturn400ForInvalidIds()
    {
        var controller = new DataController();

        var result = controller.Get(0);
        var badRequestResult = result.Result as BadRequestObjectResult;

        var expected = new ErrorDetails("invalid.id", "Id must be non-negative.");

        badRequestResult.Should().NotBeNull();
        badRequestResult?.StatusCode.Should().Be(400);
        badRequestResult?.Value.Should().BeEquivalentTo(expected);
    }

    [Test]
    public void GetShouldReturn404ForNotFoundIds()
    {
        var controller = new DataController();

        var result = controller.Get(2);
        var notFoundResult = result.Result as NotFoundObjectResult;

        var expected = new ErrorDetails("not.found", "No data found for id 2.");

        notFoundResult.Should().NotBeNull();
        notFoundResult?.StatusCode.Should().Be(404);
        notFoundResult?.Value.Should().BeEquivalentTo(expected);
    }
}