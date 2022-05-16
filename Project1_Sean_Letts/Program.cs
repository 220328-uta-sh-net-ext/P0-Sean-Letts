using ResturantAPI;
using System.Net.Http;
using System.Net.Http.Headers;

using var client = new HttpClient();

client.BaseAddress = new Uri("https://localhost:7258/api/resturant");
try{
    var response = client.GetAsync("resturant");
    response.wait();

    var result = response.Result;

    if(result.IsSuccessStatusCode){
        var readTask = result.Content.ReadAsAsnyc<List<ResturantInfo>>();
        readTask.Wait();

        var resturants = readTask.Result;
        foreach (var r in resturants){
            Console.writeline(r);
        }
    }
}
catch(System.Net.HttpRequestException ex){
    System.Console.WriteLine(ex);
}
catch(Excception ex){
    System.Console.WriteLine(ex);
}