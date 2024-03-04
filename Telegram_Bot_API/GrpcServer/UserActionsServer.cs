// Ignore Spelling: Api Grpc
using Grpc.Core;
using Service.Services.Interfaces;
using System.Threading.Tasks;
using UserActionsProto;


namespace Api.TelegramBot.GrpcServer;

public class UserActionsServer : UserActions.UserActionsBase
{
    private readonly IUserActivityHistoryService userActivityHistoryService;

    public UserActionsServer(IUserActivityHistoryService userActivityHistoryService)
    {
        this.userActivityHistoryService = userActivityHistoryService;
    }

    public override async Task<BlockUserGrpcResponseModel> BlockUser(BlockUserGrpcRequestModel request, ServerCallContext context)
    {
        return new BlockUserGrpcResponseModel { Status = await userActivityHistoryService.BlockUserAsync(request.UserName) };
    }

    public override async Task<UnblockUserGrpcResponse> UnblockUser(UnblockUserGrpcRequest request, ServerCallContext context)
    {
        return new UnblockUserGrpcResponse { Status = await userActivityHistoryService.UnBlockUserAsync(request.UserName) };
    }
    public override Task<ReStartBotGrpcResponse> ReStartBot(ReStartBotGrpcRequest request, ServerCallContext context)
    {
        return Task.FromResult(new ReStartBotGrpcResponse());
    }
    public override Task<StopBotGrpcResponse> StopBot(StopBotGrpcRequest request, ServerCallContext context)
    {
        return Task.FromResult(new StopBotGrpcResponse());
    }
}
