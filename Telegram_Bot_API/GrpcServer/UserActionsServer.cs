// Ignore Spelling: Api Grpc

using Core.Services.Bot.Abstraction;
using Core.Services.Interfaces;
using Grpc.Core;
using System.Threading.Tasks;
using UserActionsProto;

namespace Api.TelegramBot.GrpcServer
{
    public class UserActionsServer : UserActions.UserActionsBase
    {
        private readonly IUserActivityHistoryService userActivityHistoryService;
        private readonly ICommandHandler commandHandler;

        public UserActionsServer(IUserActivityHistoryService userActivityHistoryService, ICommandHandler commandHandler)
        {
            this.userActivityHistoryService = userActivityHistoryService;
            this.commandHandler = commandHandler;
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
            commandHandler.ReStartBot();
            return Task.FromResult(new ReStartBotGrpcResponse());
        }
        public override Task<StopBotGrpcResponse> StopBot(StopBotGrpcRequest request, ServerCallContext context)
        {
            commandHandler.StopBot();
            return Task.FromResult(new StopBotGrpcResponse());
        }
    }
}
