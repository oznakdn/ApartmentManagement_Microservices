using Aggregator.WebApi.Models;
using System;

namespace Aggregator.WebApi.Services;

public class ApartmentService : ServiceBase
{

    public ApartmentService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("Apartment");
    }


    public async Task<ApartmentCountsResponse>GetApartmentCounts(string token)
    {
        AddAuthorizationHeader(token);
        string siteCountUrl = "/site/getSiteCount";
        var siteCountResponse = await _httpClient.GetStringAsync(siteCountUrl);
        int siteCount = Convert.ToInt32(siteCountResponse);



        string blockCountUrl = "/block/getBlockCount";
        var blockCountResponse = await _httpClient.GetStringAsync(blockCountUrl);
        int blockCount = Convert.ToInt32(blockCountResponse);


        string unitCountUrl = "/unit/getUnitCount";
        var unitCountResponse = await _httpClient.GetStringAsync(unitCountUrl);
        int unitCount = Convert.ToInt32(unitCountResponse);

        return new ApartmentCountsResponse(siteCount, blockCount, unitCount);

    }

}
