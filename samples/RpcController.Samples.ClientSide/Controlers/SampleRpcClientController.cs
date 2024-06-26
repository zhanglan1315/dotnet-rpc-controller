using Microsoft.AspNetCore.Mvc;
using RpcController.Samples.Shared;

namespace RpcController.Samples.ClientSide.Controllers;

/// <summary>
/// Controller
/// </summary>
[ApiController]
[Route("/")]
public class SampleRpcClientController : ControllerBase
{
    private readonly ISampleRpcService _sampleRpcService;

    public SampleRpcClientController(ISampleRpcService sanpleRpcService)
    {
        _sampleRpcService = sanpleRpcService;
    }

    [HttpPost("upload-file")]
    public async Task<string> FromFormFile(IFormFile file)
    {
        return await _sampleRpcService.FromFormFile(file);
    }

    [HttpPost("upload-files")]
    public async Task<string[]> FromFormFiles(IFormFile[] files)
    {
        return  (await _sampleRpcService.FromFormFiles(files)).ToArray();
    }

    [HttpPost("test-default-value")]
    public Dictionary<string, string> FromQueryDefault()
    {
        return new Dictionary<string, string>()
        {
            ["()"] = _sampleRpcService.FromQueryDefault(),
            ["(null, null, null, null)"] = _sampleRpcService.FromQueryDefault(null, null, null, null),
            ["(a2: 5)"] = _sampleRpcService.FromQueryDefault(a2: 5),
            ["(b: 10)"] = _sampleRpcService.FromQueryDefault(b: "10"),
            ["(a: 5, b: 10)"] = _sampleRpcService.FromQueryDefault(a: 5, a2: 10, b: "hello", b2: "world"),
        };
    }
}
