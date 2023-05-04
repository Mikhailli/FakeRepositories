
using System.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using WebForFakeRepositories.Models;

namespace WebForFakeRepositories.Controllers;

public class HomeController
{
    private readonly IWebHostEnvironment _appEnvironment;
    private readonly UnitOfWork _unitOfWork;
    private readonly BLService _service;
    
    public HomeController(IWebHostEnvironment appEnvironment)
    {
        _appEnvironment = appEnvironment;
        _unitOfWork = new UnitOfWork();
        _service = new BLService();
    }
    
    [HttpPost]
    public JsonResult GetTimeToWatchAllSeries(string animeName)
    {
        var result =_service.GetTimeToWatchAllSeries(animeName);

        return Json.Decode(result);
    }
}