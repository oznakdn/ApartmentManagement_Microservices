using Grpc.Core;
using MediatR;

namespace Financial.gRPC.Services;

public class ExpencesService(IMediator mediator) : Expences.ExpencesBase
{

    public async override Task<CreateExpenseResponse> CreateExpense(CreateExpenseRequest request, ServerCallContext context)
    {

        var result = await mediator.Send(new Application.Commands.CreateExpence.CreateExpenceRequest(request.Title, request.Description, (decimal)request.TotalAmount));

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

}
