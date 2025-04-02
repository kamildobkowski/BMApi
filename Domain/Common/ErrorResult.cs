using System.Net;
using System.Text.Json.Serialization;

namespace Domain.Common;

public class ErrorResult
{
	[JsonIgnore]
	public HttpStatusCode StatusCode { get; set; }
	public string Title { get; set; } = string.Empty;
	public List<Error> Errors { get; set; } = [];
}