using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Financial.gRPC.Services;


[Authorize]
public class ExpencesService(IMediator mediator) : Expences.ExpencesBase
{

    public async override Task<CreateExpenseResponse> CreateExpense(CreateExpenseRequest request, ServerCallContext context)
    {

        var result = await mediator.Send(new Application.Commands.CreateExpence.CreateExpenceRequest(request.SiteId,request.Title, request.Description, (decimal)request.TotalAmount));

        if(!result.IsSuccess)
            throw new RpcException(new Status(StatusCode.Internal, result.Message));

        var response = new CreateExpenseResponse
        {
            IsSuccess = result.IsSuccess,
            Message = result.Message
        };

        return await Task.FromResult(response);

    }

    public async override Task<CreateExpenceItemsResponse> CreateExpenceItems(CreateExpenceItemsRequest request, ServerCallContext context)
    {
        var result = await mediator.Send(new Application.Commands.CreateExpenceItems.CreateExpenceItemsRequest(request.ExpenceId, request.UnitIds,request.LastPaymentDate));

        if (!result.IsSuccess)
            throw new RpcException(new Status(StatusCode.Internal, result.Message));

        var response = new CreateExpenceItemsResponse
        {
            IsSuccess = result.IsSuccess,
            Message = result.Message
        };

        return await Task.FromResult(response);

    }

    public override async Task<GetNotPaidExpenceItemsResponse> GetNotPaidExpenceItems(GetNotPaidExpenceItemsRequest request, ServerCallContext context)
    {
        var result = await mediator.Send(new Application.Queries.GetNonPaidExpenceItems.GetNonPaidExpenceItemsRequest(request.Id));


        var response = new GetNotPaidExpenceItemsResponse();

        foreach (var item in result)
        {
            response.Items.Add(new ItemsResponse
            {
                UnitId = item.UnitId,
                Amount = Convert.ToDouble(item.Amount),
                LastPaymentDate = item.LastPaymentDate
            });
        }

        return await Task.FromResult(response);
    }

}
